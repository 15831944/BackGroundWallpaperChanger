using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using FINALSTREAM.BackGroundWallpaperChanger.Library;
using FINALSTREAM.Commons.Parser;
using FINALSTREAM.Commons.Utils;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    /// <summary>
    /// BgwcSettingクラス
    /// </summary>
    public class BgwcSetting
    {

        /*
            メンバ変数
        */
        # region Member


        string _strWallpaperFolder;
        string _strWallpaper;
        bool _bolAlwaysOnTop;
        bool _bolStartUp;
        int _intChangeMode;


        # endregion


        /*
            プロパティ
        */
        # region Property


        /// <summary>
        /// WallpaperFolderパス
        /// </summary>
        public string WallpaperFolder
        {
            set { this._strWallpaperFolder = value; }
            get { return this._strWallpaperFolder; }
        }

        /// <summary>
        /// 壁紙のパス
        /// </summary>
        public string Wallpaper
        {
            set { this._strWallpaper = value; }
            get { return this._strWallpaper; }
        }

        /// <summary>
        /// 常に手前に表示
        /// </summary>
        public bool AlwaysOnTop
        {
            set { this._bolAlwaysOnTop = value; }
            get { return _bolAlwaysOnTop; }
        }

        /// <summary>
        /// Windows起動時に実行
        /// </summary>
        public bool StartUp
        {
            set { this._bolStartUp = value; }
            get { return this._bolStartUp; }
        }

        /// <summary>
        /// 壁紙チェンジモード
        /// </summary>
        public int ChangeMode
        {
            set { this._intChangeMode = value; }
            get { return this._intChangeMode; }
        }
        #endregion


        /*
            パブリックメソッド
        */
        #region Public Method

        /// <summary>
        /// 設定をXMLに保存する
        /// </summary>
        /// <returns></returns>
        public bool SaveAllSetting()
        {

            XmlSerializer xmlSerializer = new XmlSerializer();
            xmlSerializer.save(this, typeof(BgwcSetting), MainProgram.ApplicationPath + "\\BGWCSetting.xml");

            SaveWallpaperSetting();

            return true;
        }

        /// <summary>
        /// グリッドの情報をViewSetFileに保存する
        /// </summary>
        /// <returns></returns>
        public bool SaveWallpaperSetting()
        {
            // ViewSettingファイルへ出力
            ViewSetting vset = new ViewSetting();
            int i = 1;
            while (i <= MainProgram.frmMain.Grid.Rows.Count - 1)
            {
                if ((string)MainProgram.frmMain.Grid[i, (int)MainProgram.enuGrid.VIEWPOS].Value != ""
                    || (string)MainProgram.frmMain.Grid[i, (int)MainProgram.enuGrid.COLOR].Value != "None")
                {
                    GVWI gvwi = new GVWI();

                    gvwi.Key = (string)MainProgram.frmMain.Grid[i, (int)MainProgram.enuGrid.FILENAME].Value;
                    gvwi.ViewPos = (string)MainProgram.frmMain.Grid[i, (int)MainProgram.enuGrid.VIEWPOS].Value;
                    gvwi.Color = (string)MainProgram.frmMain.Grid[i, (int)MainProgram.enuGrid.RGBCOLOR].Value;
                    vset.List.Add(gvwi);
                    gvwi = null;
                }
                i++;
            }
            vset.WriteViewSetting(Application.StartupPath + "\\" + MainProgram.conViewSetFileName);

            return true;
        }


        /// <summary>
        /// XMLから設定をロードする
        /// </summary>
        /// <returns>成功したか</returns>
        public bool LoadSetting()
        {

            // アプリケーションの設定を読み込む
            if (File.Exists(MainProgram.ApplicationPath + "\\BGWCSetting.xml"))
            {
                BgwcSetting bgwcset = new BgwcSetting();
                XmlSerializer xmlSerializer = new XmlSerializer();
                bgwcset = (BgwcSetting)xmlSerializer.load(typeof(BgwcSetting), MainProgram.ApplicationPath + "\\BGWCSetting.xml");

                // WallpaperFolder
                _strWallpaperFolder = bgwcset.WallpaperFolder;
                // Wallpaper
                _strWallpaper = bgwcset.Wallpaper;
                // AlwaysOnTop
                _bolAlwaysOnTop = bgwcset.AlwaysOnTop;
                // StartUp
                _bolStartUp = bgwcset.StartUp;
                // ChangeMode
                _intChangeMode = bgwcset.ChangeMode;
            }

            // 壁紙フォルダが設定されない場合はWallpaperフォルダを指定する
            if (_strWallpaperFolder == "" || _strWallpaperFolder == null)
            {
                _strWallpaperFolder = MainProgram.ApplicationPath + "\\Wallpaper";
            }
            return true;
        }

        #endregion


    }
}
