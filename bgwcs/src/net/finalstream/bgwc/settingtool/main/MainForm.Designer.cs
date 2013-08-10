namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.lblTitleBack = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblSite = new System.Windows.Forms.Label();
            this.grpDispInfo = new System.Windows.Forms.GroupBox();
            this.lblWallpaperSize = new System.Windows.Forms.Label();
            this.lblWallpaperS = new System.Windows.Forms.Label();
            this.lblScreenSize = new System.Windows.Forms.Label();
            this.lblDisplayResolution = new System.Windows.Forms.Label();
            this.txtNowWallpaper = new System.Windows.Forms.TextBox();
            this.lblNowWallpaper = new System.Windows.Forms.Label();
            this.picDisplay = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtWallpaperFolder = new System.Windows.Forms.TextBox();
            this.lblWallpaperList = new System.Windows.Forms.Label();
            this.grpPreview = new System.Windows.Forms.GroupBox();
            this.lblPreviewSize = new System.Windows.Forms.Label();
            this.txtPreviewFile = new System.Windows.Forms.TextBox();
            this.picPreview = new System.Windows.Forms.PictureBox();
            this.picView = new System.Windows.Forms.PictureBox();
            this.btnDelSkip = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.picTrashBox = new System.Windows.Forms.PictureBox();
            this.picWallpaperList = new System.Windows.Forms.PictureBox();
            this.btnSetting = new System.Windows.Forms.Button();
            this.btnFolder = new System.Windows.Forms.Button();
            this.picFolder = new System.Windows.Forms.PictureBox();
            this.griList = new SourceGrid.Grid();
            this.grpDispInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).BeginInit();
            this.grpPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTrashBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWallpaperList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFolder)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitleBack
            // 
            this.lblTitleBack.BackColor = System.Drawing.Color.Black;
            this.lblTitleBack.Location = new System.Drawing.Point(-2, -1);
            this.lblTitleBack.Name = "lblTitleBack";
            this.lblTitleBack.Size = new System.Drawing.Size(679, 40);
            this.lblTitleBack.TabIndex = 1;
            this.lblTitleBack.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblTitleBack_MouseMove);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Black;
            this.lblTitle.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTitle.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblTitle.Location = new System.Drawing.Point(6, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(397, 12);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "BackGround Wallpaper Changer Version 8.0 Technology Preview ";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.BackColor = System.Drawing.Color.Black;
            this.lblCopyright.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.lblCopyright.Location = new System.Drawing.Point(421, 9);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(214, 12);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "Copyright (C) 2003-2012 FINALSTREAM";
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.BackColor = System.Drawing.Color.Black;
            this.lblSite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSite.ForeColor = System.Drawing.Color.Orange;
            this.lblSite.Location = new System.Drawing.Point(458, 22);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(146, 12);
            this.lblSite.TabIndex = 4;
            this.lblSite.Text = "http://www.finalstream.net/";
            this.lblSite.Click += new System.EventHandler(this.lblSite_Click);
            this.lblSite.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lblSite_MouseMove);
            // 
            // grpDispInfo
            // 
            this.grpDispInfo.Controls.Add(this.lblWallpaperSize);
            this.grpDispInfo.Controls.Add(this.lblWallpaperS);
            this.grpDispInfo.Controls.Add(this.lblScreenSize);
            this.grpDispInfo.Controls.Add(this.lblDisplayResolution);
            this.grpDispInfo.Controls.Add(this.txtNowWallpaper);
            this.grpDispInfo.Controls.Add(this.lblNowWallpaper);
            this.grpDispInfo.Controls.Add(this.picDisplay);
            this.grpDispInfo.Location = new System.Drawing.Point(4, 42);
            this.grpDispInfo.Name = "grpDispInfo";
            this.grpDispInfo.Size = new System.Drawing.Size(662, 58);
            this.grpDispInfo.TabIndex = 5;
            this.grpDispInfo.TabStop = false;
            // 
            // lblWallpaperSize
            // 
            this.lblWallpaperSize.Location = new System.Drawing.Point(437, 37);
            this.lblWallpaperSize.Name = "lblWallpaperSize";
            this.lblWallpaperSize.Size = new System.Drawing.Size(163, 12);
            this.lblWallpaperSize.TabIndex = 5;
            this.lblWallpaperSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblWallpaperS
            // 
            this.lblWallpaperS.AutoSize = true;
            this.lblWallpaperS.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblWallpaperS.Location = new System.Drawing.Point(326, 37);
            this.lblWallpaperS.Name = "lblWallpaperS";
            this.lblWallpaperS.Size = new System.Drawing.Size(79, 12);
            this.lblWallpaperS.TabIndex = 4;
            this.lblWallpaperS.Text = "Wallpaper Size";
            // 
            // lblScreenSize
            // 
            this.lblScreenSize.Location = new System.Drawing.Point(148, 37);
            this.lblScreenSize.Name = "lblScreenSize";
            this.lblScreenSize.Size = new System.Drawing.Size(163, 12);
            this.lblScreenSize.TabIndex = 3;
            this.lblScreenSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblDisplayResolution
            // 
            this.lblDisplayResolution.AutoSize = true;
            this.lblDisplayResolution.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblDisplayResolution.Location = new System.Drawing.Point(29, 37);
            this.lblDisplayResolution.Name = "lblDisplayResolution";
            this.lblDisplayResolution.Size = new System.Drawing.Size(101, 12);
            this.lblDisplayResolution.TabIndex = 2;
            this.lblDisplayResolution.Text = "Display Resolution";
            // 
            // txtNowWallpaper
            // 
            this.txtNowWallpaper.BackColor = System.Drawing.SystemColors.Control;
            this.txtNowWallpaper.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNowWallpaper.Location = new System.Drawing.Point(115, 17);
            this.txtNowWallpaper.Name = "txtNowWallpaper";
            this.txtNowWallpaper.ReadOnly = true;
            this.txtNowWallpaper.Size = new System.Drawing.Size(534, 12);
            this.txtNowWallpaper.TabIndex = 1;
            this.txtNowWallpaper.Enter += new System.EventHandler(this.txtNowWallpaper_Enter);
            // 
            // lblNowWallpaper
            // 
            this.lblNowWallpaper.AutoSize = true;
            this.lblNowWallpaper.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblNowWallpaper.Location = new System.Drawing.Point(29, 17);
            this.lblNowWallpaper.Name = "lblNowWallpaper";
            this.lblNowWallpaper.Size = new System.Drawing.Size(80, 12);
            this.lblNowWallpaper.TabIndex = 0;
            this.lblNowWallpaper.Text = "Now Wallpaper";
            // 
            // picDisplay
            // 
            this.picDisplay.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.display;
            this.picDisplay.Location = new System.Drawing.Point(4, 13);
            this.picDisplay.Name = "picDisplay";
            this.picDisplay.Size = new System.Drawing.Size(16, 16);
            this.picDisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picDisplay.TabIndex = 0;
            this.picDisplay.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label1.Location = new System.Drawing.Point(30, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "Wallpaper Folder";
            // 
            // txtWallpaperFolder
            // 
            this.txtWallpaperFolder.Location = new System.Drawing.Point(8, 124);
            this.txtWallpaperFolder.Multiline = true;
            this.txtWallpaperFolder.Name = "txtWallpaperFolder";
            this.txtWallpaperFolder.Size = new System.Drawing.Size(596, 21);
            this.txtWallpaperFolder.TabIndex = 7;
            // 
            // lblWallpaperList
            // 
            this.lblWallpaperList.AutoSize = true;
            this.lblWallpaperList.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblWallpaperList.Location = new System.Drawing.Point(30, 155);
            this.lblWallpaperList.Name = "lblWallpaperList";
            this.lblWallpaperList.Size = new System.Drawing.Size(77, 12);
            this.lblWallpaperList.TabIndex = 10;
            this.lblWallpaperList.Text = "Wallpaper List";
            // 
            // grpPreview
            // 
            this.grpPreview.Controls.Add(this.lblPreviewSize);
            this.grpPreview.Controls.Add(this.txtPreviewFile);
            this.grpPreview.Controls.Add(this.picPreview);
            this.grpPreview.Controls.Add(this.picView);
            this.grpPreview.Location = new System.Drawing.Point(473, 151);
            this.grpPreview.Name = "grpPreview";
            this.grpPreview.Size = new System.Drawing.Size(192, 200);
            this.grpPreview.TabIndex = 11;
            this.grpPreview.TabStop = false;
            this.grpPreview.Text = "    Preview";
            // 
            // lblPreviewSize
            // 
            this.lblPreviewSize.Location = new System.Drawing.Point(9, 180);
            this.lblPreviewSize.Name = "lblPreviewSize";
            this.lblPreviewSize.Size = new System.Drawing.Size(171, 17);
            this.lblPreviewSize.TabIndex = 1;
            this.lblPreviewSize.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // txtPreviewFile
            // 
            this.txtPreviewFile.BackColor = System.Drawing.SystemColors.Control;
            this.txtPreviewFile.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtPreviewFile.Location = new System.Drawing.Point(7, 165);
            this.txtPreviewFile.Name = "txtPreviewFile";
            this.txtPreviewFile.ReadOnly = true;
            this.txtPreviewFile.Size = new System.Drawing.Size(176, 12);
            this.txtPreviewFile.TabIndex = 0;
            this.txtPreviewFile.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPreviewFile.Enter += new System.EventHandler(this.txtPreviewFile_Enter);
            // 
            // picPreview
            // 
            this.picPreview.BackColor = System.Drawing.SystemColors.Control;
            this.picPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picPreview.Location = new System.Drawing.Point(7, 23);
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(176, 132);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picPreview.TabIndex = 8;
            this.picPreview.TabStop = false;
            this.picPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseClick);
            // 
            // picView
            // 
            this.picView.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.view;
            this.picView.Location = new System.Drawing.Point(6, 0);
            this.picView.Name = "picView";
            this.picView.Size = new System.Drawing.Size(16, 16);
            this.picView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picView.TabIndex = 7;
            this.picView.TabStop = false;
            // 
            // btnDelSkip
            // 
            this.btnDelSkip.Location = new System.Drawing.Point(46, 320);
            this.btnDelSkip.Name = "btnDelSkip";
            this.btnDelSkip.Size = new System.Drawing.Size(100, 25);
            this.btnDelSkip.TabIndex = 12;
            this.btnDelSkip.Text = "デリートスキップ";
            this.btnDelSkip.UseVisualStyleBackColor = true;
            this.btnDelSkip.Click += new System.EventHandler(this.btnDelSkip_Click);
            // 
            // btnChange
            // 
            this.btnChange.Location = new System.Drawing.Point(154, 320);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(100, 25);
            this.btnChange.TabIndex = 13;
            this.btnChange.Text = "チェンジ";
            this.btnChange.UseVisualStyleBackColor = true;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(260, 320);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 25);
            this.btnApply.TabIndex = 14;
            this.btnApply.Text = "適用";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(366, 320);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 25);
            this.btnRefresh.TabIndex = 15;
            this.btnRefresh.Text = "更新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // picTrashBox
            // 
            this.picTrashBox.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.trashcan_empty;
            this.picTrashBox.Location = new System.Drawing.Point(4, 314);
            this.picTrashBox.Name = "picTrashBox";
            this.picTrashBox.Size = new System.Drawing.Size(32, 32);
            this.picTrashBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picTrashBox.TabIndex = 15;
            this.picTrashBox.TabStop = false;
            this.picTrashBox.DoubleClick += new System.EventHandler(this.picTrashBox_DoubleClick);
            // 
            // picWallpaperList
            // 
            this.picWallpaperList.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.list;
            this.picWallpaperList.Location = new System.Drawing.Point(8, 151);
            this.picWallpaperList.Name = "picWallpaperList";
            this.picWallpaperList.Size = new System.Drawing.Size(16, 16);
            this.picWallpaperList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWallpaperList.TabIndex = 11;
            this.picWallpaperList.TabStop = false;
            // 
            // btnSetting
            // 
            this.btnSetting.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.setting;
            this.btnSetting.Location = new System.Drawing.Point(640, 124);
            this.btnSetting.Name = "btnSetting";
            this.btnSetting.Size = new System.Drawing.Size(25, 21);
            this.btnSetting.TabIndex = 9;
            this.btnSetting.UseVisualStyleBackColor = true;
            this.btnSetting.Click += new System.EventHandler(this.btnSetting_Click);
            // 
            // btnFolder
            // 
            this.btnFolder.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.folder2;
            this.btnFolder.Location = new System.Drawing.Point(610, 124);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(25, 21);
            this.btnFolder.TabIndex = 8;
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // picFolder
            // 
            this.picFolder.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.folder;
            this.picFolder.Location = new System.Drawing.Point(8, 104);
            this.picFolder.Name = "picFolder";
            this.picFolder.Size = new System.Drawing.Size(16, 16);
            this.picFolder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picFolder.TabIndex = 6;
            this.picFolder.TabStop = false;
            this.picFolder.DoubleClick += new System.EventHandler(this.picFolder_DoubleClick);
            // 
            // griList
            // 
            this.griList.AllowDrop = true;
            this.griList.BackColor = System.Drawing.SystemColors.Window;
            this.griList.EnableSort = true;
            this.griList.Location = new System.Drawing.Point(8, 173);
            this.griList.Name = "griList";
            this.griList.OptimizeMode = SourceGrid.CellOptimizeMode.ForRows;
            this.griList.SelectionMode = SourceGrid.GridSelectionMode.Row;
            this.griList.Size = new System.Drawing.Size(458, 139);
            this.griList.TabIndex = 0;
            this.griList.TabStop = true;
            this.griList.ToolTipText = "";
            this.griList.DragDrop += new System.Windows.Forms.DragEventHandler(this.griList_DragDrop);
            this.griList.DragEnter += new System.Windows.Forms.DragEventHandler(this.griList_DragEnter);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(675, 354);
            this.Controls.Add(this.griList);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnChange);
            this.Controls.Add(this.btnDelSkip);
            this.Controls.Add(this.picTrashBox);
            this.Controls.Add(this.grpPreview);
            this.Controls.Add(this.lblWallpaperList);
            this.Controls.Add(this.picWallpaperList);
            this.Controls.Add(this.btnSetting);
            this.Controls.Add(this.btnFolder);
            this.Controls.Add(this.txtWallpaperFolder);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.picFolder);
            this.Controls.Add(this.grpDispInfo);
            this.Controls.Add(this.lblSite);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblTitleBack);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.RightToLeftLayout = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Activated += new System.EventHandler(this.MainForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.grpDispInfo.ResumeLayout(false);
            this.grpDispInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDisplay)).EndInit();
            this.grpPreview.ResumeLayout(false);
            this.grpPreview.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTrashBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWallpaperList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picFolder)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitleBack;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCopyright;
        private System.Windows.Forms.Label lblSite;
        private System.Windows.Forms.GroupBox grpDispInfo;
        private System.Windows.Forms.PictureBox picDisplay;
        private System.Windows.Forms.Label lblNowWallpaper;
        private System.Windows.Forms.TextBox txtNowWallpaper;
        private System.Windows.Forms.Label lblDisplayResolution;
        private System.Windows.Forms.Label lblScreenSize;
        private System.Windows.Forms.Label lblWallpaperSize;
        private System.Windows.Forms.Label lblWallpaperS;
        private System.Windows.Forms.PictureBox picFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtWallpaperFolder;
        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.Button btnSetting;
        private System.Windows.Forms.PictureBox picWallpaperList;
        private System.Windows.Forms.Label lblWallpaperList;
        private System.Windows.Forms.GroupBox grpPreview;
        private System.Windows.Forms.PictureBox picView;
        private System.Windows.Forms.TextBox txtPreviewFile;
        private System.Windows.Forms.PictureBox picPreview;
        private System.Windows.Forms.Label lblPreviewSize;
        private System.Windows.Forms.PictureBox picTrashBox;
        private System.Windows.Forms.Button btnDelSkip;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnRefresh;
        private SourceGrid.Grid griList;

    }
}

