using System;
using System.Collections.Generic;
using System.Text;

namespace FINALSTREAM.BackGroundWallpaperChanger.Library
{
    /// <summary>
    /// �ǎ��ʐݒ���i�[�N���X
    /// </summary>
    public class GVWI
    {
        private string _key;
        private string _viewpos;
        private string _color;

        /// <summary>
        /// �L�[(BGWC�t�H���_����̃t�@�C���p�X)
        /// </summary>
        public string Key{
            set { this._key = value;}
            get { return this._key; }
        }

        /// <summary>
        /// �ǎ��\���ʒu
        /// </summary>
        public string ViewPos
        {
            set { this._viewpos = value; }
            get { return this._viewpos; }
        }

        /// <summary>
        /// �ǎ��w�i�F
        /// </summary>
        public string Color
        {
            set { this._color = value; }
            get { return this._color; }
        }

    }
}
