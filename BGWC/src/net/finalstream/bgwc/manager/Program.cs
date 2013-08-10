using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using FINALSTREAM.BackGroundWallpaperChanger.SettingTool;
using System.Runtime.InteropServices;

namespace FINALSTREAM.BackGroundWallpaperChanger.Manager
{

    static class Program
    {

        /*
            Win32API宣言 
        */
        // キーボード押下状態取得関数
        [DllImport("user32.dll")]
        private static extern int GetKeyState(int nVirtKey);

        /*
            定数
        */
        private const int VK_SPACE = 0x20;  // Spaceキー
        private const int VK_BACK = 0x08;   // BackSpaceキー
        private const int VK_PAUSE = 0x13;  // Pauseキー

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // こっから書いたとこ
            try{

                if (args.Length != 0)
                {
                    // コマンドラインに引数があるとき

                    // 拡張子設定
                    string strComFileExt = Path.GetExtension(args[0]).ToLower();

                    if (strComFileExt == ".jpg"
                        || strComFileExt == ".jpeg"
                        || strComFileExt == ".gif"
                        || strComFileExt == ".bmp"
                        || strComFileExt == ".png"){
                        
                        //ドロップされたのが壁紙だったら壁紙設定
                        MainProgram.Main(new String[] { "/add" + args[0] });
                        Environment.Exit(0);     

                    }

                }else{
                    //コマンドラインに引数がないとき

                    if (GetKeyState(VK_SPACE)<0){
                        // スペースが押されていたら設定ツールを起動して終了
                        MainProgram.Main(new String[] { });
                        Environment.Exit(0);
                    }
                    else if (GetKeyState(VK_BACK) < 0)
                    {
                        // BackSpaceキーが押されていたらデリートスキップして終了
                        MainProgram.Main(new String[] { "/delskip" });
                        Environment.Exit(0);
                    }else if(GetKeyState(VK_PAUSE)<0){
                        // Pauseキーが押されていたら壁紙を無効にして終了
                        MainProgram.Main(new String[] { "/nowall" });
                        Environment.Exit(0);
                    }
                }
                // 壁紙チェンジ処理をするためBGWSetTool.exeを/randomで起動。(2007/02/12 BGWSetTool.exeにランダマイズ機能を統合したため)
                MainProgram.Main(new String[]{"/change"});

            }catch(Exception err){
                // エラーメッセージ出力
                MessageBox.Show(err.Message); 
            }

        }
    }
}