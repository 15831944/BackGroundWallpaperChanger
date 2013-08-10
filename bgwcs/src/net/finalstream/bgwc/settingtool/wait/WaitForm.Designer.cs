namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    partial class WaitForm
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
            this.picWait = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).BeginInit();
            this.SuspendLayout();
            // 
            // picWait
            // 
            this.picWait.Image = global::FINALSTREAM.BackGroundWallpaperChanger.SettingTool.Properties.Resources.bgwcwaitlogo;
            this.picWait.Location = new System.Drawing.Point(0, 0);
            this.picWait.Name = "picWait";
            this.picWait.Size = new System.Drawing.Size(314, 200);
            this.picWait.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picWait.TabIndex = 0;
            this.picWait.TabStop = false;
            // 
            // WaitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 200);
            this.Controls.Add(this.picWait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitForm";
            this.ShowInTaskbar = false;
            this.Text = "WaitForm";
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picWait;
    }
}