namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    partial class FadeForm
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
            this.picWait.BackColor = System.Drawing.Color.Transparent;
            this.picWait.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.picWait.ErrorImage = null;
            this.picWait.Location = new System.Drawing.Point(0, 0);
            this.picWait.Name = "picWait";
            this.picWait.Size = new System.Drawing.Size(314, 200);
            this.picWait.TabIndex = 0;
            this.picWait.TabStop = false;
            // 
            // FadeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 200);
            this.Controls.Add(this.picWait);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FadeForm";
            this.ShowInTaskbar = false;
            this.Text = "WaitForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FadeForm_Paint);
            ((System.ComponentModel.ISupportInitialize)(this.picWait)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picWait;



    }
}