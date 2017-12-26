using System;

public class FileTypeUtil
{
    public string GetFileType(string FilePath)
    {

        
            FileStream fs = new FileStream(@FilePath, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(fs);
            string fileclass = "";
            try
            {
                for (int i = 0; i < 2; i++)
                {
                    fileclass += reader.ReadByte().ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (fileclass == "8075")
            {
                MessageBox.Show("xlsx,zip,pptx,mmap,zip");
            }
            if (fileclass == "208207")
            {
                MessageBox.Show("xls.doc.ppt");
            }
            if (fileclass == "4944")
            {
                MessageBox.Show("csv");
            }
            fs.Close();
        
	}
}
