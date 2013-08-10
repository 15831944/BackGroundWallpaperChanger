using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FINALSTREAM.BackGroundWallpaperChanger.Library
{
    /// <summary>
    /// ViewSetting�N���X
    /// </summary>
    public class ViewSetting
    {

        /*
            Private�����o 
        */
        #region PrivateMember
        
        /// <summary>
        /// GVWI�񋓌^
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
            �v���p�e�B
        */
        /// <summary>
        /// ViewSettingList
        /// </summary>
        public List<GVWI> List
        {
            get{ return this._list;}
        }

        /*
            �p�u���b�N���\�b�h
        */
        # region PublicMethod

        /// <summary>
        /// �R���X�g���N�^
        /// </summary>
        public ViewSetting()
        {
            // ViewSettingList��������
            this._list = new List<GVWI>();
            List.Capacity = 1000;
        }

        /// <summary>
        /// ViewSetting�t�@�C����ǂݍ����ViewSettingList�Ɋi�[����
        /// </summary>
        /// <param name="strFile">ViewSetting�t�@�C���p�X</param>
        public void ReadViewSetting(string strFile)
        {
            // �����R�[�h�ݒ�
            System.Text.Encoding enc = System.Text.Encoding.GetEncoding(932);
            // �t�@�C����1�s���ǂݍ��ݔz��Ɋi�[����
            string[] lines = System.IO.File.ReadAllLines(strFile, enc);

            // lines�z����P�����o���ď�������
            foreach (string line in lines)
            {
                GVWI gvwi = new GVWI();
                // �������#�ŋ�؂��Ĕz��Ɋi�[
                string[] strVal = line.Split('#');
                // �z������ꂼ��ViewSettig�փZ�b�g
                gvwi.Key = strVal[(int)enuGVWI.KEY];
                gvwi.ViewPos = strVal[(int)enuGVWI.VIEWPOS];
                gvwi.Color = strVal[(int)enuGVWI.COLOR];
                // ViewSettingList��ViewSetting��ǉ�
                List.Add(gvwi);
                // ViewSetting��j��
                gvwi = null;
            }
            //ViewSettingList���L���p�V�e�B�𒲐�
            List.TrimExcess();
            
        }

        /// <summary>
        /// ViewSettingList���t�@�C���֏o�͂���
        /// </summary>
        /// <param name="strOutputPath">�o�͐�t�@�C���p�X</param>
        public void WriteViewSetting(string strOutputPath)
        {

            //StreamWriter�̏�����
            //�t�@�C�����㏑�����AShift JIS�ŏ�������
            StreamWriter sw =
                new StreamWriter(strOutputPath,
                false,
                System.Text.Encoding.GetEncoding(932));

            // ViewSettingList����P�����o���ďo��
            foreach (GVWI gvwi in List)
            {
                sw.WriteLine(gvwi.Key
                        + "#" + gvwi.ViewPos
                        + "#" + gvwi.Color);
            }

            // StreamWriter�N���[�Y
            sw.Close();

        }

        #endregion


    }
}
