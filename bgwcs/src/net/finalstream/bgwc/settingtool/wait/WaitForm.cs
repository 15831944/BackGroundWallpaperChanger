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
    /// �X�v���b�V���X�N���[���N���X
    /// </summary>
    public partial class WaitForm : Form
    {

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public WaitForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �X�v���b�V���X�N���[���\������
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
        /// �X�v���b�V���X�N���[��������
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