using UnityEngine;
using System.Collections;

public class GlobalManager : MonoBehaviour {
    /// <summary>
    /// 0 未解锁 。1，2，3 颗星 。4解锁未通过
    /// </summary>
    public static string _BigLevel_1 = "4000000000000000";
    public static string _BigLevel_2 = "0000000000000000";
    public static string _BigLevel_3 = "0000000000000000";
    public static string _BigLevel_4 = "0000000000000000";
    public static string _BigLevel_5 = "0000000000000000";

    public static int _FontSize_480 = 19;
    public static int _FontSize_540 = 22;

    public static float _BGMVolume = 0.5f;
    public static int _MinFinishTime = 100;
    public static int _MaxFinishTime = 100;
    public static int _MaxCombo = 1;
    public static int _SumCombo = 1;
    public static int _OnceClickScore = 1;
    public static int _AvarageClickScore = 1;
    public static string _BestLevel = "1-1";
    public static string _CurrentPanel = "mainMenu";
    public static int _SoundEffect = 1;

    public static int _CurrentSmallLevel = 1;
    public static int _CurrentBigLevel = 1;
    public static bool _BackLevel = false;


    public static int _MaxBigLevel = 1;
    public static int _MaxSmallLevel = 1;


    public static string[] _Prop = new string[5]{"0","0","0","0","0"};
    public static int[] _PropScore = new int[5] { 0, 0, 0, 0, 0 };
    public static string _PropStr = "00000";



    ///<summary>
    ///记录通过关卡的逻辑
    /// </summary>
    //
    public static void SaveUnlockedLevel( int tBigLevel,int tSmallLevel,int tState){
        
        string tLevelState = PlayerPrefs.GetString("BigLevel_" + tBigLevel,"4000000000000000");
        tLevelState = tLevelState.Remove(tSmallLevel-1,1);
        tLevelState = tLevelState.Insert(tSmallLevel-1, tState.ToString());
       // Debug.Log(PlayerPrefs.GetString("BigLevel_" + tBigLevel));
        PlayerPrefs.SetString("BigLevel_" + tBigLevel,tLevelState);
        PlayerPrefs.Save();
	}

    public static int ShowUnlockLevelStars(int tBigLevel, int tSmallLevel)
    {

        string tLevelState = PlayerPrefs.GetString("BigLevel_" + tBigLevel, "4000000000000000");
        int tStarNum = int.Parse( tLevelState.Substring(tSmallLevel-1,1));
        return tStarNum;
    }
    /// <summary>
    /// 每关道具 存储
    /// </summary>

    //public static void SavePropsNum(int tPropsID, int tPropsNum ,int tState)
    //{
    //    string tID = PlayerPrefs.GetString(_PropStr,"00000");
    //    tID = tID.Remove(tPropsNum-1,1);
    //    tID = tID.Insert(tPropsNum-1, tState.ToString());
    //    PlayerPrefs.SetString(_PropStr, tID);
    //    PlayerPrefs.Save();
    //}

    //public static int ShowPropsNum(int tPropNum)
    //{
    //    string tBigLevelState = PlayerPrefs.GetString(_PropStr,"00000");
    //    int tNum = int.Parse(tBigLevelState.Substring(tPropNum , 1));
    //    return tNum;
    //}
   
    /// <summary>
    /// 存储成就
    /// </summary>
    public static void SaveAllPlayerPrefs() {
        PlayerPrefs.SetInt("SoundEffect",_SoundEffect);
        PlayerPrefs.SetFloat("BGMVolume", _BGMVolume);
        PlayerPrefs.SetInt("MinFinishTime", _MinFinishTime);
        PlayerPrefs.SetInt("MaxFinishTime", _MaxFinishTime);
        PlayerPrefs.SetInt("MaxCombo", _MaxCombo);
        PlayerPrefs.SetInt("SumCombo", _SumCombo);
        PlayerPrefs.SetInt("OnceClickScore", _OnceClickScore);
        PlayerPrefs.SetInt("AvarageClickScore", _AvarageClickScore);
        PlayerPrefs.SetString("BestLevel", _BestLevel);
        PlayerPrefs.SetInt("MaxBigLevel", _MaxBigLevel);
        PlayerPrefs.SetInt("MaxSmallLevel", _MaxSmallLevel);
        for (int i = 0; i < _Prop.Length; i++)
        {         
            PlayerPrefs.SetString("_Prop_" + i, _Prop[i] + "," +_PropScore[i]);
        }
        PlayerPrefs.Save();
        
    }
 


