using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ReplaceText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string inputFilePath = GetFilePath("Zadejte cestu k vstupnímu XML souboru: ");
                string outputFilePath = GetFilePath("Zadejte cestu k výstupnímu souboru: ");

                if (!File.Exists(inputFilePath))
                {
                    throw new FileNotFoundException("Vstupní soubor nebyl nalezen.", inputFilePath);
                }

                Encoding encoding = DetectEncodingRobust(inputFilePath);
                Console.WriteLine($"Detekované kódování: {encoding.EncodingName}");

                if (encoding.EncodingName != "Windows-1250" && encoding.EncodingName != "Central European (Windows)")
                {
                    Console.WriteLine("Přepínám na Windows-1250 kódování...");
                    encoding = Encoding.GetEncoding(1250);
                }

                ProcessFile(inputFilePath, outputFilePath, encoding);

                Console.WriteLine("Zpracování dokončeno.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Došlo k chybě: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            finally
            {
                Console.WriteLine("Stiskněte libovolnou klávesu pro ukončení...");
                Console.ReadKey();
            }
        }

        private static string GetFilePath(string prompt)
        {
            Console.Write(prompt);
            return Console.ReadLine().Trim();
        }

        private static Encoding DetectEncodingRobust(string filePath)
        {
            var encodingsToTry = new[]
            {
                Encoding.GetEncoding(1250),  // Windows-1250
                Encoding.UTF8,
                Encoding.GetEncoding(852),   // IBM852
                Encoding.GetEncoding(28592)  // ISO-8859-2
            };

            foreach (var encoding in encodingsToTry)
            {
                try
                {
                    using (var reader = new StreamReader(filePath, encoding, true))
                    {
                        string firstLine = reader.ReadLine();
                        if (firstLine != null && firstLine.Contains("<?xml"))
                        {
                            Match match = Regex.Match(firstLine, @"encoding=[""'](.+?)[""']");
                            if (match.Success)
                            {
                                string encodingName = match.Groups[1].Value;
                                try
                                {
                                    return Encoding.GetEncoding(encodingName);
                                }
                                catch
                                {
                                    // Pokud selže, pokračuje dalším kódováním
                                }
                            }
                            return encoding; // Vrátí, které úspěšně přečetlo XML hlavičku
                        }
                    }
                }
                catch
                {
                    // Pokud selže, zkusí další kódování
                }
            }

            // Pokud všechno selže, vrátí Windows-1250
            Console.WriteLine("Nepodařilo se detekovat kódování. Použije se Windows-1250.");
            return Encoding.GetEncoding(1250);
        }

        private static void ProcessFile(string inputFilePath, string outputFilePath, Encoding inputEncoding)
        {
            Console.WriteLine("Začínám zpracování souboru...");
            var output = new StringBuilder();

            string[] lines = File.ReadAllLines(inputFilePath, inputEncoding);

            bool startRecording = false;

            foreach (var line in lines)
            {
                if (line.Contains("<titulek>"))
                {
                    Console.WriteLine("Nalezen tag <titulek> - začínám zaznamenávat.");
                    startRecording = true;
                }

                if (startRecording)
                {
                    var processedLine = ProcessLine(line);
                    output.AppendLine(processedLine);
                }
            }

            string finalOutput = output.ToString().TrimEnd();

            if (finalOutput.EndsWith("</kniha>"))
            {
                finalOutput = finalOutput.Substring(0, finalOutput.Length - "</kniha>".Length).TrimEnd();
            }

            if (string.IsNullOrWhiteSpace(finalOutput))
            {
                Console.WriteLine("Varování: Nebyl zpracován žádný obsah. Výstupní soubor bude prázdný.");
            }
            else
            {
                try
                {
                    // Změní kódování na UTF-8
                    File.WriteAllText(outputFilePath, finalOutput, Encoding.UTF8);

                    Console.WriteLine($"Výstup byl úspěšně zapsán do: {outputFilePath}");
                    Console.WriteLine($"Délka výsledného obsahu: {finalOutput.Length} znaků");
                    Console.WriteLine("Použité kódování: UTF-8");

                    string fileContent = File.ReadAllText(outputFilePath, Encoding.UTF8);
                    Console.WriteLine($"Prvních 100 znaků obsahu souboru: {fileContent.Substring(0, Math.Min(100, fileContent.Length))}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při zápisu do souboru: {ex.Message}");
                    throw;
                }
            }
        }

        private static string ProcessLine(string line)
        {
            // Vytvoří regex, který najde všechny <odkazo> nebo <odkaz> a nahradí je textem bez změny struktury XML
            string pattern = @"<odkazo n=""(?<odkazo>.*?)""/>|<odkaz n=""(?<odkaz>.*?)""/>";
            line = Regex.Replace(line, pattern, match =>
            {
                string odkazValue = match.Groups["odkaz"].Success ? match.Groups["odkaz"].Value : null;
                string odkazoValue = match.Groups["odkazo"].Success ? match.Groups["odkazo"].Value : null;

                if (!string.IsNullOrEmpty(odkazoValue))
                {
                    return "\\fo" + odkazoValue + "\\fo*";
                }

                if (!string.IsNullOrEmpty(odkazValue))
                {
                    return "\\f" + odkazValue + "\\f*";
                }

                return match.Value;
            });

            return line;
        }
    }
}
