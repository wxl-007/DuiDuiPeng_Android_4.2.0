using UnityEngine;
using System.Collections;

public class GameUIManager : MonoBehaviour
{


    public GUITexture _TimeBar;
    int _LevelTime;
    float _BarScale = 0.66f;
    float _UnitLength = 0.05f;
    float tUnitLength;
    float tUnitX;

    public int _CurrentScore;

    public GameObject _WinPanel;
    public GameObject _LosePanel;
    public GameObject _PausePanel;
    public GameObject _RestartPanel;
    public GameObject _BackMenuPanel;

    public GameObject[] _Prop;

    public GUIText _LeftScore;
    public GUIText _RightScore;
    public GUIText _CurrentLevel;
    public GUIText _UsedTime;
    public GUIText _ComboNum;
    public GUIText _ClickTopScore;

    public GUIText _LosePanelLevelTxt;

    bool _IsCtrl = false;
    bool IsRotate = false;
    bool _IsWin = false;
    bool _IsBeginCount = false;

    public Transform _TimerProp;
    public Transform _RemoveProp;


    public GameObject _ItemController;
    public GameObject _LoadingPanel;

    public GUITexture _LoadingBar;

    public Texture _WinBGTexture;

    public GUITexture _DestroyEff;

    public GUITexture[] _ComboPic;
    public Camera _MainCamera;
    private float rotAngle = 0;
    private Vector2 pivotPoint;
    public GUIText _LevelNum;

    public GameObject _AlphaBlack;
    public GameObject _SoundPlayer;

    GameObject _CurrentProp;
    /// <summary>
    /// 记分状态
    /// </summary>

    void Awake() {
        _LevelNum.text = GlobalManager._CurrentBigLevel + "-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2,'0');
        _IsBeginCount = false;
        Time.timeScale = 1;
        //Debug.Log(Screen.width + "   ,   height" + Screen.height);
        GlobalManager.LoadAllPlayerPrefs();
        this.InitPropsBtn();
        StartCoroutine(LoadingCtrl());
        this.CameraPosInit();
        this.TextSizeCtrl();
        
    }

    /// <summary>
    /// 初始相机位置适配
    /// </summary>
    void CameraPosInit() {
        if (GlobalManager._CurrentBigLevel == 1) {
            if (GlobalManager._CurrentSmallLevel == 1) {
                _MainCamera.transform.position = new Vector3(1.5f, 2, -8.280285f);
            }
            else if (GlobalManager._CurrentSmallLevel == 2)
            {
                _MainCamera.transform.position = new Vector3(2f, 2, -8.280285f);
            }

            else if (GlobalManager._CurrentSmallLevel == 3)
            {
                _MainCamera.transform.position = new Vector3(2f, 2.8f, -8.280285f);
            }
        }
    }



    // Use this for initialization
    void Start()
    {     
        _IsWin = false;
        _WinPanel.SetActive(false);
        _LosePanel.SetActive(false);
        //_LevelTime = DataCon.LimitTime;
        _LevelTime = DataCon.DataCon_LimitTime;
        tUnitLength = (_BarScale - 0.01f) / _LevelTime;
        tUnitX = 10 * tUnitLength * _UnitLength;
        InvokeRepeating("TimeController", 1f, 1f);
        _RightScore.text = "/" + DataCon.DataCon_FullScore;
        _LeftScore.text = "0";

        //int tUsedTime = 90;
       // int tI =  Mathf.RoundToInt(tUsedTime / 60);
      //  Debug.Log("0" + Mathf.RoundToInt(tUsedTime / 60) + ":" + (tUsedTime - tI* 60).ToString().PadLeft(2, '0'));
        
    }


