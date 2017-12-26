using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksums;

namespace ZipUtils
{
    public class ZipTool
    {

        public static int avg = 1024 * 1024 * 100;//100MB写一次  
        static void Main(string[] args)
        {
            string fileToZip = "C:\\Users\\snhjl\\Desktop\\test2";
            string zipedFile = "C:\\Users\\snhjl\\Desktop\\test2.zip";
            //压缩文件夹
            bool result1 = ToZip(fileToZip, zipedFile);

            string file = "C:\\Users\\snhjl\\Desktop\\HttpURLConnHelper.java";
            string zipedFile2 = "C:\\Users\\snhjl\\Desktop\\test3.zip";
            //压缩文件
            bool result2 = ToZip(file, zipedFile2);

            string fileToUnZip = "C:\\Users\\snhjl\\Desktop\\test2.zip";
            string zipedFolder = "C:\\Users\\snhjl\\Desktop\\test4";
            //解压zip压缩包到指定文件夹
            bool result3 = UnMultiLevelZip(fileToUnZip, zipedFolder);
      
            Console.WriteLine("压缩文件是否成功："+result2);
            Console.WriteLine("解压缩是否成功："+result3);
            Console.ReadKey();
        }
        public static bool ToZip(string fileToZip, string zipedFile)
        {

            bool result = false;
            if (Directory.Exists(fileToZip))
            {
                //判断原始文件是文件夹
                result = MultiLevelZip(fileToZip, zipedFile);
            }
            else if (File.Exists(fileToZip))
            {
                //判断原始文件是单个文件
                result = SingleLevelZip(fileToZip, zipedFile);
            }
            return result;
        }
        ///<summary>
        ///压缩文件 和 文件夹，不压缩顶级目录
        ///</summary>
        ///<param name="FolderToZip">待压缩的文件夹，全路径格式</param>
        ///<param name="ZipedFile">压缩后生成的压缩文件名，全路径格式</param>
        ///<returns>压缩是否成功</returns>
        public static bool MultiLevelZip(string FolderToZip, string ZipedFile)
        {
            if (!Directory.Exists(FolderToZip))
                return false;
            if (ZipedFile == string.Empty)
            {
                //如果为空则文件名为待压缩的文件名加上.rar
                ZipedFile = FolderToZip + ".zip";
            }
            ZipOutputStream s = new ZipOutputStream(File.Create(ZipedFile));
            s.SetLevel(6);
            string[] filenames = Directory.GetFiles(FolderToZip);
            ZipEntry entry = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            foreach (string file in filenames)
            {
                //压缩文件
                fs = File.OpenRead(file);
                byte[] buffer = new byte[avg];
                entry = new ZipEntry(Path.GetFileName(file));
                entry.DateTime = DateTime.Now;
                entry.Size = fs.Length;
                s.PutNextEntry(entry);
                for (int i = 0; i < fs.Length; i += avg)
                {
                    if (i + avg > fs.Length)
                    {
                        //不足100MB的部分写剩余部分
                        buffer = new byte[fs.Length - i];
                    }
                    fs.Read(buffer, 0, buffer.Length);
                    s.Write(buffer, 0, buffer.Length);
                }
            }
            if (fs != null)
            {
                fs.Close();
                fs = null;
            }
            if (entry != null)
                entry = null;
            GC.Collect();
            GC.Collect(1);
            //压缩目录
            string[] folders = Directory.GetDirectories(FolderToZip);
            foreach (string folder in folders)
            {
                if (!ZipDirectory(folder, s, ""));
            }
            s.Finish();
            s.Close();
            return true;
        }

        /// <summary>
        /// 压缩文件或文件夹
        /// </summary>
        /// <param name="fileToZip">要压缩的路径</param>
        /// <param name="zipedFile">压缩后的文件名</param>
        /// <returns>压缩结果</returns>
        public static bool SingleLevelZip(string fileToZip, string zipedFile)
        {
            bool result = Zip(fileToZip, zipedFile, null);
            return result;
        }

        /// <summary>
        /// 压缩文件或文件夹
        /// </summary>
        /// <param name="fileToZip">要压缩的路径</param>
        /// <param name="zipedFile">压缩后的文件名</param>
        /// <param name="password">密码</param>
        /// <returns>压缩结果</returns>
        public static bool Zip(string fileToZip, string zipedFile, string password)
        {
            bool result = false;
            if (Directory.Exists(fileToZip))
                result = ZipDirectory(fileToZip, zipedFile, password);
            else if (File.Exists(fileToZip))
                result = ZipFile(fileToZip, zipedFile, password);
            return result;
        }

