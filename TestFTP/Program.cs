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
        public static List<string> Local_Short_listFiles;
        public static string[] main_Long_files;
        public static string[] main_Short_files;

        

        static void Main(string[] args)
        {
            Local_long_listFiles = new List<string>();
            Local_Short_listFiles = new List<string>();
            

            string dirName = ConfigurationManager.AppSettings["dirName"];


            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs_ = Directory.GetDirectories(dirName);
                for(int i = 0; i < dirs_.Length; i++)
                {
                    Local_long_listFiles.Add(dirs_[i]);
                    dirs_[i] = new FileInfo(dirs_[i]).Name; // Выделяем короткое название из пути
                    Local_Short_listFiles.Add(dirs_[i]);

                    Console.WriteLine(Local_long_listFiles[i]);
                    Console.WriteLine(Local_Short_listFiles[i]);

                    /* string[] files = Directory.GetFiles(s);
                     foreach (string p in files)
                     {
                         Console.WriteLine(p);
                     }*/
                }
            }


            FTPConnection ftp = new FTPConnection();
            ftp.ConnectMode = FTPConnectMode.ACTIVE;
            ftp.ServerAddress = ConfigurationManager.AppSettings["ServerAddress"];
            ftp.UserName = ConfigurationManager.AppSettings["UserName"];
            ftp.Password = ConfigurationManager.AppSettings["UserName"];
            ftp.Connect();

            FTPFile[] filesFTP = ftp.GetFileInfos();
            if(filesFTP.Length > 0)
            {
                //int a = 0;
                for(int i = 0; i < Local_Short_listFiles.Count; i++)
                {
                    //Console.WriteLine(filesFTP[i].Name);
                    for(int k = 0; k < filesFTP.Length; k++)
                    {
                        if(filesFTP[i].Name == Local_Short_listFiles[k])
                        {
                            break;
                        }
                        if (k == filesFTP.Length - 1)
                        {
                            try
                            {
                                ftp.CreateDirectory(Local_Short_listFiles[i]);
                                main_Long_files = Directory.GetFiles(Local_long_listFiles[i]);
                                main_Short_files = Directory.GetFiles(Local_long_listFiles[i]);

                                for (int s = 0; s < main_Short_files.Length; s++)
                                {
                                    main_Short_files[s] = new FileInfo(main_Short_files[s]).Name;
                                    ftp.UploadFile(main_Long_files[s], ($"/{Local_Short_listFiles[i]}/{main_Short_files[s]}"), true);
                                }
                            }
                            catch 
                            {
                                Console.WriteLine("Ошибка при создании папки и копировании файлов");
                            }
                        }
                    }
                    // удаляем каталог после записи
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Local_long_listFiles[i]);
                        dirInfo.Delete(true);
                        Console.WriteLine("Каталог удален");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Local_Short_listFiles.Count; i++)
                {
                    try
                    {
                        ftp.CreateDirectory(Local_Short_listFiles[i]);
                        main_Long_files = Directory.GetFiles(Local_long_listFiles[i]);
                        main_Short_files = Directory.GetFiles(Local_long_listFiles[i]);

                        for (int s = 0; s < main_Short_files.Length; s++)
                        {
                            main_Short_files[s] = new FileInfo(main_Short_files[s]).Name;
                            ftp.UploadFile(main_Long_files[s], ($"/{Local_Short_listFiles[i]}/{main_Short_files[s]}"), true);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Ошибка при создании папки и копировании файлов");
                    }
                    // удаляем каталог после записи
                    try
                    {
                        DirectoryInfo dirInfo = new DirectoryInfo(Local_long_listFiles[i]);
                        dirInfo.Delete(true);
                        Console.WriteLine("Каталог удален");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            

            //ftp.CreateDirectory("testDir");
            //ftp.UploadFile("C:/VisualStudioProject/TestFTP/NET/TestFTP/TestFTP/bin/Debug/test.txt", "/testDir/test.txt", true);
            //ftp.DownloadFile("test.txt", "testDir/test.txt");


            /*string[] fileDetails = ftp.GetFiles();
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
            string[] dirs = Directory.GetDirectories(@"D:\\APP", "*", SearchOption.TopDirectoryOnly);
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