    /// <summary>
    /// 等待场景
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadingCtrl()
    { 
        yield return new WaitForSeconds(2);
        for (int i = 0; i < 5; i++)
        {
            //_Prop[i].GetComponent<PropsBtn>()._LeftNum = 0;
            _Prop[i].GetComponent<PropsBtn>().BtnAct();
        }
        _LoadingPanel.SetActive(false);
        _IsBeginCount = true;
    }

    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// <param name="tGameObjName"></param>
    void GetMouseDown(string tGameObjName)
    {
        switch (tGameObjName)
        {
            case "MenuBtn":
                this.BackMenuBtn();
                Debug.Log("MenuBtn");
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                break;
            case "RestartBtn":
                Debug.Log("RestartBtn");
                Application.LoadLevel("GameScene");
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                break;
            case "PauseBtn":
                Debug.Log("PauseBtn");
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                this.PauseGame();
                break;
            case "SoundBtn":
                this.GSSoundBtn();
                Debug.Log("SoundBtn");
                break;
            case "NextLevelBtn":
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                this.NextLevelBtn();
              //  Debug.Log("nextLevel");
                break;
            case "BackBtn":
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                Debug.Log("BackMenu");
                GlobalManager._CurrentPanel = "mainMenu";
                Application.LoadLevel("UI");
                Time.timeScale = 1;
                break;
            case "ResumeBtn":
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                this.ResumeGame();
                Debug.Log("Resume game");
                break;
            case "BG":
                Debug.Log("pause !! ");
                break;

            case "RestartPanelBtn":
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                this.RestartGame();
                break;
            case "CancelBtn":
                _AlphaBlack.SetActive(false);
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                _RestartPanel.SetActive(false);
                Time.timeScale = 1;
                break;
            case "CancelBtn_Menu":
                _AlphaBlack.SetActive(false);
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                _BackMenuPanel.SetActive(false);
                Time.timeScale = 1;
                break;
            case "BackMenu":
                _AlphaBlack.SetActive(false);
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                Application.LoadLevel("UI");
                Time.timeScale = 1;
                GlobalManager._BackLevel = true;
                break;

            case "GiveUpBtn":
                _AlphaBlack.SetActive(false);
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                 GlobalManager._CurrentPanel = "mainMenu";
                Application.LoadLevel("UI");
                Time.timeScale = 1;
                GlobalManager.UpdateMaxLevel();
                GlobalManager.UpdateLevelData();

                break;
        }
    }

    public void GSSoundBtn() {
        _SoundPlayer.GetComponent<SoundPlayer>().GSSoundCtrl();
    }

    /// <summary>
    /// 返回菜单界面
    /// </summary>
    void BackMenuBtn() {
        _RestartPanel.SetActive(false);
        _PausePanel.SetActive(false);
        _LosePanel.SetActive(false);
        _WinPanel.SetActive(false);

        this.ShowDialogueBox(_BackMenuPanel);
        //_BackMenuPanel.SetActive(true);
       // Time.timeScale = 0;
    }

    /// <summary>
    /// 暂停游戏
    /// </summary>
    void PauseGame()
    {
       this.ShowDialogueBox(_PausePanel);
       // _PausePanel.SetActive(true);
        //Time.timeScale = 0;
    }

    /// <summary>
    /// 继续游戏
    /// </summary>
    void ResumeGame() {
        _AlphaBlack.SetActive(false);
        _PausePanel.SetActive(false);
        Time.timeScale = 1;
    }


    /// <summary>
    /// 重新开始游戏
    /// </summary>
    void RestartGame()
    {
        this.ShowDialogueBox(_RestartPanel);
       // _RestartPanel.SetActive(true);
       // Time.timeScale = 0;
    }

    /// <summary>
    /// 时间控制
    /// </summary>
    void TimeController()
    {
        if (_LevelTime > 0)
        {
            _LevelTime--;
            _TimeBar.transform.localScale = new Vector3(_TimeBar.transform.localScale.x - tUnitLength, _TimeBar.transform.localScale.y, _TimeBar.transform.localScale.z);
            _TimeBar.transform.position = new Vector3(_TimeBar.transform.position.x - tUnitX, _TimeBar.transform.position.y, _TimeBar.transform.position.z);
        }
        if (_LevelTime <= 0)
        {
            if (_CurrentScore <= DataCon.DataCon_FullScore)
                this.GameOver();
            _TimeBar.transform.localScale = Vector3.zero ;
        }
    }

