using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
       private static string bucketName = "kiki-test";

        public Form1()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        //浏览
        private void button3_Click(object sender, EventArgs e)
        {
            //打开文件对话框，获取文件路径

            //将文件路径填入文本框
            UploadFilePath.Text = ("C:\\Users\\Administrator.DIY-20170222TLQ\\Desktop\\ecd\\temp\\doctor.txt");

            logPrint("文件路径 ：" + UploadFilePath.Text);
        }
        //上传
        private void button1_Click(object sender, EventArgs e)
        {
            string userGuid = UploadUserId.Text;
            string reportNum = UploadReportNum.Text;
            //获取文件
            string uploadFile = UploadFilePath.Text;
            string bucketKey = userGuid + "/" + reportNum + ".txt";

            // string content = FileReadAndWrite.ReadFile(uploadFile);
            //上传文件到OSS
            string msg = PutObjectSample.PutObjectFromFile(uploadFile,bucketName, bucketKey);

            logPrint(msg);
        }

        private void Download_Click(object sender, EventArgs e)
        {
            string userGuid = DownUserId.Text;
            string reportNum = DownReportNum.Text;
            string fileName =  reportNum + ".txt";
            string bucketKey = userGuid + "/" + fileName;

            string dirToDownload = Config.DirToDownload;

            Downpath.Text = (dirToDownload);
            //从OSS下载文件
            string msg = GetObjectSample.GetObject(bucketName, bucketKey, dirToDownload, fileName);
            logPrint(msg);
        }

        private void logPrint(string log)
        {
            string current_log = richTextBox1.Text;
            richTextBox1.Text = (current_log + "\n" + log);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
         
        }

        private void UploadUserId_TextChanged(object sender, EventArgs e)
        {

        }

        private void UploadReportNum_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
}
