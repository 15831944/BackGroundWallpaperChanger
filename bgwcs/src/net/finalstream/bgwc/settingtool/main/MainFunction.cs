using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing;
using FINALSTREAM.Commons.Utils;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    /// <summary>
    /// MainForm�pFunction�N���X
    /// </summary>
    public class MainFunction
    {

        /*
            Private�����o
        */
        private string _strRegWallpaperPath;



        /*
            �v���p�e�B
        */
        /// <summary>
        /// ���W�X�g���ɐݒ肳��Ă���Windows�ǎ��t�@�C���p�X
        /// </summary>
        public string RegWallpaperPath
        {
            get
            {
                this.GetRegWallpaperPath();
                return this._strRegWallpaperPath;
            }
            set {this._strRegWallpaperPath = value;}
        }




        /*
           �p�u���b�N���\�b�h
        */
        #region PublicMethod

        /// <summary>
        /// �ǎ��̃T�C�Y���擾����
        /// </summary>
        /// <param name="strFile">�ǎ��t�@�C���p�X</param>
        /// <returns>�擾�����T�C�Y(Width x Height)</returns>
        public string GetWallpaperSize(string strFile)
        {
            string strSize;
            using (FileStream fs = File.OpenRead(strFile))
            using (Image img = Image.FromStream(fs, false, false))
            {
                strSize = img.Width + " x " + img.Height;
            }
            return strSize;
        }

        /// <summary>
        /// �f���[�g�X�L�b�v�����s����
        /// </summary>
        /// <param name="DeleteFile">�폜�t�@�C���p�X</param>
        /// <param name="bolChange">�폜��A�ǎ��ύX���邩</param>
        public void DeleteSkip(string DeleteFile, bool bolChange)
        {

            // �ݒ肳��Ă���ǎ���TrashBox�Ɉړ�
            if (DeleteFile != "" && File.Exists(DeleteFile))
            {
                try
                {
                    // �t�@�C�����ړ�
                    File.Move(DeleteFile, MainProgram.ApplicationPath + "\\TrashBox\\" + Path.GetFileName(DeleteFile));
                }
                catch (Exception ex)
                {
                    MainProgram.ShowMessage(ex.Message, MainProgram.MES_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MainProgram.ShowMessage(MainProgram.MES_FILE_DELETED, MainProgram.MES_TITLE_INFO, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // �ǎ��̕ύX
            if (bolChange == true)
            {
                WallpaperChange();
            }
        }

        /// <summary>
        /// MainForm�̕ǎ������X�V����
        /// </summary>
        /// <param name="strNowWallpaper">���݂̕ǎ��t�@�C���p�X</param>
        /// <param name="strPreviewWallpaper">�v���r���[�p�t�@�C���p�X</param>
        public void NowWallpaperUpdate(string strNowWallpaper, string strPreviewWallpaper)
        {
            // �ǎ��̃p�X���X�V
            MainProgram.frmMain.NowWallpaper = strNowWallpaper;
            // �𑜓x���X�V
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            MainProgram.frmMain.ScreenSize = rect.Width.ToString() + " x " + rect.Height.ToString();
            // �ǎ���Preview���X�V
            MainProgram.frmMain.SetPreviewWallpaper = strPreviewWallpaper;
            // �ǎ��T�C�Y���X�V
            MainProgram.frmMain.WallpaperSize = MainProgram.frmMain.PreviewBaseWidth.ToString() + " x " + MainProgram.frmMain.PreviewBaseHeight.ToString();

        }

        /// <summary>
        /// �ǎ���ύX����(�`�F���W)
        /// </summary>
        public void WallpaperChange()
        {
            int num = 0;

            int lstCount = MainProgram.frmMain.Grid.RowsCount;
            int lstMaxNum = lstCount;

            if (lstCount > 1)
            {
                // �`�F���W���[�h�ɂ�菈�����킯��
                switch (MainProgram.frmMain.frmOpt.ChangeMode)
                {
                    case (int)MainProgram.enuChgMode.RANDOM:
                        // �����_��

                        Random rnd = new Random();

                        num = rnd.Next(1, lstMaxNum);
                        while (MainProgram.frmMain.WallpaperFolder + "\\" + (string)MainProgram.frmMain.Grid[num, (int)MainProgram.enuGrid.FILENAME].Value ==
                           MainProgram.frmMain.NowWallpaper)
                        {
                            num = rnd.Next(1, lstMaxNum);
                        }
                        break;

                    case (int)MainProgram.enuChgMode.NORMAL:
                        // �m�[�}��

                        num = MainProgram.frmMain.GetKeyIndex(Path.GetFileName(MainProgram.frmMain.NowWallpaper));
                        num++;
                        if (lstMaxNum == num)
                        {
                            num = 1;
                        }
                        break;

                    case (int)MainProgram.enuChgMode.NONE:
                        // �Ȃ�

                        return;
                }
            }
            else if (lstCount == 2)
            {
                // �P�s�����Ȃ��Ƃ��͂����I������B
                num = 2;
            }
            else if (lstCount == 1)
            {
                // �t�H���_�ɉ摜���Ȃ��ꍇ�͏I��(�w�b�_�����Ȃ�)

                MainProgram.ShowMessage(MainProgram.MES_GRAPHIC_NOTHING + MainProgram.frmMain.WallpaperFolder,
                    MainProgram.MES_TITLE_INFO,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Environment.Exit(0);
                return;
            }

            // BGWC�ǎ��ݒ�
            BGWChanerWallpaperSetting((string)MainProgram.frmMain.Grid[num, (int)MainProgram.enuGrid.FILENAME].Value);

        }


        /// <summary>
        /// BGWC�ǎ���Windows�̕ǎ��Ƃ��Đݒ肷��
        /// </summary>
        /// <param name="strWallpaperKey">�ǎ��Ƃ��Đݒ肷��BGWCKey</param>
        public void BGWChanerWallpaperSetting(string strWallpaperKey)
        {
            // �t�@�C�������݂��邩�m�F
            if (File.Exists(MainProgram.frmMain.WallpaperFolder + "\\" + strWallpaperKey))
            {

                GeneralFunction gfunc = new GeneralFunction();

                // WaitForm����
                FadeForm fadefrm = new FadeForm();

                // Wait��ʕ\��
                fadefrm.StartWait();

                // �摜��BGWC�ǎ��Ƃ��ĕۑ�
                new Bitmap(MainProgram.frmMain.WallpaperFolder + "\\" + strWallpaperKey).Save(MainProgram.gBGWChangerGraphicPath, ImageFormat.Bmp);

                // �ʐݒ肪����Εǎ��̈ʒu��ݒ�
                int idx = MainProgram.frmMain.GetKeyIndex(strWallpaperKey);

                switch ((string)MainProgram.frmMain.Grid[idx, (int)MainProgram.enuGrid.VIEWPOS].Value)
                {
                    case MainProgram.conWallpaperPos_Ori:
                        // ����
                        MainProgram.frmMain.frmOpt.OptFunc.WallpaperStyleWrite(
                            (int)MainProgram.enuTileWallpaper.CENTER,
                            (int)MainProgram.enuWallpaperStyle.NONE);
                        break;
                    case MainProgram.conWallpaperPos_Arr:
                        // ���ׂ�
                        MainProgram.frmMain.frmOpt.OptFunc.WallpaperStyleWrite(
                            (int)MainProgram.enuTileWallpaper.FILL,
                            (int)MainProgram.enuWallpaperStyle.NONE);
                        break;
                    case MainProgram.conWallpaperPos_Fit:
                        MainProgram.frmMain.frmOpt.OptFunc.WallpaperStyleWrite(
                            (int)MainProgram.enuTileWallpaper.CENTER,
                            (int)MainProgram.enuWallpaperStyle.STRETCH);
                        break;
                }

                // Windows�̕ǎ��Ƃ��Đݒ肷��
                gfunc.SettingWallpaper(MainProgram.gBGWChangerGraphicPath);

                // ���ʂ̕ǎ��ʒu�̃��W�X�g���ɖ߂�
                switch (MainProgram.frmMain.frmOpt.WallpaperStyle)
                {
                    case (int)MainProgram.enuViewPos.ORIGINAL:
                        MainProgram.frmMain.frmOpt.OptFunc.WallpaperStyleWrite(
                            (int)MainProgram.enuTileWallpaper.CENTER,
                            (int)MainProgram.enuWallpaperStyle.NONE);
                        break;
                    case (int)MainProgram.enuViewPos.ARRANGE:
                        MainProgram.frmMain.frmOpt.OptFunc.WallpaperStyleWrite(
                            (int)MainProgram.enuTileWallpaper.FILL,
                            (int)MainProgram.enuWallpaperStyle.NONE);
                        break;
                    case (int)MainProgram.enuViewPos.FIT:
                        MainProgram.frmMain.frmOpt.OptFunc.WallpaperStyleWrite(
                            (int)MainProgram.enuTileWallpaper.CENTER,
                            (int)MainProgram.enuWallpaperStyle.STRETCH);
                        break;
                }

                string[] strRGB;

                if ((string)MainProgram.frmMain.Grid[idx, (int)MainProgram.enuGrid.RGBCOLOR].Value != "")
                {
                    // �ʐݒ肪����Εǎ��̐F��ݒ�

                    string strWK = (string)MainProgram.frmMain.Grid[idx, (int)MainProgram.enuGrid.RGBCOLOR].Value;
                    if (strWK != null)
                    {
                        strRGB = strWK.Split(' ');
                    }
                    else
                    {
                        string strCommonColor = (string)RegistryUtils.
                        getCURegistryKeyValue(MainProgram.conColorSubKey, "Background");
                        strRGB = strCommonColor.Split(' ');
                    }
                }
                else
                {
                    // �ʐݒ肪�Ȃ��ꍇ�̓��W�X�g�����狤�ʂ̐F���擾

                    string strCommonColor = (string)RegistryUtils.
                        getCURegistryKeyValue(MainProgram.conColorSubKey, "Background");
                    strRGB = strCommonColor.Split(' ');
                }
                // �f�X�N�g�b�v�̐F��ݒ�
                gfunc.SetDesktopColor(Color.FromArgb(int.Parse(strRGB[0]), int.Parse(strRGB[1]), int.Parse(strRGB[2])));


                // NowWallpaper���X�V
                NowWallpaperUpdate(MainProgram.frmMain.WallpaperFolder + "\\" + (string)MainProgram.frmMain.Grid[idx, (int)MainProgram.enuGrid.FILENAME].Value, MainProgram.gBGWChangerGraphicPath);

                // �ݒ茳�ǎ��p�X��ݒ�N���X�ɃZ�b�g
                MainProgram.set.Wallpaper = MainProgram.frmMain.NowWallpaper;

                fadefrm.EndWait();
            }

        }


        /// <summary>
        /// �O���b�h�̕ǎ��F��ݒ肷��
        /// </summary>
        /// <param name="bolMode">�ǎ��F�ݒ胂�[�h(True�Ȃ�ݒ�/False�Ȃ�N���A)</param>
        /// <param name="col">True�̏ꍇ�ɐݒ肷��J���[</param>
        public void WallpaperColorSetting(bool bolMode, Color col)
        {

            if (bolMode)
            {
                // �ǎ��F���Z�b�g

                SourceGrid.Cells.Views.Cell SelectView = new SourceGrid.Cells.Views.Cell();
                SelectView.BackColor = col;
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].View = SelectView;
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].Value = "";
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.RGBCOLOR].Value = col.R + " " + col.G + " " + col.B;
            }
            else
            {
                // �ǎ��F���N���A

                SourceGrid.Cells.Views.Cell SelectView = new SourceGrid.Cells.Views.Cell();
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].View = SelectView;
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].Value = "None";
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.RGBCOLOR].Value = "";

            }

        }

        #endregion

        /*
            �v���C�x�[�g���\�b�h 
        */
        /// <summary>
        /// ���W�X�g���ɐݒ肳��Ă���Windows�ǎ��t�@�C���p�X���擾���ă����o�ɕێ�����
        /// </summary>
        private void GetRegWallpaperPath()
        {

            RegWallpaperPath = (string)RegistryUtils.getCURegistryKeyValue(MainProgram.conWallpaperSubKey,"Wallpaper");

        }
        
    }
}
