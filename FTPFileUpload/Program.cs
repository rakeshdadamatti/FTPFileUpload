using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FTPFileUpload
{
    class Program
    {
        static void Main(string[] args)
        {
                    string folderPath = @"D:\Rakesh\MyProjects\FTPFileUpload\UploadFile\";
                    string[] getFiles = System.IO.Directory.GetFiles(folderPath, "*.zip");

                    if (getFiles.Length != 0)
                    {
                        string fileName = Path.GetFileName(getFiles[0]);
                        string filePath = Path.Combine(folderPath, fileName);
                        if (File.Exists(filePath))
                        {
                            Upload(filePath, fileName);
                        }
                    }
                    else
                    {
                        Console.WriteLine("File not found!");
                        Console.ReadKey();
                    }
            }

        static void Upload(string filePath,string fileName)
        {
            try
            {
            string FTP_SERVER = "ftp://speedtest.tele2.net/upload/";   //FTP server path

            FtpWebRequest request =
            (FtpWebRequest)WebRequest.Create(FTP_SERVER + fileName);

            // Un comment below code & pass username & password if FTP is secured with credentials
            // request.Credentials = new NetworkCredential(FTP_USERNAME, FTP_PASSWORD);   
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream fileStream = File.OpenRead(filePath))
            using (Stream ftpStream = request.GetRequestStream())
            {
                byte[] buffer = new byte[fileStream.Length];
                int read;
                while ((read = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ftpStream.Write(buffer, 0, read);
                    Console.WriteLine("Uploaded {0} bytes", fileStream.Position);
                }
                Console.WriteLine("Uploaded Successfully.");
                Console.ReadKey();
            }
            }
            catch (WebException e)
            {
                Console.WriteLine("Status:" + e.Status + "\n" + "Error Message:" + e.Message);
                Console.ReadKey();
            }
        }
        }
    }
