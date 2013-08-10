using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;


namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    public partial class OptionForm : Form
    {
        /*
            メンバ変数
        */
        private OptionFunction _optFunc;
        private bool _bolWallpaperNothing; // 壁紙なしフラグ
        private int _intChangeMode; // 壁紙チェンジモード

        /*
            プロパティ
        */
        #region Property

        /// <summary>
        /// オプションフォーム用オブジェクト
        /// </summary>
        public OptionFunction OptFunc
        {
            get { return this._optFunc; }
        }

        /// <summary>
        /// 壁紙なし
        /// </summary>
        public bool WallpaperNothing
        {
            set { this._bolWallpaperNothing = value; }
            get{ return this._bolWallpaperNothing;}
        }
        
        /// <summary>
        /// 常に手前
        /// </summary>
        public bool AlwaysOnTop
        {
            get { return this.chkForemost.Checked; }
        }

        /// <summary>
        /// スタートアップ
        /// </summary>
        public bool StartUp
        {
            get { return this.chkStartup.Checked; }
        }

        /// <summary>
        /// 壁紙チェンジモード
        /// </summary>
        public int ChangeMode
        {
            set { 
                this._intChangeMode = value;
            }
            get { return this._intChangeMode; }
        }

        /// <summary>
        /// 壁紙スタイル
        /// </summary>
        public int WallpaperStyle
        {
            set { this.cboWallpaperStyle.SelectedIndex = value; }
            get { return this.cboWallpaperStyle.SelectedIndex; }
        }

        /// <summary>
        /// 壁紙背景色
        /// </summary>
        public Color PreviewColor
        {
            set { this.picPreviewColor.BackColor = value; }
        }
        

        #endregion


        /*
            列挙型
         */
        // 壁紙表示スタイル
        enum enuWallpaperStyle
        {
            WS_TILE,        // 並べて
            WS_CENTER,      // 原寸
            WS_FIT          // 拡大
        }



        /*
            イベント
        */
        #region Event


        /// <summary>
        /// OptionFormをロードしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionForm_Load(object sender, EventArgs e)
        {
            // 設定値をコントロールにセット
            chkForemost.Checked = MainProgram.set.AlwaysOnTop;
            chkStartup.Checked = MainProgram.set.StartUp;
            // 壁紙の表示スタイルを読み込み
            OptFunc.WallpaperStyleRead();
            chkWallNothing.Checked = this.WallpaperNothing;
        }

        /// <summary>
        ///   OptionFormを閉じたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OptionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        /// <summary>
        /// 常に手前チェックボックスが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkForemost_CheckedChanged(object sender, EventArgs e)
        {
            MainProgram.Foremost(!this.TopMost);
        }

        /// <summary>
        /// スタートアップチェックボックスが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkStartup_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkStartup.Checked == true)
            {
                _optFunc.StartUpRegist();
            }
            else
            {
                _optFunc.StartUpUnRegist();
            }
        }

        /// <summary>
        /// 壁紙なしチェックボックスが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkWallNothing_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkWallNothing.Checked == true)
            {
                _optFunc.NonWallpaper(true);
            }
            else
            {
                _optFunc.NonWallpaper(false);
            }

        }

        /// <summary>
        /// 壁紙スタイルチェンジコンボが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboWallpaperStyle_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (MainProgram.gWallpaperStyleWriteFlag == false || chkWallNothing.Checked == true)
            {
                return;
            }

            _optFunc.ChangeWallpaperStyle();
        }

        /// <summary>
        /// 壁紙色ボックスをクリックしたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPreviewColor_Click(object sender, EventArgs e)
        {
            GeneralFunction gfunc = new GeneralFunction();
            ColorDialog cd = new ColorDialog();

            cd.Color = this.picPreviewColor.BackColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                this.picPreviewColor.BackColor = cd.Color;
                // デスクトップの色を設定
                gfunc.SetDesktopColor(cd.Color);

                // BackGroundWrite
                _optFunc.BackGroundWrite(cd.Color);

            }
        }

        /// <summary>
        /// ランダムオプションが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optRandom_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeMode = (int)MainProgram.enuChgMode.RANDOM;
        }


        /// <summary>
        /// ノーマルオプションが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optNormal_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeMode = (int)MainProgram.enuChgMode.NORMAL;
        }

        /// <summary>
        /// なしオプションが変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void optNone_CheckedChanged(object sender, EventArgs e)
        {
            this.ChangeMode = (int)MainProgram.enuChgMode.NONE;
        }


        #endregion


        /*
            パブリックメソッド 
        */
        #region Public Method



        /// <summary>
        /// コンストラクタ
        /// </summary>
        public OptionForm()
        {
            InitializeComponent();

            // OptFunc生成
            this._optFunc = new OptionFunction();

            // 設定オブジェクトから壁紙チェンジモードをロード
            this._intChangeMode = MainProgram.set.ChangeMode;
            // ロードした壁紙チェンジモードを適用
            switch (this._intChangeMode)
            {
                case (int)MainProgram.enuChgMode.RANDOM:
                    this.optRandom.Checked = true;
                    break;

                case (int)MainProgram.enuChgMode.NORMAL:
                    this.optNormal.Checked = true;
                    break;

                case (int)MainProgram.enuChgMode.NONE:
                    this.optNone.Checked = true;
                    break;
            }
        }




        #endregion



        
        
    }
}