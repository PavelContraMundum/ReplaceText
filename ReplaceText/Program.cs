using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace ReplaceText
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Registrujeme dodatečné kódovací stránky
                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string inputFilePath = GetFilePath("Zadejte cestu k vstupnímu XML souboru: ");
                string outputFilePath = GetFilePath("Zadejte cestu k výstupnímu souboru: ");

                if (!File.Exists(inputFilePath))
                {
                    throw new FileNotFoundException("Vstupní soubor nebyl nalezen.", inputFilePath);
                }

                Encoding encoding = DetectEncodingRobust(inputFilePath);
                Console.WriteLine($"Detekované kódování: {encoding.EncodingName}");

                // Použijeme CP1250, pokud je to specifikováno v souboru
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
                                    // Pokud selže, pokračujeme dalším kódováním
                                }
                            }
                            return encoding; // Vrátíme kódování, které úspěšně přečetlo XML hlavičku
                        }
                    }
                }
                catch
                {
                    // Pokud selže, zkusíme další kódování
                }
            }

            // Pokud všechno selže, vrátíme Windows-1250
            Console.WriteLine("Nepodařilo se detekovat kódování. Použije se Windows-1250.");
            return Encoding.GetEncoding(1250);
        }

        private static void ProcessFile(string inputFilePath, string outputFilePath, Encoding encoding)
        {
            Console.WriteLine("Začínám zpracování souboru...");
            var output = new StringBuilder();

            using (var reader = new StreamReader(inputFilePath, encoding))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);

                // Reset StreamReader na začátek souboru
                reader.BaseStream.Position = 0;
                reader.DiscardBufferedData();

                string line;
                bool startRecording = false;

                while ((line = reader.ReadLine()) != null)
                {
                    // Přeskočíme XML deklaraci a DOCTYPE
                    if (line.StartsWith("<?xml") || line.StartsWith("<!DOCTYPE"))
                    {
                        continue;
                    }

                    if (line.Contains("<titulek>"))
                    {
                        Console.WriteLine("Nalezen tag <titulek> - začínám zaznamenávat.");
                        startRecording = true;
                    }

                    if (startRecording)
                    {
                        line = ProcessLine(line, xmlDoc);
                        output.AppendLine(line);
                    }
                }
            }

            string finalOutput = output.ToString().TrimEnd();

            // Odebrání tagu </kniha> na konci
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
                    File.WriteAllText(outputFilePath, finalOutput, encoding);

                    Console.WriteLine($"Výstup byl úspěšně zapsán do: {outputFilePath}");
                    Console.WriteLine($"Délka výsledného obsahu: {finalOutput.Length} znaků");
                    Console.WriteLine($"Použité kódování: {encoding.EncodingName}");

                    string fileContent = File.ReadAllText(outputFilePath, encoding);
                    Console.WriteLine($"Prvních 100 znaků obsahu souboru: {fileContent.Substring(0, Math.Min(100, fileContent.Length))}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Chyba při zápisu do souboru: {ex.Message}");
                    throw;
                }
            }
        }

        private static string ProcessLine(string line, XmlDocument xmlDoc)
        {
            if (line.Contains("<defpozno") || line.Contains("<defpozn"))
            {
                line = ProcessDefpozn(line, "defpozno", "\\fo");
                line = ProcessDefpozn(line, "defpozn", "\\f");
            }
            else
            {
                bool modified;
                do
                {
                    modified = false;
                    line = ReplaceOdkaz(line, xmlDoc, ref modified);
                    line = ReplaceOdkazo(line, xmlDoc, ref modified);
                } while (modified);
            }

            return line;
        }

        private static string ProcessDefpozn(string line, string tagName, string replaceTag)
        {
            string pattern = $@"<{tagName} n=""(.+?)"">(.*?)</{tagName}>";
            return Regex.Replace(line, pattern, match =>
            {
                string n = match.Groups[1].Value;
                string content = match.Groups[2].Value;
                return $"{replaceTag}{content}{replaceTag}*";
            });
        }

        private static string ReplaceOdkaz(string line, XmlDocument xmlDoc, ref bool modified)
        {
            int startIndex = line.IndexOf("<odkaz n=\"");
            if (startIndex != -1)
            {
                int endIndex = FindEndIndex(line, startIndex);
                if (endIndex != -1)
                {
                    string odkaz = ExtractOdkazValue(line, startIndex);
                    if (!string.IsNullOrEmpty(odkaz))
                    {
                        string newText = GetDefpoznText(xmlDoc, odkaz);
                        if (!string.IsNullOrEmpty(newText))
                        {
                            line = ReplaceTextWithTags(line, startIndex, endIndex, newText, "\\f");
                            modified = true;
                        }
                    }
                }
            }
            return line;
        }

        private static string ReplaceOdkazo(string line, XmlDocument xmlDoc, ref bool modified)
        {
            int startIndex = line.IndexOf("<odkazo n=\"");
            if (startIndex != -1)
            {
                int endIndex = FindEndIndex(line, startIndex);
                if (endIndex != -1)
                {
                    string odkaz = ExtractOdkazValue(line, startIndex);
                    if (!string.IsNullOrEmpty(odkaz))
                    {
                        string newText = GetDefpoznoText(xmlDoc, odkaz);
                        if (!string.IsNullOrEmpty(newText))
                        {
                            line = ReplaceTextWithTags(line, startIndex, endIndex, newText, "\\fo");
                            modified = true;
                        }
                    }
                }
            }
            return line;
        }

        private static int FindEndIndex(string line, int startIndex)
        {
            Regex r = new Regex("\"/>");
            Match m = r.Match(line, startIndex);
            return m.Success ? m.Index + m.Length : -1;
        }

        private static string ExtractOdkazValue(string line, int startIndex)
        {
            int startQuote = line.IndexOf('"', startIndex) + 1;
            int endQuote = line.IndexOf('"', startQuote);
            return endQuote > startQuote ? line.Substring(startQuote, endQuote - startQuote) : null;
        }

        private static string GetDefpoznText(XmlDocument xmlDoc, string odkaz)
        {
            XmlNode defpoznNode = xmlDoc.SelectSingleNode($"//defpozn[@n='{odkaz}']");
            return defpoznNode?.InnerXml;
        }

        private static string GetDefpoznoText(XmlDocument xmlDoc, string odkaz)
        {
            XmlNode defpoznoNode = xmlDoc.SelectSingleNode($"//defpozno[@n='{odkaz}']");
            return defpoznoNode?.InnerXml;
        }

        private static string ReplaceTextWithTags(string line, int startIndex, int endIndex, string newText, string tag)
        {
            string sample = line.Substring(startIndex, endIndex - startIndex);
            string modified = line.Replace(sample, $"{tag}{newText}{tag}*");
            return modified;
        }
    }
}
