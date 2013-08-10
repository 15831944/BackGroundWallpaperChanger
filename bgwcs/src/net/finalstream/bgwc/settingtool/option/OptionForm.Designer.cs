namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    /// <summary>
    /// オプションフォームクラス
    /// </summary>
    partial class OptionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pnlMode = new System.Windows.Forms.Panel();
            this.optNone = new System.Windows.Forms.RadioButton();
            this.optNormal = new System.Windows.Forms.RadioButton();
            this.optRandom = new System.Windows.Forms.RadioButton();
            this.picPreviewColor = new System.Windows.Forms.PictureBox();
            this.lblWallpaperColor = new System.Windows.Forms.Label();
            this.cboWallpaperStyle = new System.Windows.Forms.ComboBox();
            this.lblWallpaperPosition = new System.Windows.Forms.Label();
            this.chkWallNothing = new System.Windows.Forms.CheckBox();
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.chkForemost = new System.Windows.Forms.CheckBox();
            this.picWallpaperList = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.pnlMode.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreviewColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWallpaperList)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pnlMode);
            this.groupBox1.Controls.Add(this.picPreviewColor);
            this.groupBox1.Controls.Add(this.lblWallpaperColor);
            this.groupBox1.Controls.Add(this.cboWallpaperStyle);
            this.groupBox1.Controls.Add(this.lblWallpaperPosition);
            this.groupBox1.Controls.Add(this.chkWallNothing);
            this.groupBox1.Controls.Add(this.chkStartup);
            this.groupBox1.Controls.Add(this.chkForemost);
            this.groupBox1.Controls.Add(this.picWallpaperList);
            this.groupBox1.Location = new System.Drawing.Point(8, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 241);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "    Option";
            // 
            // pnlMode
            // 
            this.pnlMode.Controls.Add(this.optNone);
            this.pnlMode.Controls.Add(this.optNormal);
            this.pnlMode.Controls.Add(this.optRandom);
            this.pnlMode.Location = new System.Drawing.Point(6, 22);
            this.pnlMode.Name = "pnlMode";
            this.pnlMode.Size = new System.Drawing.Size(146, 68);
            this.pnlMode.TabIndex = 24;
            // 
            // optNone
            // 
            this.optNone.AutoSize = true;
            this.optNone.Location = new System.Drawing.Point(3, 47);
            this.optNone.Name = "optNone";
            this.optNone.Size = new System.Drawing.Size(42, 16);
            this.optNone.TabIndex = 18;
            this.optNone.TabStop = true;
            this.optNone.Text = "なし";
            this.optNone.UseVisualStyleBackColor = true;
            this.optNone.CheckedChanged += new System.EventHandler(this.optNone_CheckedChanged);
            // 
            // optNormal
            // 
            this.optNormal.AutoSize = true;
            this.optNormal.Location = new System.Drawing.Point(3, 25);
            this.optNormal.Name = "optNormal";
            this.optNormal.Size = new System.Drawing.Size(60, 16);
            this.optNormal.TabIndex = 17;
            this.optNormal.TabStop = true;
            this.optNormal.Text = "ノーマル";
            this.optNormal.UseVisualStyleBackColor = true;
            this.optNormal.CheckedChanged += new System.EventHandler(this.optNormal_CheckedChanged);
            // 
            // optRandom
            // 
            this.optRandom.AutoSize = true;
            this.optRandom.Location = new System.Drawing.Point(3, 3);
            this.optRandom.Name = "optRandom";
            this.optRandom.Size = new System.Drawing.Size(77, 16);
            this.optRandom.TabIndex = 16;
            this.optRandom.TabStop = true;
            this.optRandom.Text = "ランダマイズ";
            this.optRandom.UseVisualStyleBackColor = true;
            this.optRandom.CheckedChanged += new System.EventHandler(this.optRandom_CheckedChanged);
            // 
            // picPreviewColor
            // 
            this.picPreviewColor.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picPreviewColor.Location = new System.Drawing.Point(7, 215);
            this.picPreviewColor.Name = "picPreviewColor";
            this.picPreviewColor.Size = new System.Drawing.Size(114, 17);
            this.picPreviewColor.TabIndex = 23;
            this.picPreviewColor.TabStop = false;
            this.picPreviewColor.Click += new System.EventHandler(this.picPreviewColor_Click);
            // 
            // lblWallpaperColor
            // 
            this.lblWallpaperColor.AutoSize = true;
            this.lblWallpaperColor.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblWallpaperColor.Location = new System.Drawing.Point(6, 200);
            this.lblWallpaperColor.Name = "lblWallpaperColor";
            this.lblWallpaperColor.Size = new System.Drawing.Size(85, 12);
            this.lblWallpaperColor.TabIndex = 22;
            this.lblWallpaperColor.Text = "Wallpaper Color";
            // 
            // cboWallpaperStyle
            // 
            this.cboWallpaperStyle.FormattingEnabled = true;
            this.cboWallpaperStyle.Items.AddRange(new object[] {
            "原寸で表示",
            "並べて表示",
            "フィット表示"});
            this.cboWallpaperStyle.Location = new System.Drawing.Point(7, 177);
            this.cboWallpaperStyle.Name = "cboWallpaperStyle";
            this.cboWallpaperStyle.Size = new System.Drawing.Size(115, 20);
            this.cboWallpaperStyle.TabIndex = 21;
            this.cboWallpaperStyle.SelectionChangeCommitted += new System.EventHandler(this.cboWallpaperStyle_SelectionChangeCommitted);
            // 
            // lblWallpaperPosition
            // 
            this.lblWallpaperPosition.AutoSize = true;
            this.lblWallpaperPosition.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.lblWallpaperPosition.Location = new System.Drawing.Point(7, 161);
            this.lblWallpaperPosition.Name = "lblWallpaperPosition";
            this.lblWallpaperPosition.Size = new System.Drawing.Size(99, 12);
            this.lblWallpaperPosition.TabIndex = 20;
            this.lblWallpaperPosition.Text = "Wallpaper Position";
            // 
            // chkWallNothing
            // 
            this.chkWallNothing.AutoSize = true;
            this.chkWallNothing.Location = new System.Drawing.Point(6, 140);
            this.chkWallNothing.Name = "chkWallNothing";
            this.chkWallNothing.Size = new System.Drawing.Size(81, 16);
            this.chkWallNothing.TabIndex = 19;
            this.chkWallNothing.Text = "壁紙を無効";
            this.chkWallNothing.UseVisualStyleBackColor = true;
            this.chkWallNothing.CheckedChanged += new System.EventHandler(this.chkWallNothing_CheckedChanged);
            // 
            // chkStartup
            // 
            this.chkStartup.AutoSize = true;
            this.chkStartup.Location = new System.Drawing.Point(6, 118);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(148, 16);
            this.chkStartup.TabIndex = 18;
            this.chkStartup.Text = "Windows起動時にチェンジ";
            this.chkStartup.UseVisualStyleBackColor = true;
            this.chkStartup.CheckedChanged += new System.EventHandler(this.chkStartup_CheckedChanged);
            // 
            // chkForemost
            // 
            this.chkForemost.AutoSize = true;
            this.chkForemost.Location = new System.Drawing.Point(6, 96);
            this.chkForemost.Name = "chkForemost";
            this.chkForemost.Size = new System.Drawing.Size(102, 16);
            this.chkForemost.TabIndex = 17;
            this.chkForemost.Text = "常に手前に表示";
            this.chkForemost.UseVisualStyleBackColor = true;
            this.chkForemost.CheckedChanged += new System.EventHandler(this.chkForemost_CheckedChanged);
            // 
            // picWallpaperList
            // 
            this.picWallpaperList.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.dev;
            this.picWallpaperList.Location = new System.Drawing.Point(6, 0);
            this.picWallpaperList.Name = "picWallpaperList";
            this.picWallpaperList.Size = new System.Drawing.Size(16, 16);
            this.picWallpaperList.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWallpaperList.TabIndex = 12;
            this.picWallpaperList.TabStop = false;
            // 
            // OptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(173, 257);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OptionForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Load += new System.EventHandler(this.OptionForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptionForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.pnlMode.ResumeLayout(false);
            this.pnlMode.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPreviewColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWallpaperList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox picWallpaperList;
        private System.Windows.Forms.CheckBox chkForemost;
        private System.Windows.Forms.Label lblWallpaperPosition;
        private System.Windows.Forms.CheckBox chkWallNothing;
        private System.Windows.Forms.CheckBox chkStartup;
        private System.Windows.Forms.ComboBox cboWallpaperStyle;
        private System.Windows.Forms.Label lblWallpaperColor;
        private System.Windows.Forms.PictureBox picPreviewColor;
        private System.Windows.Forms.Panel pnlMode;
        private System.Windows.Forms.RadioButton optNone;
        private System.Windows.Forms.RadioButton optNormal;
        private System.Windows.Forms.RadioButton optRandom;
    }
}