    public static void LoadAllPlayerPrefs() {

        for (int tBigLevel = 1; tBigLevel < 6; tBigLevel++)
        {
            if(tBigLevel ==1)
                PlayerPrefs.GetString("BigLevel_" + tBigLevel, "4000000000000000"); 
            else
                PlayerPrefs.GetString("BigLevel_" + tBigLevel, "0000000000000000");
        }
        GlobalManager._SoundEffect = PlayerPrefs.GetInt("SoundEffect",1);
        GlobalManager._BGMVolume =  PlayerPrefs.GetFloat("BGMVolume", _BGMVolume);
        GlobalManager._MinFinishTime = PlayerPrefs.GetInt("MinFinishTime", 1);
        GlobalManager._MaxFinishTime = PlayerPrefs.GetInt("MaxFinishTime", 1);
        GlobalManager._MaxCombo = PlayerPrefs.GetInt("MaxCombo", 1);
        GlobalManager._SumCombo = PlayerPrefs.GetInt("SumCombo", 1);
        GlobalManager._OnceClickScore = PlayerPrefs.GetInt("OnceClickScore", 1);
        GlobalManager._AvarageClickScore = PlayerPrefs.GetInt("AvarageClickScore", 1);
        GlobalManager._BestLevel = PlayerPrefs.GetString("BestLevel",_BestLevel);

        GlobalManager._MaxBigLevel = PlayerPrefs.GetInt("MaxBigLevel",1);
        GlobalManager._MaxSmallLevel = PlayerPrefs.GetInt("MaxSmallLevel",1);

        for (int i = 0; i < _Prop.Length; i++)
        {
            string[] tPStr = PlayerPrefs.GetString("_Prop_" + i, _Prop[i] + "," + _PropScore[i]).Split(',');
           // Debug.Log(tPStr[0]  + "   " + tPStr[1]);
            GlobalManager._PropScore[i] = int.Parse(tPStr[1]);
            GlobalManager._Prop[i] = tPStr[0];
            PlayerPrefs.GetString("_Prop_" + i, _Prop[i] + "," + _PropScore[i]);
        }
        PlayerPrefs.Save();

    }


    public static void UpdateCurrentLevel() {
        if (_CurrentSmallLevel == 16)
        {
            _CurrentSmallLevel = 1;
            _CurrentBigLevel++;
        }
        else {
            _CurrentSmallLevel++;
        }
        if (_CurrentBigLevel > _MaxBigLevel) {
            _MaxBigLevel = _CurrentBigLevel;
            _MaxSmallLevel = _CurrentSmallLevel;
        }
        if (_CurrentBigLevel == _MaxBigLevel)
        {
            if (_CurrentSmallLevel > _MaxSmallLevel)
            {
                _MaxSmallLevel = _CurrentSmallLevel;
            }
        }
        if (ShowUnlockLevelStars(_CurrentBigLevel, _CurrentSmallLevel) == 0)
            SaveUnlockedLevel(_CurrentBigLevel,_CurrentSmallLevel,4);
        SaveAllPlayerPrefs();
    }



    public static void UpdateMaxLevel()
    {
        if (_MaxBigLevel == _CurrentBigLevel && _MaxSmallLevel == _CurrentSmallLevel)
        {
            if (_MaxSmallLevel == 16)
            {
                _MaxSmallLevel = 1;
                _MaxBigLevel += 1;
            }
            else
            {
                _MaxSmallLevel++;
            }
        }
        SaveAllPlayerPrefs();
    }



    public static void UpdateLevelData() {

        if (_CurrentSmallLevel % 4 == 0)
        {
            if (_CurrentSmallLevel == 16)
            {
                DataCon.DataCon_LimitTime = 200;
                DataCon.DataCon_FullScore = 1000;
            }
            DataCon.DataCon_LimitTime += 100;
        }
        DataCon.DataCon_FullScore += 10;

        if (_CurrentBigLevel == 1)
        {
            if (_CurrentSmallLevel == 1)
            {
                DataCon.DataCon_XCount = 4;
                DataCon.DataCon_YCount = 5;

            }
            else if (_CurrentSmallLevel == 2)
            {
                DataCon.DataCon_XCount = 5;
                DataCon.DataCon_YCount = 5;

            }
            else if (_CurrentSmallLevel == 3)
            {
                DataCon.DataCon_XCount = 5;
                DataCon.DataCon_YCount = 6;
            }
            else if (_CurrentSmallLevel == 4)
            {
                DataCon.DataCon_XCount = 5;
                DataCon.DataCon_YCount = 7;

            }
            else if (_CurrentSmallLevel >= 5)
            {
                DataCon.DataCon_XCount = 5;
                DataCon.DataCon_YCount = 7;
            }
            else
            {
                DataCon.DataCon_XCount = 5;
                DataCon.DataCon_YCount = 7;
            }
        }
        else {
            DataCon.DataCon_XCount = 5;
            DataCon.DataCon_YCount = 7;
        }
    }
}
