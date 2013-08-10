using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    
    /// <summary>
    /// MainProgramクラス
    /// </summary>
    static public class MainProgram
    {
        /*
            グローバル変数 
        */
        #region Global Member
        /// <summary>
        /// BgwcSettingオブジェクト
        /// </summary>
        static public BgwcSetting set;
        /// <summary>
        /// BGWCGraphicPath(BGWChangerGraphic.bmpのファイルパス)
        /// </summary>
        static public string gBGWChangerGraphicPath;            
        /// <summary>
        /// ExeFileパス
        /// </summary>
        static public string gExeFilePath;
        /// <summary>
        /// 壁紙スタイル変更フラグ
        /// </summary>
        static public bool gWallpaperStyleWriteFlag =false;

        #endregion

        /*
            プライベート変数 
        */
        static private MainForm _frmMain;                       // MainFormオブジェクト
        static private string _strApplicationPath = "";         // アプリケーションのパス
        static private string _strCommandLine = "";             // コマンドライン引数


        /*
            定数
        */
        #region Const Value

        /// <summary>
        /// 壁紙のレジストサブキー
        /// </summary>
        public const string conWallpaperSubKey = "Control Panel\\Desktop";
        /// <summary>
        /// カラーのレジストサブキー
        /// </summary>
        public const string conColorSubKey = "Control Panel\\Colors";
        /// <summary>
        /// スタートアップのレジストサブキー
        /// </summary>
        public const string conStartupSubKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        /// <summary>
        /// ViewSettingファイル名
        /// </summary>
        public const string conViewSetFileName = "bgwc.vs"; 
        /// <summary>
        /// Exeファイル名
        /// </summary>
        public const string conExecFileName = "BGWC.exe"; 
        /// <summary>
        /// 壁紙位置(原寸)の表示名
        /// </summary>
        public const string conWallpaperPos_Ori = "Original";
        /// <summary>
        /// 壁紙位置(並べ替え)の表示名
        /// </summary>
        public const string conWallpaperPos_Arr = "Arrange";
        /// <summary>
        /// 壁紙位置(拡大)の表示名
        /// </summary>
        public const string conWallpaperPos_Fit = "Fit";                                                
        /// <summary>
        /// アップデートボタン表示名
        /// </summary>
        public const string CAP_UPDATE_BTN = "更新";                                                    

        #endregion

        /*
            メッセージ定数
        */
        #region Message Const

        /// <summary>
        /// エラータイトル
        /// </summary>
        public const string MES_TITLE_ERROR = "Error Message";
        /// <summary>
        /// インフォメーションタイトル
        /// </summary>
        public const string MES_TITLE_INFO = "Infomation Message";
        /// <summary>
        /// クエッションタイトル
        /// </summary>
        public const string MES_TITLE_QUESTION = "Question";
        /// <summary>
        /// ファイル名変更タイトル
        /// </summary>
        public const string MES_TITLE_CHANGEFILENAME = "Change FileName";
        /// <summary>
        /// 壁紙が削除されているとき
        /// </summary>
        public const string MES_FILE_DELETED = "設定している壁紙は既に削除されています。";
        /// <summary>
        /// ファイ名を入力
        /// </summary>
        public const string MES_FILE_NEWNAME = "新しいファイル名を入力してください。拡張子は不要です";
        /// <summary>
        /// ファイルがすでに存在するとき
        /// </summary>
        public const string MES_FILE_EXISTS = "同じ名前のファイルは既に存在します";
        /// <summary>
        /// 指定したフォルダに画像が無いとき
        /// </summary>
        public const string MES_GRAPHIC_NOTHING = "指定したフォルダに画像がありません。\n以下の指定されたフォルダをご確認ください。\n\n";
        /// <summary>
        /// デリートスキップを実行したとき
        /// </summary>
        public const string MES_DELETESKIP = "デリートスキップを実行します";


        #endregion

        /*
            列挙型
         */
        /// <summary>
        /// チェンジモード
        /// </summary>
        public enum enuChgMode
        {
            /// <summary>
            /// ランダマイズ
            /// </summary>
            RANDOM = 0,
            /// <summary>
            /// ノーマル
            /// </summary>
            NORMAL,
            /// <summary>
            /// なし
            /// </summary>
            NONE
        }

        /// <summary>
        /// グリッド
        /// </summary>
        public enum enuGrid
        {
            /// <summary>
            /// ファイル名
            /// </summary>
            FILENAME = 0,
            /// <summary>
            /// 壁紙サイズ
            /// </summary>
            WALLPAPERSIZE,
            /// <summary>
            /// 表示位置
            /// </summary>
            VIEWPOS,
            /// <summary>
            /// 壁紙色
            /// </summary>
            COLOR,
            /// <summary>
            /// 壁紙色(RGB)
            /// </summary>
            RGBCOLOR
        }

        
        /// <summary>
        /// 壁紙位置
        /// </summary>
        public enum enuViewPos
        {
            /// <summary>
            /// 原寸
            /// </summary>
            ORIGINAL,
            /// <summary>
            /// 並べ替え
            /// </summary>
            ARRANGE,
            /// <summary>
            /// 拡大
            /// </summary>
            FIT
        }
        

        /// <summary>
        /// TileWallpaper
        /// </summary>
        public enum enuTileWallpaper
        {
            /// <summary>
            /// center
            /// </summary>
            CENTER =0,
            /// <summary>
            /// fill
            /// </summary>
            FILL =1
        }
        

        /// <summary>
        /// WallpaperStyle
        /// </summary>
        public enum enuWallpaperStyle
        {
            /// <summary>
            /// none
            /// </summary>
            NONE=0,
            /// <summary>
            /// stretch
            /// </summary>
            STRETCH = 2 
        }



        /*
            プロパティ
        */
        #region Property

        /// <summary>
        /// MainForm
        /// </summary>
        static public MainForm frmMain
        {
            get{ return _frmMain;}
        }

        /// <summary>
        /// アプリケーションパス
        /// </summary>
        static public string ApplicationPath
        {
            get{ return _strApplicationPath; }
            set{ _strApplicationPath = value; }
        }

        /// <summary>
        /// コマンドライン文字列
        /// </summary>
        static public string CommandLine
        {
            get { return _strCommandLine; }
            set { _strCommandLine = value; }
        }

        #endregion


        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        public static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // アプリケーションパス格納
            ApplicationPath = Application.StartupPath;

            // 設定オブジェクト生成
            set = new BgwcSetting();

            // 設定をロード
            set.LoadSetting();

            // コマンドライン取得
            if (args.Length > 0)
            {
                CommandLine = args[0];
            }

            // BGWChangerGraphicのパスを設定
            gBGWChangerGraphicPath = ApplicationPath + "\\BGWChangerGraphic.bmp";
            // BGWChanger.exeのパスを設定
            gExeFilePath = ApplicationPath + "\\" + conExecFileName;

            _frmMain = new MainForm();
            Application.Run(_frmMain);
            
            /*
                終了されたとき
            */

            // 設定を保存
            set.AlwaysOnTop = _frmMain.frmOpt.AlwaysOnTop;
            set.StartUp = _frmMain.frmOpt.StartUp;
            set.ChangeMode = _frmMain.frmOpt.ChangeMode;
            set.SaveAllSetting();
            
        }

        /*
            パブリックメソッド
        */
        /// <summary>
        /// メッセージボックスを表示する
        /// </summary>
        /// <param name="strMessage">メッセージ</param>
        /// <param name="strTitle">タイトル</param>
        /// <param name="msgbtn">ボタンの種類</param>
        /// <param name="msgico">アイコン</param>
        static public void ShowMessage(string strMessage,string strTitle,MessageBoxButtons msgbtn,MessageBoxIcon msgico)
        {
            // 常に手前に表示を解除
            Foremost(false);
            // メッセージを出力
            MessageBox.Show(strMessage,strTitle,msgbtn,msgico);
            // 常に手前に表示を設定している場合は戻す
            if (frmMain.frmOpt.AlwaysOnTop == true)
            {
                Foremost(true);
            }
        }

        /// <summary>
        /// 結果を受け取れるダイアログを表示する
        /// </summary>
        /// <param name="strMessage">メッセージ</param>
        /// <param name="strTitle">タイトル</param>
        /// <param name="msgbtn">ボタンの種類</param>
        /// <param name="msgico">アイコン</param>
        /// <returns></returns>
        static public DialogResult ShowMessageRes(string strMessage, string strTitle, MessageBoxButtons msgbtn, MessageBoxIcon msgico)
        {
            // 常に手前に表示を解除
            Foremost(false);
            // メッセージを出力
            DialogResult dr = MessageBox.Show(strMessage, strTitle, msgbtn, msgico);
            // 常に手前に表示を設定している場合は戻す
            if (frmMain.frmOpt.AlwaysOnTop == true)
            {
                Foremost(true);
            }
            return dr;
        }

        /// <summary>
        /// 常に手前に表示する
        /// </summary>
        /// <param name="Mode">true:設定 false:解除</param>
        static public void Foremost(bool Mode)
        {
            if (Mode)
            {
                frmMain.TopMost = true;
                frmMain.frmOpt.TopMost = true;
            }
            else
            {
                frmMain.TopMost = false;
                frmMain.frmOpt.TopMost = false;
            }
        }

        
    }
}