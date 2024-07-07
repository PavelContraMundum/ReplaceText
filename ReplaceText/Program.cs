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
            string inputFilePath = "D:\\Downloads\\Studijni Bible\\Studijni_Bible\\GenMod.xml";
            string outputFilePath = "D:\\Downloads\\GenModified.txt";

            try
            {
                ProcessFile(inputFilePath, outputFilePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void ProcessFile(string inputFilePath, string outputFilePath)
        {
            bool startWriting = false;
            var output = new StringBuilder();

            using (var reader = new StreamReader(inputFilePath, Encoding.UTF8))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(inputFilePath);

                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!startWriting && line.Contains("<titulek>"))
                    {
                        startWriting = true;
                    }

                    if (startWriting)
                    {
                        line = ProcessLine(line, xmlDoc);
                        output.AppendLine(line);
                    }
                }
            }

            // Odstranění posledního tagu </kniha>
            string finalOutput = output.ToString().TrimEnd();
            int lastBookTagIndex = finalOutput.LastIndexOf("</kniha>");
            if (lastBookTagIndex != -1)
            {
                finalOutput = finalOutput.Substring(0, lastBookTagIndex);
            }

            if (string.IsNullOrWhiteSpace(finalOutput))
            {
                Console.WriteLine("Warning: No content was processed. The output file will be empty.");
            }
            else
            {
                try
                {
                    //  File.WriteAllText(outputFilePath, finalOutput);


                    using (StreamWriter writer = new StreamWriter(outputFilePath, false, Encoding.UTF8))
                    {
                        writer.Write(finalOutput);
                    }

                    File.WriteAllText(outputFilePath, finalOutput);
                    Console.WriteLine("Waiting for 5 seconds...");
                    System.Threading.Thread.Sleep(5000);  // Počká 5 sekund
                    if (File.Exists(outputFilePath))
                    {
                        string contentAfterWait = File.ReadAllText(outputFilePath);
                        Console.WriteLine($"File content after 5 seconds: {contentAfterWait.Substring(0, Math.Min(100, contentAfterWait.Length))}");
                    }
                 //   string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                 //   string newOutputFilePath = Path.Combine(desktopPath, "GenModified.txt");
                   
                //    File.WriteAllText(newOutputFilePath, finalOutput);

                    Console.WriteLine($"Attempting to write {finalOutput.Length} characters to file.");

                    // Ověření, zda byl soubor skutečně vytvořen a obsahuje data
                    if (File.Exists(outputFilePath))
                    {
                        long fileSize = new FileInfo(outputFilePath).Length;
                        Console.WriteLine($"File created. File size: {fileSize} bytes");

                        if (fileSize > 0)
                        {
                            Console.WriteLine($"Output successfully written to: {outputFilePath}");
                            Console.WriteLine($"Final content length: {finalOutput.Length} characters");

                            string fileContent = File.ReadAllText(outputFilePath);
                            Console.WriteLine($"First 100 characters of file content: {fileContent.Substring(0, Math.Min(100, fileContent.Length))}");
                            //Console.ReadLine(); 
                        }
                        else
                        {
                            Console.WriteLine("Warning: File was created but is empty.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error: File was not created.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error writing to file: {ex.Message}");
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                }
            }
        }

        private static string ProcessLine(string line, XmlDocument xmlDoc)
        {
            bool modified;
            do
            {
                modified = false;
                line = ReplaceOdkaz(line, xmlDoc, ref modified);
                line = ReplaceOdkazo(line, xmlDoc, ref modified);
            } while (modified);

            return line;
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
                            line = ReplaceTextWithTags(line, startIndex, endIndex, newText, "f");
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
                            line = ReplaceTextWithTags(line, startIndex, endIndex, newText, "fo");
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
            string modified = line.Replace(sample, $"/{tag}{newText}/{tag}*");
            return modified;
        }
    }
}