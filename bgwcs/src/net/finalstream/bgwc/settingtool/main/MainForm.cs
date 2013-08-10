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
    /// メインフォーム
    /// </summary>
    public partial class MainForm : Form
    {

        /*
            Privateメンバ 
        */
        #region PrivateMember

        private OptionForm _frmOpt;
        private MainFunction _MainFunc;
        private int _PreviewBaseWidth;  // Previewの元Width
        private int _PreviewBaseHeight; // Previewの元Height
        private List<string> _lstKeyIndexList;   // Key検索用リスト
        private MouseButtons _flgMouseButton = System.Windows.Forms.MouseButtons.Left; // 左ダブルクリック検知用フラグ
        


        #endregion

        /*
            プロパティ
        */
        #region Property
        /// <summary>
        /// MainForm用関数
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
        /// グリッド
        /// </summary>
        public SourceGrid.Grid Grid
        {
            get { return this.griList; }
        }

        /// <summary>
        /// 現在設定されている壁紙パス
        /// </summary>
        public string NowWallpaper
        {
            set { this.txtNowWallpaper.Text = value; }
            get { return this.txtNowWallpaper.Text; }
        }

        /// <summary>
        /// モニタの解像度
        /// </summary>
        public string ScreenSize
        {
            set { this.lblScreenSize.Text = value + " pixel"; }
            get { return this.lblScreenSize.Text; }
        }

        /// <summary>
        /// 壁紙の大きさ
        /// </summary>
        public string WallpaperSize
        {
            set { this.lblWallpaperSize.Text = value + " pixel"; }
            get { return this.lblWallpaperSize.Text; }
        }

        /// <summary>
        /// プレビューしている壁紙の大きさ
        /// </summary>
        public string PreviewSize
        {
            set { this.lblPreviewSize.Text =value + " pixel"; }
            get { return this.lblPreviewSize.Text; }
        }

        /// <summary>
        /// プレビュー画像
        /// </summary>
        public PictureBox PreviewPicture
        {
            get { return this.picPreview; }
        }

        /// <summary>
        /// プレビュー画像の設定
        /// </summary>
        public string SetPreviewWallpaper
        {
            set
            {
                SettingPreviewWallpaper(value);
            }
        }

        /// <summary>
        /// BGWC壁紙フォルダ
        /// </summary>
        public string WallpaperFolder
        {
            set { this.txtWallpaperFolder.Text = value; }
            get { return this.txtWallpaperFolder.Text; }
        }

        /// <summary>
        /// プレビュー元Width
        /// </summary>
        public int PreviewBaseWidth
        {
            set { this._PreviewBaseWidth = value; }
            get { return this._PreviewBaseWidth; }
        }

        /// <summary>
        /// プレビュー元Height
        /// </summary>
        public int PreviewBaseHeight
        {
            set { this._PreviewBaseHeight = value; }
            get { return this._PreviewBaseHeight; }
        }

        /// <summary>
        /// 左クリックイベント検知用フラグ
        /// </summary>
        public MouseButtons FlgMouseButton
        {
            set { this._flgMouseButton = value; }
            get { return this._flgMouseButton; }
        }

        /// <summary>
        /// プレビューファイル名
        /// </summary>
        public string PreviewFileName
        {
            set { this.txtPreviewFile.Text = value; }
            get { return this.txtPreviewFile.Text; }
        }

        #endregion

        /*
            イベント
        */
        #region Event

        /// <summary>
        /// MainFormをロードしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            GeneralFunction gfunc = new GeneralFunction();

            // コマンドラインで"delskip"をわたされたら
            if (MainProgram.CommandLine == "/delskip")
            {
                MainFunc.DeleteSkip(this.NowWallpaper, true);
                Environment.Exit(0);
            }
            else if (MainProgram.CommandLine == "/nowall")
            {
                // "/nowall"がわたされたら
                // レジストリキーをオープン
                RegistryKey regkey = Registry.CurrentUser.OpenSubKey(MainProgram.conWallpaperSubKey);
                regkey.SetValue("Wallpaper", "");
                // 壁紙を無効に設定
                regkey.Close();
                Environment.Exit(0);
            }
            else if (MainProgram.CommandLine.Length > 4 && MainProgram.CommandLine.Substring(0, 4) == "/add")
            {
                // 壁紙がドロップされたとき
                // Me.Hide
                // 前のaddの文字をとって壁紙登録
                AddWallpaper(MainProgram.CommandLine.Substring(4));
                // 登録後終了する
                Environment.Exit(0);
            }

            // 設定されている壁紙を取得(Regist)
            // もしBGWChangerと違う壁紙が設定されていた場合
            if (MainFunc.RegWallpaperPath != MainProgram.gBGWChangerGraphicPath)
            {
                // 壁紙情報をそっちに入れ替える
                MainFunc.NowWallpaperUpdate(MainFunc.RegWallpaperPath, MainFunc.RegWallpaperPath);
            }
            else
            {
                // BGWChangerGraphic存在チェック
                if (File.Exists(MainProgram.gBGWChangerGraphicPath))
                {
                    // 設定されている壁紙の元ファイルパスを読み込む
                    MainFunc.NowWallpaperUpdate(MainProgram.set.Wallpaper, MainProgram.gBGWChangerGraphicPath);
                }
            }

            if (MainFunc.RegWallpaperPath == "")
            {
                MainProgram.frmMain.frmOpt.WallpaperNothing = true;
            }

            // 壁紙リスト生成
            ListCreate();

            // 壁紙チェンジ
            if (MainProgram.CommandLine == "/change" && griList.Rows.Count > 0)
            {
                MainFunc.WallpaperChange();
                gfunc.WallpaperChanged();
                return;
            }

            // プレビューに現在の壁紙の名前とサイズをロード
            this.PreviewFileName = Path.GetFileName(txtNowWallpaper.Text);
            this.lblPreviewSize.Text = this.lblWallpaperSize.Text;

            MainProgram.gWallpaperStyleWriteFlag = true;

            // すべてロードしてから表示
            this.Visible = true;

        }

        /// <summary>
        /// MainFormを閉じたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 設定オブジェクトに格納
            MainProgram.set.WallpaperFolder = this.txtWallpaperFolder.Text;
            MainProgram.set.Wallpaper = this.txtNowWallpaper.Text;
        }

        /// <summary>
        /// MainFormを動かしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Move(object sender, EventArgs e)
        {
            // 位置、サイズをMainFormにあわせる
            frmOpt.Top = MainProgram.frmMain.Top;
            frmOpt.Left = MainProgram.frmMain.Left + MainProgram.frmMain.Width;
            frmOpt.Height = MainProgram.frmMain.Height;
        }

        /// <summary>
        /// MainFormがアクティブになったとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Activated(object sender, EventArgs e)
        {
            // OptionForm非表示
            frmOpt.Visible = false;
            // 設定ボタン有効
            btnSetting.Enabled = true;
        }


        /// <summary>
        /// BGWC壁紙フォルダ選択ボタンクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFolder_Click(object sender, EventArgs e)
        {
            //FolderBrowserDialogクラスのインスタンスを作成
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            //上部に表示する説明テキストを指定する
            fbd.Description = "壁紙があるフォルダを指定してください。";
            //ルートフォルダを指定する
            //デフォルトでDesktop
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            //最初に選択するフォルダを指定する
            //RootFolder以下にあるフォルダである必要がある
            fbd.SelectedPath = this.txtWallpaperFolder.Text;
            //ユーザーが新しいフォルダを作成できるようにする
            fbd.ShowNewFolderButton = false;

            //ダイアログを表示する
            if (fbd.ShowDialog(this) == DialogResult.OK)
            {
                //選択されたフォルダを表示する
                this.txtWallpaperFolder.Text = fbd.SelectedPath;
                ListCreate();
            }

        }


        /// <summary>
        /// 設定ボタンクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {

            if (frmOpt.Visible == false)
            {
                // OptionFormが表示されてないとき

                // 位置、サイズをMainFormにあわせる
                MainForm_Move(sender, e);
                btnSetting.Enabled = false;
                // OptionForm表示
                frmOpt.Show();
            }
            else
            {
                // OptionFormが表示されているととき

                frmOpt.Hide();
            }
        }

        /// <summary>
        /// 適用ボタンをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (griList.Rows.Count > 1)
            {
                // グリッドをアクティブにする
                this.griList.Focus();
                // 壁紙に設定 
                MainProgram.frmMain.MainFunc.BGWChanerWallpaperSetting((string)griList[griList.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.FILENAME].Value);
                // フォームをアクティブにする
                MainProgram.frmMain.Activate();
            }
        }

        /// <summary>
        /// BGWC壁紙フォルダアイコンをダブルクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picFolder_DoubleClick(object sender, EventArgs e)
        {
            if (Directory.Exists(txtWallpaperFolder.Text))
            {
                // BGWC壁紙フォルダをエクスプローラで表示
                System.Diagnostics.Process.Start("explorer.exe", "/n," + txtWallpaperFolder.Text);
            }
        }

        /// <summary>
        /// ゴミ箱をダブルクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picTrashBox_DoubleClick(object sender, EventArgs e)
        {
            if (Directory.Exists(Application.StartupPath + "\\TrashBox"))
            {
                // ゴミ箱(フォルダ)をエクスプローラで表示
                System.Diagnostics.Process.Start("explorer.exe", "/n," + Application.StartupPath + "\\TrashBox");
            }
        }


        /// <summary>
        /// デリートスキップボタンをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelSkip_Click(object sender, EventArgs e)
        {
            if (MainProgram.ShowMessageRes(MainProgram.MES_DELETESKIP, MainProgram.MES_TITLE_QUESTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // デリートスキップ実行
                MainFunc.DeleteSkip(this.NowWallpaper, true);
                // グリッドを再描画
                ListCreate();
            }
        }

        /// <summary>
        /// 現在の壁紙にデータが入力されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNowWallpaper_Enter(object sender, EventArgs e)
        {
            this.txtNowWallpaper.Select(this.txtNowWallpaper.Text.Length, 0);
        }

        /// <summary>
        /// Previewファイルにデータが入力されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPreviewFile_Enter(object sender, EventArgs e)
        {
            this.txtPreviewFile.Select(this.txtPreviewFile.Text.Length, 0);
        }

        /// <summary>
        /// チェンジボタンをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChange_Click(object sender, EventArgs e)
        {
            // 壁紙チェンジ
            MainProgram.frmMain.MainFunc.WallpaperChange();
            // 現在の壁紙を選択状態にする
            ListWallpaperSelected(GetWallpaperKey(NowWallpaper));
        }


        /// <summary>
        /// 更新ボタンをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            // リフレッシュ
            AllRefresh();

        }

        /// <summary>
        /// Preview画像をクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPreview_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // 右クリック

                if (griList.RowsCount > 1)
                {
                    Bitmap bmp = new Bitmap(this.picPreview.Image);
                    //フォーカスを当てないとActicvePositionで落ちるので
                    MainProgram.frmMain.griList.Focus();
                    // 壁紙色をグリッドにセット
                    MainFunc.WallpaperColorSetting(true, bmp.GetPixel(e.X, e.Y));
                }
            }
        }

        /// <summary>
        /// URLにマウスが移動したとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSite_MouseMove(object sender, MouseEventArgs e)
        {
            this.lblSite.ForeColor = Color.Gold;
        }

        /// <summary>
        /// URLからマウスが離れたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblTitleBack_MouseMove(object sender, MouseEventArgs e)
        {
            this.lblSite.ForeColor = Color.Orange;
        }

        /// <summary>
        /// URLをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblSite_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.finalstream.net/");
        }

        /// <summary>
        /// グリッドにＤ＆Ｄされたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void griList_DragDrop(object sender, DragEventArgs e)
        {
            // ドラッグ＆ドロップされたファイル
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string getfile in files)
            {
                if (this.CheckFileExt(Path.GetExtension(getfile)))
                {
                    AddWallpaper(getfile);
                }
            }

            // リスト更新
            AllRefresh();

        }

        /// <summary>
        /// グリッにＤ＆Ｄされたとき（Ｄ＆Ｄ判定）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void griList_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {

                // ドラッグ中のファイルやディレクトリの取得
                string[] drags = (string[])e.Data.GetData(DataFormats.FileDrop);

                foreach (string d in drags)
                {
                    if (!System.IO.File.Exists(d))
                    {
                        // ファイル以外であればイベント・ハンドラを抜ける
                        return;
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }
        }


        #endregion



        /*
            SourceGrid用イベント 
        */
        #region SGEvent

        // 選択行変更時
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
            パブリックメソッド 
        */
        #region PublicMethod

        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            // MainFunc生成
            this._MainFunc = new MainFunction();

            // OptionForm生成
            this._frmOpt = new OptionForm();

            // グリッドの初期設定
            InitGrid();

            // 設定をロード
            this.txtWallpaperFolder.Text = MainProgram.set.WallpaperFolder;

        }

        /// <summary>
        /// キー検索用Indexから指定されたキーがあるか検索する
        /// </summary>
        /// <param name="strKey">キー(BGWC壁紙フォルダからのパス)</param>
        /// <returns>Index番号(見つからなければ-1)</returns>
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
        /// キー検索用Indexを更新する
        /// </summary>
        /// <param name="strBeforeKey">変更前のキー(BGWC壁紙フォルダからのパス)</param>
        /// <param name="strAfterKey">変更後のキー(BGWC壁紙フォルダからのパス)</param>
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
            プライベートメソッド
        */
        #region PrivateMethod

        /// <summary>
        /// ファイル拡張子で壁紙対象か判定する
        /// </summary>
        /// <param name="strFileExt">ファイル拡張子</param>
        /// <returns>壁紙対象か</returns>
        private bool CheckFileExt(string strFileExt)
        {
            // 拡張子を小文字に変換
            strFileExt = strFileExt.ToLower();
            // 拡張子チェック
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
        /// 壁紙を登録する
        /// </summary>
        /// <param name="strAddFilePath">登録する壁紙のパス</param>
        private void AddWallpaper(string strAddFilePath)
        {

            // 常に手前を解除
            MainProgram.Foremost(false);

            // コピー先パス
            string strCopyFilePath = this.txtWallpaperFolder.Text + "\\" + Path.GetFileName(strAddFilePath);

            // コピーファイルが存在するか確認
            if (File.Exists(strCopyFilePath))
            {
                // もしすでに存在していたら

                //拡張子取得
                string strExt = Path.GetExtension(strAddFilePath);
                //違う名前を入力してもらう
                string strNewFileName
                    = Microsoft.VisualBasic.Interaction.InputBox(MainProgram.MES_FILE_NEWNAME, MainProgram.MES_FILE_EXISTS, "Wallpaper" + DateTime.Now.ToString("yyyymmdd") + DateTime.Now.ToString("hhmmss"), -1, -1);
                if (strNewFileName == "")
                {
                    // キャンセルされたら登録せずに終了 
                    return;
                }
                // コピー先パスを更新
                strCopyFilePath = this.WallpaperFolder + "\\" + strNewFileName + "." + strExt;
            }

            // 壁紙を登録(ファイル移動)
            try
            {
                File.Move(strAddFilePath, strCopyFilePath);
            }
            catch (Exception ex)
            {
                MainProgram.ShowMessage(ex.Message, MainProgram.MES_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // 登録後壁紙設定するか確認する。
            DialogResult ret = MessageBox.Show(Path.GetFileName(strCopyFilePath) + "を壁紙に設定しますか？", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (ret == DialogResult.Yes)
            {
                // OKが選択されたら

                // リストを再構築
                this.ListCreate();
                // 追加した壁紙を選択状態にする
                ListWallpaperSelected(GetWallpaperKey(strCopyFilePath));
                // 追加した壁紙を現在の壁紙にする
                MainFunc.NowWallpaperUpdate(strCopyFilePath, strCopyFilePath);

                // 壁紙にする
                MainProgram.frmMain.btnApply_Click(new Object(), new EventArgs());
            }

            // 常に手前だったら戻す
            if (MainProgram.frmMain.frmOpt.AlwaysOnTop == true)
            {
                MainProgram.Foremost(true);
            }
        }

        /// <summary>
        /// 指定した画像ファイルをPreviewに表示する
        /// </summary>
        /// <param name="strWallpaper">Previewに表示する画像ファイルパス</param>
        private void SettingPreviewWallpaper(string strWallpaper)
        {
            if (File.Exists(strWallpaper))
            {
                using (FileStream fs = File.OpenRead(strWallpaper))
                using (Image img = Image.FromStream(fs, false, false))
                {
                    // サムネイル作成前に元のサイズを保存
                    this.PreviewBaseWidth = img.Width;
                    this.PreviewBaseHeight = img.Height;

                    // 縦横比固定倍率
                    double zoom = (double)picPreview.Width / (double)img.Width;

                    // Previewに表示
                    picPreview.Image = new Bitmap(img, picPreview.Width, (int)(zoom * img.Height));

                }
            }
        }


        /// <summary>
        /// 壁紙ファイルパスからBGWCKeyを取得する
        /// </summary>
        /// <param name="strWallpaperPath">壁紙ファイルパス</param>
        /// <returns>取得したBGWCKey</returns>
        private string GetWallpaperKey(string strWallpaperPath)
        {
            return strWallpaperPath.Replace(WallpaperFolder + "\\", "");
        }

        /// <summary>
        /// 指定したBGWCKeyの壁紙を選択状態にする
        /// </summary>
        /// <param name="strKey">BGWCKey</param>
        private void ListWallpaperSelected(string strKey)
        {
            // BGWCKeyIndexを検索
            int idx = this.GetKeyIndex(strKey);
            if (idx == -1)
            {
                // BGWCKeyが見つからなければ1行目を選択
                griList.Selection.SelectRow(1, true);
            }
            else
            {
                // BGWCKeyが見つかればその行を選択
                griList.Selection.SelectRow(idx, true);
                griList.Selection.FocusRow(idx);
            }
        }

        /// <summary>
        /// すべてを更新する
        /// </summary>
        private void AllRefresh(){

            // 設定オブジェクト生成
            BgwcSetting set = new BgwcSetting();

            // 更新する前に現在の個別壁紙設定を保存
            set.SaveWallpaperSetting();
            // 現在の壁紙のパス更新
            MainFunc.NowWallpaperUpdate(this.NowWallpaper, MainFunc.RegWallpaperPath);
            // 壁紙リストを再構築
            ListCreate();

        }


        #endregion



        /*
            SourceGrid用プライベートメソッド 
        */
        #region SGPrivateMethod

        /// <summary>
        /// グリッドを初期化する
        /// </summary>
        private void InitGrid()
        {
            // グリッドスタイル
            griList.BorderStyle = BorderStyle.Fixed3D;

            // グリッドの列数
            griList.ColumnsCount = 5;
            griList.FixedRows = 1;
   
            // ヘッダ生成
            griList.Rows.Insert(0);
            // ファイルパス
            griList[0, (int)MainProgram.enuGrid.FILENAME] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.FileName);
            griList[0, (int)MainProgram.enuGrid.FILENAME].Column.Width = 200;
            // 壁紙サイズ
            griList[0, (int)MainProgram.enuGrid.WALLPAPERSIZE] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.Size);
            griList[0, (int)MainProgram.enuGrid.WALLPAPERSIZE].Column.Width = 90;
            // 壁紙位置
            griList[0, (int)MainProgram.enuGrid.VIEWPOS] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.DisplayPosition);
            griList[0, (int)MainProgram.enuGrid.VIEWPOS].Column.Width = 97;
            // 壁紙色
            griList[0, (int)MainProgram.enuGrid.COLOR] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.WallpaperColor);
            griList[0, (int)MainProgram.enuGrid.COLOR].Column.Width = 50;
            // 壁紙色(RGB値) ※Hidden
            griList[0, (int)MainProgram.enuGrid.RGBCOLOR] = new SourceGrid.Cells.ColumnHeader(Properties.Resources.RGBColor);
            griList[0, (int)MainProgram.enuGrid.RGBCOLOR].Column.Width = 0;

            //選択用イベントコントローラ追加
            griList.Selection.SelectionChanged += new SourceGrid.RangeRegionChangedEventHandler(Selection_SelectionChanged);
        }


        /// <summary>
        /// グリッドにリストを生成する
        /// </summary>
        private void ListCreate()
        {
            // グリッドクリア
            this.GridClear();

            // BGWC壁紙フォルダが存在するときのみリスト作成
            if (Directory.Exists(this.txtWallpaperFolder.Text))
            {
                // BGWC壁紙フォルダ以下にあるファイルを取得し配列に格納
                string[] strAllFiles = Directory.GetFiles(this.txtWallpaperFolder.Text,"*",SearchOption.AllDirectories);

                // 壁紙に関するファイルだけ抽出
                List<string> lstGraphicFile = new List<string>();   // 壁紙用ファイルリスト初期化
                lstGraphicFile.Capacity = strAllFiles.Length;
                foreach (string strFile in strAllFiles)
                {
                    if(this.CheckFileExt(Path.GetExtension(strFile))){
                        lstGraphicFile.Add(strFile);   
                    }
                }

                // リストを作成

                // SourceGrid用イベント初期化
                CellDoubleClickEvent doubleclickController = new CellDoubleClickEvent();
                FileNameCellClickEvent filenameClickController = new FileNameCellClickEvent();
                //FileNameEditStartEvent filenameeditstartController = new FileNameEditStartEvent();
                WallpaperColorCellClickEvent wallpaperColorClickController = new WallpaperColorCellClickEvent();
                KeyDownEvent keydownController = new KeyDownEvent();

                // 壁紙位置リスト生成
                SourceGrid.Cells.Editors.ComboBox cbEditor = new SourceGrid.Cells.Editors.ComboBox(typeof(string));
                cbEditor.StandardValues = new string[] { "", MainProgram.conWallpaperPos_Ori, MainProgram.conWallpaperPos_Arr, MainProgram.conWallpaperPos_Fit };
                cbEditor.EditableMode = SourceGrid.EditableMode.Focus | SourceGrid.EditableMode.SingleClick | SourceGrid.EditableMode.AnyKey;
                cbEditor.Control.DropDownStyle = ComboBoxStyle.DropDownList;

                // 検索用IndexList初期化
                _lstKeyIndexList = new List<string>();
                _lstKeyIndexList.Capacity = 1000;
                // ヘッダの分を追加しておく
                _lstKeyIndexList.Add("");

                // 壁紙用ファイルリストから１つずつとりだす
                foreach (string strFile in lstGraphicFile)
                {
                    // 新規行番号を取得
                    int i = griList.RowsCount;
                    // 新規行を追加
                    griList.Rows.Insert(i);

                    // ファイル名用セル生成
                    griList[i,(int)MainProgram.enuGrid.FILENAME] = new SourceGrid.Cells.Cell(strFile.Replace(this.txtWallpaperFolder.Text + "\\",""),typeof(string));
                    griList[i, (int)MainProgram.enuGrid.FILENAME].Editor.EditableMode = SourceGrid.EditableMode.None;
                    griList[i, (int)MainProgram.enuGrid.FILENAME].AddController(doubleclickController);
                    griList[i, (int)MainProgram.enuGrid.FILENAME].AddController(filenameClickController);
                    //griList[i, (int)MainProgram.enuGrid.FILENAME].AddController(filenameeditstartController);
                    griList[i, (int)MainProgram.enuGrid.FILENAME].AddController(keydownController);
                    // 検索用Indexリストに追加
                    _lstKeyIndexList.Add(strFile.Replace(WallpaperFolder + "\\", ""));
                    // 追加するファイルパス格納
                    string strAddFilePath = txtWallpaperFolder.Text + "\\" + griList[i, (int)MainProgram.enuGrid.FILENAME].Value;
                    
                    // 壁紙サイズ用セル生成
                    //if (this.CheckFileExt((Path.GetExtension(strFile)))){
                    griList[i, (int)MainProgram.enuGrid.WALLPAPERSIZE] = new SourceGrid.Cells.Cell(MainFunc.GetWallpaperSize(strAddFilePath));
                    //}

                    // 壁紙位置用セル生成
                    griList[i, (int)MainProgram.enuGrid.VIEWPOS] = new SourceGrid.Cells.Cell("", cbEditor);
                    griList[i, (int)MainProgram.enuGrid.VIEWPOS].View = SourceGrid.Cells.Views.ComboBox.Default;

                    // 壁紙色用セル生成
                    griList[i, (int)MainProgram.enuGrid.COLOR] = new SourceGrid.Cells.Cell("None", typeof(string));
                    griList[i, (int)MainProgram.enuGrid.COLOR].AddController(wallpaperColorClickController);
                    griList[i, (int)MainProgram.enuGrid.COLOR].Editor.EditableMode = SourceGrid.EditableMode.None;
                    
                    // 壁紙色(RGB)用セル生成
                    griList[i, (int)MainProgram.enuGrid.RGBCOLOR] = new SourceGrid.Cells.Cell("", typeof(string));
                }
                // 検索用Indexリストのキャパシティを調整
                _lstKeyIndexList.TrimExcess();


                // 個別壁紙表示設定が存在する場合(ViewSettingファイルが存在)
                if (File.Exists(MainProgram.ApplicationPath + "\\" + MainProgram.conViewSetFileName))
                {
                    // 個別壁紙表示設定を取得 (DLL)
                    ViewSetting vset = new ViewSetting();

                    // ViewSettingファイルからロード
                    vset.ReadViewSetting(MainProgram.ApplicationPath + "\\" + MainProgram.conViewSetFileName);

                    // ViewSettingListから１つずつ取り出す
                    foreach (GVWI gvwi in vset.List)
                    {

                        // BGWCKeyをもつIndexを検索
                        int idx = GetKeyIndex(gvwi.Key);

                        // 見つかったとき
                        if (idx > 0)
                        {
                            // ヘッダの分プラス
                            //idx++;

                            // 壁紙位置を更新
                            griList[idx, (int)MainProgram.enuGrid.VIEWPOS] = new SourceGrid.Cells.Cell(gvwi.ViewPos, cbEditor);
                            griList[idx, (int)MainProgram.enuGrid.VIEWPOS].View = SourceGrid.Cells.Views.ComboBox.Default;

                            if (gvwi.Color != "")
                            {
                                // 壁紙色を更新
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

                // 現在の壁紙を選択状態にする
                ListWallpaperSelected(GetWallpaperKey(NowWallpaper));
                
            }
        }

        /// <summary>
        /// グリッドをクリアする
        /// </summary>
        private void GridClear()
        {
            if (griList.RowsCount > 1)
            {
                // ヘッダを残してあとは全行削除
                griList.Rows.RemoveRange(1, griList.RowsCount - 1);
            }
        }

        #endregion


        /*
            SourceGrid用イベントクラス 
        */
        #region SGEventClass

        /// <summary>
        /// セルダブルクリックイベントクラス
        /// </summary>
        private class CellDoubleClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            public override void OnDoubleClick(SourceGrid.CellContext sender, EventArgs e)
            {
                base.OnDoubleClick(sender, e);
                // 壁紙に設定
                if (MainProgram.frmMain.FlgMouseButton == MouseButtons.Left)
                {
                    MainProgram.frmMain.btnApply_Click(sender, e);
                }
            }
        }

        /// <summary>
        /// ファイル名セルクリックイベントクラス
        /// </summary>
        private class FileNameCellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);

                // クリックされたボタンを記憶
                MainProgram.frmMain.FlgMouseButton = e.Button;
            }
        }


        /// <summary>
        /// キーダウンイベントクラス
        /// </summary>
        private class KeyDownEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            bool bolEnterKeyFlg = false; // エンターキーフラグ(Enterキーで壁紙適用できるようにするため)

            /// <summary>
            /// グリッド上でキーが押下されたとき
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnKeyDown(SourceGrid.CellContext sender, KeyEventArgs e)
            {
                base.OnKeyDown(sender, e);
                if (e.KeyCode == Keys.Enter && bolEnterKeyFlg == false)
                {
                    // Enterキー

                    // 壁紙に設定
                    MainProgram.frmMain.btnApply_Click(sender, e);
                    // グリッドをアクティブにする
                    
                    bolEnterKeyFlg = true;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    // Deleteキー

                    //フォーカスを当てないとActicvePositionで落ちるので
                    MainProgram.frmMain.griList.Focus();
                    // デリートスキップ実行
                    MainProgram.frmMain.MainFunc.DeleteSkip(MainProgram.frmMain.WallpaperFolder + "\\" + MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.FILENAME].Value, false);
                    // デリートスキップした行を削除
                    MainProgram.frmMain.Grid.Rows.Remove(MainProgram.frmMain.Grid.Selection.ActivePosition.Row);
                }
                else if (e.KeyCode == Keys.Enter && bolEnterKeyFlg == true)
                {
                    // Enterキーが２回走る(2回目)

                    // フラグ初期化
                    bolEnterKeyFlg = false;
                }
                else if (e.KeyCode == Keys.F2)
                {
                    // F2キー
                    changeFileName(MainProgram.frmMain.WallpaperFolder + "\\" + (string)MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.FILENAME].Value);
                }
            }

            /// <summary>
            /// ファイル名を変更する
            /// </summary>
            /// <param name="strEditFilePath">変更対象ファイルパス</param>
            private void changeFileName(string strEditFilePath){
                // 常に手前を解除
                MainProgram.Foremost(false);

                // ファイルが存在するか確認
                if (File.Exists(strEditFilePath))
                {
                    // ファイルを名前を入力してもらう
                    string strNewFileName
                        = Microsoft.VisualBasic.Interaction.InputBox(MainProgram.MES_FILE_NEWNAME, MainProgram.MES_TITLE_CHANGEFILENAME, Path.GetFileNameWithoutExtension(strEditFilePath), -1, -1);
                    try
                    {
                        if (strNewFileName != "")
                        {
                            // OKクリック
                            if (Path.GetFileNameWithoutExtension(strEditFilePath) != strNewFileName)
                            {
                                // ファイル名変更
                                File.Move(strEditFilePath, Path.GetDirectoryName(strEditFilePath) + "\\" + strNewFileName + Path.GetExtension(strEditFilePath));
                                
                                // キー更新 
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
                                // プレビューファイル名更新
                                MainProgram.frmMain.PreviewFileName = Path.GetFileName(strAfterKey);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        // エラーメッセージ
                        MainProgram.ShowMessage(ex.Message, MainProgram.MES_TITLE_ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }


                }
                

                // 常に手前だったら戻す
                if (MainProgram.frmMain.frmOpt.AlwaysOnTop == true)
                {
                    MainProgram.Foremost(true);
                }
            }

        }

        /// <summary>
        /// 壁紙色セルクリックイベントクラス
        /// </summary>
        private class WallpaperColorCellClickEvent : SourceGrid.Cells.Controllers.ControllerBase
        {
            /// <summary>
            /// グリッド上でマウスがクリックされたとき
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public override void OnMouseUp(SourceGrid.CellContext sender, MouseEventArgs e)
            {
                base.OnMouseUp(sender, e);
                //フォーカスを当てないとActicvePositionで落ちるので
                MainProgram.frmMain.griList.Focus();
                
                if (e.Button == MouseButtons.Left)
                {
                    // 左クリック

                    ColorDialog cd = new ColorDialog();
                    cd.Color = MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].View.BackColor;
                    // カラーダイアログ表示
                    if (cd.ShowDialog() == DialogResult.OK)
                    {
                        // グリッドに壁紙色をセット
                        MainProgram.frmMain.MainFunc.WallpaperColorSetting(true, cd.Color);
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    // グリッドの壁紙色をクリア
                    MainProgram.frmMain.MainFunc.WallpaperColorSetting(false, new Color());
                }
            }
        }

        #endregion

       
    }
}