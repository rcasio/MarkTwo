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
            this.MultiligualThreadLabelProgressBar = new System.Windows.Forms.ProgressBar();
            this.MultiligualThreadLabel = new System.Windows.Forms.Label();
            this.MultiligualThreadText = new System.Windows.Forms.RichTextBox();
            this.ServerThread01Text = new System.Windows.Forms.RichTextBox();
            this.ServerThread01ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ServerThread01 = new System.Windows.Forms.Label();
            this.ClientThread01Text = new System.Windows.Forms.RichTextBox();
            this.ClientThread01progressBar = new System.Windows.Forms.ProgressBar();
            this.ClientThread01Label = new System.Windows.Forms.Label();
            this.ServerThread02Text = new System.Windows.Forms.RichTextBox();
            this.ServerThread02ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ServerThread02 = new System.Windows.Forms.Label();
            this.ClientThread02Text = new System.Windows.Forms.RichTextBox();
            this.ClientThread02progressBar = new System.Windows.Forms.ProgressBar();
            this.ClientThread02 = new System.Windows.Forms.Label();
            this.ExtreactionReadyText = new System.Windows.Forms.RichTextBox();
            this.ClientThread01TableLabel = new System.Windows.Forms.Label();
            this.ClientThread02TableLabel = new System.Windows.Forms.Label();
            this.ServerThread01TableLabel = new System.Windows.Forms.Label();
            this.ServerThread02TableLabel = new System.Windows.Forms.Label();
            this.MultilingualThreadTableLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
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
            this.ExtreactionReadyProgressBar.Size = new System.Drawing.Size(1123, 44);
            this.ExtreactionReadyProgressBar.TabIndex = 34;
            // 
            // MultiligualThreadLabelProgressBar
            // 
            this.MultiligualThreadLabelProgressBar.Location = new System.Drawing.Point(429, 126);
            this.MultiligualThreadLabelProgressBar.Name = "MultiligualThreadLabelProgressBar";
            this.MultiligualThreadLabelProgressBar.Size = new System.Drawing.Size(375, 15);
            this.MultiligualThreadLabelProgressBar.TabIndex = 36;
            // 
            // MultiligualThreadLabel
            // 
            this.MultiligualThreadLabel.AutoSize = true;
            this.MultiligualThreadLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.MultiligualThreadLabel.Location = new System.Drawing.Point(427, 105);
            this.MultiligualThreadLabel.Name = "MultiligualThreadLabel";
            this.MultiligualThreadLabel.Size = new System.Drawing.Size(81, 12);
            this.MultiligualThreadLabel.TabIndex = 35;
            this.MultiligualThreadLabel.Text = "다국어 스레드";
            // 
            // MultiligualThreadText
            // 
            this.MultiligualThreadText.Location = new System.Drawing.Point(429, 156);
            this.MultiligualThreadText.Name = "MultiligualThreadText";
            this.MultiligualThreadText.Size = new System.Drawing.Size(375, 555);
            this.MultiligualThreadText.TabIndex = 47;
            this.MultiligualThreadText.Text = "";
            // 
            // ServerThread01Text
            // 
            this.ServerThread01Text.Location = new System.Drawing.Point(824, 467);
            this.ServerThread01Text.Name = "ServerThread01Text";
            this.ServerThread01Text.Size = new System.Drawing.Size(357, 244);
            this.ServerThread01Text.TabIndex = 63;
            this.ServerThread01Text.Text = "";
            // 
            // ServerThread01ProgressBar
            // 
            this.ServerThread01ProgressBar.Location = new System.Drawing.Point(824, 437);
            this.ServerThread01ProgressBar.Name = "ServerThread01ProgressBar";
            this.ServerThread01ProgressBar.Size = new System.Drawing.Size(357, 15);
            this.ServerThread01ProgressBar.TabIndex = 62;
            // 
            // ServerThread01
            // 
            this.ServerThread01.AutoSize = true;
            this.ServerThread01.ForeColor = System.Drawing.Color.DarkGray;
            this.ServerThread01.Location = new System.Drawing.Point(822, 416);
            this.ServerThread01.Name = "ServerThread01";
            this.ServerThread01.Size = new System.Drawing.Size(81, 12);
            this.ServerThread01.TabIndex = 61;
            this.ServerThread01.Text = "서버01 스레드";
            // 
            // ClientThread01Text
            // 
            this.ClientThread01Text.Location = new System.Drawing.Point(824, 156);
            this.ClientThread01Text.Name = "ClientThread01Text";
            this.ClientThread01Text.Size = new System.Drawing.Size(357, 244);
            this.ClientThread01Text.TabIndex = 60;
            this.ClientThread01Text.Text = "";
            // 
            // ClientThread01progressBar
            // 
            this.ClientThread01progressBar.Location = new System.Drawing.Point(824, 126);
            this.ClientThread01progressBar.Name = "ClientThread01progressBar";
            this.ClientThread01progressBar.Size = new System.Drawing.Size(357, 15);
            this.ClientThread01progressBar.TabIndex = 59;
            // 
            // ClientThread01Label
            // 
            this.ClientThread01Label.AutoSize = true;
            this.ClientThread01Label.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientThread01Label.Location = new System.Drawing.Point(822, 105);
            this.ClientThread01Label.Name = "ClientThread01Label";
            this.ClientThread01Label.Size = new System.Drawing.Size(117, 12);
            this.ClientThread01Label.TabIndex = 58;
            this.ClientThread01Label.Text = "클라이언트01 스레드";
            // 
            // ServerThread02Text
            // 
            this.ServerThread02Text.Location = new System.Drawing.Point(1196, 470);
            this.ServerThread02Text.Name = "ServerThread02Text";
            this.ServerThread02Text.Size = new System.Drawing.Size(356, 241);
            this.ServerThread02Text.TabIndex = 69;
            this.ServerThread02Text.Text = "";
            // 
            // ServerThread02ProgressBar
            // 
            this.ServerThread02ProgressBar.Location = new System.Drawing.Point(1196, 440);
            this.ServerThread02ProgressBar.Name = "ServerThread02ProgressBar";
            this.ServerThread02ProgressBar.Size = new System.Drawing.Size(356, 15);
            this.ServerThread02ProgressBar.TabIndex = 68;
            // 
            // ServerThread02
            // 
            this.ServerThread02.AutoSize = true;
            this.ServerThread02.ForeColor = System.Drawing.Color.DarkGray;
            this.ServerThread02.Location = new System.Drawing.Point(1194, 419);
            this.ServerThread02.Name = "ServerThread02";
            this.ServerThread02.Size = new System.Drawing.Size(81, 12);
            this.ServerThread02.TabIndex = 67;
            this.ServerThread02.Text = "서버02 스레드";
            // 
            // ClientThread02Text
            // 
            this.ClientThread02Text.Location = new System.Drawing.Point(1196, 156);
            this.ClientThread02Text.Name = "ClientThread02Text";
            this.ClientThread02Text.Size = new System.Drawing.Size(356, 244);
            this.ClientThread02Text.TabIndex = 66;
            this.ClientThread02Text.Text = "";
            // 
            // ClientThread02progressBar
            // 
            this.ClientThread02progressBar.Location = new System.Drawing.Point(1195, 126);
            this.ClientThread02progressBar.Name = "ClientThread02progressBar";
            this.ClientThread02progressBar.Size = new System.Drawing.Size(357, 15);
            this.ClientThread02progressBar.TabIndex = 65;
            // 
            // ClientThread02
            // 
            this.ClientThread02.AutoSize = true;
            this.ClientThread02.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientThread02.Location = new System.Drawing.Point(1194, 105);
            this.ClientThread02.Name = "ClientThread02";
            this.ClientThread02.Size = new System.Drawing.Size(117, 12);
            this.ClientThread02.TabIndex = 64;
            this.ClientThread02.Text = "클라이언트02 스레드";
            // 
            // ExtreactionReadyText
            // 
            this.ExtreactionReadyText.BackColor = System.Drawing.SystemColors.Window;
            this.ExtreactionReadyText.Location = new System.Drawing.Point(22, 126);
            this.ExtreactionReadyText.Name = "ExtreactionReadyText";
            this.ExtreactionReadyText.ReadOnly = true;
            this.ExtreactionReadyText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ExtreactionReadyText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ExtreactionReadyText.Size = new System.Drawing.Size(386, 585);
            this.ExtreactionReadyText.TabIndex = 70;
            this.ExtreactionReadyText.Text = "";
            // 
            // ClientThread01TableLabel
            // 
            this.ClientThread01TableLabel.AutoSize = true;
            this.ClientThread01TableLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientThread01TableLabel.Location = new System.Drawing.Point(976, 105);
            this.ClientThread01TableLabel.Name = "ClientThread01TableLabel";
            this.ClientThread01TableLabel.Size = new System.Drawing.Size(145, 12);
            this.ClientThread01TableLabel.TabIndex = 71;
            this.ClientThread01TableLabel.Text = "테이블 이름 [테이블이름]";
            // 
            // ClientThread02TableLabel
            // 
            this.ClientThread02TableLabel.AutoSize = true;
            this.ClientThread02TableLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientThread02TableLabel.Location = new System.Drawing.Point(1346, 105);
            this.ClientThread02TableLabel.Name = "ClientThread02TableLabel";
            this.ClientThread02TableLabel.Size = new System.Drawing.Size(145, 12);
            this.ClientThread02TableLabel.TabIndex = 72;
            this.ClientThread02TableLabel.Text = "테이블 이름 [테이블이름]";
            // 
            // ServerThread01TableLabel
            // 
            this.ServerThread01TableLabel.AutoSize = true;
            this.ServerThread01TableLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ServerThread01TableLabel.Location = new System.Drawing.Point(976, 416);
            this.ServerThread01TableLabel.Name = "ServerThread01TableLabel";
            this.ServerThread01TableLabel.Size = new System.Drawing.Size(145, 12);
            this.ServerThread01TableLabel.TabIndex = 73;
            this.ServerThread01TableLabel.Text = "테이블 이름 [테이블이름]";
            // 
            // ServerThread02TableLabel
            // 
            this.ServerThread02TableLabel.AutoSize = true;
            this.ServerThread02TableLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ServerThread02TableLabel.Location = new System.Drawing.Point(1346, 419);
            this.ServerThread02TableLabel.Name = "ServerThread02TableLabel";
            this.ServerThread02TableLabel.Size = new System.Drawing.Size(145, 12);
            this.ServerThread02TableLabel.TabIndex = 74;
            this.ServerThread02TableLabel.Text = "테이블 이름 [테이블이름]";
            // 
            // MultilingualThreadTableLabel
            // 
            this.MultilingualThreadTableLabel.AutoSize = true;
            this.MultilingualThreadTableLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.MultilingualThreadTableLabel.Location = new System.Drawing.Point(564, 105);
            this.MultilingualThreadTableLabel.Name = "MultilingualThreadTableLabel";
            this.MultilingualThreadTableLabel.Size = new System.Drawing.Size(145, 12);
            this.MultilingualThreadTableLabel.TabIndex = 75;
            this.MultilingualThreadTableLabel.Text = "테이블 이름 [테이블이름]";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkGray;
            this.label2.Location = new System.Drawing.Point(20, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 12);
            this.label2.TabIndex = 76;
            this.label2.Text = "전체 테이블 개수 : ";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(20, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 12);
            this.label3.TabIndex = 77;
            this.label3.Text = "전체 레이블 개수 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkGray;
            this.label4.Location = new System.Drawing.Point(20, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 12);
            this.label4.TabIndex = 78;
            this.label4.Text = "시간 :";
            // 
            // ConverterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1574, 753);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.MultilingualThreadTableLabel);
            this.Controls.Add(this.ServerThread02TableLabel);
            this.Controls.Add(this.ServerThread01TableLabel);
            this.Controls.Add(this.ClientThread02TableLabel);
            this.Controls.Add(this.ClientThread01TableLabel);
            this.Controls.Add(this.ServerThread02Text);
            this.Controls.Add(this.ServerThread02ProgressBar);
            this.Controls.Add(this.ServerThread02);
            this.Controls.Add(this.ClientThread02Text);
            this.Controls.Add(this.ClientThread02progressBar);
            this.Controls.Add(this.ClientThread02);
            this.Controls.Add(this.ServerThread01Text);
            this.Controls.Add(this.ServerThread01ProgressBar);
            this.Controls.Add(this.ServerThread01);
            this.Controls.Add(this.ClientThread01Text);
            this.Controls.Add(this.ClientThread01progressBar);
            this.Controls.Add(this.ClientThread01Label);
            this.Controls.Add(this.MultiligualThreadText);
            this.Controls.Add(this.MultiligualThreadLabelProgressBar);
            this.Controls.Add(this.MultiligualThreadLabel);
            this.Controls.Add(this.ExtreactionReadyProgressBar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Server_Target_Data);
            this.Controls.Add(this.Server_ProgressText);
            this.Controls.Add(this.TargetPath);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.Client_Target_Data);
            this.Controls.Add(this.Excel_Directory);
            this.Controls.Add(this.Client_ProgressText);
            this.Controls.Add(this.ExtreactionReadyText);
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
        private System.Windows.Forms.Label MultiligualThreadLabel;
        private System.Windows.Forms.Label ServerThread01;
        private System.Windows.Forms.Label ClientThread01Label;
        private System.Windows.Forms.Label ServerThread02;
        private System.Windows.Forms.Label ClientThread02;
        public System.Windows.Forms.RichTextBox ExtreactionReadyText;
        public System.Windows.Forms.ProgressBar ExtreactionReadyProgressBar;
        public System.Windows.Forms.ProgressBar MultiligualThreadLabelProgressBar;
        public System.Windows.Forms.RichTextBox MultiligualThreadText;
        public System.Windows.Forms.RichTextBox ServerThread01Text;
        public System.Windows.Forms.ProgressBar ServerThread01ProgressBar;
        public System.Windows.Forms.RichTextBox ClientThread01Text;
        public System.Windows.Forms.ProgressBar ClientThread01progressBar;
        public System.Windows.Forms.RichTextBox ServerThread02Text;
        public System.Windows.Forms.ProgressBar ServerThread02ProgressBar;
        public System.Windows.Forms.RichTextBox ClientThread02Text;
        public System.Windows.Forms.ProgressBar ClientThread02progressBar;
        public System.Windows.Forms.Label ClientThread01TableLabel;
        public System.Windows.Forms.Label ClientThread02TableLabel;
        public System.Windows.Forms.Label ServerThread01TableLabel;
        public System.Windows.Forms.Label ServerThread02TableLabel;
        public System.Windows.Forms.Label MultilingualThreadTableLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}

