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
            this.SuspendLayout();
            // 
            // Client_ProgressText
            // 
            this.Client_ProgressText.AutoSize = true;
            this.Client_ProgressText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Client_ProgressText.Location = new System.Drawing.Point(69, 26);
            this.Client_ProgressText.Name = "Client_ProgressText";
            this.Client_ProgressText.Size = new System.Drawing.Size(81, 12);
            this.Client_ProgressText.TabIndex = 2;
            this.Client_ProgressText.Text = "진행 테이블 : ";
            // 
            // Excel_Directory
            // 
            this.Excel_Directory.AutoSize = true;
            this.Excel_Directory.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Excel_Directory.Location = new System.Drawing.Point(69, 505);
            this.Excel_Directory.Name = "Excel_Directory";
            this.Excel_Directory.Size = new System.Drawing.Size(53, 12);
            this.Excel_Directory.TabIndex = 3;
            this.Excel_Directory.Text = "엑셀경로";
            this.Excel_Directory.Click += new System.EventHandler(this.Excel_Directory_Click);
            // 
            // Client_Target_Data
            // 
            this.Client_Target_Data.AutoSize = true;
            this.Client_Target_Data.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Client_Target_Data.Location = new System.Drawing.Point(69, 43);
            this.Client_Target_Data.Name = "Client_Target_Data";
            this.Client_Target_Data.Size = new System.Drawing.Size(53, 12);
            this.Client_Target_Data.TabIndex = 4;
            this.Client_Target_Data.Text = "데이터 : ";
            // 
            // StartButton
            // 
            this.StartButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.StartButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.StartButton.Location = new System.Drawing.Point(71, 103);
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
            this.TargetPath.Location = new System.Drawing.Point(69, 523);
            this.TargetPath.Name = "TargetPath";
            this.TargetPath.Size = new System.Drawing.Size(53, 12);
            this.TargetPath.TabIndex = 19;
            this.TargetPath.Text = "이동경로";
            this.TargetPath.Click += new System.EventHandler(this.TargetPath_Click);
            // 
            // Server_Target_Data
            // 
            this.Server_Target_Data.AutoSize = true;
            this.Server_Target_Data.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Server_Target_Data.Location = new System.Drawing.Point(69, 244);
            this.Server_Target_Data.Name = "Server_Target_Data";
            this.Server_Target_Data.Size = new System.Drawing.Size(53, 12);
            this.Server_Target_Data.TabIndex = 32;
            this.Server_Target_Data.Text = "데이터 : ";
            // 
            // Server_ProgressText
            // 
            this.Server_ProgressText.AutoSize = true;
            this.Server_ProgressText.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Server_ProgressText.Location = new System.Drawing.Point(69, 227);
            this.Server_ProgressText.Name = "Server_ProgressText";
            this.Server_ProgressText.Size = new System.Drawing.Size(81, 12);
            this.Server_ProgressText.TabIndex = 31;
            this.Server_ProgressText.Text = "진행 테이블 : ";
            // 
            // ConverterWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1174, 545);
            this.Controls.Add(this.Server_Target_Data);
            this.Controls.Add(this.Server_ProgressText);
            this.Controls.Add(this.TargetPath);
            this.Controls.Add(this.StartButton);
            this.Controls.Add(this.Client_Target_Data);
            this.Controls.Add(this.Excel_Directory);
            this.Controls.Add(this.Client_ProgressText);
            this.Name = "ConverterWindow";
            this.Text = "Visual Design";
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
    }
}

