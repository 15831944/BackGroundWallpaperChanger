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

        // SystemParametersInfoの呼び出し時に使用する定数
        private const int SPI_SETDESKWALLPAPER = 20;  // デスクトップの壁紙を設定
        private const int SPIF_UPDATEINIFILE = 0x1; // 更新する
        private const int SPIF_SENDWININICHANGE = 0x2; // 全てのアプリケーションに通知して更新する
        private const int WM_ACTIVATE = 0x0006;
        private const int BM_CLICK = 0x00F5;
        private const int COLOR_DESKTOP = 1;
        private const string CAP_BGWCONNECTING = "BGWCHANGER CONNECTING...";

        /*
            Win32API宣言 
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
        /// 指定した画像を壁紙を設定する
        /// </summary>
        /// <param name="strWallpaperPath">設定する画像のファイルパス</param>
        public void SettingWallpaper(string strWallpaperPath)
        {
            SystemParametersInfo(SPI_SETDESKWALLPAPER,
                0,
                strWallpaperPath,
                SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

        }


        /// <summary>
        /// 指定した色にデスクトップの色を変更する
        /// </summary>
        /// <param name="color">変更したいカラー</param>
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
        /// 壁紙設定完了後処理
        /// </summary>
        public void WallpaperChanged()
        {
            // 壁紙設定終了のメッセージを送信。
            IntPtr hwnd = FindWindow(string.Empty, CAP_BGWCONNECTING);
            // ウインドウにメッセージを送る
            if (hwnd == new IntPtr(0))
            {
                // ハンドルを取得できなかったらBGWChanger.exeが呼び出されたと判断して終了する
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
