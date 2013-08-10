using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    public partial class OptionForm : Form
    {
        /*
            �����o�ϐ�
        */
        private OptionFunction _optFunc;
        private bool _bolWallpaperNothing; // �ǎ��Ȃ��t���O
        private int _intChangeMode; // �ǎ��`�F���W���[�h

        /*
            �v���p�e�B
        */
        #region Property

        /// <summary>
        /// �I�v�V�����t�H�[���p�I�u�W�F�N�g
        /// </summary>
        public OptionFunction OptFunc
        {
            get { return this._optFunc; }
        }

        /// <summary>
        /// �ǎ��Ȃ�
        /// </summary>
        public bool WallpaperNothing
        {
            set { this._bolWallpaperNothing = value; }
            get{ return this._bolWallpaperNothing;}
        }
        
        /// <summary>
        /// ��Ɏ�O
        /// </summary>
        public bool AlwaysOnTop
        {
            get { return this.chkForemost.Checked; }
        }

        /// <summary>
        /// �X�^�[�g�A�b�v
        /// </summary>
        public bool StartUp
        {
            get { return this.chkStartup.Checked; }
        }

        /// <summary>
        /// �ǎ��`�F���W���[�h
        /// </summary>
        public int ChangeMode
        {
            set { 
                this._intChangeMode = value;
            }
            get { return this._intChangeMode; }
        }

        /// <summary>
        /// �ǎ��X�^�C��
        /// </summary>
        public int WallpaperStyle
        {
            set { this.cboWallpaperStyle.SelectedIndex = value; }
            get { return this.cboWallpaperStyle.SelectedIndex; }
        }

        /// <summary>
        /// �ǎ��w�i�F
        /// </summary>
        public Color PreviewColor
        {
            set { this.picPreviewColor.BackColor = value; }
        }
        

        #endregion


        /*
            �񋓌^
         */
        // �ǎ��\���X�^�C��
        enum enuWallpaperStyle
        {
            WS_TILE,        // ���ׂ�
            WS_CENTER,      // ����
            WS_FIT          // �g��
        }



        /*
            �C�x���g
        */
        #region Event


        /// <summary>
        /// OptionForm�����[�h�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionForm_Load(object sender, EventArgs e)
        {
            // �ݒ�l���R���g���[���ɃZ�b�g
            chkForemost.Checked = MainProgram.set.AlwaysOnTop;
            chkStartup.Checked = MainProgram.set.StartUp;
            // �ǎ��̕\���X�^�C����ǂݍ���
            OptFunc.WallpaperStyleRead();
            chkWallNothing.Checked = this.WallpaperNothing;
        }

        /// <summary>
        ///   OptionForm������Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// ��Ɏ�O�`�F�b�N�{�b�N�X���ύX���ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkForemost_CheckedChanged(object sender, EventArgs e)
        {
            MainProgram.Foremost(!this.TopMost);
        }

        /// <summary>
        /// �X�^�[�g�A�b�v�`�F�b�N�{�b�N�X���ύX���ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkStartup.Checked == true)
            {
                _optFunc.StartUpRegist();
            }
            else
            {
                _optFunc.StartUpUnRegist();
            }
        }

        /// <summary>
        /// �ǎ��Ȃ��`�F�b�N�{�b�N�X���ύX���ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkWallNothing_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkWallNothing.Checked == true)
            {
                _optFunc.NonWallpaper(true);
            }
            else
            {
                _optFunc.NonWallpaper(false);
            }

        }

        /// <summary>
        /// �ǎ��X�^�C���`�F���W�R���{���ύX���ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboWallpaperStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (MainProgram.gWallpaperStyleWriteFlag == false || chkWallNothing.Checked == true)
            {
                return;
            }

            _optFunc.ChangeWallpaperStyle();
        }

        /// <summary>
        /// �ǎ��F�{�b�N�X���N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPreviewColor_Click(object sender, EventArgs e)
        {
            GeneralFunction gfunc = new GeneralFunction();
            ColorDialog cd = new ColorDialog();

            cd.Color = this.picPreviewColor.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.picPreviewColor.BackColor = cd.Color;
                // �f�X�N�g�b�v�̐F��ݒ�
                gfunc.SetDesktopColor(cd.Color);

                // BackGroundWrite
                _optFunc.BackGroundWrite(cd.Color);

            }
        }

        /// <summary>
        /// �����_���I�v�V�������ύX���ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optRandom_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeMode = (int)MainProgram.enuChgMode.RANDOM;
        }


        /// <summary>
        /// �m�[�}���I�v�V�������ύX���ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optNormal_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeMode = (int)MainProgram.enuChgMode.NORMAL;
        }

        /// <summary>
        /// �Ȃ��I�v�V�������ύX���ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optNone_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeMode = (int)MainProgram.enuChgMode.NONE;
        }


        #endregion


        /*
            �p�u���b�N���\�b�h 
        */
        #region Public Method



        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public OptionForm()
        {
            InitializeComponent();

            // OptFunc����
            this._optFunc = new OptionFunction();

            // �ݒ�I�u�W�F�N�g����ǎ��`�F���W���[�h�����[�h
            this._intChangeMode = MainProgram.set.ChangeMode;
            // ���[�h�����ǎ��`�F���W���[�h��K�p
            switch (this._intChangeMode)
            {
                case (int)MainProgram.enuChgMode.RANDOM:
                    this.optRandom.Checked = true;
                    break;

                case (int)MainProgram.enuChgMode.NORMAL:
                    this.optNormal.Checked = true;
                    break;

                case (int)MainProgram.enuChgMode.NONE:
                    this.optNone.Checked = true;
                    break;
            }
        }




        #endregion



        
        
    }
}