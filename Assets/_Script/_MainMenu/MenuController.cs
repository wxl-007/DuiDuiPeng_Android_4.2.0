using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {

	

    string _CurrentPanel = "";

    public GUITexture _BeginBtn;
    public GUITexture _RecordBtn;
    public GUITexture _HelpBtn;
    public GUITexture _QuitBtn;
   // public GameObject _MainBtns;

    public GUITexture _LeftBtn;
    public GUITexture _RightBtn;
    public GUITexture _BackBtn;
   // public GameObject _BigLevelBtns;

    public GUITexture _SoundBtn;

    string _MainPanel = "mainMenu";
    string _Panel_1 = "panel_1";
 //   string _Panel_2 = "panel_2";
 //   string _Panel_3 = "panel_3";
 //   string _Panel_4 = "panel_4";
 //   string _Panel_5 = "panel_5";
    string _Help_Panel = "helpPanel";
    string _Record_Panel = "recordPanel";

    public GUITexture _Title;

    public GUITexture[] _Panels;
    

 //   public GUITexture[] _SamllLevel;
 //   public GameObject _SmallLevels;

    public GameObject _HelpPanelObjs;

    public GameObject _RecordPanels;

    public GameObject[] _SmallLevelBtns;
    public GameObject[] _TempLevels;
    public GameObject _BigPanelObjs;
    public GameObject _BigPanelSet;
    public GameObject _MainPanelObjs;

   // private GameObject _SmallLevelPanels;


    public GUIText _BestScore;
    public GUIText _AverageScore;
    public GUIText _MaxTime;
    public GUIText _MinTime;
    public GUIText _BestLevel;
    public GUIText _MaxCombo;
    public GUIText _SumCombo;

    public Texture _BeginBtn_1;
    public Texture _BeginBtn_2;

    public GameObject _SmallLvlPrefab;
    int _CurBigLevelNum;

    public GameObject _SoundPlayer;

    bool _DirActive = false;


    void Awake() {
        GlobalManager.LoadAllPlayerPrefs();
        _BestScore.text = PlayerPrefs.GetInt("BestScore").ToString();
        _AverageScore.text = PlayerPrefs.GetInt("AvarageClickScore").ToString();
        _MinTime.text = PlayerPrefs.GetInt("MinFinishTime").ToString() ;
        _MaxTime.text =PlayerPrefs.GetInt("MaxFinishTime").ToString() ;
        _MaxCombo.text = PlayerPrefs.GetInt("MaxCombo").ToString() ;
        _SumCombo.text =  PlayerPrefs.GetInt("SumCombo").ToString();
        _BestScore.text = PlayerPrefs.GetInt("OnceClickScore").ToString();
        _BestLevel.text = PlayerPrefs.GetString("BestLevel");
    }

    void Start()
    {
        if (GlobalManager._BackLevel == true)
        {
            _CurBigLevelNum = GlobalManager._CurrentBigLevel;
            this.EnterBigLevelPanel(_CurBigLevelNum);
        }
        _CurrentPanel = GlobalManager._CurrentPanel;
        if (_CurrentPanel == _MainPanel)
            InvokeRepeating("BeginBtnEffect",1,1);
    }

    /// <summary>
    /// 开始按钮 猫爪效果
    /// </summary>
    void BeginBtnEffect()
    {
        if (_BeginBtn.GetComponent<GUITexture>().texture != _BeginBtn_1)
        {
            _BeginBtn.GetComponent<GUITexture>().texture = _BeginBtn_1;
        }
        else
        {
            _BeginBtn.GetComponent<GUITexture>().texture = _BeginBtn_2;
        }
    }


    void Update()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                NativeDialogs.Instance.ShowMessageBox("提示", "真的要退出吗?", new string[] { "退出", "取消" }, (string button) => { QuitHint(button); });
            }
        }
    }

    void QuitHint(string code)
    {
        if (code.Equals("退出"))
        {
            Application.Quit();
        }
    }


    void ToQuit()
    {
        NativeDialogs.Instance.ShowMessageBox(
            "提示",
            "真的要退出吗?",
            new string[] { "退出", "取消" }, (string button) => { QuitHint(button); });
    }


    /// <summary>
    /// 初始化小关卡
    /// </summary>
     void InitSmallLevel(int tCurBigLevel,int tDir) {
         //_TempLevels = _SmallLevelBtns;
         if (_CurBigLevelNum == 1) _LeftBtn.enabled = false;
         else _LeftBtn.enabled = true;
         if (_CurBigLevelNum == 5) _RightBtn.enabled = false;
         else _RightBtn.enabled = true;
        //初始化每个小关卡
         if (_BigPanelSet.transform.Find("Panel_" + tCurBigLevel).FindChild("SmalLevel(Clone)") != null)
         {
             for (int i = 0; i < 4; i++) {
                 for (int j = 0; j < 4; j++) {
                     if (_BigPanelSet.transform.Find("Panel_" + tCurBigLevel).FindChild("SmalLevel(Clone)").GetComponent<SmallLevelBtn>()._ID == ((i+1)*4 +(j+1)).ToString())
                     {
                         //赋值进数组
                        // Debug.Log(_BigPanelSet.transform.Find("Panel_" + tCurBigLevel).FindChild("SmalLevel(Clone)").gameObject);
                         _SmallLevelBtns[i * 4 + j] = _BigPanelSet.transform.Find("Panel_" + tCurBigLevel).FindChild("SmalLevel(Clone)").gameObject;
                         _SmallLevelBtns[i * 4 + j].GetComponent<SmallLevelBtn>()._Target = gameObject;
                         _SmallLevelBtns[i * 4 + j].GetComponent<SmallLevelBtn>()._ID = (i * 4 + j + 1).ToString();
                         this.SmallLevelSize(_SmallLevelBtns[i * 4 + j]);
                     }
                 }

             }
             Debug.Log("Write into  = 1");
             return;
         }

         for(int j = 0;j < 16;j++){
            _TempLevels[j]= _SmallLevelBtns[j];
         }
       
        for (int i = 0; i < 4; i++) {
            for (int j = 0; j < 4; j++) {
                GameObject tSmallLevel =  (GameObject)Instantiate(_SmallLvlPrefab, new Vector3(0.26f + j * 0.17f + tDir, 0.60f - i * 0.125f, 20),Quaternion.identity);
                tSmallLevel.transform.parent = _BigPanelSet.transform.Find("Panel_" + tCurBigLevel);
                _SmallLevelBtns[i*4 + j] = tSmallLevel; 
                _SmallLevelBtns[i*4 + j].GetComponent<SmallLevelBtn>()._Target = gameObject;
                _SmallLevelBtns[i*4 + j].GetComponent<SmallLevelBtn>()._ID =(i*4 + j + 1).ToString();
                this.SmallLevelSize(_SmallLevelBtns[i * 4 + j]);
            }
        }
       // Debug.Log("Write into =2");
    }


     void SmallLevelSize(GameObject tSmallObj) {
         float tH = Screen.height;
         float tW = Screen.width;
         float tRate = tW / tH;
         if (tRate <= 0.6f)
         {
             tSmallObj.transform.localScale = new Vector3(0.16f, 0.11f, 1);
         }
         else {
             tSmallObj.transform.localScale = new Vector3(0.14f, 0.11f, 1);
         }
     }

    /// <summary>
    /// 大关卡切换
    /// </summary>
    /// <param name="tDir"></param>
     void PanelChangeEffect(int tDir) {
         //关卡加入缓存
         //if (_SmallLevelBtns[0] != null && _CurBigLevelNum != 1)
         //{
         //    Debug.Log("temp []");
         //    _TempLevels = _SmallLevelBtns;
         //}

         if (_CurBigLevelNum + tDir < 6 && _CurBigLevelNum + tDir > 0)
         {
             _CurBigLevelNum += tDir;
         }
         else 
             return;
         this.InitSmallLevel(_CurBigLevelNum,tDir);
         this.ShowSmallLevel(_CurBigLevelNum);

         if(tDir < 0){
             iTween.MoveTo(_BigPanelSet, iTween.Hash("x", _BigPanelSet.transform.localPosition.x + 1, "time", 0.7f, "easetype", "spring"));
         }
         else{
             iTween.MoveTo(_BigPanelSet, iTween.Hash("x", _BigPanelSet.transform.localPosition.x - 1, "time", 0.7f, "easetype", "spring"));
         }
        
         Invoke("FixPos",0.8f);
     }

    /// <summary>
    /// 修正位置
    /// </summary>
     void FixPos() {
         _BigPanelSet.transform.position = new Vector3(-_CurBigLevelNum, 0, 0);
         _DirActive = false;

         //清楚缓存
         for (int i = 0; i < _TempLevels.Length; i++) {
             //Debug.Log(_TempLevels[i].GetComponent<SmallLevelBtn>()._ID);
             Destroy(_TempLevels[i]);
         }
        // _TempLevels = null;
     }

    /// <summary>
    /// 进入主界面
    /// </summary>

     void EnterBigLevelPanel(int tCurBigNum ) {
         if (_SmallLevelBtns[0]= null)
         {
             for (int j = 0; j < 16; j++)
             {
                 _TempLevels[j] = _SmallLevelBtns[j];
             }
         }
         //进入大关卡
         _MainPanelObjs.SetActive(false);
         _BigPanelObjs.SetActive(true);
         this.InitSmallLevel(tCurBigNum,tCurBigNum -1);
         this.ShowSmallLevel(tCurBigNum);
     }

    /// <summary>
    /// 鼠标相应 
    /// </summary>
    /// <param name="tBtnName"></param>
    public void GetMouseDown(string tBtnName) {

        switch (tBtnName)
        {
            case "BeginBtn":
                _CurrentPanel = _Panel_1;
                _SoundPlayer.GetComponent<SoundPlayer>().StartBtnSound();
                _CurBigLevelNum = 1;
                EnterBigLevelPanel(_CurBigLevelNum);
                break;
            case "HelpBtn":
                Debug.Log("HelpBtn");
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                _CurrentPanel = _Help_Panel;
                _HelpPanelObjs.SetActive(true);
                this.ChangePanel();
                break;
            case "RecordBtn":
                Debug.Log("RecordBtn");
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                _CurrentPanel = _Record_Panel;
                this.ChangePanel();
                this.FontSizeCtrl();
                break;
            case "LeftBtn":
                this.LBtnEffect();
                break;
            case "RightBtn":
                this.RBtnEffect();
                break;
            case "SoundBtn":
                this.SoundBtnCtrl();
                break;  
            case "BackBtn":
                _CurrentPanel = _MainPanel;
                _CurBigLevelNum = 1;
                _BigPanelSet.transform.position = new Vector3( -1 , 0,0);
                this.ChangePanel();
                _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
                break;
            case"QuitBtn":
                ToQuit();
                break;   
        }   
    }

    IEnumerator PageSound() {
        _SoundPlayer.GetComponent<SoundPlayer>().BtnSound();
        yield return new WaitForSeconds(0.3f);
        _SoundPlayer.GetComponent<SoundPlayer>().BigLevelPageSound();
    }

    void SoundBtnCtrl() {
        _SoundPlayer.GetComponent<SoundPlayer>().SoundCtrl();
    }

    void RBtnEffect() {
        if (_DirActive == false)
        {
            _DirActive = true;
            StartCoroutine(PageSound());
            this.PanelChangeEffect(+1);
           // Debug.Log("RightBtn");
        }
        else  return; 
    }

    void LBtnEffect() {
        if (_DirActive == false)
        {
            _DirActive = true;
            StartCoroutine(PageSound());
            this.PanelChangeEffect(-1);
            //Debug.Log("LeftBtn");
        }
        else return;
    }

    /// <summary>
    /// / 0 未解锁 。1，2，3 颗星 。4解锁未通过
    /// </summary>
    /// <param name="tBigLevel"></param>
    void ShowSmallLevel(int tBigLevel)
    {
        string tLevelState = "";
        switch (tBigLevel)
        {
            case 1:
                tLevelState = PlayerPrefs.GetString("BigLevel_" + tBigLevel, GlobalManager._BigLevel_1);
                break;
            case 2:
                tLevelState = PlayerPrefs.GetString("BigLevel_" + tBigLevel, GlobalManager._BigLevel_2);
                break;
            case 3:
                tLevelState = PlayerPrefs.GetString("BigLevel_" + tBigLevel, GlobalManager._BigLevel_3);
                break;
            case 4:
                tLevelState = PlayerPrefs.GetString("BigLevel_" + tBigLevel, GlobalManager._BigLevel_4);
                break;
            case 5:
                tLevelState = PlayerPrefs.GetString("BigLevel_" + tBigLevel, GlobalManager._BigLevel_5);
                break;
        }

        // Debug.Log(tLevelState);
        for (int i = 0; i < tLevelState.Length; i++)
        {
            switch (tLevelState[i])
            {
                case '1':
                    _SmallLevelBtns[i].GetComponent<SmallLevelBtn>().ShowState(1);
                    break;
                case '2':
                    _SmallLevelBtns[i].GetComponent<SmallLevelBtn>().ShowState(2);
                    break;
                case '3':
                    _SmallLevelBtns[i].GetComponent<SmallLevelBtn>().ShowState(3);
                    break;
                case '4':
                    _SmallLevelBtns[i].GetComponent<SmallLevelBtn>().ShowState(4);
                    break;
                default:
                    _SmallLevelBtns[i].GetComponent<SmallLevelBtn>().ShowState(5);
                    break;
            }
        }
    }


    /// <summary>
    /// 点击关卡的传入当前关卡数据
    /// </summary>
    /// <param name="tID"></param>
    public void GetLevelBtnDown(string tID)
    {
        GlobalManager._CurrentSmallLevel = int.Parse(tID) ; 
        GlobalManager._CurrentBigLevel = _CurBigLevelNum;
        GlobalManager.UpdateLevelData();
        Application.LoadLevel("LoadingScene");
        GlobalManager._CurrentPanel = _CurrentPanel;
    }


    /// <summary>
    /// 字体尺寸调整
    /// </summary>
    void FontSizeCtrl()
    {
        float tH = Screen.height;
        float tW = Screen.width;
        float tRate = tW / tH;
        if (tRate <= 0.6f)
        {
            _BestScore.fontSize = 17;
            _AverageScore.fontSize = 15;
            _MaxTime.fontSize = 17;
            _MinTime.fontSize = 15;
            _BestLevel.fontSize = 15;
            _MaxCombo.fontSize = 15;
            _SumCombo.fontSize = 15;
        }
        else
        {
            _BestScore.fontSize = 15;
            _AverageScore.fontSize = 15;
            _MaxTime.fontSize =17;
            _MinTime.fontSize = 15;
            _BestLevel.fontSize = 15;
            _MaxCombo.fontSize = 15;
            _SumCombo.fontSize = 15;
        }
        
        
    }


    /// <summary>
    /// 改变大关卡背景
    /// </summary>
    void ChangePanel() {
      if (_CurrentPanel == _MainPanel)
        {
            _MainPanelObjs.SetActive(true);
            _BigPanelObjs.SetActive(false);
            _HelpPanelObjs.SetActive(false);        
            _RecordPanels.SetActive(false);
        }
        else if (_CurrentPanel == _Help_Panel)
        {
            _MainPanelObjs.SetActive(false);
            _BigPanelObjs.SetActive(false);
            _HelpPanelObjs.SetActive(true);
            _RecordPanels.SetActive(false);
        }
        else if (_CurrentPanel == _Record_Panel)
        {
            _RecordPanels.SetActive(true);
            _HelpPanelObjs.SetActive(false);
            _MainPanelObjs.SetActive(false);
        }
    }  

    

}
