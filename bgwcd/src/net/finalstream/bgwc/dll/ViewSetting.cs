using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FINALSTREAM.BackGroundWallpaperChanger.Library
{
    /// <summary>
    /// ViewSettingクラス
    /// </summary>
    public class ViewSetting
    {

        /*
            Privateメンバ 
        */
        #region PrivateMember
        
        /// <summary>
        /// GVWI列挙型
        /// </summary>
        private enum enuGVWI
        {
            KEY,
            VIEWPOS,
            COLOR
        }
        private List<GVWI> _list;    //ViewSettingList

        #endregion

        /*
            プロパティ
        */
        /// <summary>
        /// ViewSettingList
        /// </summary>
        public List<GVWI> List
        {
            get{ return this._list;}
        }

        /*
            パブリックメソッド
        */
        # region PublicMethod

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ViewSetting()
        {
            // ViewSettingListを初期化
            this._list = new List<GVWI>();
            List.Capacity = 1000;
        }

        /// <summary>
        /// ViewSettingファイルを読み込んでViewSettingListに格納する
        /// </summary>
        /// <param name="strFile">ViewSettingファイルパス</param>
        public void ReadViewSetting(string strFile)
        {
            // 文字コード設定
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);
            // ファイルを1行ずつ読み込み配列に格納する
            string[] lines = System.IO.File.ReadAllLines(strFile, enc);

            // lines配列を１つずつ取り出して処理する
            foreach (string line in lines)
            {
                GVWI gvwi = new GVWI();
                // 文字列を#で区切って配列に格納
                string[] strVal = line.Split('#');
                // 配列をそれぞれViewSettigへセット
                gvwi.Key = strVal[(int)enuGVWI.KEY];
                gvwi.ViewPos = strVal[(int)enuGVWI.VIEWPOS];
                gvwi.Color = strVal[(int)enuGVWI.COLOR];
                // ViewSettingListにViewSettingを追加
                List.Add(gvwi);
                // ViewSettingを破棄
                gvwi = null;
            }
            //ViewSettingListをキャパシティを調整
            List.TrimExcess();
            
        }

        /// <summary>
        /// ViewSettingListをファイルへ出力する
        /// </summary>
        /// <param name="strOutputPath">出力先ファイルパス</param>
        public void WriteViewSetting(string strOutputPath)
        {

            //StreamWriterの初期化
            //ファイルを上書きし、Shift JISで書き込む
            StreamWriter sw =
                new StreamWriter(strOutputPath,
                false,
                System.Text.Encoding.GetEncoding(932));

            // ViewSettingListから１つずつ取り出して出力
            foreach (GVWI gvwi in List)
            {
                sw.WriteLine(gvwi.Key
                        + "#" + gvwi.ViewPos
                        + "#" + gvwi.Color);
            }

            // StreamWriterクローズ
            sw.Close();

        }

        #endregion


    }
}
