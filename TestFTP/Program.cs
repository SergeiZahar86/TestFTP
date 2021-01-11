using EnterpriseDT.Net.Ftp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace TestFTP
{
    class Program
    {
        public static List<string> Local_long_listFiles;
        static void Main(string[] args)
        {
            Local_long_listFiles = new List<string>();
            string dirName = ConfigurationManager.AppSettings["dirName"];


            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs_ = Directory.GetDirectories(dirName);
                foreach (string s in dirs_)
                {
                    Console.WriteLine(s);
                    Console.WriteLine();
                    Console.WriteLine("Файлы:");
                    string[] files = Directory.GetFiles(s);
                    foreach (string p in files)
                    {
                        Console.WriteLine(p);
                        Local_long_listFiles.Add(p);
                    }
                }
            }


            FTPConnection ftp = new FTPConnection();
            ftp.ConnectMode = FTPConnectMode.ACTIVE;
            ftp.ServerAddress = ConfigurationManager.AppSettings["ServerAddress"];
            ftp.UserName = ConfigurationManager.AppSettings["UserName"];
            ftp.Password = ConfigurationManager.AppSettings["UserName"];

            ftp.Connect();
            try
            {
                ftp.CreateDirectory("testDir");
            }
            catch
            {

            }
            //ftp.UploadFile("C:/VisualStudioProject/TestFTP/NET/TestFTP/TestFTP/bin/Debug/test.txt", "/testDir/test.txt", true);
            //ftp.DownloadFile("test.txt", "testDir/test.txt");
            string[] fileDetails = ftp.GetFiles();
            foreach(string a in fileDetails)
            {
                Console.WriteLine(a);
                if(a == "./testDir")
                {
                    Console.WriteLine("папка testDir");
                }
            }
            FTPFile[] filesFTP = ftp.GetFileInfos();
            foreach(FTPFile d in filesFTP)
            {
                Console.WriteLine(d);
                if(d.Name == "testDir")
                {
                    Console.WriteLine("папка testDir");
                    ftp.ChangeWorkingDirectory(d.Name);
                    //foreach()
                    FTPFile[] files__FTP = ftp.GetFileInfos();
                    foreach(FTPFile g in files__FTP)
                    {
                        Console.WriteLine(g);
                    }

                }
            }
            ftp.Close();

            // Список локальных директорий
            /*string[] dirs = Directory.GetDirectories(@"D:\\APP", "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < dirs.Length; i++)
            {
                dirs[i] = new FileInfo(dirs[i]).Name; // Выделяем короткое название из пути
            }

            // Список удаленных директорий
            FTPFile[] file__Details = ftp.GetFileInfos("");
            foreach (FTPFile file in file__Details)
            {
                if (file.Dir && Array.Exists(dirs, x => x == file.Name))
                {
                    Console.WriteLine(file.Name + " " + file.Dir);
                }

                //Console.WriteLine(file.Name + " " + file.Dir);
            }*/




            




            Console.Read();

        }
    }
}
