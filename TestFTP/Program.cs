using EnterpriseDT.Net.Ftp;
using System;
using System.IO;

namespace TestFTP
{
    class Program
    {
        static void Main(string[] args)
        {
            string dirName = "C:/VisualStudioProject/TestFTP";

            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                    Console.WriteLine();
                    Console.WriteLine("Файлы:");
                    string[] files = Directory.GetFiles(s);
                    foreach (string p in files)
                    {
                        Console.WriteLine(p);
                    }
                }
            }
            Console.Read();


            /*FTPConnection ftp = new FTPConnection();
            ftp.ConnectMode = FTPConnectMode.ACTIVE;
            ftp.ServerAddress = "10.90.90.58";
            ftp.UserName = "incube";
            ftp.Password = "incube";

            ftp.Connect();
            ftp.CreateDirectory("testDir");
            ftp.UploadFile("C:/Users/Sergei/Desktop/C#/NET/TestFTP/TestFTP/bin/Debug/test.txt", "/testDir/test.txt", true);
            //ftp.DownloadFile("test.txt", "testDir/test.txt");
            ftp.Close();*/

        }
    }
}
