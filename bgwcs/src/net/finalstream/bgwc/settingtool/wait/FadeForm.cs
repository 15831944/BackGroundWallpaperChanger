using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    /// <summary>
    /// スプラッシュスクリーンクラス
    /// </summary>
    public partial class FadeForm : Form
    {

        // FindWindow 関数
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        private static extern System.IntPtr FindWindow(
            string lpClassName,
            string lpWindowName
        );

        // SetParent 関数
        [DllImport("USER32.DLL", CharSet = CharSet.Auto)]
        private static extern System.IntPtr SetParent(
            System.IntPtr hWndChild,
            System.IntPtr hWndNewParent
        );

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("User32.dll")]
        public static extern void ReleaseDC(IntPtr dc);

        //        HWND GetDesktopWindow(VOID);        
        [DllImport("User32.Dll")]
        static extern IntPtr GetDesktopWindow();

        [DllImport("gdi32.dll")]
        private static extern int BitBlt(IntPtr hDestDC,
            int x,
            int y,
            int nWidth,
            int nHeight,
            IntPtr hSrcDC,
            int xSrc,
            int ySrc,
            int dwRop);

        [DllImport("user32.dll")]
        static extern bool PaintDesktop(IntPtr hdc);

        [DllImport("gdi32.dll", SetLastError = true)]
        static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public enum SpecialWindowHandles
        {
            // ReSharper disable InconsistentNaming
            /// <summary>
            ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            HWND_TOP = 0,
            /// <summary>
            ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            HWND_BOTTOM = 1,
            /// <summary>
            ///     Places the window at the top of the Z order.
            /// </summary>
            HWND_TOPMOST = -1,
            /// <summary>
            ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            /// </summary>
            HWND_NOTOPMOST = -2
            // ReSharper restore InconsistentNaming
        }

        [Flags]
        public enum SetWindowPosFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FadeForm()
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

            // Program Manager のハンドルを取得する
            //System.IntPtr hProgramManagerHandle = FindWindow(null, "Program Manager");

            // 正しく取得できた場合は、Program Manager を親ウィンドウに設定する
            //if (!hProgramManagerHandle.Equals(System.IntPtr.Zero))
            //{
            //    SetParent(this.Handle, hProgramManagerHandle);
            //}
            //this.SendToBack();
            //this.SendToBack();
            //this.SendToBack();
            //this.SendToBack();
            IntPtr HWND_BOTTOM = new IntPtr(1);
            const UInt32 SWP_NOSIZE = 0x0001;
            const UInt32 SWP_NOMOVE = 0x0002;
            const UInt32 SWP_NOREDRAW = 0x0008;


            //for (int i = 0; i < 1000; i++)
            //{
            //    this.SendToBack();
            //}

            SetParent(this.Handle, GetDesktopWindow());

            Rectangle rect = Screen.PrimaryScreen.Bounds;
            this.Width = rect.Width;
            this.Height = rect.Height;
            this.picWait.Width = rect.Width;
            this.picWait.Height = rect.Height;
            //this.StartPosition = FormStartPosition.Manual;
            this.Top = 0;
            this.Left = 0;

            
            
            //Bitmapの作成
            Bitmap bmp = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                Screen.PrimaryScreen.Bounds.Height);
            //Graphicsの作成
            Graphics g = Graphics.FromImage(bmp);
            //Graphicsのデバイスコンテキストを取得
            IntPtr hDC = g.GetHdc();

            System.IntPtr hwndDesktop = GetDC(GetDesktopWindow());
            //IntPtr dc = GetDC(IntPtr.Zero);
            //IntPtr hwndDesktop = IntPtr.Zero;

            //PaintDesktop(hwndDesktop);
            
            const int SRCCOPY = 13369376;
            //Bitmapに画像をコピーする
            BitBlt(hDC, 0, 0, bmp.Width, bmp.Height,
                hwndDesktop, 0, 0, SRCCOPY);

            
            
            //解放
            g.ReleaseHdc(hDC);
            g.Dispose();
            ReleaseDC(hwndDesktop);

            
                //bmp.Save(@"c:\test.bmp");
                // this.SetStyle(ControlStyles.DoubleBuffer, true);
                // this.SetStyle(ControlStyles.UserPaint, true);
                //this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
                //this.SetStyle(ControlStyles.Opaque, true);
                //表示
                this.BackgroundImage = bmp;

            this.Show();

            SetWindowPos(this.Handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOMOVE  | SWP_NOREDRAW);
            Application.DoEvents();
            
        }

        /// <summary>
        /// スプラッシュスクリーンを消す
        /// </summary>
        public void EndWait()
        {
            

            // フェード更新間隔
            int DURATION_MS = 100;
            int elapsedms = 0;
            float fadeDurationMs = 1.0f * 1000;
            float fadeCount = fadeDurationMs / (float)DURATION_MS;
            double ope = 1.0d;
            double opechange = 0;
            opechange = (ope / fadeCount) * -1;
            while (fadeDurationMs >= elapsedms)
            {


                ope += opechange;
                this.Opacity = ope;
                System.Threading.Thread.Sleep(DURATION_MS);
                elapsedms += DURATION_MS;
                this.picWait.Refresh();
                
            }

            this.Close();
            
            if (MainProgram.frmMain.frmOpt.AlwaysOnTop == true)
            {
                MainProgram.Foremost(true);
            }
        }



        private void FadeForm_Paint(object sender, PaintEventArgs e)
        {
            
        }
        
    }
}