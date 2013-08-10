using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.BackGroundWallpaperChanger.Library
{
    /// <summary>
    /// 壁紙個別設定情報格納クラス
    /// </summary>
    public class GVWI
    {
        private string _key;
        private string _viewpos;
        private string _color;

        /// <summary>
        /// キー(BGWCフォルダからのファイルパス)
        /// </summary>
        public string Key{
            set { this._key = value;}
            get { return this._key; }
        }

        /// <summary>
        /// 壁紙表示位置
        /// </summary>
        public string ViewPos
        {
            set { this._viewpos = value; }
            get { return this._viewpos; }
        }

        /// <summary>
        /// 壁紙背景色
        /// </summary>
        public string Color
        {
            set { this._color = value; }
            get { return this._color; }
        }

    }
}
