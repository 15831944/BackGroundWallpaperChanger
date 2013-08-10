using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using FINALSTREAM.BackGroundWallpaperChanger.Library;
using System.IO;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    /// <summary>
    /// ���C���t�H�[��
    /// </summary>
    public partial class MainForm : Form
    {

        /*
            Private�����o 
        */
        #region PrivateMember

        private OptionForm _frmOpt;
        private MainFunction _MainFunc;
        private int _PreviewBaseWidth;  // Preview�̌�Width
        private int _PreviewBaseHeight; // Preview�̌�Height
        private List<string> _lstKeyIndexList;   // Key�����p���X�g
        private MouseButtons _flgMouseButton = System.Windows.Forms.MouseButtons.Left; // ���_�u���N���b�N���m�p�t���O
        


        #endregion

        /*
            �v���p�e�B
        */
        #region Property
        /// <summary>
        /// MainForm�p�֐�
        /// </summary>
        public MainFunction MainFunc
        {
            get { return this._MainFunc; }
        }

        /// <summary>
        /// OptionForm
        /// </summary>
        public OptionForm frmOpt
        {
            get { return this._frmOpt; }
        }

        /// <summary>
        /// �O���b�h
        /// </summary>
        public SourceGrid.Grid Grid
        {
            get { return this.griList; }
        }

        /// <summary>
        /// ���ݐݒ肳��Ă���ǎ��p�X
        /// </summary>
        public string NowWallpaper
        {
            set { this.txtNowWallpaper.Text = value; }
            get { return this.txtNowWallpaper.Text; }
        }

        /// <summary>
        /// ���j�^�̉𑜓x
        /// </summary>
        public string ScreenSize
        {
            set { this.lblScreenSize.Text = value + " pixel"; }
            get { return this.lblScreenSize.Text; }
        }

        /// <summary>
        /// �ǎ��̑傫��
        /// </summary>
        public string WallpaperSize
        {
            set { this.lblWallpaperSize.Text = value + " pixel"; }
            get { return this.lblWallpaperSize.Text; }
        }

        /// <summary>
        /// �v���r���[���Ă���ǎ��̑傫��
        /// </summary>
        public string PreviewSize
        {
            set { this.lblPreviewSize.Text =value + " pixel"; }
            get { return this.lblPreviewSize.Text; }
        }

        /// <summary>
        /// �v���r���[�摜
        /// </summary>
        public PictureBox PreviewPicture
        {
            get { return this.picPreview; }
        }

        /// <summary>
        /// �v���r���[�摜�̐ݒ�
        /// </summary>
        public string SetPreviewWallpaper
        {
            set
            {
                SettingPreviewWallpaper(value);
            }
        }

        /// <summary>
        /// BGWC�ǎ��t�H���_
        /// </summary>
        public string WallpaperFolder
        {
            set { this.txtWallpaperFolder.Text = value; }
            get { return this.txtWallpaperFolder.Text; }
        }

        /// <summary>
        /// �v���r���[��Width
        /// </summary>
        public int PreviewBaseWidth
        {
            set { this._PreviewBaseWidth = value; }
            get { return this._PreviewBaseWidth; }
        }

        /// <summary>
        /// �v���r���[��Height
        /// </summary>
        public int PreviewBaseHeight
        {
            set { this._PreviewBaseHeight = value; }
            get { return this._PreviewBaseHeight; }
        }

        /// <summary>
        /// ���N���b�N�C�x���g���m�p�t���O
        /// </summary>
        public MouseButtons FlgMouseButton
        {
            set { this._flgMouseButton = value; }
            get { return this._flgMouseButton; }
        }

        /// <summary>
        /// �v���r���[�t�@�C����
        /// </summary>
        public string PreviewFileName
        {
            set { this.txtPreviewFile.Text = value; }
            get { return this.txtPreviewFile.Text; }
        }

        #endregion

        /*
            �C�x���g
        */
        #region Event

        /// <summary>
        /// MainForm�����[�h�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            GeneralFunction gfunc = new GeneralFunction();

            // �R�}���h���C����"delskip"���킽���ꂽ��
            if (MainProgram.CommandLine == "/delskip")
            {
                MainFunc.DeleteSkip(this.NowWallpaper, true);
                Environment.Exit(0);
            }
            else if (MainProgram.CommandLine == "/nowall")
            {
                // "/nowall"���킽���ꂽ��
                // ���W�X�g���L�[���I�[�v��
                RegistryKey regkey = Registry.CurrentUser.OpenSubKey(MainProgram.conWallpaperSubKey);
                regkey.SetValue("Wallpaper", "");
                // �ǎ��𖳌��ɐݒ�
                regkey.Close();
                Environment.Exit(0);
            }
            else if (MainProgram.CommandLine.Length > 4 && MainProgram.CommandLine.Substring(0, 4) == "/add")
            {
                // �ǎ����h���b�v���ꂽ�Ƃ�
                // Me.Hide
                // �O��add�̕������Ƃ��ĕǎ��o�^
                AddWallpaper(MainProgram.CommandLine.Substring(4));
                // �o�^��I������
                Environment.Exit(0);
            }

            // �ݒ肳��Ă���ǎ����擾(Regist)
            // ����BGWChanger�ƈႤ�ǎ����ݒ肳��Ă����ꍇ
            if (MainFunc.RegWallpaperPath != MainProgram.gBGWChangerGraphicPath)
            {
                // �ǎ������������ɓ���ւ���
                MainFunc.NowWallpaperUpdate(MainFunc.RegWallpaperPath, MainFunc.RegWallpaperPath);
            }
            else
            {
                // BGWChangerGraphic���݃`�F�b�N
                if (File.Exists(MainProgram.gBGWChangerGraphicPath))
                {
                    // �ݒ肳��Ă���ǎ��̌��t�@�C���p�X��ǂݍ���
                    MainFunc.NowWallpaperUpdate(MainProgram.set.Wallpaper, MainProgram.gBGWChangerGraphicPath);
                }
            }

            if (MainFunc.RegWallpaperPath == "")
            {
                MainProgram.frmMain.frmOpt.WallpaperNothing = true;
            }

            // �ǎ����X�g����
            ListCreate();

            // �ǎ��`�F���W
            if (MainProgram.CommandLine == "/change" && griList.Rows.Count > 0)
            {
                MainFunc.WallpaperChange();
                gfunc.WallpaperChanged();
                return;
            }

            // �v���r���[�Ɍ��݂̕ǎ��̖��O�ƃT�C�Y�����[�h
            this.PreviewFileName = Path.GetFileName(txtNowWallpaper.Text);
            this.lblPreviewSize.Text = this.lblWallpaperSize.Text;

            MainProgram.gWallpaperStyleWriteFlag = true;

            // ���ׂă��[�h���Ă���\��
            this.Visible = true;

        }

        /// <summary>
        /// MainForm������Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // �ݒ�I�u�W�F�N�g�Ɋi�[
            MainProgram.set.WallpaperFolder = this.txtWallpaperFolder.Text;
            MainProgram.set.Wallpaper = this.txtNowWallpaper.Text;
        }

        /// <summary>
        /// MainForm�𓮂������Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Move(object sender, EventArgs e)
        {
            // �ʒu�A�T�C�Y��MainForm�ɂ��킹��
            frmOpt.Top = MainProgram.frmMain.Top;
            frmOpt.Left = MainProgram.frmMain.Left + MainProgram.frmMain.Width;
            frmOpt.Height = MainProgram.frmMain.Height;
        }

        /// <summary>
        /// MainForm���A�N�e�B�u�ɂȂ����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Activated(object sender, EventArgs e)
        {
            // OptionForm��\��
            frmOpt.Visible = false;
            // �ݒ�{�^���L��
            btnSetting.Enabled = true;
        }


        /// <summary>
        /// BGWC�ǎ��t�H���_�I���{�^���N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFolder_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialog�N���X�̃C���X�^���X���쐬
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //�㕔�ɕ\����������e�L�X�g���w�肷��
            fbd.Description = "�ǎ�������t�H���_���w�肵�Ă��������B";
            //���[�g�t�H���_���w�肷��
            //�f�t�H���g��Desktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //�ŏ��ɑI������t�H���_���w�肷��
            //RootFolder�ȉ��ɂ���t�H���_�ł���K�v������
            fbd.SelectedPath = this.txtWallpaperFolder.Text;
            //���[�U�[���V�����t�H���_���쐬�ł���悤�ɂ���
            fbd.ShowNewFolderButton = false;

            //�_�C�A���O��\������
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //�I�����ꂽ�t�H���_��\������
                this.txtWallpaperFolder.Text = fbd.SelectedPath;
                ListCreate();
            }

        }


        /// <summary>
        /// �ݒ�{�^���N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {

            if (frmOpt.Visible == false)
            {
                // OptionForm���\������ĂȂ��Ƃ�

                // �ʒu�A�T�C�Y��MainForm�ɂ��킹��
                MainForm_Move(sender, e);
                btnSetting.Enabled = false;
                // OptionForm�\��
                frmOpt.Show();
            }
            else
            {
                // OptionForm���\������Ă���ƂƂ�

                frmOpt.Hide();
            }
        }

        /// <summary>
        /// �K�p�{�^�����N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (griList.Rows.Count > 1)
            {
                // �O���b�h���A�N�e�B�u�ɂ���
                this.griList.Focus();
                // �ǎ��ɐݒ� 
                MainProgram.frmMain.MainFunc.BGWChanerWallpaperSetting((string)griList[griList.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.FILENAME].Value);
                // �t�H�[�����A�N�e�B�u�ɂ���
                MainProgram.frmMain.Activate();
            }
        }

        /// <summary>
        /// BGWC�ǎ��t�H���_�A�C�R�����_�u���N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picFolder_DoubleClick(object sender, EventArgs e)
        {
            if (Directory.Exists(txtWallpaperFolder.Text))
            {
                // BGWC�ǎ��t�H���_���G�N�X�v���[���ŕ\��
                System.Diagnostics.Process.Start("explorer.exe", "/n," + txtWallpaperFolder.Text);
            }
        }

        /// <summary>
        /// �S�~�����_�u���N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picTrashBox_DoubleClick(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\TrashBox"))
            {
                // �S�~��(�t�H���_)���G�N�X�v���[���ŕ\��
                System.Diagnostics.Process.Start("explorer.exe", "/n," + Application.StartupPath + "\\TrashBox");
            }
        }


        /// <summary>
        /// �f���[�g�X�L�b�v�{�^�����N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelSkip_Click(object sender, EventArgs e)
        {
            if (MainProgram.ShowMessageRes(MainProgram.MES_DELETESKIP, MainProgram.MES_TITLE_QUESTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // �f���[�g�X�L�b�v���s
                MainFunc.DeleteSkip(this.NowWallpaper, true);
                // �O���b�h���ĕ`��
                ListCreate();
            }
        }

        /// <summary>
        /// ���݂̕ǎ��Ƀf�[�^�����͂��ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNowWallpaper_Enter(object sender, EventArgs e)
        {
            this.txtNowWallpaper.Select(this.txtNowWallpaper.Text.Length, 0);
        }

        /// <summary>
        /// Preview�t�@�C���Ƀf�[�^�����͂��ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPreviewFile_Enter(object sender, EventArgs e)
        {
            this.txtPreviewFile.Select(this.txtPreviewFile.Text.Length, 0);
        }

        /// <summary>
        /// �`�F���W�{�^�����N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            // �ǎ��`�F���W
            MainProgram.frmMain.MainFunc.WallpaperChange();
            // ���݂̕ǎ���I����Ԃɂ���
            ListWallpaperSelected(GetWallpaperKey(NowWallpaper));
        }


        /// <summary>
        /// �X�V�{�^�����N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // ���t���b�V��
            AllRefresh();

        }

        /// <summary>
        /// Preview�摜���N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPreview_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // �E�N���b�N

                if (griList.RowsCount > 1)
                {
                    Bitmap bmp = new Bitmap(this.picPreview.Image);
                    //�t�H�[�J�X�𓖂ĂȂ���ActicvePosition�ŗ�����̂�
                    MainProgram.frmMain.griList.Focus();
                    // �ǎ��F���O���b�h�ɃZ�b�g
                    MainFunc.WallpaperColorSetting(true, bmp.GetPixel(e.X, e.Y));
                }
            }
        }

        /// <summary>
        /// URL�Ƀ}�E�X���ړ������Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSite_MouseMove(object sender, MouseEventArgs e)
        {
            this.lblSite.ForeColor = Color.Gold;
        }

        /// <summary>
        /// URL����}�E�X�����ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTitleBack_MouseMove(object sender, MouseEventArgs e)
        {
            this.lblSite.ForeColor = Color.Orange;
        }

        /// <summary>
        /// URL���N���b�N�����Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.finalstream.net/");
        }

        /// <summary>
        /// �O���b�h�ɂc���c���ꂽ�Ƃ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void griList_DragDrop(object sender, DragEventArgs e)
        {
            // �h���b�O���h���b�v���ꂽ�t�@�C��
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string getfile in files)
            {
                if (this.CheckFileExt(Path.GetExtension(getfile)))
                {
                    AddWallpaper(getfile);
                }
            }

            // ���X�g�X�V
            AllRefresh();

        }

        /// <summary>
        /// �O���b�ɂc���c���ꂽ�Ƃ��i�c���c����j
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void griList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                // �h���b�O���̃t�@�C����f�B���N�g���̎擾
                string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string d in drags)
                {
                    if (!System.IO.File.Exists(d))
                    {
                        // �t�@�C���ȊO�ł���΃C�x���g�E�n���h���𔲂���
                        return;
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }
        }


        #endregion



        /*
            SourceGrid�p�C�x���g 
        */
        #region SGEvent

        // �I���s�ύX��
        private void Selection_SelectionChanged(object sender, SourceGrid.RangeRegionChangedEventArgs e)
        {
            int intActRow = griList.Selection.ActivePosition.Row;
            if (intActRow > 0)
            {
                SettingPreviewWallpaper(txtWallpaperFolder.Text + "\\" + griList[intActRow, (int)MainProgram.enuGrid.FILENAME].Value);
                this.PreviewFileName = Path.GetFileName((string)griList[intActRow, (int)MainProgram.enuGrid.FILENAME].Value);
                lblPreviewSize.Text = (string)griList[intActRow, (int)MainProgram.enuGrid.WALLPAPERSIZE].Value + " pixel";
            }
        }

        #endregion


        /*
            �p�u���b�N���\�b�h 
        */
        #region PublicMethod

        
        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // MainFunc����
            this._MainFunc = new MainFunction();

            // OptionForm����
            this._frmOpt = new OptionForm();

            // �O���b�h�̏����ݒ�
            InitGrid();

            // �ݒ�����[�h
            this.txtWallpaperFolder.Text = MainProgram.set.WallpaperFolder;

        }

        /// <summary>
        /// �L�[�����pIndex����w�肳�ꂽ�L�[�����邩��������
        /// </summary>
        /// <param name="strKey">�L�[(BGWC�ǎ��t�H���_����̃p�X)</param>
        /// <returns>Index�ԍ�(������Ȃ����-1)</returns>
        public int GetKeyIndex(string strKey)
        {
            return _lstKeyIndexList.FindIndex(delegate(string key)
            {
                if (key == strKey)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            });
        }

        /// <summary>
        /// �L�[�����pIndex���X�V����
        /// </summary>
        /// <param name="strBeforeKey">�ύX�O�̃L�[(BGWC�ǎ��t�H���_����̃p�X)</param>
        /// <param name="strAfterKey">�ύX��̃L�[(BGWC�ǎ��t�H���_����̃p�X)</param>
        public void updateKeyIndex(string strBeforeKey,string strAfterKey)
        {
         
           int intKeyIndex = GetKeyIndex(strBeforeKey);
           if (intKeyIndex > 0)
           {
               _lstKeyIndexList.RemoveAt(intKeyIndex);
               _lstKeyIndexList.Insert(intKeyIndex, strAfterKey);
           }
        }

        #endregion



        /*
            �v���C�x�[�g���\�b�h
        */
        #region PrivateMethod

        /// <summary>
        /// �t�@�C���g���q�ŕǎ��Ώۂ����肷��
        /// </summary>
        /// <param name="strFileExt">�t�@�C���g���q</param>
        /// <returns>�ǎ��Ώۂ�</returns>
        private bool CheckFileExt(string strFileExt)
        {
            // �g���q���������ɕϊ�
            strFileExt = strFileExt.ToLower();
            // �g���q�`�F�b�N
            if (strFileExt == ".jpg"
                        || strFileExt == ".jpeg"
                        || strFileExt == ".gif"
                        || strFileExt == ".bmp"
                        || strFileExt == ".png")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// �ǎ���o�^����
        /// </summary>
        /// <param name="strAddFilePath">�o�^����ǎ��̃p�X</param>
        private void AddWallpaper(string strAddFilePath)
        {

            // ��Ɏ�O������
            MainProgram.Foremost(false);

            // �R�s�[��p�X
            string strCopyFilePath = this.txtWallpaperFolder.Text + "\\" + Path.GetFileName(strAddFilePath);

            // �R�s�[�t�@�C�������݂��邩�m�F
            if (File.Exists(strCopyFilePath))
            {
                // �������łɑ��݂��Ă�����

                //�g���q�擾
                string strExt = Path.GetExtension(strAddFilePath);
                //�Ⴄ���O����͂��Ă��炤
                string strNewFileName
                    = Microsoft.VisualBasic.Interaction.InputBox(MainProgram.MES_FILE_NEWNAME, MainProgram.MES_FILE_EXISTS, "Wallpaper" + DateTime.Now.ToString("yyyymmdd") + DateTime.Now.ToString("hhmmss"), -1, -1);
                if (strNewFileName == "")
                {
                    // �L�����Z�����ꂽ��o�^�����ɏI�� 
                    return;
                }
                // �R�s�[��p�X���X�V
                strCopyFilePath = this.WallpaperFolder + "\\" + strNewFileName + "." + strExt;
            }

            // �ǎ���o�^(�t�@�C���ړ�)
            try
            {
                File.Move(strAddFilePath, strCopyFilePath);
            }
            catch (Exception ex)
            {
                MainProgram.ShowMessage(ex.Message, MainProgram.MES_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // �o�^��ǎ��ݒ肷�邩�m�F����B
            DialogResult ret = MessageBox.Show(Path.GetFileName(strCopyFilePath) + "��ǎ��ɐݒ肵�܂����H", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (ret == DialogResult.Yes)
            {
                // OK���I�����ꂽ��

                // ���X�g���č\�z
                this.ListCreate();
                // �ǉ������ǎ���I����Ԃɂ���
                ListWallpaperSelected(GetWallpaperKey(strCopyFilePath));
                // �ǉ������ǎ������݂̕ǎ��ɂ���
                MainFunc.NowWallpaperUpdate(strCopyFilePath, strCopyFilePath);

                // �ǎ��ɂ���
                MainProgram.frmMain.btnApply_Click(new Object(), new EventArgs());
            }

            // ��Ɏ�O��������߂�
            if (MainProgram.frmMain.frmOpt.AlwaysOnTop == true)
            {
                MainProgram.Foremost(true);
            }
        }

        /// <summary>
        /// �w�肵���摜�t�@�C����Preview�ɕ\������
        /// </summary>
        /// <param name="strWallpaper">Preview�ɕ\������摜�t�@�C���p�X</param>
        private void SettingPreviewWallpaper(string strWallpaper)
        {
            if (File.Exists(strWallpaper))
            {
                using (FileStream fs = File.OpenRead(strWallpaper))
                using (Image img = Image.FromStream(fs, false, false))
                {
                    // �T���l�C���쐬�O�Ɍ��̃T�C�Y��ۑ�
                    this.PreviewBaseWidth = img.Width;
                    this.PreviewBaseHeight = img.Height;

                    // �c����Œ�{��
                    double zoom = (double)picPreview.Width / (double)img.Width;

                    // Preview�ɕ\��
                    picPreview.Image = new Bitmap(img, picPreview.Width, (int)(zoom * img.Height));

                }
            }
        }


        /// <summary>
        /// �ǎ��t�@�C���p�X����BGWCKey���擾����
        /// </summary>
        /// <param name="strWallpaperPath">�ǎ��t�@�C���p�X</param>
        /// <returns>�擾����BGWCKey</returns>
        private string GetWallpaperKey(string strWallpaperPath)
        {
            return strWallpaperPath.Replace(WallpaperFolder + "\\", "");
        }

        /// <summary>
        /// �w�肵��BGWCKey�̕ǎ���I����Ԃɂ���
        /// </summary>
        /// <param name="strKey">BGWCKey</param>
        private void ListWallpaperSelected(string strKey)
        {
            // BGWCKeyIndex������
            int idx = this.GetKeyIndex(strKey);
            if (idx == -1)
            {
                // BGWCKey��������Ȃ����1�s�ڂ�I��
                griList.Selection.SelectRow(1, true);
            }
            else
            {
                // BGWCKey��������΂��̍s��I��
                griList.Selection.SelectRow(idx, true);
                griList.Selection.FocusRow(idx);
            }
        }

        /// <summary>
        /// ���ׂĂ��X�V����
        /// </summary>
        private void AllRefresh(){

            // �ݒ�I�u�W�F�N�g����
            BgwcSetting set = new BgwcSetting();

            // �X�V����O�Ɍ��݂̌ʕǎ��ݒ��ۑ�
            set.SaveWallpaperSetting();
            // ���݂̕ǎ��̃p�X�X�V
            MainFunc.NowWallpaperUpdate(this.NowWallpaper, MainFunc.RegWallpaperPath);
            // �ǎ����X�g���č\�z
            ListCreate();

        }


        #endregion



        /*
            SourceGrid�p�v���C�x�[�g���\�b�h 
        */
        #region SGPrivateMethod

        /// <summary>
        /// �O���b�h������������
        /// </summary>
        private void InitGrid()
        {
            // �O���b�h�X�^�C��
            griList.BorderStyle = BorderStyle.Fixed3D;

            // �O���b�h�̗�
            griList.ColumnsCount = 5;
            griList.FixedRows = 1;
   
            // �w�b�_����
            griList.Rows.Insert(0);
            // �t�@�C���p�X
            griList[0, (int)MainProgram.enuGrid.FILENAME] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.FileName);
            griList[0, (int)MainProgram.enuGrid.FILENAME].Column.Width = 200;
            // �ǎ��T�C�Y
            griList[0, (int)MainProgram.enuGrid.WALLPAPERSIZE] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.Size);
            griList[0, (int)MainProgram.enuGrid.WALLPAPERSIZE].Column.Width = 90;
            // �ǎ��ʒu
            griList[0, (int)MainProgram.enuGrid.VIEWPOS] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.DisplayPosition);
            griList[0, (int)MainProgram.enuGrid.VIEWPOS].Column.Width = 97;
            // �ǎ��F
            griList[0, (int)MainProgram.enuGrid.COLOR] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.WallpaperColor);
            griList[0, (int)MainProgram.enuGrid.COLOR].Column.Width = 50;
            // �ǎ��F(RGB�l) ��Hidden
            griList[0, (int)MainProgram.enuGrid.RGBCOLOR] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.RGBColor);
            griList[0, (int)MainProgram.enuGrid.RGBCOLOR].Column.Width = 0;

            //�I��p�C�x���g�R���g���[���ǉ�
            griList.Selection.SelectionChanged += new SourceGrid.RangeRegionChangedEventHandler(Selection_SelectionChanged);
        }


        /// <summary>
        /// �O���b�h�Ƀ��X�g�𐶐�����
        /// </summary>
        private void ListCreate()
        {
            // �O���b�h�N���A
            this.GridClear();

            // BGWC�ǎ��t�H���_�����݂���Ƃ��̂݃��X�g�쐬
            if (Directory.Exists(this.txtWallpaperFolder.Text))
            {
                // BGWC�ǎ��t�H���_�ȉ��ɂ���t�@�C�����擾���z��Ɋi�[
                string[] strAllFiles = Directory.GetFiles(this.txtWallpaperFolder.Text,"*",SearchOption.AllDirectories);

                // �ǎ��Ɋւ���t�@�C���������o
                List<string> lstGraphicFile = new List<string>();   // �ǎ��p�t�@�C�����X�g������
                lstGraphicFile.Capacity = strAllFiles.Length;
                foreach (string strFile in strAllFiles)
                {
                    if(this.CheckFileExt(Path.GetExtension(strFile))){
                        lstGraphicFile.Add(strFile);   
                    }
                }

                // ���X�g���쐬

                // SourceGrid�p�C�x���g������
                CellDoubleClickEvent doubleclickController = new CellDoubleClickEvent();
                FileNameCellClickEvent filenameClickController = new FileNameCellClickEvent();
                //FileNameEditStartEvent filenameeditstartController = new FileNameEditStartEvent();
                WallpaperColorCellClickEvent wallpaperColorClickController = new WallpaperColorCellClickEvent();
                KeyDownEvent keydownController = new KeyDownEvent();

                // �ǎ��ʒu���X�g����
                SourceGrid.Cells.Editors.ComboBox cbEditor = new SourceGrid.Cells.Editors.ComboBox(typeof(string));
                cbEditor.StandardValues = new string[] { "", MainProgram.conWallpaperPos_Ori, MainProgram.conWallpaperPos_Arr, MainProgram.conWallpaperPos_Fit };
                cbEditor.EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
                cbEditor.Control.DropDownStyle = ComboBoxStyle.DropDownList;

                // �����pIndexList������
                _lstKeyIndexList = new List<string>();
                _lstKeyIndexList.Capacity = 1000;
                // �w�b�_�̕���ǉ����Ă���
                _lstKeyIndexList.Add("");

                // �ǎ��p�t�@�C�����X�g����P���Ƃ肾��
                foreach (string strFile in lstGraphicFile)
                {
                    // �V�K�s�ԍ����擾
                    int i = griList.RowsCount;
                    // �V�K�s��ǉ�
                    griList.Rows.Insert(i);

                    // �t�@�C�����p�Z������
                    griList[i,(int)MainProgram.enuGrid.FILENAME] = new SourceGrid.Cells.Cell(strFile.Replace(this.txtWallpaperFolder.Text + "\\",""),typeof(string));
                    griList[i, (int)MainProgram.enuGrid.FILENAME].Editor.EditableMode = SourceGrid.EditableMode.None;
                    griList[i, (int)MainProgram.enuGrid.FILENAME].AddController(doubleclickController);
                    griList[i, (int)MainProgram.enuGrid.FILENAME].AddController(filenameClickController);
                    //griList[i, (int)MainProgram.enuGrid.FILENAME].AddController(filenameeditstartController);
                    griList[i, (int)MainProgram.enuGrid.FILENAME].AddController(keydownController);
                    // �����pIndex���X�g�ɒǉ�
                    _lstKeyIndexList.Add(strFile.Replace(WallpaperFolder + "\\", ""));
                    // �ǉ�����t�@�C���p�X�i�[
                    string strAddFilePath = txtWallpaperFolder.Text + "\\" + griList[i, (int)MainProgram.enuGrid.FILENAME].Value;
                    
                    // �ǎ��T�C�Y�p�Z������
                    //if (this.CheckFileExt((Path.GetExtension(strFile)))){
                    griList[i, (int)MainProgram.enuGrid.WALLPAPERSIZE] = new SourceGrid.Cells.Cell(MainFunc.GetWallpaperSize(strAddFilePath));
                    //}

                    // �ǎ��ʒu�p�Z������
                    griList[i, (int)MainProgram.enuGrid.VIEWPOS] = new SourceGrid.Cells.Cell("", cbEditor);
                    griList[i, (int)MainProgram.enuGrid.VIEWPOS].View = SourceGrid.Cells.Views.ComboBox.Default;

                    // �ǎ��F�p�Z������
                    griList[i, (int)MainProgram.enuGrid.COLOR] = new SourceGrid.Cells.Cell("None", typeof(string));
                    griList[i, (int)MainProgram.enuGrid.COLOR].AddController(wallpaperColorClickController);
                    griList[i, (int)MainProgram.enuGrid.COLOR].Editor.EditableMode = SourceGrid.EditableMode.None;
                    
                    // �ǎ��F(RGB)�p�Z������
                    griList[i, (int)MainProgram.enuGrid.RGBCOLOR] = new SourceGrid.Cells.Cell("", typeof(string));
                }
                // �����pIndex���X�g�̃L���p�V�e�B�𒲐�
                _lstKeyIndexList.TrimExcess();


                // �ʕǎ��\���ݒ肪���݂���ꍇ(ViewSetting�t�@�C��������)
                if (File.Exists(MainProgram.ApplicationPath + "\\" + MainProgram.conViewSetFileName))
                {
                    // �ʕǎ��\���ݒ���擾 (DLL)
                    ViewSetting vset = new ViewSetting();

                    // ViewSetting�t�@�C�����烍�[�h
                    vset.ReadViewSetting(MainProgram.ApplicationPath + "\\" + MainProgram.conViewSetFileName);

                    // ViewSettingList����P�����o��
                    foreach (GVWI gvwi in vset.List)
                    {

                        // BGWCKey������Index������
                        int idx = GetKeyIndex(gvwi.Key);

                        // ���������Ƃ�
                        if (idx > 0)
                        {
                            // �w�b�_�̕��v���X
                            //idx++;

                            // �ǎ��ʒu���X�V
                            griList[idx, (int)MainProgram.enuGrid.VIEWPOS] = new SourceGrid.Cells.Cell(gvwi.ViewPos, cbEditor);
                            griList[idx, (int)MainProgram.enuGrid.VIEWPOS].View = SourceGrid.Cells.Views.ComboBox.Default;

                            if (gvwi.Color != "")
                            {
                                // �ǎ��F���X�V
                                griList[idx, (int)MainProgram.enuGrid.RGBCOLOR] = new SourceGrid.Cells.Cell(gvwi.Color, typeof(string));
                                string[] strRGB = gvwi.Color.Split(' ');
                                SourceGrid.Cells.Views.Cell cview = new SourceGrid.Cells.Views.Cell();
                                cview.BackColor = Color.FromArgb(int.Parse(strRGB[0]), int.Parse(strRGB[1]), int.Parse(strRGB[2]));
                                griList[idx, (int)MainProgram.enuGrid.COLOR].View = cview;
                                griList[idx, (int)MainProgram.enuGrid.COLOR].Value = "";
                            }
                        }


                    }
                }

                // ���݂̕ǎ���I����Ԃɂ���
                ListWallpaperSelected(GetWallpaperKey(NowWallpaper));
                
            }
        }

        /// <summary>
        /// �O���b�h���N���A����
        /// </summary>
        private void GridClear()
        {
            if (griList.RowsCount > 1)
            {
                // �w�b�_���c���Ă��Ƃ͑S�s�폜
                griList.Rows.RemoveRange(1, griList.RowsCount - 1);
            }
        }

        #endregion


        /*
            SourceGrid�p�C�x���g�N���X 
        */
        #region SGEventClass

        /// <summary>
        /// �Z���_�u���N���b�N�C�x���g�N���X
        /// </summary>
        private class CellDoubleClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            public override void OnDoubleClick(SourceGrid.CellContext sender, EventArgs e)
            {
                base.OnDoubleClick(sender, e);
                // �ǎ��ɐݒ�
                if (MainProgram.frmMain.FlgMouseButton == MouseButtons.Left)
                {
                    MainProgram.frmMain.btnApply_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// �t�@�C�����Z���N���b�N�C�x���g�N���X
        /// </summary>
        private class FileNameCellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);

                // �N���b�N���ꂽ�{�^�����L��
                MainProgram.frmMain.FlgMouseButton = e.Button;
            }
        }


        /// <summary>
        /// �L�[�_�E���C�x���g�N���X
        /// </summary>
        private class KeyDownEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            bool bolEnterKeyFlg = false; // �G���^�[�L�[�t���O(Enter�L�[�ŕǎ��K�p�ł���悤�ɂ��邽��)

            /// <summary>
            /// �O���b�h��ŃL�[���������ꂽ�Ƃ�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnKeyDown(SourceGrid.CellContext sender, KeyEventArgs e)
            {
                base.OnKeyDown(sender, e);
                if (e.KeyCode == Keys.Enter && bolEnterKeyFlg == false)
                {
                    // Enter�L�[

                    // �ǎ��ɐݒ�
                    MainProgram.frmMain.btnApply_Click(sender, e);
                    // �O���b�h���A�N�e�B�u�ɂ���
                    
                    bolEnterKeyFlg = true;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    // Delete�L�[

                    //�t�H�[�J�X�𓖂ĂȂ���ActicvePosition�ŗ�����̂�
                    MainProgram.frmMain.griList.Focus();
                    // �f���[�g�X�L�b�v���s
                    MainProgram.frmMain.MainFunc.DeleteSkip(MainProgram.frmMain.WallpaperFolder + "\\" + MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.FILENAME].Value, false);
                    // �f���[�g�X�L�b�v�����s���폜
                    MainProgram.frmMain.Grid.Rows.Remove(MainProgram.frmMain.Grid.Selection.ActivePosition.Row);
                }
                else if (e.KeyCode == Keys.Enter && bolEnterKeyFlg == true)
                {
                    // Enter�L�[���Q�񑖂�(2���)

                    // �t���O������
                    bolEnterKeyFlg = false;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    // F2�L�[
                    changeFileName(MainProgram.frmMain.WallpaperFolder + "\\" + (string)MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.FILENAME].Value);
                }
            }

            /// <summary>
            /// �t�@�C������ύX����
            /// </summary>
            /// <param name="strEditFilePath">�ύX�Ώۃt�@�C���p�X</param>
            private void changeFileName(string strEditFilePath){
                // ��Ɏ�O������
                MainProgram.Foremost(false);

                // �t�@�C�������݂��邩�m�F
                if (File.Exists(strEditFilePath))
                {
                    // �t�@�C���𖼑O����͂��Ă��炤
                    string strNewFileName
                        = Microsoft.VisualBasic.Interaction.InputBox(MainProgram.MES_FILE_NEWNAME, MainProgram.MES_TITLE_CHANGEFILENAME, Path.GetFileNameWithoutExtension(strEditFilePath), -1, -1);
                    try
                    {
                        if (strNewFileName != "")
                        {
                            // OK�N���b�N
                            if (Path.GetFileNameWithoutExtension(strEditFilePath) != strNewFileName)
                            {
                                // �t�@�C�����ύX
                                File.Move(strEditFilePath, Path.GetDirectoryName(strEditFilePath) + "\\" + strNewFileName + Path.GetExtension(strEditFilePath));
                                
                                // �L�[�X�V 
                                string strBeforeKey = (string)MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.FILENAME].Value;
                                string strAfterKey;
                                if (Path.GetDirectoryName(strBeforeKey) == String.Empty)
                                {
                                    strAfterKey = strNewFileName + Path.GetExtension(strEditFilePath);
                                }
                                else
                                {
                                    strAfterKey = Path.GetDirectoryName(strBeforeKey) + "\\" + strNewFileName + Path.GetExtension(strEditFilePath);
                                }
                                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.FILENAME].Value = strAfterKey;
                                MainProgram.frmMain.updateKeyIndex(strBeforeKey, strAfterKey);
                                // �v���r���[�t�@�C�����X�V
                                MainProgram.frmMain.PreviewFileName = Path.GetFileName(strAfterKey);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        // �G���[���b�Z�[�W
                        MainProgram.ShowMessage(ex.Message, MainProgram.MES_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                

                // ��Ɏ�O��������߂�
                if (MainProgram.frmMain.frmOpt.AlwaysOnTop == true)
                {
                    MainProgram.Foremost(true);
                }
            }

        }

        /// <summary>
        /// �ǎ��F�Z���N���b�N�C�x���g�N���X
        /// </summary>
        private class WallpaperColorCellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            /// <summary>
            /// �O���b�h��Ń}�E�X���N���b�N���ꂽ�Ƃ�
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);
                //�t�H�[�J�X�𓖂ĂȂ���ActicvePosition�ŗ�����̂�
                MainProgram.frmMain.griList.Focus();
                
                if (e.Button == MouseButtons.Left)
                {
                    // ���N���b�N

                    ColorDialog cd = new ColorDialog();
                    cd.Color = MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].View.BackColor;
                    // �J���[�_�C�A���O�\��
                    if (cd.ShowDialog() == DialogResult.OK)
                    {
                        // �O���b�h�ɕǎ��F���Z�b�g
                        MainProgram.frmMain.MainFunc.WallpaperColorSetting(true, cd.Color);
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    // �O���b�h�̕ǎ��F���N���A
                    MainProgram.frmMain.MainFunc.WallpaperColorSetting(false, new Color());
                }
            }
        }

        #endregion

       
    }
}