using System.IO;
using System.Xml.Serialization;
using System.Xml;
using Microsoft.Win32;
using System.Windows;
using System;

namespace OptimaFarm_TestTask.Helpers
{
    public class CompactXMLParser
    {
        public static bool TryDeSerialyze<T>(string xml, ref T result)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                using (TextReader reader = new StringReader(xml))
                using (XmlReader xmlReader = XmlReader.Create(reader))
                {
                    if (xmlSerializer.CanDeserialize(xmlReader))
                    {
                        result = (T)xmlSerializer.Deserialize(xmlReader);
                        return true;
                    }
                }

            }
            catch 
            {
                return false;
            }
            return false;
        }
    }

    public class CompactXMLSerialyzer
    {
        public static string GetXMLFromObject<T>( T toSerialyze)
        {
            if(toSerialyze == null)
                return string.Empty;

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            using (StringWriter stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, toSerialyze);
                return stringWriter.ToString();
            }
        }
    }

    public class FileHelper
    {
        public static bool GetFromWhereToLoad(ref string path)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "xml documents (.xml)|*.xml";
            var result = openFileDialog.ShowDialog();
            if (result == false) return false;
            else path = openFileDialog.FileName;
            return true;

        }

        public static bool GetWhereToSave(ref string path) 
        {
            var saveDialog = new SaveFileDialog();
            saveDialog.FileName = "Exported emploees cards";
            saveDialog.DefaultExt = ".xml";
            saveDialog.Filter = "xml documents (.xml)|*.xml";
            var result = saveDialog.ShowDialog();
            if (result == false) return false;
            path = saveDialog.FileName;
            return true;
        }

        public static string ReadFromFile(string filePath)
        {
            string s = string.Empty;
            using (StreamReader reader = new StreamReader(filePath))
            {
                try
                {
                    s = reader.ReadToEnd();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message.ToString()); }
            }
            return s;
        }

        public static async void WriteToFile(string text, string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                await writer.WriteLineAsync(text);
            }
        }
    }

    

}