using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ItemCon : MonoBehaviour
{
    /// <summary>
    /// 准备替换位置的物体
    /// </summary>
    private GameObject _ReplaceObject;
    /// <summary>
    /// 物体预制键列表
    /// </summary>
    public List<GameObject> _PreCubes;
    /// <summary>
    /// 物体列表
    /// </summary>
    private List<List<GameObject>> _CubeList = new List<List<GameObject>>();
    private List<List<int>> _CubeIdList = new List<List<int>>();

    private List<GameObject> DesCubeList = new List<GameObject>();

    int _MinNum;
    int _MaxNum;
    int _TagNum;

    public GameObject _BePlaceObj;

    public int MaxCombo = 0;

    // int[] TagID = new int[5] { 0, 0, 0, 0, 0 };
    public GameObject _UIManager;

    public int _ClickTopScore = 0;

    int _ComboNum = 0;
    int _ComboTimes = 0;
    int _ComboID = 5;

    bool IsRotate = false;
    int _Angle = 0;

    bool IsHide = false;
    bool _IsPropEffect = false;

    public GameObject _SoundPlayer;

    public bool _Move = false;
    bool _Count = false;

    /// <summary>
    /// 销毁时附的临时材质
    /// </summary>
    public Material _TemoMtrl_1;
    public Material _TemoMtrl_2;

    //public Color _AlphaColor;

    public void OnEnable()
    {
        InitData();
        InitView();
    }
    /// <summary>
    /// 初始化 cube 数据
    /// </summary>
    public void InitData()
    {
        _CubeIdList.Clear();
        int tXCount = DataCon.DataCon_XCount;
        int tYCount = DataCon.DataCon_YCount;
        for (int y = 0; y <= tYCount; y++)                       //    <tYCount   --->  <= tYCount 
        {
            List<int> tList = new List<int>();
            for (int x = 0; x < tXCount; x++)
            {
                int tIndex = RandCubeNum();
                tList.Add(tIndex);
                //  Debug.Log(tIndex);
            }
            _CubeIdList.Add(tList);
        }
    }
    /// <summary>
    /// 初始UI
    /// </summary>
    public void InitView()
    {
      //  int tXCount = DataCon.DataCon_XCount;
      //  int tYCount = DataCon.DataCon_YCount;
        _CubeList.Clear();
        for (int y = 0; y < _CubeIdList.Count - 1; y++)                //    y <_CubeIdList.Count   --->  y <_CubeIdList.Count-1
        {
            List<GameObject> tList = new List<GameObject>();
            for (int x = 0; x < _CubeIdList[y].Count; x++)
            {
                GameObject tGameObject = InitCube(y, x);
                tList.Add(tGameObject);
            }
            _CubeList.Add(tList);
        }
        StartCoroutine(this.CheckSame());
    }

    /// <summary>
    /// 初始cube
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    private GameObject InitCube(int y, int x)
    {
        GameObject tGameObject = (GameObject)Instantiate(_PreCubes[_CubeIdList[y][x]], new Vector3(x * DataCon.DataCon_X, y * DataCon.DataCon_Y, 10), Quaternion.Euler(new Vector3(0, 90, 0))) as GameObject;
        tGameObject.GetComponent<ItemEntity>().Pos = new Vector2(x, y);
        tGameObject.GetComponent<ItemEntity>().Tag = 1;
        tGameObject.GetComponent<TsButtonMessage>()._Taget = gameObject;
        tGameObject.GetComponent<TsButtonMessage>()._Method = "GetMouseDown";
        return tGameObject;
    }
    /*
    /// <summary>
    /// 获取鼠标点击物体
    /// </summary>
    /// <param name="tPara">触发事件的物体</param>
    public void GetMouseDown(object tPara)
    {

        GameObject tGameObject = (GameObject)tPara;
        ItemEntity tItemEntity = tGameObject.GetComponent<ItemEntity>();
        if (_IsReplace)
        {
            if (CheckNeighbor((int)tGameObject.transform.localPosition.x, (int)tGameObject.transform.localPosition.y))
            {
                _ComboTimes = 0;
                _ComboID = 5;
                //交换坐标
                Vector3 tlocalPosition = _ReplaceObject.transform.localPosition;
                //移动
                this.MoveSM(_ReplaceObject, tGameObject.transform.localPosition);
                this.MoveSM(tGameObject, tlocalPosition);
                //sound
                _SoundPlayer.GetComponent<SoundPlayer>().MoveSound();

                Vector2 tCurPos = tGameObject.GetComponent<ItemEntity>().Pos;
                Vector2 tReplacePos = _ReplaceObject.GetComponent<ItemEntity>().Pos;

                tGameObject.GetComponent<ItemEntity>().Pos = tReplacePos;
                _ReplaceObject.GetComponent<ItemEntity>().Pos = tCurPos;

                _CubeIdList[(int)tCurPos.y][(int)tCurPos.x] = _ReplaceObject.GetComponent<ItemEntity>().Id;
                _CubeIdList[(int)tReplacePos.y][(int)tReplacePos.x] = tGameObject.GetComponent<ItemEntity>().Id;

                _CubeList[(int)tCurPos.y][(int)tCurPos.x] = _ReplaceObject;
                _CubeList[(int)tReplacePos.y][(int)tReplacePos.x] = tGameObject;

                _ReplaceObject = null;
                if (_BePlaceObj == null)
                    _BePlaceObj = _ReplaceObject;
                else
                    _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
                _IsReplace = !_IsReplace;
                StartCoroutine(CheckSame());
                return;
                //检测tag

            }
            else
            {
                if (_BePlaceObj == null)
                    _BePlaceObj = _ReplaceObject;
                else
                    _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
                _ReplaceObject = tGameObject;
                _ReplaceObject.GetComponent<TsButtonMessage>().PulseMove();
                _BePlaceObj = _ReplaceObject;

                return;
            }
        }

        _IsReplace = !_IsReplace;
        _ReplaceObject = tGameObject;
        _ReplaceObject.GetComponent<TsButtonMessage>().PulseMove();
        _BePlaceObj = _ReplaceObject;
    }
    */



    /// <summary>
    /// 替换
    /// </summary>
    /// <param name="tPara"></param>
    /*
        public void GetMouseUp(GameObject tGameObject)
        {
            //GameObject tGameObject = (GameObject)tPara;
            ItemEntity tItemEntity = tGameObject.GetComponent<ItemEntity>();
            if (CheckNeighbor((int)tGameObject.transform.localPosition.x, (int)tGameObject.transform.localPosition.y))
            {

                _ComboTimes = 0;
                _ComboID = 5;
                //交换坐标
                Vector3 tlocalPosition = _ReplaceObject.transform.localPosition;
                //移动
                this.MoveSM(_ReplaceObject, tGameObject.transform.localPosition);
                Debug.Log(tGameObject.transform.localPosition + "     " + _ReplaceObject.transform.localPosition );
                //this.MoveSM(tGameObject, tlocalPosition);
                //Debug.Log("move");
                //sound
                _SoundPlayer.GetComponent<SoundPlayer>().MoveSound();

                Vector2 tCurPos = tGameObject.GetComponent<ItemEntity>().Pos;
                Vector2 tReplacePos = _ReplaceObject.GetComponent<ItemEntity>().Pos;

                tGameObject.GetComponent<ItemEntity>().Pos = tReplacePos;
                _ReplaceObject.GetComponent<ItemEntity>().Pos = tCurPos;

                _CubeIdList[(int)tCurPos.y][(int)tCurPos.x] = _ReplaceObject.GetComponent<ItemEntity>().Id;
                _CubeIdList[(int)tReplacePos.y][(int)tReplacePos.x] = tGameObject.GetComponent<ItemEntity>().Id;

                _CubeList[(int)tCurPos.y][(int)tCurPos.x] = _ReplaceObject;
                _CubeList[(int)tReplacePos.y][(int)tReplacePos.x] = tGameObject;

                _ReplaceObject = null;
                if (_BePlaceObj == null)
                    _BePlaceObj = _ReplaceObject;
                else
                    _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
              //  _IsReplace = !_IsReplace;
                StartCoroutine(CheckSame());
                return;
                //检测tag

            }
            else
            {
                if (_BePlaceObj == null)
                    _BePlaceObj = _ReplaceObject;
                else
                    _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
                _ReplaceObject = tGameObject;
                _ReplaceObject.GetComponent<TsButtonMessage>().PulseMove();
                _BePlaceObj = _ReplaceObject;

                return;
            }
    
        }
    */
    public void GetMouseDown(GameObject tGameObject)
    {
        //GameObject tGameObject = (GameObject)tPara;
        if (IsRotate != true)
        {
            //ItemEntity tItemEntity = tGameObject.GetComponent<ItemEntity>();
            _ReplaceObject = tGameObject;
            _ReplaceObject.GetComponent<TsButtonMessage>().PulseMove();
            _BePlaceObj = _ReplaceObject;
        }
        // Invoke("Exchange", 0.5f);
    }


    public void Exchange()
    {
        _Move = true;
    }

    /// <summary>
    /// 检测是否相邻
    /// </summary>
    /// <param name="tX"></param>
    /// <param name="tY"></param>
    /// <returns></returns>
    bool CheckNeighbor(int tX, int tY)
    {
        if (_ReplaceObject != null)
        {
            if (Mathf.Abs(_ReplaceObject.transform.position.x - tX) <= 1)
            {
                if (Mathf.Abs(tY - _ReplaceObject.transform.position.y) <= 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 随机CubeID
    /// </summary>
    /// <returns></returns>
    int RandCubeNum()
    {
        for (int i = 0; i < GlobalManager._CurrentBigLevel; i++)
        {
            _MinNum = i * 5;
            _MaxNum = (i + 1) * 5;
        }
        int tRandomCube = Random.Range(_MinNum, _MaxNum);
        return tRandomCube;
    }

    void ScoreCount(int tScore)
    {
        _UIManager.SendMessage("ScoreController", tScore * 10);
        Debug.Log("Got Score = " + tScore * 10);
    }

    /// <summary>
    /// 检测相同
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheckSame()
    {
        yield return new WaitForSeconds(0.5f);
       // _ComboTimes = 0;
       // Debug.Log(_ComboTimes);
        _ComboID = 5;
        int tSame = 1;
        int tXCount = DataCon.DataCon_XCount;
        int tYCount = DataCon.DataCon_YCount;
        #region 行检测
        //从第一行开始
        for (int y = 0; y < tYCount; y++)
        {
            tSame = 1;
            int tInitX = 0;
            //从第一列开始 到倒数第二列结束
            for (int x = 0; x < tXCount - 1; x++)
            {

                //检测相同
                if (_CubeIdList[y][x] == _CubeIdList[y][x + 1])
                {
                    if (tSame == 1)
                    {
                        //记录消除开始的点
                        tInitX = x;
                    }
                    tSame++;
                    //达到消除的条件
                    if (tSame >= DataCon.DataCon_MaxSame)
                    {
                        //变更消除TAG
                        for (int i = 0; i < tSame; i++)
                        {
                            _CubeList[y][tInitX + i].GetComponent<ItemEntity>().Tag = 0;

                        }
                        if (_ComboID != _CubeList[y][tInitX].GetComponent<ItemEntity>().Id)
                        {
                            //  _ComboTimes++;
                            _ComboID = _CubeList[y][tInitX].GetComponent<ItemEntity>().Id;
                        }
                    }
                }
                else
                {
                    tSame = 1;
                }
            }
        }
        #endregion
        #region 列检测
        //从第一列开始
        for (int x = 0; x < tXCount; x++)
        {
            tSame = 1;
            int tInitY = 0;
            //从第一行开始 到倒数第二行结束
            for (int y = 0; y < tYCount - 1; y++)
            {
                //检测相同
                if (_CubeIdList[y][x] == _CubeIdList[y + 1][x])
                {
                    if (tSame == 1)
                    {
                        //记录消除开始的点
                        tInitY = y;
                    }
                    tSame++;
                    //达到消除的条件
                    if (tSame >= DataCon.DataCon_MaxSame)
                    {
                        //变更消除TAG
                        for (int i = 0; i < tSame; i++)
                        {
                            _CubeList[tInitY + i][x].GetComponent<ItemEntity>().Tag = 0;
                        }
                        if (_ComboID != _CubeList[tInitY][x].GetComponent<ItemEntity>().Id)
                        {
                            // _ComboTimes++;
                            _ComboID = _CubeList[tInitY][x].GetComponent<ItemEntity>().Id;
                        }
                    }
                }
                else
                {
                    tSame = 1;
                }
            }
        }
        #endregion
        #region 右斜检测
        int tMacCount = tXCount + tYCount - 1;
        for (int x = 0; x < tMacCount; x++)
        {
            Vector2 tInitPos = Vector2.zero;
            int tCount = x + 1;
            int tX = x;
            int tY = 0;
            tSame = 1;
            if (x > tXCount - 1)
            {
                tX = tXCount - 1;
                tY = x - tX;
                tCount = tXCount;
                if (tCount > tYCount - tY)
                {
                    tCount = tYCount - tY;
                }
            }
            if (tCount >= 4)
            {
                for (int y = 0; y < tCount - 1; y++)
                {
                    if (tX - y >= 0 && tX - y <= tXCount - 1 && tY + y >= 0 && tY + y < tYCount - 1)
                    {
                        if (_CubeIdList[tY + y][tX - y] == _CubeIdList[tY + y + 1][tX - y - 1])
                        {
                            if (tSame == 1)
                            {
                                tInitPos = new Vector2(tX - y, tY + y);
                            }
                            tSame++; //达到消除的条件
                            if (tSame >= DataCon.DataCon_MaxSame)
                            {
                                //变更消除TAG
                                for (int i = 0; i < tSame; i++)
                                {
                                    _CubeList[(int)tInitPos.y + i][(int)tInitPos.x - i].GetComponent<ItemEntity>().Tag = 0;

                                }
                                if (_ComboID != _CubeList[(int)tInitPos.y][(int)tInitPos.x].GetComponent<ItemEntity>().Id)
                                {
                                    //  _ComboTimes++;
                                    _ComboID = _CubeList[(int)tInitPos.y][(int)tInitPos.x].GetComponent<ItemEntity>().Id;
                                }
                            }
                        }
                        else
                        {
                            tSame = 1;
                        }
                    }
                }
            }
        }
        #endregion
        #region 左斜检测
        for (int x = 0; x < tMacCount; x++)
        {
            Vector2 tInitPos = Vector2.zero;
            int tCount = x + 1;
            int tX = tXCount - x - 1;
            int tY = 0;
            tSame = 1;
            if (x >= tXCount)
            {
                tX = 0;
                tY = x - tXCount + 1;
                tCount = tXCount;
                if (tCount > tYCount - tY)
                {
                    tCount = tYCount - tY;
                }
            }
            if (tCount >= 4)
            {
                for (int y = 0; y < tCount - 1; y++)
                {
                    if (tX + y < tXCount - 1 && tY + y < tYCount - 1)
                    {
                        if (_CubeIdList[tY + y][tX + y] == _CubeIdList[tY + y + 1][tX + y + 1])
                        {
                            if (tSame == 1)
                            {
                                tInitPos = new Vector2(tX + y, tY + y);
                            }
                            tSame++; //达到消除的条件
                            if (tSame >= DataCon.DataCon_MaxSame)
                            {
                                //变更消除TAG
                                for (int i = 0; i < tSame; i++)
                                {
                                    _CubeList[(int)tInitPos.y + i][(int)tInitPos.x + i].GetComponent<ItemEntity>().Tag = 0;

                                }
                                if (_ComboID != _CubeList[(int)tInitPos.y][(int)tInitPos.x].GetComponent<ItemEntity>().Id)
                                {
                                    // _ComboTimes++;
                                    _ComboID = _CubeList[(int)tInitPos.y][(int)tInitPos.x].GetComponent<ItemEntity>().Id;
                                }
                            }
                        }
                        else
                        {
                            tSame = 1;
                        }
                    }
                }
            }
        }

        if (_Count == true)
        {
        for (int i = 0; i < tXCount-1; i++) {
            for (int j = 0; j < tYCount - 1; j++) {
                if (_CubeList[j][i].GetComponent<ItemEntity>().Tag == 0) {
                    _ComboTimes++;
                }
            }
        }
       
            if (_ClickTopScore < _ComboTimes * 10)
            {
                _ClickTopScore = _ComboTimes * 10;
               
            }
        }

        #endregion
            this.RefreshView();
    }


    /// <summary>
    /// 刷新 消除tag = 0 单位 
    /// </summary>
    public void RefreshView()
    {

        if (IsHide == true)
        {
            for (int i = 0; i < DesCubeList.Count; i++)
            {

                IsRotate = false;
                DesCubeList[i].SetActive(false);
            }
            _ComboNum++;
            IsHide = false;
            CubeFall();

            return;
        }

        int tXCount = DataCon.DataCon_XCount;
        int tYCount = DataCon.DataCon_YCount;

        for (int y = 0; y < tYCount; y++)
        {
            for (int x = 0; x < tXCount; x++)
            {
                if (_CubeList[y][x].GetComponent<ItemEntity>().Tag == 0)
                {
                    //特效
                    DesCubeList.Add(_CubeList[y][x]);
                }
            }
        }


        for (int i = 0; i < DesCubeList.Count; i++)
        {
            DesCubeList[i].GetComponent<BoxCollider>().enabled = false;
        }
        if (DesCubeList.Count != 0)
        {

            IsRotate = true;
        }
        else
        {
            //Debug.Log("combo  :" + _ComboNum);
            if (_ComboNum >= MaxCombo)
            {
                MaxCombo = _ComboNum;
            }

            _ComboNum = 0;
            return;
        }
        this.RotateEffect();
    }



    void Update()
    {
        if (IsRotate == true)
        {

            for (int i = 0; i < DesCubeList.Count; i++)
            {
                DesCubeList[i].transform.Rotate(Vector3.up, 3, Space.World);
            }
            _Angle++;
            _TemoMtrl_1.color -= new Color(0, 0, 0, 0.03f);
            _TemoMtrl_2.color -= new Color(0, 0, 0, 0.03f);
            if (_Angle >= 30)
            {
                for (int i = 0; i < DesCubeList.Count; i++)
                {
                    DesCubeList[i].transform.Find("SparkE").GetComponentsInChildren<ParticleEmitter>()[0].emit = true;
                    DesCubeList[i].transform.Find("SparkE").GetComponentsInChildren<ParticleEmitter>()[1].emit = true;
                }

                _Angle = 0;
                IsHide = true;
                IsRotate = false;
                StartCoroutine(this.CheckSame());
                _SoundPlayer.GetComponent<SoundPlayer>().DestorySound();
            }
        }



        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50))
                {
                    if (hit.transform.gameObject != _ReplaceObject)
                    {
                        GameObject tGameObject = hit.transform.gameObject;
                      //  ItemEntity tItemEntity = tGameObject.GetComponent<ItemEntity>();
                        if (CheckNeighbor((int)tGameObject.transform.localPosition.x, (int)tGameObject.transform.localPosition.y))
                        {

                            _ComboTimes = 0;
                            _Count = true;
                            _ComboID = 5;
                            //交换坐标
                            Vector3 tlocalPosition = _ReplaceObject.transform.localPosition;
                            //移动
                            this.MoveSM(_ReplaceObject, tGameObject.transform.localPosition);
                            this.MoveSM(tGameObject, tlocalPosition);
                            //sound
                            _SoundPlayer.GetComponent<SoundPlayer>().MoveSound();

                            Vector2 tCurPos = tGameObject.GetComponent<ItemEntity>().Pos;
                            Vector2 tReplacePos = _ReplaceObject.GetComponent<ItemEntity>().Pos;

                            tGameObject.GetComponent<ItemEntity>().Pos = tReplacePos;
                            _ReplaceObject.GetComponent<ItemEntity>().Pos = tCurPos;

                            _CubeIdList[(int)tCurPos.y][(int)tCurPos.x] = _ReplaceObject.GetComponent<ItemEntity>().Id;
                            _CubeIdList[(int)tReplacePos.y][(int)tReplacePos.x] = tGameObject.GetComponent<ItemEntity>().Id;

                            _CubeList[(int)tCurPos.y][(int)tCurPos.x] = _ReplaceObject;
                            _CubeList[(int)tReplacePos.y][(int)tReplacePos.x] = tGameObject;

                            _ReplaceObject = null;

                            if (_BePlaceObj == null)
                                _BePlaceObj = _ReplaceObject;
                            else
                                _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
                            StartCoroutine(CheckSame());
                            //  return;
                            //检测tag

                        }
                        else
                        {
                            _ReplaceObject = null;
                            if (_BePlaceObj == null)
                                _BePlaceObj = _ReplaceObject;
                            else
                                _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                _ReplaceObject = null;
                if (_BePlaceObj == null)
                    _BePlaceObj = _ReplaceObject;
                else
                    _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
            }
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 50))
                {
                    if (hit.transform.gameObject != _ReplaceObject)
                    {
                        GameObject tGameObject = hit.transform.gameObject;
                     //   ItemEntity tItemEntity = tGameObject.GetComponent<ItemEntity>();
                        if (CheckNeighbor((int)tGameObject.transform.localPosition.x, (int)tGameObject.transform.localPosition.y))
                        {

                            _ComboTimes = 0;
                            _Count = true;
                            _ComboID = 5;
                            //交换坐标
                            Vector3 tlocalPosition = _ReplaceObject.transform.localPosition;
                            //移动
                            this.MoveSM(_ReplaceObject, tGameObject.transform.localPosition);
                            this.MoveSM(tGameObject, tlocalPosition);
                            //sound
                            _SoundPlayer.GetComponent<SoundPlayer>().MoveSound();

                            Vector2 tCurPos = tGameObject.GetComponent<ItemEntity>().Pos;
                            Vector2 tReplacePos = _ReplaceObject.GetComponent<ItemEntity>().Pos;

                            tGameObject.GetComponent<ItemEntity>().Pos = tReplacePos;
                            _ReplaceObject.GetComponent<ItemEntity>().Pos = tCurPos;

                            _CubeIdList[(int)tCurPos.y][(int)tCurPos.x] = _ReplaceObject.GetComponent<ItemEntity>().Id;
                            _CubeIdList[(int)tReplacePos.y][(int)tReplacePos.x] = tGameObject.GetComponent<ItemEntity>().Id;

                            _CubeList[(int)tCurPos.y][(int)tCurPos.x] = _ReplaceObject;
                            _CubeList[(int)tReplacePos.y][(int)tReplacePos.x] = tGameObject;

                            _ReplaceObject = null;

                            if (_BePlaceObj == null)
                                _BePlaceObj = _ReplaceObject;
                            else
                                _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
                            StartCoroutine(CheckSame());
                            //  return;
                            //检测tag

                        }
                        else
                        {
                            _ReplaceObject = null;
                            if (_BePlaceObj == null)
                                _BePlaceObj = _ReplaceObject;
                            else
                                _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
                        }
                    }
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                _ReplaceObject = null;
                if (_BePlaceObj == null)
                    _BePlaceObj = _ReplaceObject;
                else
                    _BePlaceObj.GetComponent<TsButtonMessage>().StopPulse();
            }
        }

    }


    /// <summary>
    /// 消除隐藏的cube
    /// </summary>
    void DesHideCube()
    {
        for (int i = 0; i < DesCubeList.Count; i++)
        {
            Destroy(DesCubeList[i]);
        }
        //单点击分数

        DesCubeList.Clear();
        _TemoMtrl_1.color = Vector4.one;
        _TemoMtrl_2.color = Vector4.one;
    }

    /// <summary>
    /// 消除后的下降
    /// </summary>
    public void CubeFall()
    {
        int tXCount = DataCon.DataCon_XCount;
        int tYCount = DataCon.DataCon_YCount;

        for (int x = 0; x < tXCount; x++)
        {
            for (int y = tYCount - 1; y >= 0; y--)
            {

                if (_CubeList[y][x].GetComponent<ItemEntity>().Tag == 0 && _CubeList[y][x].activeSelf == false)
                {
                    //计算道具百分比 
                    if (_IsPropEffect == false)
                    {   
                        _UIManager.GetComponent<GameUIManager>().PropsBtnCtrl(_CubeList[y][x].GetComponent<ItemEntity>().Id);
                    }
                    if (y < tYCount)
                    {
                        for (int i = y + 1; i < tYCount; i++)    // i < tYCount;   --> i<= tYCoeunt
                        {

                            Vector3 tlocalPosition = new Vector3(x, i - 1, 10);

                            //移动 
                            this.MoveSM(_CubeList[i][x], tlocalPosition);

                            Vector2 tCurPos = new Vector2(x, i - 1);
                            _CubeList[i][x].GetComponent<ItemEntity>().Pos = tCurPos;
                            _CubeList[i][x].GetComponent<ItemEntity>().Tag = 1;

                            _CubeIdList[(int)tCurPos.y][(int)tCurPos.x] = _CubeList[i][x].GetComponent<ItemEntity>().Id;
                            _CubeList[i - 1][x] = _CubeList[i][x];

                        }
                        _CubeIdList[tYCount][x] = RandCubeNum();
                        GameObject tG = this.InitCube(tYCount, x);
                        _CubeIdList[tYCount - 1][x] = _CubeIdList[tYCount][x];
                        _CubeList[tYCount - 1][x] = tG;
                        //移
                        this.MoveSM(tG, new Vector3(x, tYCount - 1, 10));
                        tG.GetComponent<ItemEntity>().Pos = new Vector2(x, tYCount - 1);

                    }
                    else
                    {
                        _CubeIdList[tYCount][x] = RandCubeNum();
                        _CubeIdList[tYCount - 1][x] = _CubeIdList[tYCount][x];
                        GameObject tG = this.InitCube(tYCount, x);
                        _CubeList[tYCount - 1][x] = tG;
                        //移动
                        this.MoveSM(tG, new Vector3(x, tYCount - 1, 10));
                        tG.GetComponent<ItemEntity>().Pos = new Vector2(x, tYCount - 1);
                    }
                    StartCoroutine(this.CheckSame());
                    this.CheckTagID();
                }
            }
        }
        this.DesHideCube();
        _IsPropEffect = false;
    }

    /// <summary>
    /// 记分器
    /// </summary>
    public void CheckTagID()
    {

        _TagNum++;
        _UIManager.SendMessage("ScoreController", 10);
    }

    /// <summary>
    /// 消除物体 检测id
    /// </summary>
    /// <param name="tID"></param>
    public void RemoveIDCube(int tID)
    {
        int tXCount = DataCon.DataCon_XCount;
        int tYCount = DataCon.DataCon_YCount;
        _IsPropEffect = true;

        for (int x = 0; x < tXCount; x++)
        {
            for (int y = tYCount - 1; y >= 0; y--)
            {
                if (_CubeIdList[y][x] == tID)
                {
                    _CubeList[y][x].GetComponent<ItemEntity>().Tag = 0;
                }
            }
        }
        this.RefreshView();
    }
    /// <summary>
    /// 校对位置 
    /// </summary>
    /// <param name="tCube"></param>
    /// <param name="tTarget"></param>
    void MoveSM(GameObject tCube, Vector3 tTarget)
    {
        tCube.SendMessage("MoveToTarget", tTarget);
    }



    Vector2 JudgeDir(Vector3 OrgPos, Vector3 NewPos)
    {
        Vector2 tOrgPos = new Vector2(OrgPos.x, OrgPos.y);
        Vector2 tNewPos = new Vector2(NewPos.x, NewPos.y);
        Vector2 tDir = tNewPos - tOrgPos;
        float tDegree = Mathf.PI / 8;


        if (tDir.y > 0)
        {
            if (Mathf.Atan(tDir.y / tDir.x) <= tDegree)
            {
                //right
                return new Vector2(1, 0);
            }
            else if (Mathf.Atan(tDir.y / tDir.x) <= 3 * tDegree && Mathf.Atan(tDir.y / tDir.x) > tDegree)
            {
                // right +  up
                return new Vector2(1, 1);
            }
            else if (Mathf.Atan(tDir.y / tDir.x) <= 5 * tDegree && Mathf.Atan(tDir.y / tDir.x) > 3 * tDegree)
            {
                //up
                return new Vector2(0, 1);
            }
            else if (Mathf.Atan(tDir.y / tDir.x) <= 7 * tDegree && Mathf.Atan(tDir.y / tDir.x) > 5 * tDegree)
            {
                // left + up
                return new Vector2(-1, 1);
            }
            else if (Mathf.Atan(tDir.y / tDir.x) > 7 * tDegree && Mathf.Atan(tDir.y / tDir.x) < 8 * tDegree)
            {
                //left
                return new Vector2(-1, 0);
            }
            else
            {
                return new Vector2(0, 0);
            }

        }
        else
        {
            if (Mathf.Atan(tDir.y / tDir.x) <= tDegree)
            {
                //right
                return new Vector2(1, 0);
            }
            else if (Mathf.Atan(tDir.y / tDir.x) <= 3 * tDegree && Mathf.Atan(tDir.y / tDir.x) > tDegree)
            {
                // right +  down
                return new Vector2(1, -1);
            }
            else if (Mathf.Atan(tDir.y / tDir.x) <= 5 * tDegree && Mathf.Atan(tDir.y / tDir.x) > 3 * tDegree)
            {
                //down
                return new Vector2(0, -1);
            }
            else if (Mathf.Atan(tDir.y / tDir.x) <= 7 * tDegree && Mathf.Atan(tDir.y / tDir.x) > 5 * tDegree)
            {
                // left + down
                return new Vector2(-1, -1);
            }
            else if (Mathf.Atan(tDir.y / tDir.x) > 7 * tDegree && Mathf.Atan(tDir.y / tDir.x) < 8 * tDegree)
            {
                //left
                return new Vector2(-1, 0);
            }
            else
            {
                return new Vector2(0, 0);
            }

        }
    }


    void RotateEffect()
    {
        //_TemoMtrl_2
        if (DesCubeList.Count != 0)
        {
            int tID = DesCubeList[0].GetComponent<ItemEntity>().Id;
            for (int i = 0; i < DesCubeList.Count; i++)
            {
                if (tID == DesCubeList[i].GetComponent<ItemEntity>().Id)
                {
                    _TemoMtrl_1.mainTexture = DesCubeList[i].GetComponent<MeshRenderer>().material.mainTexture;
                    DesCubeList[i].GetComponent<MeshRenderer>().material = _TemoMtrl_1;
                }
                else
                {
                    _TemoMtrl_2.mainTexture = DesCubeList[i].GetComponent<MeshRenderer>().material.mainTexture;
                    DesCubeList[i].GetComponent<MeshRenderer>().material = _TemoMtrl_2;
                }
            }
        }
        else return;
    }


}