    /// <summary>
    /// 结束游戏
    /// </summary>
    void GameOver()
    {
        if (_IsWin == true)
        {
            _SoundPlayer.GetComponent<SoundPlayer>().WinPanel();

            this.WinLevel();
        }
        else
        {
            _SoundPlayer.GetComponent<SoundPlayer>().LosePanel();

            this.LoseLevel();
        }

    }


    /// <summary>
    /// 胜利关卡
    /// </summary>
    void WinLevel()
    {

        int tStar;
        CancelInvoke("TimeController");
        
            if (_LevelTime >= DataCon.DataCon_LimitTime * 2 / 3)
            {
                tStar = 3;
                if (GlobalManager.ShowUnlockLevelStars(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel) <= tStar)
                    GlobalManager.SaveUnlockedLevel(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel, 3);
                else if (GlobalManager.ShowUnlockLevelStars(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel) == 4) {
                    GlobalManager.SaveUnlockedLevel(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel, 3);
                }
            }
            else if (_LevelTime < DataCon.DataCon_LimitTime * 2 / 3 && _LevelTime >= DataCon.DataCon_LimitTime * 1 / 3)
            {
                tStar = 2;
                if (GlobalManager.ShowUnlockLevelStars(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel) <= tStar)
                    GlobalManager.SaveUnlockedLevel(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel, 2);
                else if (GlobalManager.ShowUnlockLevelStars(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel) == 4)
                {
                    GlobalManager.SaveUnlockedLevel(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel, 3);
                }
            }
            else if (_LevelTime > 0 && _LevelTime <= DataCon.DataCon_LimitTime * 1 / 3)
            {
                tStar = 1;
                if (GlobalManager.ShowUnlockLevelStars(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel) <= tStar)
                    GlobalManager.SaveUnlockedLevel(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel, 1);
                else if (GlobalManager.ShowUnlockLevelStars(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel) == 4)
                {
                    GlobalManager.SaveUnlockedLevel(GlobalManager._CurrentBigLevel, GlobalManager._CurrentSmallLevel, 3);
                }
            }
        
        


        if (DataCon.DataCon_LimitTime - _LevelTime < PlayerPrefs.GetInt("MinFinishTime"))
        {
            PlayerPrefs.SetInt("MinFinishTime", DataCon.DataCon_LimitTime - _LevelTime);
        }
        else if (DataCon.DataCon_LimitTime - _LevelTime > PlayerPrefs.GetInt("MaxFinishTime"))
        {
            PlayerPrefs.SetInt("MaxFinishTime", DataCon.DataCon_LimitTime - _LevelTime);
        }
        GlobalManager.SaveAllPlayerPrefs();
        this.WinPanelTxt();
        this.ShowDialogueBox(_WinPanel);
       // _WinPanel.SetActive(true);

    }
    

    /// <summary>
    /// 关卡失败
    /// </summary>
    void LoseLevel()
    {
        CancelInvoke("TimeController");
        this.ShowDialogueBox(_LosePanel);

        //_LosePanel.SetActive(true);
    }

    /// <summary>
    /// 分数控制
    /// </summary>
    /// <param name="tScore"></param>
    public void ScoreController(int tScore)
    {
        if (_IsBeginCount == true)
        {
            if (_CurrentScore + tScore < DataCon.DataCon_FullScore)
            {

                _CurrentScore += tScore;

            }
            if (_CurrentScore + tScore >= DataCon.DataCon_FullScore)
            {
                _CurrentScore = DataCon.DataCon_FullScore;
                _IsWin = true;

                this.GameOver();
            }

            _LeftScore.text = _CurrentScore.ToString();
        }
    }

