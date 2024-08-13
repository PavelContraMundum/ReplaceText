using System;
using System.Collections.Generic;
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

                Encoding cp1250Encoding = Encoding.GetEncoding(1250);
                Encoding utf8Encoding = Encoding.UTF8;

                // Převod souboru na UTF-8 při čtení
                string fileContent = File.ReadAllText(inputFilePath, cp1250Encoding);

                Console.WriteLine("Převádím obsah souboru na UTF-8...");

                // Zpracování obsahu souboru
                ProcessFile(fileContent, outputFilePath, utf8Encoding);

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

        private static void ProcessFile(string fileContent, string outputFilePath, Encoding encoding)
        {
            Console.WriteLine("Začínám zpracování souboru...");

            var defpoznDictionary = new Dictionary<string, string>();
            var defpoznoDictionary = new Dictionary<string, string>();

            // Uloží všechny poznámky
            StoreAllDefpozn(fileContent, defpoznDictionary);
            StoreAllDefpozno(fileContent, defpoznoDictionary);

            // Najde začátek obsahu (od <titulek>)
            int startIndex = fileContent.IndexOf("<titulek>");
            if (startIndex == -1)
            {
                throw new Exception("Tag <titulek> nebyl nalezen.");
            }

            // Zpracuje celý obsah najednou
            string processedContent = ProcessContent(fileContent.Substring(startIndex), defpoznDictionary, defpoznoDictionary);

            // Zapíše zpracovaný obsah do výstupního souboru v UTF-8 kódování
            File.WriteAllText(outputFilePath, processedContent, encoding);

            Console.WriteLine($"Výstup byl úspěšně zapsán do: {outputFilePath}");
        }

        private static void StoreAllDefpozn(string content, Dictionary<string, string> defpoznDictionary)
        {
            string pattern = @"<defpozn n=""(.+?)"">(.*?)</defpozn>";
            foreach (Match match in Regex.Matches(content, pattern, RegexOptions.Singleline))
            {
                string n = match.Groups[1].Value;
                string poznContent = match.Groups[2].Value;
                defpoznDictionary[n] = poznContent;
            }
        }

        private static void StoreAllDefpozno(string content, Dictionary<string, string> defpoznoDictionary)
        {
            string pattern = @"<defpozno n=""(.+?)"">(.*?)</defpozno>";
            foreach (Match match in Regex.Matches(content, pattern, RegexOptions.Singleline))
            {
                string n = match.Groups[1].Value;
                string poznContent = match.Groups[2].Value;
                defpoznoDictionary[n] = poznContent;
            }
        }

        private static string ProcessContent(string content, Dictionary<string, string> defpoznDictionary, Dictionary<string, string> defpoznoDictionary)
        {
            bool modified;
            do
            {
                modified = false;
                var result = ReplaceOdkaz(content, defpoznDictionary);
                content = result.Item1;
                modified |= result.Item2;

                result = ReplaceOdkazo(content, defpoznoDictionary);
                content = result.Item1;
                modified |= result.Item2;
            } while (modified);

            return content;
        }

        private static (string, bool) ReplaceOdkaz(string content, Dictionary<string, string> defpoznDictionary)
        {
            bool modified = false;
            string pattern = @"<odkaz n=""(.+?)""/>";
            string result = Regex.Replace(content, pattern, match =>
            {
                string n = match.Groups[1].Value;
                if (defpoznDictionary.TryGetValue(n, out string poznContent))
                {
                    modified = true;
                    return $"\\f{poznContent}\\f*";
                }
                return match.Value;
            });
            return (result, modified);
        }

        private static (string, bool) ReplaceOdkazo(string content, Dictionary<string, string> defpoznoDictionary)
        {
            bool modified = false;
            string pattern = @"<odkazo n=""(.+?)""/>";
            string result = Regex.Replace(content, pattern, match =>
            {
                string n = match.Groups[1].Value;
                if (defpoznoDictionary.TryGetValue(n, out string poznContent))
                {
                    modified = true;
                    return $"\\fo{poznContent}\\fo*";
                }
                return match.Value;
            });
            return (result, modified);
        }
    }
}
