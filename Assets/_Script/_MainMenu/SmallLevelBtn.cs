using UnityEngine;
using System.Collections;

public class SmallLevelBtn : MonoBehaviour {

    public GameObject _Target;
    string _Method = "GetLevelBtnDown";
    public string _Name;
    public string _ID;
    public GUITexture[] _Star1;
    public GUITexture[] _Star2;
    public GUITexture[] _Star3;

    public GUITexture _Lock;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void ShowState(int tStars) {
        foreach (GUITexture tChild in gameObject.GetComponentsInChildren<GUITexture>())
        {
            tChild.enabled = true;
        }
        switch (tStars) { 
            case 1:
                _Lock.enabled = false;
                _Star1[1].enabled = true;
                _Star1[0].enabled = false;
                _Star2[1].enabled = false;
                _Star2[0].enabled = true;
                _Star3[1].enabled = false;
                _Star3[0].enabled = true;
                break;
            case 2:
                _Lock.enabled = false;
                _Star1[1].enabled = true;
                _Star1[0].enabled = false;
                _Star2[1].enabled = true;
                _Star2[0].enabled = false;
                _Star3[1].enabled = false;
                _Star3[0].enabled = true;
                break;
            case 3:
                _Lock.enabled = false;
                _Star1[1].enabled = true;
                _Star1[0].enabled = false;
                _Star2[1].enabled = true;
                _Star2[0].enabled = false;
                _Star3[1].enabled = true;
                _Star3[0].enabled = false;
                break;
            case 4:
                _Lock.enabled = false;
                _Star1[1].enabled = false;
                _Star1[0].enabled = false;
                _Star2[1].enabled = false;
                _Star2[0].enabled = false;
                _Star3[1].enabled = false;
                _Star3[0].enabled = false;
                break;
            default:
                //foreach (GUITexture tChild in gameObject.GetComponentsInChildren<GUITexture>()) {
                //    if(!tChild.GetComponent<BoxCollider>())
                //        tChild.enabled=false;
                //}
                _Lock.enabled = true;
                _Star1[1].enabled = false;
                _Star1[0].enabled = false;
                _Star2[1].enabled = false;
                _Star2[0].enabled = false;
                _Star3[1].enabled = false;
                _Star3[0].enabled = false;
                //Debug.Log("Hide");
                break;
        }
    }

    public void OnMouseDown()
    {
      //  Debug.Log("Btn  : " + _ID);
        //_Name = gameObject.transform.name;
        _Target.SendMessage(_Method, _ID);
    }
}