    /// <summary>
    /// 下一关按钮
    /// </summary>
    void NextLevelBtn()
    {
        GlobalManager.UpdateCurrentLevel();
        GlobalManager.UpdateMaxLevel();
       // Debug.Log("Current small" + GlobalManager._CurrentSmallLevel + "current Big" + GlobalManager._CurrentBigLevel);
        GlobalManager.UpdateLevelData();
        Application.LoadLevel("GameScene");
    }

    /// <summary>
    /// 初始化道具按钮
    /// </summary>
    void InitPropsBtn()
    {
        string _tFile = "";
        switch(GlobalManager._CurrentBigLevel )
        {
            case 1:
                _tFile = "/Spring_";
                break;
            case 2:
                _tFile = "/Summer_";
                break;
            case 3:
                _tFile = "/Winter_";
                break;
            case 4:
                _tFile = "/Sweet_";
                break;
            case 5:
                _tFile = "/Universe_";
                break;

        }
        //Debug.Log("tFile");
        for (int i = 0; i < 5; i++)
        {
            _Prop[i].GetComponent<GUITexture>().texture = Resources.Load("Panel_" + GlobalManager._CurrentBigLevel + _tFile + (i + 1)) as Texture;
            _Prop[i].GetComponent<PropsBtn>()._Taget = gameObject;
            
                //GlobalManager._Prop[i] = PlayerPrefs.GetString("_Prop_" + i, i + "," + GlobalManager._PropScore);
            
           // Debug.Log(PlayerPrefs.GetString("_Prop_" + i));
            string[] tPropScore = PlayerPrefs.GetString("_Prop_" + i, GlobalManager._Prop[i] + "," + GlobalManager._PropScore[i]).Split(',');


            _Prop[i].GetComponent<PropsBtn>()._PropsNum = int.Parse(tPropScore[0]);
            _Prop[i].GetComponent<PropsBtn>()._LeftNum = int.Parse(tPropScore[1]);
        }
    }


    /// <summary>
    /// 道具按钮控制
    /// </summary>
    /// <param name="tIDNum"></param>
    public void PropsBtnCtrl(int tIDNum)
    {
        _Prop[tIDNum-5*(GlobalManager._CurrentBigLevel -1)].GetComponent<PropsBtn>().PropsNumCount(10);
    }

    /// <summary>
    /// 胜利的结果显示
    /// </summary>
    void WinPanelTxt()
    {
        
        switch (GlobalManager._CurrentBigLevel)
        {
            case 1:
                _CurrentLevel.text = "1-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
            case 2:
                _CurrentLevel.text = "2-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
            case 3:
                _CurrentLevel.text = "3-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
            case 4:
                _CurrentLevel.text = "4-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
            case 5:
                _CurrentLevel.text = "5-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
        }
        //游戏记录数据
        int tUsedTime =  DataCon.DataCon_LimitTime - _LevelTime;

        int tI = Mathf.RoundToInt(tUsedTime / 60);
       _UsedTime.text = "0" + tI + ":" + (tUsedTime - tI * 60).ToString().PadLeft(2, '0');
       // _UsedTime.text = "0" +Mathf.RoundToInt(tUsedTime/60) + ":" + tUsedTime.ToString().PadLeft(2,'0');
        if (tUsedTime <= GlobalManager._MinFinishTime) {
            PlayerPrefs.SetInt("_MinFinishTime", tUsedTime);
            PlayerPrefs.SetString("BestLevel", GlobalManager._CurrentBigLevel + "-" + GlobalManager._CurrentSmallLevel);
        }

        _ComboNum.text =_ItemController.GetComponent<ItemCon>().MaxCombo.ToString();
        if (_ItemController.GetComponent<ItemCon>().MaxCombo > GlobalManager._MaxCombo){
            PlayerPrefs.SetInt("MaxCombo", _ItemController.GetComponent<ItemCon>().MaxCombo);
        }
        
            
        _ClickTopScore.text = _ItemController.GetComponent<ItemCon>()._ClickTopScore.ToString();
        if (_ItemController.GetComponent<ItemCon>()._ClickTopScore > GlobalManager._OnceClickScore) {
            PlayerPrefs.SetInt("OnceClickScore", _ItemController.GetComponent<ItemCon>()._ClickTopScore);
        }

        for (int i = 0; i < 5; i++)
        {
            GlobalManager._PropScore[i] = _Prop[i].GetComponent<PropsBtn>()._LeftNum;
            GlobalManager._Prop[i] = _Prop[i].GetComponent<PropsBtn>()._PropsNum.ToString();
           // Debug.Log(_Prop[i].GetComponent<PropsBtn>()._PropsNum + "  " + GlobalManager._Prop[i]);
            PlayerPrefs.SetString("_Prop_" + i, _Prop[i].GetComponent<PropsBtn>()._PropsNum + "," + GlobalManager._PropScore[i]);
        }
        PlayerPrefs.Save();
     }

   
    /// <summary>
    /// 失败显示结果
    /// </summary>
    void LosePanelTxt() {
       
        switch (GlobalManager._CurrentBigLevel)
        {
            case 1:
                _LosePanelLevelTxt.text = "1-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
            case 2:
                _LosePanelLevelTxt.text = "2-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
            case 3:
                _LosePanelLevelTxt.text = "3-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
            case 4:
                _LosePanelLevelTxt.text = "4-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
            case 5:
                _LosePanelLevelTxt.text = "5-" + GlobalManager._CurrentSmallLevel.ToString().PadLeft(2, '0');
                break;
        }
    }


