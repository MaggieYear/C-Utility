using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class FileReadAndWrite
    {

        /// <summary>  
        /// 写文件  
        /// </summary>  
        /// <param name="Path">文件路径</param>  
        /// <param name="Name">文件名(包括后缀名)</param>  
        /// <param name="content">内容</param>  
        /// <returns></returns>  
        public static bool WriteFile(string Path, string Name, string content)
        {
            try
            {
               
                if (!Directory.Exists(Path))
                {
                    Directory.CreateDirectory(Path);
                }
               
                if (!File.Exists(Name))
                {
                    FileStream fs = File.Create(Name);
                    fs.Close();
                }
                string FilePath = Path + Name;
                StreamWriter sw = new StreamWriter(FilePath, false, System.Text.Encoding.GetEncoding("utf-8"));          
                sw.WriteLine(content);
                sw.Close();
                sw.Dispose();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>  
        /// 读文件  
        /// </summary>  
        /// <param name="path">文件路径</param>  
        /// <returns></returns>  
        public static string ReadFile(string Path)
        {
            try
            {
                StreamReader sr = new StreamReader(Path);
                Stream fileStream = sr.BaseStream;
                string content = sr.ReadToEnd().ToString();
                sr.Close();
                return content;
            }
            catch
            {
                return "<span style='color:red; font-size:x-large;'>Sorry,The Ariticle wasn't found!! It may have been deleted accidentally from Server.</span>";
            }
        }
    }
}
