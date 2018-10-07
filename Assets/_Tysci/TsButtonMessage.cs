using UnityEngine;
using System.Collections;

public class TsButtonMessage : MonoBehaviour 
{
    /// <summary>
    /// 广播接收器
    /// </summary>
    [HideInInspector]
    public GameObject _Taget;
    public string _Method;
    Vector3 _CorrectTarget;
     bool IsPulse = false;
    string _Mood = "0";
    bool _IsBreath = true;
    public bool _ActiveBtn = true;
    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    public void OnMouseDown()
    {
        //Debug.Log( "" +  gameObject.transform.localPosition);
        if (gameObject.GetComponent<BoxCollider>().enabled == true)
        {
            if (Time.timeScale == 1)
            {
                _Taget.SendMessage(_Method, gameObject);
            }
        }
    }


    /*
    public void OnMouseUp()
    {
        Debug.Log("Up    " + gameObject.transform.localPosition);

        if (gameObject.GetComponent<BoxCollider>().enabled == true)
        {
            if (Time.timeScale == 1)
            {
                _Taget.SendMessage(_Method_Up, gameObject);
            }
        }

    }
    */


    public void MoveToTarget(Vector3 tTarget) {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        _CorrectTarget = tTarget;
        iTween.MoveTo(gameObject,iTween.Hash("x",tTarget.x,"y",tTarget.y,"z",tTarget.z,"time",0.3f,"easetype","linear"));
        Invoke("MoveCorrect",0.6f);
    }

    void MoveCorrect() {
         gameObject.GetComponent<BoxCollider>().enabled =  true;
        gameObject.transform.position = _CorrectTarget;
      
    }

    public void PulseMove( ) {
        IsPulse = ! IsPulse;
       // Debug.Log(IsPulse);
        if (IsPulse == true)
        {
            _Mood = "1";
        }
        else {
            _Mood = "0";
            gameObject.transform.localScale = Vector3.one * 0.9f;
        }
    }

    public void StopPulse() {
        IsPulse = false;
        _Mood = "1";
        gameObject.transform.localScale = Vector3.one * 0.9f;
    }


    void Update() {
        if (IsPulse == true && _Mood == "1") {
          
            if (_IsBreath == true)
            {
                if (gameObject.transform.localScale.x <= 0.9f)
                {
                    gameObject.transform.localScale += Vector3.one * 0.003f;
                    _IsBreath = false;
                }
                else
                {
                    gameObject.transform.localScale -= Vector3.one * 0.003f;
                    _IsBreath = true;
                }
            }
            else {  
                if (gameObject.transform.localScale.x >= 1)
                {
                    gameObject.transform.localScale -= Vector3.one * 0.003f;
                    _IsBreath = true;
                }
                else {
                    gameObject.transform.localScale += Vector3.one * 0.003f;
                    _IsBreath = false;
                }
            }
        }
    }
}