   /// <summary>
   /// 道具键控制
   /// </summary>
   /// <param name="tProp"></param>
    public void PropsBtnCtrl(GameObject tProp) {
        _IsCtrl = true;
        _CurrentProp = tProp;
    }

    /// <summary>
    /// 控制道具按钮
    /// </summary>
    public void LosePropsBtnCtrl() {
        if (Vector2.Distance(_CurrentProp.transform.position, _TimerProp.position) <= 0.08f)
        {
            this.AddTimeMethod( 20);
            Debug.Log("add  time  20 ");
            _CurrentProp.GetComponent<PropsBtn>().CorrectPos();
            _CurrentProp.GetComponent<PropsBtn>()._PropsNum--;
            _CurrentProp.GetComponent<PropsBtn>().BtnAct();

        }
        else if (Vector2.Distance(_CurrentProp.transform.position, _RemoveProp.position) <= 0.08f)
        {
            this.RemoveMethod();
            _CurrentProp.GetComponent<PropsBtn>().CorrectPos();
            _CurrentProp.GetComponent<PropsBtn>()._PropsNum--;
            _CurrentProp.GetComponent<PropsBtn>().BtnAct();

          //  Debug.Log("Remove ");
        }
        else if (Vector2.Distance(_CurrentProp.transform.position , _RemoveProp.position) > 0.08f && Vector2.Distance(_CurrentProp.transform.position, _TimerProp.position) > 0.08f)
        {
           // Debug.Log("return it ");
        }
        _IsCtrl = false;
        _CurrentProp = null;
      
    }

    /// <summary>
    /// 时间道具方法
    /// </summary>
    /// <param name="tAddTime"></param>
    void AddTimeMethod( int tAddTime) {
        if (_LevelTime + tAddTime >= DataCon.DataCon_LimitTime)
        {
            
            int tTime =DataCon.DataCon_LimitTime-_LevelTime;
            _LevelTime = DataCon.DataCon_LimitTime;
            _TimeBar.transform.localScale = new Vector3(_TimeBar.transform.localScale.x + tTime * tUnitLength, _TimeBar.transform.localScale.y, _TimeBar.transform.localScale.z);
            _TimeBar.transform.position = new Vector3(_TimeBar.transform.position.x + tTime * tUnitX, _TimeBar.transform.position.y, _TimeBar.transform.position.z);
        }
        else {
            _LevelTime += tAddTime;
            _TimeBar.transform.localScale = new Vector3(_TimeBar.transform.localScale.x + tAddTime * tUnitLength, _TimeBar.transform.localScale.y, _TimeBar.transform.localScale.z);
            _TimeBar.transform.position = new Vector3(_TimeBar.transform.position.x + tAddTime * tUnitX, _TimeBar.transform.position.y, _TimeBar.transform.position.z);     
        }    
    }

