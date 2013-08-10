using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    class GeneralFunction
    {

        // SystemParametersInfo�̌Ăяo�����Ɏg�p����萔
        private const int SPI_SETDESKWALLPAPER = 20;  // �f�X�N�g�b�v�̕ǎ���ݒ�
        private const int SPIF_UPDATEINIFILE = 0x1; // �X�V����
        private const int SPIF_SENDWININICHANGE = 0x2; // �S�ẴA�v���P�[�V�����ɒʒm���čX�V����
        private const int WM_ACTIVATE = 0x0006;
        private const int BM_CLICK = 0x00F5;
        private const int COLOR_DESKTOP = 1;
        private const string CAP_BGWCONNECTING = "BGWCHANGER CONNECTING...";

        /*
            Win32API�錾 
        */
        [DllImport("user32.dll")]
        static extern bool SetSysColors(int cElements, int[] lpaElements,
           uint[] lpaRgbValues);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("USER32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        [DllImport("user32.dll")]
        static extern int SystemParametersInfo(int uAction, int uParam, string pvParam, int fuWinIni);



        /// <summary>
        /// �w�肵���摜��ǎ���ݒ肷��
        /// </summary>
        /// <param name="strWallpaperPath">�ݒ肷��摜�̃t�@�C���p�X</param>
        public void SettingWallpaper(string strWallpaperPath)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                strWallpaperPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

        }


        /// <summary>
        /// �w�肵���F�Ƀf�X�N�g�b�v�̐F��ύX����
        /// </summary>
        /// <param name="color">�ύX�������J���[</param>
        public void SetDesktopColor(Color color)
        {
            int[] elements = { COLOR_DESKTOP };
            uint[] colors = { (uint)ColorTranslator.ToWin32(color) };

            //for (int i = 0; i == 1; i++)
            //{
            SetSysColors(1, elements, colors);
            //}
        }


        /// <summary>
        /// �ǎ��ݒ芮���㏈��
        /// </summary>
        public void WallpaperChanged()
        {
            // �ǎ��ݒ�I���̃��b�Z�[�W�𑗐M�B
            IntPtr hwnd = FindWindow(string.Empty, CAP_BGWCONNECTING);
            // �E�C���h�E�Ƀ��b�Z�[�W�𑗂�
            if (hwnd == new IntPtr(0))
            {
                // �n���h�����擾�ł��Ȃ�������BGWChanger.exe���Ăяo���ꂽ�Ɣ��f���ďI������
                MainProgram.frmMain.Close();
                //Environment.Exit(0);
            }
            IntPtr hChild = FindWindowEx(hwnd, IntPtr.Zero, string.Empty, MainProgram.CAP_UPDATE_BTN);
            if (hChild == new IntPtr(0)) { return; }
            SendMessage(hChild, WM_ACTIVATE, new IntPtr(1), IntPtr.Zero);
            SendMessage(hChild, BM_CLICK, new IntPtr(0), new IntPtr(0));

        }


    }
}
