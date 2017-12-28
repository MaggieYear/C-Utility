namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.UploadReportNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.UploadUserId = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Upload = new System.Windows.Forms.Button();
            this.UploadFilePath = new System.Windows.Forms.TextBox();
            this.SelectFile = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Download = new System.Windows.Forms.Button();
            this.DownReportNum = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.DownUserId = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Downpath = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.UploadReportNum);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.UploadUserId);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.Upload);
            this.groupBox1.Controls.Add(this.UploadFilePath);
            this.groupBox1.Controls.Add(this.SelectFile);
            this.groupBox1.Location = new System.Drawing.Point(31, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(409, 130);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "upload";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // UploadReportNum
            // 
            this.UploadReportNum.Location = new System.Drawing.Point(131, 88);
            this.UploadReportNum.Name = "UploadReportNum";
            this.UploadReportNum.Size = new System.Drawing.Size(100, 21);
            this.UploadReportNum.TabIndex = 9;
            this.UploadReportNum.TextChanged += new System.EventHandler(this.UploadReportNum_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(56, 91);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "报告编码";
            // 
            // UploadUserId
            // 
            this.UploadUserId.Location = new System.Drawing.Point(131, 60);
            this.UploadUserId.Name = "UploadUserId";
            this.UploadUserId.Size = new System.Drawing.Size(100, 21);
            this.UploadUserId.TabIndex = 7;
            this.UploadUserId.TextChanged += new System.EventHandler(this.UploadUserId_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(56, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 6;
            this.label5.Text = "用户编码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "选择你要上传的文件";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Upload
            // 
            this.Upload.Location = new System.Drawing.Point(319, 86);
            this.Upload.Name = "Upload";
            this.Upload.Size = new System.Drawing.Size(75, 23);
            this.Upload.TabIndex = 4;
            this.Upload.Text = "上传";
            this.Upload.UseVisualStyleBackColor = true;
            this.Upload.Click += new System.EventHandler(this.button1_Click);
            // 
            // UploadFilePath
            // 
            this.UploadFilePath.Location = new System.Drawing.Point(131, 31);
            this.UploadFilePath.Name = "UploadFilePath";
            this.UploadFilePath.Size = new System.Drawing.Size(182, 21);
            this.UploadFilePath.TabIndex = 2;
            this.UploadFilePath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // SelectFile
            // 
            this.SelectFile.Location = new System.Drawing.Point(319, 31);
            this.SelectFile.Name = "SelectFile";
            this.SelectFile.Size = new System.Drawing.Size(75, 23);
            this.SelectFile.TabIndex = 3;
            this.SelectFile.Text = "浏览";
            this.SelectFile.UseVisualStyleBackColor = true;
            this.SelectFile.Click += new System.EventHandler(this.button3_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "所有文件(*.*)|*.*|文本文件(*.txt)|*.txt|WPS文档(*.wps)|*.wps|Word文档(*.doc)|*.doc";
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Download);
            this.groupBox2.Controls.Add(this.DownReportNum);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.DownUserId);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.Downpath);
            this.groupBox2.Location = new System.Drawing.Point(31, 160);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(409, 136);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "download";
            // 
            // Download
            // 
            this.Download.Location = new System.Drawing.Point(319, 97);
            this.Download.Name = "Download";
            this.Download.Size = new System.Drawing.Size(75, 23);
            this.Download.TabIndex = 6;
            this.Download.Text = "下载";
            this.Download.UseVisualStyleBackColor = true;
            this.Download.Click += new System.EventHandler(this.Download_Click);
            // 
            // DownReportNum
            // 
            this.DownReportNum.Location = new System.Drawing.Point(131, 64);
            this.DownReportNum.Name = "DownReportNum";
            this.DownReportNum.Size = new System.Drawing.Size(100, 21);
            this.DownReportNum.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(46, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "报告编码";
            // 
            // DownUserId
            // 
            this.DownUserId.Location = new System.Drawing.Point(131, 30);
            this.DownUserId.Name = "DownUserId";
            this.DownUserId.Size = new System.Drawing.Size(100, 21);
            this.DownUserId.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "用户编码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "选择你要下载的路径";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // Downpath
            // 
            this.Downpath.Location = new System.Drawing.Point(131, 99);
            this.Downpath.Name = "Downpath";
            this.Downpath.Size = new System.Drawing.Size(182, 21);
            this.Downpath.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(31, 337);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(409, 96);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "";
            this.richTextBox1.TextChanged += new System.EventHandler(this.richTextBox1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 319);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(23, 12);
            this.label7.TabIndex = 3;
            this.label7.Text = "log";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(477, 461);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "OSS上传下载操作";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.TextBox UploadFilePath;
        private System.Windows.Forms.Button SelectFile;
        private System.Windows.Forms.Button Upload;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox Downpath;
        private System.Windows.Forms.TextBox UploadReportNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox UploadUserId;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Download;
        private System.Windows.Forms.TextBox DownReportNum;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox DownUserId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label7;
    }
}

