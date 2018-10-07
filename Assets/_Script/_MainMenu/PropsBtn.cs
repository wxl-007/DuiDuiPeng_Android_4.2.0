using UnityEngine;
using System.Collections;

public class PropsBtn : MonoBehaviour {
    /// <summary>
    /// ��� ������Ϣ���� + ����
    /// </summary>
    public GameObject _Taget;
    string _Method ;
   
    /// <summary>
    /// ������ 
    /// </summary>
    public int _PropsNum;
    public GUIText NumTexture;
    /// <summary>
    /// ����״̬
    /// </summary>
    bool _IsActive;
    /// <summary>
    /// ���߻������� 
    /// </summary>
    public int _LeftNum = 0;
    public int _RightNum = 100;
    /// <summary>
    /// 
    /// </summary>
    public GUIText _PropsNumText;
    /// <summary>
    /// ��¼ԭʼλ��
    /// </summary>
    Vector3 _MyPos;

    /// <summary>
    /// ���߸���  ��λ
    /// </summary>
    public Texture LNums;
    public Texture RNums;



    void Start() {
        this.FontSizeCtrl();
        this.BtnAct();
        _MyPos = gameObject.transform.position;
    }

    /// <summary>
    /// ����Ƿ��Ծ
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
    /// ���߰ٷֱ�
    /// </summary>
    void ChangeNumTexture() {
        NumTexture.text = _LeftNum + "/" + _RightNum;

        _PropsNumText.text = _PropsNum.ToString();
    }

    /// <summary>
    /// ������
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
    /// ������¼�
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
    /// ����뿪�¼�
    /// </summary>
    public void OnMouseUp() {
        _Taget.SendMessage("LosePropsBtnCtrl");
        iTween.MoveTo(gameObject, iTween.Hash("time", 0.5f, "x", _MyPos.x, "y", _MyPos.y, "z", _MyPos.z, "easetype", "linear", "ignoretimescale",true));
        Invoke("CorrectPos",0.7f);
    }

    /// <summary>
    /// У��λ��
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
