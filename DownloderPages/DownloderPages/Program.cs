using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloderPages
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Downloader downloader = new Downloader();

            Console.WriteLine("Выберите файл с списком сайтов: ");
            string[] pages = GetPages();

            string pathToSave = GetPathToSave();

            Console.Write("Сохранять в HTML? [y/N]");
            var key = Console.ReadKey();
            string keyName = key.KeyChar.ToString().ToLower();

            bool saveToHTML = false;
            if (keyName == "y" || keyName == "у")
            {
                saveToHTML = true;
            }
            else
            {
                saveToHTML = false;
            }

            int index = 1;
            foreach (var page in pages)
            {
                if (saveToHTML)
                {
                    downloader.DownloadPage(page, pathToSave+index+".html",2);
                }
                else
                {
                    downloader.DownloadPage(page, pathToSave + index + ".txt",2);
                }
                
                index++;
            }

        }

        private static string[] GetPages()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        string fileContent = reader.ReadToEnd();
                        string[] lines = fileContent.Split('\n');
                        return lines;
                    }
                   
                }
                return null;
            }
        }

        private static string GetPathToSave()
        {
            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    string filePath = saveFileDialog.FileName;

                    return filePath;

                }
                return null;
            }
        }
    }

    class Downloader
    {
        public void DownloadPage(string url,string pathToSave, int timeWait)
        {
            using(WebClient webClient = new WebClient())
            {

                webClient.DownloadFile(url, pathToSave);
            }
        }

    }
}