        /// <summary>
        /// 压缩文件夹
        /// </summary>
        /// <param name="folderToZip">要压缩的文件夹路径</param>
        /// <param name="zipedFile">压缩文件完整路径</param>
        /// <param name="password">密码</param>
        /// <returns>是否压缩成功</returns>
        public static bool ZipDirectory(string folderToZip, string zipedFile, string password)
        {
            bool result = false;
            if (!Directory.Exists(folderToZip))
                return result;
            ZipOutputStream zipStream = new ZipOutputStream(File.Create(zipedFile));
            zipStream.SetLevel(6);
            if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
            result = ZipDirectory(folderToZip, zipStream, "");
            zipStream.Finish();
            zipStream.Close();
            return result;
        }
        #region 压缩
        /// <summary>
        /// 递归压缩文件夹的内部方法
        /// </summary>
        /// <param name="folderToZip">要压缩的文件夹路径</param>
        /// <param name="zipStream">压缩输出流</param>
        /// <param name="parentFolderName">此文件夹的上级文件夹</param>
        /// <returns></returns>
        private static bool ZipDirectory(string folderToZip, ZipOutputStream zipStream, string parentFolderName)
        {
            bool result = true;
            string[] folders, files;
            ZipEntry ent = null;
            FileStream fs = null;
            Crc32 crc = new Crc32();
            try
            {
                ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/"));
                zipStream.PutNextEntry(ent);
                zipStream.Flush();
                files = Directory.GetFiles(folderToZip);
                foreach (string file in files)
                {
                    fs = File.OpenRead(file);
                    byte[] buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    ent = new ZipEntry(Path.Combine(parentFolderName, Path.GetFileName(folderToZip) + "/" + Path.GetFileName(file)));
                    ent.DateTime = DateTime.Now;
                    ent.Size = fs.Length;
                    fs.Dispose();
                    crc.Reset();
                    crc.Update(buffer);
                    ent.Crc = crc.Value;
                    zipStream.PutNextEntry(ent);
                    zipStream.Write(buffer, 0, buffer.Length);
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    //  fs.Close();
                    fs.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            folders = Directory.GetDirectories(folderToZip);
            foreach (string folder in folders)
                if (!ZipDirectory(folder, zipStream, folderToZip))
                    return false;
            return result;
        }

        /// <summary>
        /// 压缩文件
        /// </summary>
        /// <param name="fileToZip">要压缩的文件全名</param>
        /// <param name="zipedFile">压缩后的文件名</param>
        /// <param name="password">密码</param>
        /// <returns>压缩结果</returns>
        public static bool ZipFile(string fileToZip, string zipedFile, string password)
        {
            bool result = true;
            ZipOutputStream zipStream = null;
            FileStream fs = null;
            ZipEntry ent = null;
            if (!File.Exists(fileToZip))
                return false;
            try
            {
                fs = File.OpenRead(fileToZip);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                // fs.Close();
                fs.Dispose();
                fs = File.Create(zipedFile);
                zipStream = new ZipOutputStream(fs);
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                ent = new ZipEntry(Path.GetFileName(fileToZip));
                zipStream.PutNextEntry(ent);
                zipStream.SetLevel(6);
                zipStream.Write(buffer, 0, buffer.Length);
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (zipStream != null)
                {
                    zipStream.Finish();
                    zipStream.Close();
                }
                if (ent != null)
                {
                    ent = null;
                }
                if (fs != null)
                {
                    //fs.Close();
                    fs.Dispose();
                }
            }
            GC.Collect();
            GC.Collect(1);
            return result;
        }

        public static bool UnMultiLevelZip(string fileToUnZip, string zipedFolder)
        {
            bool result = UnZipToDirectory(fileToUnZip, zipedFolder, null);
            return result;
        }

        /// <summary>     
        /// 解压功能     
        /// </summary>     
        /// <param name="fileToUnZip">待解压的文件</param>     
        /// <param name="zipedFolder">指定解压目标目录</param>     
        /// <param name="password">密码</param>     
        /// <returns>解压结果</returns>     
        private static bool UnZipToDirectory(string fileToUnZip, string zipedFolder, string password)
        {
            bool result = true;
            FileStream fs = null;
            ZipInputStream zipStream = null;
            ZipEntry ent = null;
            string fileName;

            if (!File.Exists(fileToUnZip))
                return false;

            if (!Directory.Exists(zipedFolder))
                Directory.CreateDirectory(zipedFolder);

            try
            {
                zipStream = new ZipInputStream(File.OpenRead(fileToUnZip.Trim()));
                if (!string.IsNullOrEmpty(password)) zipStream.Password = password;
                while ((ent = zipStream.GetNextEntry()) != null)
                {
                    if (!string.IsNullOrEmpty(ent.Name))
                    {
                        fileName = Path.Combine(zipedFolder, ent.Name);
                        fileName = fileName.Replace('/', '\\');

                        if (fileName.EndsWith("\\"))
                        {
                            Directory.CreateDirectory(fileName);
                            continue;
                        }

                        using (fs = File.Create(fileName))
                        {
                            int size = 2048;
                            byte[] data = new byte[size];
                            while (true)
                            {
                                size = zipStream.Read(data, 0, data.Length);
                                if (size > 0)
                                    fs.Write(data, 0, size);
                                else
                                    break;
                            }
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (zipStream != null)
                {
                    zipStream.Close();
                    zipStream.Dispose();
                }
                if (ent != null)
                {
                    ent = null;
                }
                GC.Collect();
                GC.Collect(1);
            }
            return result;
        }
    }
    #endregion
}
