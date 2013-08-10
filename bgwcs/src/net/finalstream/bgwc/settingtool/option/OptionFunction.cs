using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FINALSTREAM.Commons.Utils;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    /// <summary>
    /// �I�v�V�����֐��N���X
    /// </summary>
    public class OptionFunction
    {

        /*
            �p�u���b�N���\�b�h
        */
        #region Public Method


        /// <summary>
        /// �ǎ��\���X�^�C�������W�X�g������ǂݍ���
        /// </summary>
        public void WallpaperStyleRead()
        {

            // TileWallpaper�l���擾
            int intTileWallpaperValue = int.Parse((string)RegistryUtils.getCURegistryKeyValue(MainProgram.conWallpaperSubKey, "TileWallpaper"));
            // WallpaperStyle�l���擾
            int intWallpaperStyleValue = int.Parse((string)RegistryUtils.getCURegistryKeyValue(MainProgram.conWallpaperSubKey, "WallpaperStyle"));

            if (intTileWallpaperValue == (int)MainProgram.enuTileWallpaper.FILL &&
                intWallpaperStyleValue == (int)MainProgram.enuWallpaperStyle.NONE)
            {
                // ���ׂĕ\��
                MainProgram.frmMain.frmOpt.WallpaperStyle = (int)MainProgram.enuViewPos.ARRANGE;
            }
            else if (intTileWallpaperValue == (int)MainProgram.enuTileWallpaper.CENTER &&
                intWallpaperStyleValue == (int)MainProgram.enuWallpaperStyle.NONE)
            {
                // �����ɕ\��
                MainProgram.frmMain.frmOpt.WallpaperStyle = (int)MainProgram.enuViewPos.ORIGINAL;
            }
            else if (intTileWallpaperValue == (int)MainProgram.enuTileWallpaper.CENTER &&
                intWallpaperStyleValue == (int)MainProgram.enuWallpaperStyle.STRETCH)
            {
                // �g�債�ĕ\��
                MainProgram.frmMain.frmOpt.WallpaperStyle = (int)MainProgram.enuViewPos.FIT;
            }

            // �ǎ��̐F���擾�B
            MainProgram.frmMain.frmOpt.PreviewColor = SystemColors.Desktop;


        }


        /// <summary>
        /// �ǎ��̕\���X�^�C�������W�X�g���ɏ�������
        /// </summary>
        /// <param name="intTileWallpaperValue">TileWallpaper�̒l</param>
        /// <param name="intWallpaperStyleValue">WallpaperStyle�̒l</param>
        public void WallpaperStyleWrite(int intTileWallpaperValue, int intWallpaperStyleValue)
        {

            // �l���X�V
            RegistryUtils.setCURegistryKeyStringValue(MainProgram.conWallpaperSubKey, "TileWallpaper", intTileWallpaperValue.ToString());
            RegistryUtils.setCURegistryKeyStringValue(MainProgram.conWallpaperSubKey, "WallpaperStyle", intWallpaperStyleValue.ToString());

        }

        /// <summary>
        /// �X�^�[�g�A�b�v��ݒ肷��(���W�X�g��)
        /// </summary>
        public void StartUpRegist()
        {
            // ���s����t�@�C��������������
            RegistryUtils.setCURegistryKeyStringValue(MainProgram.conStartupSubKey, "BGWChanger", MainProgram.gExeFilePath);

        }

        /// <summary>
        /// �X�^�[�g�A�b�v�̐ݒ����������(���W�X�g��)
        /// </summary>
        public void StartUpUnRegist()
        {

            // �l���폜
            RegistryUtils.deleteCURegistryKeyStringValue(MainProgram.conStartupSubKey, "BGWChanger");

        }

        /// <summary>
        /// �ǎ��̖�����ݒ�(����)����
        /// </summary>
        /// <param name="bolVal">true:�ݒ� false:����</param>
        public void NonWallpaper(bool bolVal)
        {
            GeneralFunction gfunc = new GeneralFunction();

            // WaitForm����
            WaitForm waitfrm = new WaitForm();

            waitfrm.StartWait();

            if (bolVal)
            {
                // �����ɂ���
                RegistryUtils.setCURegistryKeyStringValue(MainProgram.conWallpaperSubKey, "Wallpaper", "");

                // �ǎ��𖳌��ɐݒ�
                gfunc.SettingWallpaper("");
            }
            else
            {
                // �L���ɂ���
                RegistryUtils.setCURegistryKeyStringValue(MainProgram.conWallpaperSubKey, "Wallpaper", MainProgram.gBGWChangerGraphicPath);

                // �ǎ���L���ɐݒ�
                gfunc.SettingWallpaper(MainProgram.gBGWChangerGraphicPath);

            }

            waitfrm.EndWait();

        }

        /// <summary>
        /// �ǎ��X�^�C����ύX����
        /// </summary>
        public void ChangeWallpaperStyle()
        {
            GeneralFunction gfunc = new GeneralFunction();

            // WaitForm����
            WaitForm waitfrm = new WaitForm();

            waitfrm.StartWait();

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

            gfunc.SettingWallpaper(MainProgram.frmMain.MainFunc.RegWallpaperPath);

            waitfrm.EndWait();
        }

        /// <summary>
        /// �ǎ��F��ݒ肷��(���W�X�g��)
        /// </summary>
        /// <param name="col"></param>
        public void BackGroundWrite(Color col)
        {

            RegistryUtils.setCURegistryKeyStringValue(MainProgram.conColorSubKey, "Background", col.R.ToString() + " " + col.G.ToString() + " " + col.B.ToString());

        }


        #endregion
        
    }
}
