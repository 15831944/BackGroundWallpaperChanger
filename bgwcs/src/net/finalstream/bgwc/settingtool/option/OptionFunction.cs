using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using FINALSTREAM.Commons.Utils;

namespace FINALSTREAM.BackGroundWallpaperChanger.SettingTool
{
    /// <summary>
    /// オプション関数クラス
    /// </summary>
    public class OptionFunction
    {

        /*
            パブリックメソッド
        */
        #region Public Method


        /// <summary>
        /// 壁紙表示スタイルをレジストリから読み込む
        /// </summary>
        public void WallpaperStyleRead()
        {

            // TileWallpaper値を取得
            int intTileWallpaperValue = int.Parse((string)RegistryUtils.getCURegistryKeyValue(MainProgram.conWallpaperSubKey, "TileWallpaper"));
            // WallpaperStyle値を取得
            int intWallpaperStyleValue = int.Parse((string)RegistryUtils.getCURegistryKeyValue(MainProgram.conWallpaperSubKey, "WallpaperStyle"));

            if (intTileWallpaperValue == (int)MainProgram.enuTileWallpaper.FILL &&
                intWallpaperStyleValue == (int)MainProgram.enuWallpaperStyle.NONE)
            {
                // 並べて表示
                MainProgram.frmMain.frmOpt.WallpaperStyle = (int)MainProgram.enuViewPos.ARRANGE;
            }
            else if (intTileWallpaperValue == (int)MainProgram.enuTileWallpaper.CENTER &&
                intWallpaperStyleValue == (int)MainProgram.enuWallpaperStyle.NONE)
            {
                // 中央に表示
                MainProgram.frmMain.frmOpt.WallpaperStyle = (int)MainProgram.enuViewPos.ORIGINAL;
            }
            else if (intTileWallpaperValue == (int)MainProgram.enuTileWallpaper.CENTER &&
                intWallpaperStyleValue == (int)MainProgram.enuWallpaperStyle.STRETCH)
            {
                // 拡大して表示
                MainProgram.frmMain.frmOpt.WallpaperStyle = (int)MainProgram.enuViewPos.FIT;
            }

            // 壁紙の色を取得。
            MainProgram.frmMain.frmOpt.PreviewColor = SystemColors.Desktop;


        }


        /// <summary>
        /// 壁紙の表示スタイルをレジストリに書き込む
        /// </summary>
        /// <param name="intTileWallpaperValue">TileWallpaperの値</param>
        /// <param name="intWallpaperStyleValue">WallpaperStyleの値</param>
        public void WallpaperStyleWrite(int intTileWallpaperValue, int intWallpaperStyleValue)
        {

            // 値を更新
            RegistryUtils.setCURegistryKeyStringValue(MainProgram.conWallpaperSubKey, "TileWallpaper", intTileWallpaperValue.ToString());
            RegistryUtils.setCURegistryKeyStringValue(MainProgram.conWallpaperSubKey, "WallpaperStyle", intWallpaperStyleValue.ToString());

        }

        /// <summary>
        /// スタートアップを設定する(レジストリ)
        /// </summary>
        public void StartUpRegist()
        {
            // 実行するファイル名を書き込み
            RegistryUtils.setCURegistryKeyStringValue(MainProgram.conStartupSubKey, "BGWChanger", MainProgram.gExeFilePath);

        }

        /// <summary>
        /// スタートアップの設定を解除する(レジストリ)
        /// </summary>
        public void StartUpUnRegist()
        {

            // 値を削除
            RegistryUtils.deleteCURegistryKeyStringValue(MainProgram.conStartupSubKey, "BGWChanger");

        }

        /// <summary>
        /// 壁紙の無効を設定(解除)する
        /// </summary>
        /// <param name="bolVal">true:設定 false:解除</param>
        public void NonWallpaper(bool bolVal)
        {
            GeneralFunction gfunc = new GeneralFunction();

            // WaitForm生成
            WaitForm waitfrm = new WaitForm();

            waitfrm.StartWait();

            if (bolVal)
            {
                // 無効にする
                RegistryUtils.setCURegistryKeyStringValue(MainProgram.conWallpaperSubKey, "Wallpaper", "");

                // 壁紙を無効に設定
                gfunc.SettingWallpaper("");
            }
            else
            {
                // 有効にする
                RegistryUtils.setCURegistryKeyStringValue(MainProgram.conWallpaperSubKey, "Wallpaper", MainProgram.gBGWChangerGraphicPath);

                // 壁紙を有効に設定
                gfunc.SettingWallpaper(MainProgram.gBGWChangerGraphicPath);

            }

            waitfrm.EndWait();

        }

        /// <summary>
        /// 壁紙スタイルを変更する
        /// </summary>
        public void ChangeWallpaperStyle()
        {
            GeneralFunction gfunc = new GeneralFunction();

            // WaitForm生成
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
        /// 壁紙色を設定する(レジストリ)
        /// </summary>
        /// <param name="col"></param>
        public void BackGroundWrite(Color col)
        {

            RegistryUtils.setCURegistryKeyStringValue(MainProgram.conColorSubKey, "Background", col.R.ToString() + " " + col.G.ToString() + " " + col.B.ToString());

        }


        #endregion
        
    }
}
