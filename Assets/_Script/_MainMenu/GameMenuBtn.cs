using UnityEngine;
using System.Collections;

public class GameMenuBtn : MonoBehaviour {


    public GameObject _Target;
    string _MethodName = "GetMouseDown";

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown() {
        //Debug.Log("sendMessage" + gameObject.transform.name );
        _Target.SendMessage(_MethodName,gameObject.transform.name);
    }
}