    /// <summary>
    /// 消除方法 
    /// </summary>
    void RemoveMethod() {
        _ItemController.GetComponent<ItemCon>().RemoveIDCube(int.Parse(_CurrentProp.name.Substring(5))+( GlobalManager._CurrentBigLevel-1)*5 - 1);
    }


    // Update is called once per frame
    void Update()
    {
        if (_IsCtrl == true)
        {
            _CurrentProp.transform.position = Camera.main.ScreenToViewportPoint(new Vector3( Input.mousePosition.x,Input.mousePosition.y,Input.mousePosition.z +5 ));
        }
        if (IsRotate == true) {
            rotAngle += 1f;
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                this.BackMenuBtn();
               // NativeDialogs.Instance.ShowMessageBox("提示", "真的要退出吗?", new string[] { "退出", "取消" }, (string button) => { QuitHint(button); });
            }
            this.AndroidMenuBtn();
        }
    }

    //public IEnumerator DestroyEffect(Vector2 tPos) {
    //    _DestroyEff.enabled = true;
    //    _DestroyEff.transform.position = tPos;
    //    for (int i = 1; i < 6; i++)
    //    {
    //        _DestroyEff.texture = Resources.Load("CircleEffect/Circle_" + i)as Texture;
    //           yield return new WaitForSeconds(0.07f);
    //    }
    //    _DestroyEff.enabled = false;
    //}


    public IEnumerator ComboEffect(int tCombo)
    {
        switch (tCombo) { 
            case 2:
                _ComboPic[1].enabled = true;
                break;
            case 3:
                _ComboPic[2].enabled = true;
                break;
            case 4:
                _ComboPic[3].enabled = true;
                break;
            case 5:
                _ComboPic[4].enabled = true;
                break;
            default:
                _ComboPic[0].enabled = true;
                break;

        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < _ComboPic.Length; i++) {
            _ComboPic[i].enabled = false;
        }
    }


    void ToQuit()
    { 
         NativeDialogs.Instance.ShowMessageBox(
            "提示",
            "真的要退出吗?",
            new string[] { "退出", "取消" }, (string button) => { QuitHint(button); });
    }


    void QuitHint(string code)
    {
        if (code.Equals("退出"))
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// 安卓的返回键
    /// </summary>
    void AndroidMenuBtn() {
        if (Input.GetKeyDown( KeyCode.Menu)) {
            this.BackMenuBtn();
        }
    }


    /// <summary>
    /// 字体大小适配
    /// </summary>
    void TextSizeCtrl() 
    { 
       // Debug.Log(Screen.width + "   ,   height" + Screen.height);
       // if (Screen.width >= 300) {
            _LevelNum.fontSize = 25;
            _LeftScore.fontSize = 28;
            _RightScore.fontSize = 28;
            _CurrentLevel.fontSize = 20;
            _UsedTime.fontSize = 18;
            _ComboNum.fontSize = 20;
            _ClickTopScore.fontSize = 20;   
            _LosePanelLevelTxt.fontSize = 35;
            _CurrentLevel.fontSize = 25;
        
       // }
    }


    void ShowDialogueBox(GameObject tBox) {
        _AlphaBlack.SetActive(true);
        tBox.transform.position = new Vector3(0,-1,0);
        Time.timeScale = 0;
        tBox.SetActive(true);
        iTween.MoveTo(tBox, iTween.Hash("x", 0, "y", 0, "time", 0.8f, "easetype", "easeOutBounce", "ignoretimescale", true));
    }

}