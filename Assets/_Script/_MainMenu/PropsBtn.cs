using UnityEngine;
using System.Collections;

public class PropsBtn : MonoBehaviour {
    /// <summary>
    /// 点击 发送信息物体 + 方法
    /// </summary>
    public GameObject _Taget;
    string _Method ;
   
    /// <summary>
    /// 道具数 
    /// </summary>
    public int _PropsNum;
    public GUIText NumTexture;
    /// <summary>
    /// 自身状态
    /// </summary>
    bool _IsActive;
    /// <summary>
    /// 道具积累数据 
    /// </summary>
    public int _LeftNum = 0;
    public int _RightNum = 100;
    /// <summary>
    /// 
    /// </summary>
    public GUIText _PropsNumText;
    /// <summary>
    /// 记录原始位置
    /// </summary>
    Vector3 _MyPos;

    /// <summary>
    /// 道具个数  两位
    /// </summary>
    public Texture LNums;
    public Texture RNums;



    void Start() {
        this.FontSizeCtrl();
        this.BtnAct();
        _MyPos = gameObject.transform.position;
    }

    /// <summary>
    /// 检测是否活跃
    /// </summary>
    public void BtnAct() {
        if (_PropsNum > 0)
        {
            gameObject.GetComponent<GUITexture>().color = new Color(0.5f, 0.5f, 0.5f, 0.6f);
            _IsActive = true;
        }
        else
        {
            _PropsNum = 0;
            gameObject.GetComponent<GUITexture>().color = new Color(200/256f, 200/256f, 200/256f, 33/256f);
            _IsActive = false;
        }
        this.ChangeNumTexture();
    }

    /// <summary>
    /// 道具百分比
    /// </summary>
    void ChangeNumTexture() {
        NumTexture.text = _LeftNum + "/" + _RightNum;

        _PropsNumText.text = _PropsNum.ToString();
    }

    /// <summary>
    /// 道具数
    /// </summary>
    /// <param name="tDelta"></param>
    public void PropsNumCount(int tDelta){
        
        if (_LeftNum + tDelta >= _RightNum)
        {
            _LeftNum = _LeftNum + tDelta - _RightNum;
            _PropsNum++;
        }
        else {
            _LeftNum += tDelta;
        }
        this.BtnAct();
    }


    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    /// 
    public void OnMouseDown()
    {
        if (_IsActive == true)
        {
            _Taget.SendMessage("PropsBtnCtrl", gameObject);
        }
    }

    /// <summary>
    /// 鼠标离开事件
    /// </summary>
    public void OnMouseUp() {
        _Taget.SendMessage("LosePropsBtnCtrl");
        iTween.MoveTo(gameObject, iTween.Hash("time", 0.5f, "x", _MyPos.x, "y", _MyPos.y, "z", _MyPos.z, "easetype", "linear", "ignoretimescale",true));
        Invoke("CorrectPos",0.7f);
    }

    /// <summary>
    /// 校正位置
    /// </summary>
   public void CorrectPos() {
        gameObject.transform.position = _MyPos;
    }

   void FontSizeCtrl() {  
       //if (Screen.width >=400) {
           gameObject.transform.parent.Find("Text_Rates").GetComponent<GUIText>().fontSize = 20;
          // Debug.Log("in");
      // }
   }
}
