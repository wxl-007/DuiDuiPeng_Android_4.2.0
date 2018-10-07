using UnityEngine;
using System.Collections;

public class LoadingCtrl : MonoBehaviour {
    public GUITexture _LoadingBar;
    public Texture[] _LoadingPic;

    public GameObject _CurrentPic;
    
    private AsyncOperation asy;

    int _Num = 0;
    void Start() {
        _Num = 0;
        InvokeRepeating("LoadPicCtrl",0.2f,0.2f);

    }

    void LoadPicCtrl() {
        _Num++;
        //Òì²½¼ÓÔØ
        if (_Num >= 8)
        {
            Application.LoadLevelAsync("GameScene");

        }

        for (int i = 0; i < 5; i++)
        {
            if (_CurrentPic.GetComponent<GUITexture>().texture == _LoadingPic[i]) {
                if (i + 1 < 5)
                {
                    _CurrentPic.GetComponent<GUITexture>().texture = _LoadingPic[i + 1];
                }
                else
                {
                    _CurrentPic.GetComponent<GUITexture>().texture = _LoadingPic[0];

                }
                return;
            }
            
        }
        
    }
}
