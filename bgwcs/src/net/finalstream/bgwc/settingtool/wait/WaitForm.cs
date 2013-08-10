using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    /// <summary>
    /// スプラッシュスクリーンクラス
    /// </summary>
    public partial class WaitForm : Form
    {

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public WaitForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// スプラッシュスクリーン表示する
        /// </summary>
        public void StartWait(){
            if(MainProgram.frmMain.frmOpt.AlwaysOnTop == true){
                MainProgram.Foremost(false);
            }

            this.StartPosition = FormStartPosition.CenterScreen;
            
            this.Show();
            Application.DoEvents();
            
        }

        /// <summary>
        /// スプラッシュスクリーンを消す
        /// </summary>
        public void EndWait()
        {
            
            this.Close();
            
            if (MainProgram.frmMain.frmOpt.AlwaysOnTop == true)
            {
                MainProgram.Foremost(true);
            }
        }

        
    }
}