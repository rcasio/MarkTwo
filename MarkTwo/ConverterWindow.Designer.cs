namespace MarkTwo
{
    partial class ConverterWindow
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.Client_ProgressText = new System.Windows.Forms.Label();
            this.Excel_Directory = new System.Windows.Forms.Label();
            this.Client_Target_Data = new System.Windows.Forms.Label();
            this.StartButton = new System.Windows.Forms.Button();
            this.TargetPath = new System.Windows.Forms.Label();
            this.Server_Target_Data = new System.Windows.Forms.Label();
            this.Server_ProgressText = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ExtreactionReadyProgressBar = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label2 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox4 = new System.Windows.Forms.RichTextBox();
            this.progressBar7 = new System.Windows.Forms.ProgressBar();
            this.label7 = new System.Windows.Forms.Label();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.progressBar3 = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.richTextBox3 = new System.Windows.Forms.RichTextBox();
            this.progressBar4 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox5 = new System.Windows.Forms.RichTextBox();
            this.progressBar5 = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.richTextBox6 = new System.Windows.Forms.RichTextBox();
            this.progressBar6 = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.ProgressText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Client_ProgressText
            // 
            this.Client_ProgressText.AutoSize = true;
            this.Client_ProgressText.ForeColor = System.Drawing.Color.DarkGray;
            this.Client_ProgressText.Location = new System.Drawing.Point(316, 420);
            this.Client_ProgressText.Name = "Client_ProgressText";
            this.Client_ProgressText.Size = new System.Drawing.Size(81, 12);
            this.Client_ProgressText.TabIndex = 2;
            this.Client_ProgressText.Text = "진행 테이블 : ";
            // 
            // Excel_Directory
            // 
            this.Excel_Directory.AutoSize = true;
            this.Excel_Directory.ForeColor = System.Drawing.Color.DarkGray;
            this.Excel_Directory.Location = new System.Drawing.Point(316, 441);
            this.Excel_Directory.Name = "Excel_Directory";
            this.Excel_Directory.Size = new System.Drawing.Size(53, 12);
            this.Excel_Directory.TabIndex = 3;
            this.Excel_Directory.Text = "엑셀경로";
            this.Excel_Directory.Click += new System.EventHandler(this.Excel_Directory_Click);
            // 
            // Client_Target_Data
            // 
            this.Client_Target_Data.AutoSize = true;
            this.Client_Target_Data.ForeColor = System.Drawing.Color.DarkGray;
            this.Client_Target_Data.Location = new System.Drawing.Point(326, 332);
            this.Client_Target_Data.Name = "Client_Target_Data";
            this.Client_Target_Data.Size = new System.Drawing.Size(53, 12);
            this.Client_Target_Data.TabIndex = 4;
            this.Client_Target_Data.Text = "데이터 : ";
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StartButton.ForeColor = System.Drawing.Color.DarkGray;
            this.StartButton.Location = new System.Drawing.Point(22, 6);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(75, 23);
            this.StartButton.TabIndex = 5;
            this.StartButton.Text = "시작(R)";
            this.StartButton.UseVisualStyleBackColor = false;
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // TargetPath
            // 
            this.TargetPath.AutoSize = true;
            this.TargetPath.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.TargetPath.Location = new System.Drawing.Point(316, 470);
            this.TargetPath.Name = "TargetPath";
            this.TargetPath.Size = new System.Drawing.Size(53, 12);
            this.TargetPath.TabIndex = 19;
            this.TargetPath.Text = "이동경로";
            this.TargetPath.Click += new System.EventHandler(this.TargetPath_Click);
            // 
            // Server_Target_Data
            // 
            this.Server_Target_Data.AutoSize = true;
            this.Server_Target_Data.ForeColor = System.Drawing.Color.DarkGray;
            this.Server_Target_Data.Location = new System.Drawing.Point(316, 388);
            this.Server_Target_Data.Name = "Server_Target_Data";
            this.Server_Target_Data.Size = new System.Drawing.Size(53, 12);
            this.Server_Target_Data.TabIndex = 32;
            this.Server_Target_Data.Text = "데이터 : ";
            this.Server_Target_Data.Click += new System.EventHandler(this.Server_Target_Data_Click);
            // 
            // Server_ProgressText
            // 
            this.Server_ProgressText.AutoSize = true;
            this.Server_ProgressText.ForeColor = System.Drawing.Color.DarkGray;
            this.Server_ProgressText.Location = new System.Drawing.Point(316, 354);
            this.Server_ProgressText.Name = "Server_ProgressText";
            this.Server_ProgressText.Size = new System.Drawing.Size(81, 12);
            this.Server_ProgressText.TabIndex = 31;
            this.Server_ProgressText.Text = "진행 테이블 : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(427, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "테이블 추출 준비 작업";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ExtreactionReadyProgressBar
            // 
            this.ExtreactionReadyProgressBar.Location = new System.Drawing.Point(429, 35);
            this.ExtreactionReadyProgressBar.Name = "ExtreactionReadyProgressBar";
            this.ExtreactionReadyProgressBar.Size = new System.Drawing.Size(996, 44);
            this.ExtreactionReadyProgressBar.TabIndex = 34;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(429, 126);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(375, 15);
            this.progressBar2.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(427, 105);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "데이터 추출";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(429, 156);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(375, 144);
            this.richTextBox1.TabIndex = 47;
            this.richTextBox1.Text = "";
            // 
            // richTextBox4
            // 
            this.richTextBox4.Location = new System.Drawing.Point(429, 372);
            this.richTextBox4.Name = "richTextBox4";
            this.richTextBox4.Size = new System.Drawing.Size(375, 144);
            this.richTextBox4.TabIndex = 57;
            this.richTextBox4.Text = "";
            // 
            // progressBar7
            // 
            this.progressBar7.Location = new System.Drawing.Point(429, 342);
            this.progressBar7.Name = "progressBar7";
            this.progressBar7.Size = new System.Drawing.Size(375, 15);
            this.progressBar7.TabIndex = 56;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.DarkGray;
            this.label7.Location = new System.Drawing.Point(427, 321);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 12);
            this.label7.TabIndex = 55;
            this.label7.Text = "데이터 추출";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(824, 372);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(291, 144);
            this.richTextBox2.TabIndex = 63;
            this.richTextBox2.Text = "";
            // 
            // progressBar3
            // 
            this.progressBar3.Location = new System.Drawing.Point(824, 342);
            this.progressBar3.Name = "progressBar3";
            this.progressBar3.Size = new System.Drawing.Size(291, 15);
            this.progressBar3.TabIndex = 62;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(822, 321);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 12);
            this.label3.TabIndex = 61;
            this.label3.Text = "데이터 추출";
            // 
            // richTextBox3
            // 
            this.richTextBox3.Location = new System.Drawing.Point(824, 156);
            this.richTextBox3.Name = "richTextBox3";
            this.richTextBox3.Size = new System.Drawing.Size(291, 144);
            this.richTextBox3.TabIndex = 60;
            this.richTextBox3.Text = "";
            // 
            // progressBar4
            // 
            this.progressBar4.Location = new System.Drawing.Point(824, 126);
            this.progressBar4.Name = "progressBar4";
            this.progressBar4.Size = new System.Drawing.Size(291, 15);
            this.progressBar4.TabIndex = 59;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(822, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 12);
            this.label4.TabIndex = 58;
            this.label4.Text = "데이터 추출";
            // 
            // richTextBox5
            // 
            this.richTextBox5.Location = new System.Drawing.Point(1134, 372);
            this.richTextBox5.Name = "richTextBox5";
            this.richTextBox5.Size = new System.Drawing.Size(291, 144);
            this.richTextBox5.TabIndex = 69;
            this.richTextBox5.Text = "";
            // 
            // progressBar5
            // 
            this.progressBar5.Location = new System.Drawing.Point(1134, 342);
            this.progressBar5.Name = "progressBar5";
            this.progressBar5.Size = new System.Drawing.Size(291, 15);
            this.progressBar5.TabIndex = 68;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.DarkGray;
            this.label5.Location = new System.Drawing.Point(1132, 321);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(69, 12);
            this.label5.TabIndex = 67;
            this.label5.Text = "데이터 추출";
            // 
            // richTextBox6
            // 
            this.richTextBox6.Location = new System.Drawing.Point(1134, 156);
            this.richTextBox6.Name = "richTextBox6";
            this.richTextBox6.Size = new System.Drawing.Size(291, 144);
            this.richTextBox6.TabIndex = 66;
            this.richTextBox6.Text = "";
            // 
            // progressBar6
            // 
            this.progressBar6.Location = new System.Drawing.Point(1134, 126);
            this.progressBar6.Name = "progressBar6";
            this.progressBar6.Size = new System.Drawing.Size(291, 15);
            this.progressBar6.TabIndex = 65;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.DarkGray;
            this.label6.Location = new System.Drawing.Point(1132, 105);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 12);
            this.label6.TabIndex = 64;
            this.label6.Text = "데이터 추출";
            // 
            // ProgressText
            // 
            this.ProgressText.BackColor = System.Drawing.SystemColors.Window;
            this.ProgressText.Location = new System.Drawing.Point(22, 35);
            this.ProgressText.Name = "ProgressText";
            this.ProgressText.ReadOnly = true;
            this.ProgressText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ProgressText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ProgressText.Size = new System.Drawing.Size(386, 481);
            this.ProgressText.TabIndex = 70;
            this.ProgressText.Text = "";
            // 
            // ConverterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1437, 622);
            this.Controls.Add(this.richTextBox5);
            this.Controls.Add(this.progressBar5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.richTextBox6);
            this.Controls.Add(this.progressBar6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.progressBar3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.richTextBox3);
            this.Controls.Add(this.progressBar4);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.richTextBox4);
            this.Controls.Add(this.progressBar7);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ExtreactionReadyProgressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Server_Target_Data);
            this.Controls.Add(this.Server_ProgressText);
            this.Controls.Add(this.TargetPath);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.Client_Target_Data);
            this.Controls.Add(this.Excel_Directory);
            this.Controls.Add(this.Client_ProgressText);
            this.Controls.Add(this.ProgressText);
            this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Name = "ConverterWindow";
            this.ShowIcon = false;
            this.Text = "MarkTwo v0.5";
            this.Load += new System.EventHandler(this.ConverterWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Client_ProgressText;
        private System.Windows.Forms.Label Excel_Directory;
        private System.Windows.Forms.Label Client_Target_Data;
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label TargetPath;
        private System.Windows.Forms.Label Server_Target_Data;
        private System.Windows.Forms.Label Server_ProgressText;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar ExtreactionReadyProgressBar;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox4;
        private System.Windows.Forms.ProgressBar progressBar7;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.ProgressBar progressBar3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox richTextBox3;
        private System.Windows.Forms.ProgressBar progressBar4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox richTextBox5;
        private System.Windows.Forms.ProgressBar progressBar5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox richTextBox6;
        private System.Windows.Forms.ProgressBar progressBar6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox ProgressText;
    }
}

