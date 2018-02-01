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
            this.Field_Comment = new System.Windows.Forms.Label();
            this.Field_Comment_ClientOnly = new System.Windows.Forms.Label();
            this.Row_Comment = new System.Windows.Forms.Label();
            this.Default_RowComment_LineCount = new System.Windows.Forms.Label();
            this.Client_TotalCount_Table = new System.Windows.Forms.Label();
            this.Client_TotalCount_Data = new System.Windows.Forms.Label();
            this.Client_Count_ConvertData = new System.Windows.Forms.Label();
            this.Client_Total_ProcessPercent = new System.Windows.Forms.Label();
            this.Client_Count_CommentData = new System.Windows.Forms.Label();
            this.Client_Leftover_Data = new System.Windows.Forms.Label();
            this.Client_FileSize = new System.Windows.Forms.Label();
            this.ClientTypeList = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TargetPath = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ServerTypeList = new System.Windows.Forms.Label();
            this.Client_ProgressBar = new System.Windows.Forms.ProgressBar();
            this.Server_FileSize = new System.Windows.Forms.Label();
            this.Server_Leftover_Data = new System.Windows.Forms.Label();
            this.Server_Count_CommentData = new System.Windows.Forms.Label();
            this.Server_Total_ProcessPercent = new System.Windows.Forms.Label();
            this.Server_Count_ConvertData = new System.Windows.Forms.Label();
            this.Server_TotalCount_Data = new System.Windows.Forms.Label();
            this.Server_TotalCount_Table = new System.Windows.Forms.Label();
            this.Server_ProgressBar = new System.Windows.Forms.ProgressBar();
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
            // Field_Comment
            // 
            this.Field_Comment.AutoSize = true;
            this.Field_Comment.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Field_Comment.Location = new System.Drawing.Point(593, 43);
            this.Field_Comment.Name = "Field_Comment";
            this.Field_Comment.Size = new System.Drawing.Size(57, 12);
            this.Field_Comment.TabIndex = 6;
            this.Field_Comment.Text = "필드 주석";
            // 
            // Field_Comment_ClientOnly
            // 
            this.Field_Comment_ClientOnly.AutoSize = true;
            this.Field_Comment_ClientOnly.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Field_Comment_ClientOnly.Location = new System.Drawing.Point(593, 61);
            this.Field_Comment_ClientOnly.Name = "Field_Comment_ClientOnly";
            this.Field_Comment_ClientOnly.Size = new System.Drawing.Size(107, 12);
            this.Field_Comment_ClientOnly.TabIndex = 7;
            this.Field_Comment_ClientOnly.Text = "필드_클라이언트만";
            // 
            // Row_Comment
            // 
            this.Row_Comment.AutoSize = true;
            this.Row_Comment.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Row_Comment.Location = new System.Drawing.Point(593, 80);
            this.Row_Comment.Name = "Row_Comment";
            this.Row_Comment.Size = new System.Drawing.Size(45, 12);
            this.Row_Comment.TabIndex = 8;
            this.Row_Comment.Text = "행 주석";
            // 
            // Default_RowComment_LineCount
            // 
            this.Default_RowComment_LineCount.AutoSize = true;
            this.Default_RowComment_LineCount.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Default_RowComment_LineCount.Location = new System.Drawing.Point(593, 26);
            this.Default_RowComment_LineCount.Name = "Default_RowComment_LineCount";
            this.Default_RowComment_LineCount.Size = new System.Drawing.Size(73, 12);
            this.Default_RowComment_LineCount.TabIndex = 9;
            this.Default_RowComment_LineCount.Text = "기본 주석 행";
            // 
            // Client_TotalCount_Table
            // 
            this.Client_TotalCount_Table.AutoSize = true;
            this.Client_TotalCount_Table.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Client_TotalCount_Table.Location = new System.Drawing.Point(333, 26);
            this.Client_TotalCount_Table.Name = "Client_TotalCount_Table";
            this.Client_TotalCount_Table.Size = new System.Drawing.Size(77, 12);
            this.Client_TotalCount_Table.TabIndex = 10;
            this.Client_TotalCount_Table.Text = "전체 테이블 :";
            // 
            // Client_TotalCount_Data
            // 
            this.Client_TotalCount_Data.AutoSize = true;
            this.Client_TotalCount_Data.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Client_TotalCount_Data.Location = new System.Drawing.Point(333, 43);
            this.Client_TotalCount_Data.Name = "Client_TotalCount_Data";
            this.Client_TotalCount_Data.Size = new System.Drawing.Size(77, 12);
            this.Client_TotalCount_Data.TabIndex = 11;
            this.Client_TotalCount_Data.Text = "전체 데이터 :";
            // 
            // Client_Count_ConvertData
            // 
            this.Client_Count_ConvertData.AutoSize = true;
            this.Client_Count_ConvertData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Client_Count_ConvertData.Location = new System.Drawing.Point(333, 99);
            this.Client_Count_ConvertData.Name = "Client_Count_ConvertData";
            this.Client_Count_ConvertData.Size = new System.Drawing.Size(89, 12);
            this.Client_Count_ConvertData.TabIndex = 12;
            this.Client_Count_ConvertData.Text = "변경된 데이터 :";
            // 
            // Client_Total_ProcessPercent
            // 
            this.Client_Total_ProcessPercent.AutoSize = true;
            this.Client_Total_ProcessPercent.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Client_Total_ProcessPercent.Location = new System.Drawing.Point(333, 118);
            this.Client_Total_ProcessPercent.Name = "Client_Total_ProcessPercent";
            this.Client_Total_ProcessPercent.Size = new System.Drawing.Size(49, 12);
            this.Client_Total_ProcessPercent.TabIndex = 13;
            this.Client_Total_ProcessPercent.Text = "진행도 :";
            // 
            // Client_Count_CommentData
            // 
            this.Client_Count_CommentData.AutoSize = true;
            this.Client_Count_CommentData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Client_Count_CommentData.Location = new System.Drawing.Point(333, 80);
            this.Client_Count_CommentData.Name = "Client_Count_CommentData";
            this.Client_Count_CommentData.Size = new System.Drawing.Size(101, 12);
            this.Client_Count_CommentData.TabIndex = 14;
            this.Client_Count_CommentData.Text = "주석처리 데이터 :";
            // 
            // Client_Leftover_Data
            // 
            this.Client_Leftover_Data.AutoSize = true;
            this.Client_Leftover_Data.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Client_Leftover_Data.Location = new System.Drawing.Point(333, 61);
            this.Client_Leftover_Data.Name = "Client_Leftover_Data";
            this.Client_Leftover_Data.Size = new System.Drawing.Size(77, 12);
            this.Client_Leftover_Data.TabIndex = 15;
            this.Client_Leftover_Data.Text = "남은 데이터 :";
            // 
            // Client_FileSize
            // 
            this.Client_FileSize.AutoSize = true;
            this.Client_FileSize.Location = new System.Drawing.Point(332, 138);
            this.Client_FileSize.Name = "Client_FileSize";
            this.Client_FileSize.Size = new System.Drawing.Size(77, 12);
            this.Client_FileSize.TabIndex = 16;
            this.Client_FileSize.Text = "누적 바이트 :";
            this.Client_FileSize.Click += new System.EventHandler(this.test_Click);
            // 
            // ClientTypeList
            // 
            this.ClientTypeList.AutoSize = true;
            this.ClientTypeList.Location = new System.Drawing.Point(854, 46);
            this.ClientTypeList.Name = "ClientTypeList";
            this.ClientTypeList.Size = new System.Drawing.Size(41, 12);
            this.ClientTypeList.TabIndex = 17;
            this.ClientTypeList.Text = "리스트";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(854, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(145, 12);
            this.label1.TabIndex = 18;
            this.label1.Text = "클라이언트 자료형 리스트";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(854, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "서버 자료형 리스트";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // ServerTypeList
            // 
            this.ServerTypeList.AutoSize = true;
            this.ServerTypeList.Location = new System.Drawing.Point(854, 247);
            this.ServerTypeList.Name = "ServerTypeList";
            this.ServerTypeList.Size = new System.Drawing.Size(41, 12);
            this.ServerTypeList.TabIndex = 21;
            this.ServerTypeList.Text = "리스트";
            this.ServerTypeList.Click += new System.EventHandler(this.ServerTypeList_Click);
            // 
            // Client_ProgressBar
            // 
            this.Client_ProgressBar.Location = new System.Drawing.Point(335, 166);
            this.Client_ProgressBar.Name = "Client_ProgressBar";
            this.Client_ProgressBar.Size = new System.Drawing.Size(503, 29);
            this.Client_ProgressBar.TabIndex = 22;
            // 
            // Server_FileSize
            // 
            this.Server_FileSize.AutoSize = true;
            this.Server_FileSize.Location = new System.Drawing.Point(332, 339);
            this.Server_FileSize.Name = "Server_FileSize";
            this.Server_FileSize.Size = new System.Drawing.Size(77, 12);
            this.Server_FileSize.TabIndex = 29;
            this.Server_FileSize.Text = "누적 바이트 :";
            // 
            // Server_Leftover_Data
            // 
            this.Server_Leftover_Data.AutoSize = true;
            this.Server_Leftover_Data.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Server_Leftover_Data.Location = new System.Drawing.Point(333, 262);
            this.Server_Leftover_Data.Name = "Server_Leftover_Data";
            this.Server_Leftover_Data.Size = new System.Drawing.Size(77, 12);
            this.Server_Leftover_Data.TabIndex = 28;
            this.Server_Leftover_Data.Text = "남은 데이터 :";
            // 
            // Server_Count_CommentData
            // 
            this.Server_Count_CommentData.AutoSize = true;
            this.Server_Count_CommentData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Server_Count_CommentData.Location = new System.Drawing.Point(333, 281);
            this.Server_Count_CommentData.Name = "Server_Count_CommentData";
            this.Server_Count_CommentData.Size = new System.Drawing.Size(101, 12);
            this.Server_Count_CommentData.TabIndex = 27;
            this.Server_Count_CommentData.Text = "주석처리 데이터 :";
            // 
            // Server_Total_ProcessPercent
            // 
            this.Server_Total_ProcessPercent.AutoSize = true;
            this.Server_Total_ProcessPercent.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Server_Total_ProcessPercent.Location = new System.Drawing.Point(333, 319);
            this.Server_Total_ProcessPercent.Name = "Server_Total_ProcessPercent";
            this.Server_Total_ProcessPercent.Size = new System.Drawing.Size(49, 12);
            this.Server_Total_ProcessPercent.TabIndex = 26;
            this.Server_Total_ProcessPercent.Text = "진행도 :";
            // 
            // Server_Count_ConvertData
            // 
            this.Server_Count_ConvertData.AutoSize = true;
            this.Server_Count_ConvertData.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Server_Count_ConvertData.Location = new System.Drawing.Point(333, 300);
            this.Server_Count_ConvertData.Name = "Server_Count_ConvertData";
            this.Server_Count_ConvertData.Size = new System.Drawing.Size(89, 12);
            this.Server_Count_ConvertData.TabIndex = 25;
            this.Server_Count_ConvertData.Text = "변경된 데이터 :";
            // 
            // Server_TotalCount_Data
            // 
            this.Server_TotalCount_Data.AutoSize = true;
            this.Server_TotalCount_Data.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Server_TotalCount_Data.Location = new System.Drawing.Point(333, 244);
            this.Server_TotalCount_Data.Name = "Server_TotalCount_Data";
            this.Server_TotalCount_Data.Size = new System.Drawing.Size(77, 12);
            this.Server_TotalCount_Data.TabIndex = 24;
            this.Server_TotalCount_Data.Text = "전체 데이터 :";
            // 
            // Server_TotalCount_Table
            // 
            this.Server_TotalCount_Table.AutoSize = true;
            this.Server_TotalCount_Table.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Server_TotalCount_Table.Location = new System.Drawing.Point(333, 227);
            this.Server_TotalCount_Table.Name = "Server_TotalCount_Table";
            this.Server_TotalCount_Table.Size = new System.Drawing.Size(77, 12);
            this.Server_TotalCount_Table.TabIndex = 23;
            this.Server_TotalCount_Table.Text = "전체 테이블 :";
            // 
            // Server_ProgressBar
            // 
            this.Server_ProgressBar.Location = new System.Drawing.Point(334, 373);
            this.Server_ProgressBar.Name = "Server_ProgressBar";
            this.Server_ProgressBar.Size = new System.Drawing.Size(503, 29);
            this.Server_ProgressBar.TabIndex = 30;
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
            this.Controls.Add(this.Server_ProgressBar);
            this.Controls.Add(this.Server_FileSize);
            this.Controls.Add(this.Server_Leftover_Data);
            this.Controls.Add(this.Server_Count_CommentData);
            this.Controls.Add(this.Server_Total_ProcessPercent);
            this.Controls.Add(this.Server_Count_ConvertData);
            this.Controls.Add(this.Server_TotalCount_Data);
            this.Controls.Add(this.Server_TotalCount_Table);
            this.Controls.Add(this.Client_ProgressBar);
            this.Controls.Add(this.ServerTypeList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TargetPath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ClientTypeList);
            this.Controls.Add(this.Client_FileSize);
            this.Controls.Add(this.Client_Leftover_Data);
            this.Controls.Add(this.Client_Count_CommentData);
            this.Controls.Add(this.Client_Total_ProcessPercent);
            this.Controls.Add(this.Client_Count_ConvertData);
            this.Controls.Add(this.Client_TotalCount_Data);
            this.Controls.Add(this.Client_TotalCount_Table);
            this.Controls.Add(this.Default_RowComment_LineCount);
            this.Controls.Add(this.Row_Comment);
            this.Controls.Add(this.Field_Comment_ClientOnly);
            this.Controls.Add(this.Field_Comment);
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
        private System.Windows.Forms.Label Field_Comment;
        private System.Windows.Forms.Label Field_Comment_ClientOnly;
        private System.Windows.Forms.Label Row_Comment;
        private System.Windows.Forms.Label Default_RowComment_LineCount;
        private System.Windows.Forms.Label Client_TotalCount_Table;
        private System.Windows.Forms.Label Client_TotalCount_Data;
        private System.Windows.Forms.Label Client_Count_ConvertData;
        private System.Windows.Forms.Label Client_Total_ProcessPercent;
        private System.Windows.Forms.Label Client_Count_CommentData;
        private System.Windows.Forms.Label Client_Leftover_Data;
        private System.Windows.Forms.Label Client_FileSize;
        private System.Windows.Forms.Label ClientTypeList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label TargetPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label ServerTypeList;
        private System.Windows.Forms.ProgressBar Client_ProgressBar;
        private System.Windows.Forms.Label Server_FileSize;
        private System.Windows.Forms.Label Server_Leftover_Data;
        private System.Windows.Forms.Label Server_Count_CommentData;
        private System.Windows.Forms.Label Server_Total_ProcessPercent;
        private System.Windows.Forms.Label Server_Count_ConvertData;
        private System.Windows.Forms.Label Server_TotalCount_Data;
        private System.Windows.Forms.Label Server_TotalCount_Table;
        private System.Windows.Forms.ProgressBar Server_ProgressBar;
        private System.Windows.Forms.Label Server_Target_Data;
        private System.Windows.Forms.Label Server_ProgressText;
    }
}

