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
    /// BgwcSetting�N���X
    /// </summary>
    public class BgwcSetting
    {

        /*
            �����o�ϐ�
        */
        # region Member


        string _strWallpaperFolder;
        string _strWallpaper;
        bool _bolAlwaysOnTop;
        bool _bolStartUp;
        int _intChangeMode;


        # endregion


        /*
            �v���p�e�B
        */
        # region Property


        /// <summary>
        /// WallpaperFolder�p�X
        /// </summary>
        public string WallpaperFolder
        {
            set { this._strWallpaperFolder = value; }
            get { return this._strWallpaperFolder; }
        }

        /// <summary>
        /// �ǎ��̃p�X
        /// </summary>
        public string Wallpaper
        {
            set { this._strWallpaper = value; }
            get { return this._strWallpaper; }
        }

        /// <summary>
        /// ��Ɏ�O�ɕ\��
        /// </summary>
        public bool AlwaysOnTop
        {
            set { this._bolAlwaysOnTop = value; }
            get { return _bolAlwaysOnTop; }
        }

        /// <summary>
        /// Windows�N�����Ɏ��s
        /// </summary>
        public bool StartUp
        {
            set { this._bolStartUp = value; }
            get { return this._bolStartUp; }
        }

        /// <summary>
        /// �ǎ��`�F���W���[�h
        /// </summary>
        public int ChangeMode
        {
            set { this._intChangeMode = value; }
            get { return this._intChangeMode; }
        }
        #endregion


        /*
            �p�u���b�N���\�b�h
        */
        #region Public Method

        /// <summary>
        /// �ݒ��XML�ɕۑ�����
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
        /// �O���b�h�̏���ViewSetFile�ɕۑ�����
        /// </summary>
        /// <returns></returns>
        public bool SaveWallpaperSetting()
        {
            // ViewSetting�t�@�C���֏o��
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
        /// XML����ݒ�����[�h����
        /// </summary>
        /// <returns>����������</returns>
        public bool LoadSetting()
        {

            // �A�v���P�[�V�����̐ݒ��ǂݍ���
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

            // �ǎ��t�H���_���ݒ肳��Ȃ��ꍇ��Wallpaper�t�H���_���w�肷��
            if (_strWallpaperFolder == "" || _strWallpaperFolder == null)
            {
                _strWallpaperFolder = MainProgram.ApplicationPath + "\\Wallpaper";
            }
            return true;
        }

        #endregion


    }
}
