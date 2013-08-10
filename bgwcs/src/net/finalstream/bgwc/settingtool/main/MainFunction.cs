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
    /// MainForm用Functionクラス
    /// </summary>
    public class MainFunction
    {

        /*
            Privateメンバ
        */
        private string _strRegWallpaperPath;



        /*
            プロパティ
        */
        /// <summary>
        /// レジストリに設定されているWindows壁紙ファイルパス
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
           パブリックメソッド
        */
        #region PublicMethod

        /// <summary>
        /// 壁紙のサイズを取得する
        /// </summary>
        /// <param name="strFile">壁紙ファイルパス</param>
        /// <returns>取得したサイズ(Width x Height)</returns>
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
        /// デリートスキップを実行する
        /// </summary>
        /// <param name="DeleteFile">削除ファイルパス</param>
        /// <param name="bolChange">削除後、壁紙変更するか</param>
        public void DeleteSkip(string DeleteFile, bool bolChange)
        {

            // 設定されている壁紙をTrashBoxに移動
            if (DeleteFile != "" && File.Exists(DeleteFile))
            {
                try
                {
                    // ファイルを移動
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

            // 壁紙の変更
            if (bolChange == true)
            {
                WallpaperChange();
            }
        }

        /// <summary>
        /// MainFormの壁紙情報を更新する
        /// </summary>
        /// <param name="strNowWallpaper">現在の壁紙ファイルパス</param>
        /// <param name="strPreviewWallpaper">プレビュー用ファイルパス</param>
        public void NowWallpaperUpdate(string strNowWallpaper, string strPreviewWallpaper)
        {
            // 壁紙のパスを更新
            MainProgram.frmMain.NowWallpaper = strNowWallpaper;
            // 解像度を更新
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            MainProgram.frmMain.ScreenSize = rect.Width.ToString() + " x " + rect.Height.ToString();
            // 壁紙のPreviewを更新
            MainProgram.frmMain.SetPreviewWallpaper = strPreviewWallpaper;
            // 壁紙サイズを更新
            MainProgram.frmMain.WallpaperSize = MainProgram.frmMain.PreviewBaseWidth.ToString() + " x " + MainProgram.frmMain.PreviewBaseHeight.ToString();

        }

        /// <summary>
        /// 壁紙を変更する(チェンジ)
        /// </summary>
        public void WallpaperChange()
        {
            int num = 0;

            int lstCount = MainProgram.frmMain.Grid.RowsCount;
            int lstMaxNum = lstCount;

            if (lstCount > 1)
            {
                // チェンジモードにより処理をわける
                switch (MainProgram.frmMain.frmOpt.ChangeMode)
                {
                    case (int)MainProgram.enuChgMode.RANDOM:
                        // ランダム

                        Random rnd = new Random();

                        num = rnd.Next(1, lstMaxNum);
                        while (MainProgram.frmMain.WallpaperFolder + "\\" + (string)MainProgram.frmMain.Grid[num, (int)MainProgram.enuGrid.FILENAME].Value ==
                           MainProgram.frmMain.NowWallpaper)
                        {
                            num = rnd.Next(1, lstMaxNum);
                        }
                        break;

                    case (int)MainProgram.enuChgMode.NORMAL:
                        // ノーマル

                        num = MainProgram.frmMain.GetKeyIndex(Path.GetFileName(MainProgram.frmMain.NowWallpaper));
                        num++;
                        if (lstMaxNum == num)
                        {
                            num = 1;
                        }
                        break;

                    case (int)MainProgram.enuChgMode.NONE:
                        // なし

                        return;
                }
            }
            else if (lstCount == 2)
            {
                // １行しかないときはそれを選択する。
                num = 2;
            }
            else if (lstCount == 1)
            {
                // フォルダに画像がない場合は終了(ヘッダしかない)

                MainProgram.ShowMessage(MainProgram.MES_GRAPHIC_NOTHING + MainProgram.frmMain.WallpaperFolder,
                    MainProgram.MES_TITLE_INFO,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                Environment.Exit(0);
                return;
            }

            // BGWC壁紙設定
            BGWChanerWallpaperSetting((string)MainProgram.frmMain.Grid[num, (int)MainProgram.enuGrid.FILENAME].Value);

        }


        /// <summary>
        /// BGWC壁紙をWindowsの壁紙として設定する
        /// </summary>
        /// <param name="strWallpaperKey">壁紙として設定するBGWCKey</param>
        public void BGWChanerWallpaperSetting(string strWallpaperKey)
        {
            // ファイルが存在するか確認
            if (File.Exists(MainProgram.frmMain.WallpaperFolder + "\\" + strWallpaperKey))
            {

                GeneralFunction gfunc = new GeneralFunction();

                // WaitForm生成
                FadeForm fadefrm = new FadeForm();

                // Wait画面表示
                fadefrm.StartWait();

                // 画像をBGWC壁紙として保存
                new Bitmap(MainProgram.frmMain.WallpaperFolder + "\\" + strWallpaperKey).Save(MainProgram.gBGWChangerGraphicPath, ImageFormat.Bmp);

                // 個別設定があれば壁紙の位置を設定
                int idx = MainProgram.frmMain.GetKeyIndex(strWallpaperKey);

                switch ((string)MainProgram.frmMain.Grid[idx, (int)MainProgram.enuGrid.VIEWPOS].Value)
                {
                    case MainProgram.conWallpaperPos_Ori:
                        // 中央
                        MainProgram.frmMain.frmOpt.OptFunc.WallpaperStyleWrite(
                            (int)MainProgram.enuTileWallpaper.CENTER,
                            (int)MainProgram.enuWallpaperStyle.NONE);
                        break;
                    case MainProgram.conWallpaperPos_Arr:
                        // 並べて
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

                // Windowsの壁紙として設定する
                gfunc.SettingWallpaper(MainProgram.gBGWChangerGraphicPath);

                // 共通の壁紙位置のレジストリに戻す
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
                    // 個別設定があれば壁紙の色を設定

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
                    // 個別設定がない場合はレジストリから共通の色を取得

                    string strCommonColor = (string)RegistryUtils.
                        getCURegistryKeyValue(MainProgram.conColorSubKey, "Background");
                    strRGB = strCommonColor.Split(' ');
                }
                // デスクトップの色を設定
                gfunc.SetDesktopColor(Color.FromArgb(int.Parse(strRGB[0]), int.Parse(strRGB[1]), int.Parse(strRGB[2])));


                // NowWallpaperを更新
                NowWallpaperUpdate(MainProgram.frmMain.WallpaperFolder + "\\" + (string)MainProgram.frmMain.Grid[idx, (int)MainProgram.enuGrid.FILENAME].Value, MainProgram.gBGWChangerGraphicPath);

                // 設定元壁紙パスを設定クラスにセット
                MainProgram.set.Wallpaper = MainProgram.frmMain.NowWallpaper;

                fadefrm.EndWait();
            }

        }


        /// <summary>
        /// グリッドの壁紙色を設定する
        /// </summary>
        /// <param name="bolMode">壁紙色設定モード(Trueなら設定/Falseならクリア)</param>
        /// <param name="col">Trueの場合に設定するカラー</param>
        public void WallpaperColorSetting(bool bolMode, Color col)
        {

            if (bolMode)
            {
                // 壁紙色をセット

                SourceGrid.Cells.Views.Cell SelectView = new SourceGrid.Cells.Views.Cell();
                SelectView.BackColor = col;
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].View = SelectView;
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].Value = "";
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.RGBCOLOR].Value = col.R + " " + col.G + " " + col.B;
            }
            else
            {
                // 壁紙色をクリア

                SourceGrid.Cells.Views.Cell SelectView = new SourceGrid.Cells.Views.Cell();
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].View = SelectView;
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.COLOR].Value = "None";
                MainProgram.frmMain.Grid[MainProgram.frmMain.Grid.Selection.ActivePosition.Row, (int)MainProgram.enuGrid.RGBCOLOR].Value = "";

            }

        }

        #endregion

        /*
            プライベートメソッド 
        */
        /// <summary>
        /// レジストリに設定されているWindows壁紙ファイルパスを取得してメンバに保持する
        /// </summary>
        private void GetRegWallpaperPath()
        {

            RegWallpaperPath = (string)RegistryUtils.getCURegistryKeyValue(MainProgram.conWallpaperSubKey,"Wallpaper");

        }
        
    }
}
