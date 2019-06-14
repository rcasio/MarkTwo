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
            this.components = new System.ComponentModel.Container();
            this.StartButton = new System.Windows.Forms.Button();
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
            this.ClientThread01TableLabel = new System.Windows.Forms.Label();
            this.ClientThread02TableLabel = new System.Windows.Forms.Label();
            this.ServerThread01TableLabel = new System.Windows.Forms.Label();
            this.ServerThread02TableLabel = new System.Windows.Forms.Label();
            this.MultilingualThreadTableLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.TimerLabel = new System.Windows.Forms.Label();
            this.TimerObj = new System.Windows.Forms.Timer(this.components);
            this.ExtreactionReadyText = new System.Windows.Forms.RichTextBox();
            this.ClientDataCount = new System.Windows.Forms.Label();
            this.ServerDataCount = new System.Windows.Forms.Label();
            this.ClientThread04TableLabel = new System.Windows.Forms.Label();
            this.ClientThread03TableLabel = new System.Windows.Forms.Label();
            this.ClientThread04Text = new System.Windows.Forms.RichTextBox();
            this.ClientThread04progressBar = new System.Windows.Forms.ProgressBar();
            this.ClientThread04Label = new System.Windows.Forms.Label();
            this.ClientThread03Text = new System.Windows.Forms.RichTextBox();
            this.ClientThread03Label = new System.Windows.Forms.Label();
            this.ClientThread03progressBar = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.DarkGray;
            this.label1.Location = new System.Drawing.Point(283, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "테이블 추출 준비 작업";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // ExtreactionReadyProgressBar
            // 
            this.ExtreactionReadyProgressBar.Location = new System.Drawing.Point(22, 126);
            this.ExtreactionReadyProgressBar.Name = "ExtreactionReadyProgressBar";
            this.ExtreactionReadyProgressBar.Size = new System.Drawing.Size(386, 15);
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
            this.MultiligualThreadText.Size = new System.Drawing.Size(375, 722);
            this.MultiligualThreadText.TabIndex = 47;
            this.MultiligualThreadText.Text = "";
            // 
            // ServerThread01Text
            // 
            this.ServerThread01Text.Location = new System.Drawing.Point(824, 682);
            this.ServerThread01Text.Name = "ServerThread01Text";
            this.ServerThread01Text.Size = new System.Drawing.Size(357, 196);
            this.ServerThread01Text.TabIndex = 63;
            this.ServerThread01Text.Text = "";
            // 
            // ServerThread01ProgressBar
            // 
            this.ServerThread01ProgressBar.Location = new System.Drawing.Point(824, 652);
            this.ServerThread01ProgressBar.Name = "ServerThread01ProgressBar";
            this.ServerThread01ProgressBar.Size = new System.Drawing.Size(357, 15);
            this.ServerThread01ProgressBar.TabIndex = 62;
            // 
            // ServerThread01
            // 
            this.ServerThread01.AutoSize = true;
            this.ServerThread01.ForeColor = System.Drawing.Color.DarkGray;
            this.ServerThread01.Location = new System.Drawing.Point(822, 631);
            this.ServerThread01.Name = "ServerThread01";
            this.ServerThread01.Size = new System.Drawing.Size(81, 12);
            this.ServerThread01.TabIndex = 61;
            this.ServerThread01.Text = "서버01 스레드";
            // 
            // ClientThread01Text
            // 
            this.ClientThread01Text.Location = new System.Drawing.Point(824, 156);
            this.ClientThread01Text.Name = "ClientThread01Text";
            this.ClientThread01Text.Size = new System.Drawing.Size(357, 196);
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
            this.ServerThread02Text.Location = new System.Drawing.Point(1196, 685);
            this.ServerThread02Text.Name = "ServerThread02Text";
            this.ServerThread02Text.Size = new System.Drawing.Size(356, 193);
            this.ServerThread02Text.TabIndex = 69;
            this.ServerThread02Text.Text = "";
            // 
            // ServerThread02ProgressBar
            // 
            this.ServerThread02ProgressBar.Location = new System.Drawing.Point(1196, 655);
            this.ServerThread02ProgressBar.Name = "ServerThread02ProgressBar";
            this.ServerThread02ProgressBar.Size = new System.Drawing.Size(356, 15);
            this.ServerThread02ProgressBar.TabIndex = 68;
            // 
            // ServerThread02
            // 
            this.ServerThread02.AutoSize = true;
            this.ServerThread02.ForeColor = System.Drawing.Color.DarkGray;
            this.ServerThread02.Location = new System.Drawing.Point(1194, 634);
            this.ServerThread02.Name = "ServerThread02";
            this.ServerThread02.Size = new System.Drawing.Size(81, 12);
            this.ServerThread02.TabIndex = 67;
            this.ServerThread02.Text = "서버02 스레드";
            // 
            // ClientThread02Text
            // 
            this.ClientThread02Text.Location = new System.Drawing.Point(1196, 156);
            this.ClientThread02Text.Name = "ClientThread02Text";
            this.ClientThread02Text.Size = new System.Drawing.Size(356, 196);
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
            this.ServerThread01TableLabel.Location = new System.Drawing.Point(976, 631);
            this.ServerThread01TableLabel.Name = "ServerThread01TableLabel";
            this.ServerThread01TableLabel.Size = new System.Drawing.Size(145, 12);
            this.ServerThread01TableLabel.TabIndex = 73;
            this.ServerThread01TableLabel.Text = "테이블 이름 [테이블이름]";
            // 
            // ServerThread02TableLabel
            // 
            this.ServerThread02TableLabel.AutoSize = true;
            this.ServerThread02TableLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ServerThread02TableLabel.Location = new System.Drawing.Point(1346, 634);
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
            this.label2.Location = new System.Drawing.Point(20, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 12);
            this.label2.TabIndex = 76;
            this.label2.Text = "클라이언트 데이터 개수 :";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(20, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 12);
            this.label3.TabIndex = 77;
            this.label3.Text = "서버 데이터 개수 :";
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.TimeLabel.Location = new System.Drawing.Point(20, 93);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(37, 12);
            this.TimeLabel.TabIndex = 78;
            this.TimeLabel.Text = "시간 :";
            // 
            // TimerLabel
            // 
            this.TimerLabel.AutoSize = true;
            this.TimerLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.TimerLabel.Location = new System.Drawing.Point(63, 93);
            this.TimerLabel.Name = "TimerLabel";
            this.TimerLabel.Size = new System.Drawing.Size(77, 12);
            this.TimerLabel.TabIndex = 79;
            this.TimerLabel.Text = "999999999999";
            // 
            // TimerObj
            // 
            this.TimerObj.Interval = 1000;
            this.TimerObj.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // ExtreactionReadyText
            // 
            this.ExtreactionReadyText.BackColor = System.Drawing.SystemColors.Window;
            this.ExtreactionReadyText.Location = new System.Drawing.Point(22, 156);
            this.ExtreactionReadyText.Name = "ExtreactionReadyText";
            this.ExtreactionReadyText.ReadOnly = true;
            this.ExtreactionReadyText.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ExtreactionReadyText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.ExtreactionReadyText.Size = new System.Drawing.Size(386, 722);
            this.ExtreactionReadyText.TabIndex = 70;
            this.ExtreactionReadyText.Text = "";
            // 
            // ClientDataCount
            // 
            this.ClientDataCount.AutoSize = true;
            this.ClientDataCount.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientDataCount.Location = new System.Drawing.Point(167, 46);
            this.ClientDataCount.Name = "ClientDataCount";
            this.ClientDataCount.Size = new System.Drawing.Size(11, 12);
            this.ClientDataCount.TabIndex = 80;
            this.ClientDataCount.Text = "0";
            // 
            // ServerDataCount
            // 
            this.ServerDataCount.AutoSize = true;
            this.ServerDataCount.ForeColor = System.Drawing.Color.DarkGray;
            this.ServerDataCount.Location = new System.Drawing.Point(136, 70);
            this.ServerDataCount.Name = "ServerDataCount";
            this.ServerDataCount.Size = new System.Drawing.Size(11, 12);
            this.ServerDataCount.TabIndex = 81;
            this.ServerDataCount.Text = "0";
            // 
            // ClientThread04TableLabel
            // 
            this.ClientThread04TableLabel.AutoSize = true;
            this.ClientThread04TableLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientThread04TableLabel.Location = new System.Drawing.Point(1346, 371);
            this.ClientThread04TableLabel.Name = "ClientThread04TableLabel";
            this.ClientThread04TableLabel.Size = new System.Drawing.Size(145, 12);
            this.ClientThread04TableLabel.TabIndex = 89;
            this.ClientThread04TableLabel.Text = "테이블 이름 [테이블이름]";
            // 
            // ClientThread03TableLabel
            // 
            this.ClientThread03TableLabel.AutoSize = true;
            this.ClientThread03TableLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientThread03TableLabel.Location = new System.Drawing.Point(976, 371);
            this.ClientThread03TableLabel.Name = "ClientThread03TableLabel";
            this.ClientThread03TableLabel.Size = new System.Drawing.Size(145, 12);
            this.ClientThread03TableLabel.TabIndex = 88;
            this.ClientThread03TableLabel.Text = "테이블 이름 [테이블이름]";
            // 
            // ClientThread04Text
            // 
            this.ClientThread04Text.Location = new System.Drawing.Point(1196, 422);
            this.ClientThread04Text.Name = "ClientThread04Text";
            this.ClientThread04Text.Size = new System.Drawing.Size(356, 196);
            this.ClientThread04Text.TabIndex = 87;
            this.ClientThread04Text.Text = "";
            // 
            // ClientThread04progressBar
            // 
            this.ClientThread04progressBar.Location = new System.Drawing.Point(1195, 392);
            this.ClientThread04progressBar.Name = "ClientThread04progressBar";
            this.ClientThread04progressBar.Size = new System.Drawing.Size(357, 15);
            this.ClientThread04progressBar.TabIndex = 86;
            // 
            // ClientThread04Label
            // 
            this.ClientThread04Label.AutoSize = true;
            this.ClientThread04Label.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientThread04Label.Location = new System.Drawing.Point(1194, 371);
            this.ClientThread04Label.Name = "ClientThread04Label";
            this.ClientThread04Label.Size = new System.Drawing.Size(117, 12);
            this.ClientThread04Label.TabIndex = 85;
            this.ClientThread04Label.Text = "클라이언트04 스레드";
            // 
            // ClientThread03Text
            // 
            this.ClientThread03Text.Location = new System.Drawing.Point(824, 422);
            this.ClientThread03Text.Name = "ClientThread03Text";
            this.ClientThread03Text.Size = new System.Drawing.Size(357, 196);
            this.ClientThread03Text.TabIndex = 84;
            this.ClientThread03Text.Text = "";
            // 
            // ClientThread03Label
            // 
            this.ClientThread03Label.AutoSize = true;
            this.ClientThread03Label.ForeColor = System.Drawing.Color.DarkGray;
            this.ClientThread03Label.Location = new System.Drawing.Point(822, 371);
            this.ClientThread03Label.Name = "ClientThread03Label";
            this.ClientThread03Label.Size = new System.Drawing.Size(117, 12);
            this.ClientThread03Label.TabIndex = 82;
            this.ClientThread03Label.Text = "클라이언트03 스레드";
            this.ClientThread03Label.Click += new System.EventHandler(this.label7_Click);
            // 
            // ClientThread03progressBar
            // 
            this.ClientThread03progressBar.Location = new System.Drawing.Point(824, 392);
            this.ClientThread03progressBar.Name = "ClientThread03progressBar";
            this.ClientThread03progressBar.Size = new System.Drawing.Size(357, 15);
            this.ClientThread03progressBar.TabIndex = 83;
            // 
            // ConverterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1574, 890);
            this.Controls.Add(this.ClientThread04TableLabel);
            this.Controls.Add(this.ClientThread03TableLabel);
            this.Controls.Add(this.ClientThread04Text);
            this.Controls.Add(this.ClientThread04progressBar);
            this.Controls.Add(this.ClientThread04Label);
            this.Controls.Add(this.ClientThread03Text);
            this.Controls.Add(this.ClientThread03progressBar);
            this.Controls.Add(this.ClientThread03Label);
            this.Controls.Add(this.ServerDataCount);
            this.Controls.Add(this.ClientDataCount);
            this.Controls.Add(this.TimerLabel);
            this.Controls.Add(this.TimeLabel);
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
            this.Controls.Add(this.StartButton);
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
        private System.Windows.Forms.Button StartButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label MultiligualThreadLabel;
        private System.Windows.Forms.Label ServerThread01;
        private System.Windows.Forms.Label ClientThread01Label;
        private System.Windows.Forms.Label ServerThread02;
        private System.Windows.Forms.Label ClientThread02;
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
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label TimerLabel;
        public System.Windows.Forms.Timer TimerObj;
        public System.Windows.Forms.RichTextBox ExtreactionReadyText;
        public System.Windows.Forms.Label ClientDataCount;
        public System.Windows.Forms.Label ServerDataCount;
        public System.Windows.Forms.Label ClientThread04TableLabel;
        public System.Windows.Forms.Label ClientThread03TableLabel;
        public System.Windows.Forms.RichTextBox ClientThread04Text;
        public System.Windows.Forms.ProgressBar ClientThread04progressBar;
        private System.Windows.Forms.Label ClientThread04Label;
        public System.Windows.Forms.RichTextBox ClientThread03Text;
        private System.Windows.Forms.Label ClientThread03Label;
        public System.Windows.Forms.ProgressBar ClientThread03progressBar;
    }
}

