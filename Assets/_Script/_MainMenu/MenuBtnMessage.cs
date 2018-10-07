using UnityEngine;
using System.Collections;

public class MenuBtnMessage : MonoBehaviour {

    //[HideInInspector]
    public GameObject _Taget;
    string _Method = "GetMouseDown";
    string _Name;
    /// <summary>
    /// 鼠标点击事件
    /// </summary>
    public void OnMouseDown()
    {
    //   Debug.Log(gameObject.name + gameObject.transform.position);

        _Name = gameObject.transform.name;
        _Taget.SendMessage(_Method, _Name);
    }
}
