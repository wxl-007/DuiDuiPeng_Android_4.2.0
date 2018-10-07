using UnityEngine;
using System.Collections;

public class SoundPlayer : MonoBehaviour {


    public AudioClip BGM;
   // public GameObject playerPrefab;
    float _VolumeRange;
    public Texture _SoundPic_On;
    public Texture _SoundPic_Off;
    public GameObject[] _SoundBtn;

    public Texture _GSSPic_On;
    public Texture _GSSPic_Off;

    public AudioClip _BtnEffect;
    public AudioClip _StartBtnEffect;

    public AudioClip _LosePanelSound;
    public AudioClip _WinPanelSound;

    public AudioClip _PageSound;

    public AudioClip _DestorySound;
    public AudioClip _MoveSound;


    void Start()
    {
        if (Application.loadedLevelName == "GameScene")
        {
            PlayBGM();
            _SoundBtn = GameObject.FindGameObjectsWithTag("Sound");
            if (GlobalManager._SoundEffect == 1)
            {
                for (int i = 0; i < _SoundBtn.Length; i++)
                {
                    _SoundBtn[i].GetComponent<GUITexture>().texture = _GSSPic_On;
                }
                gameObject.GetComponent<AudioSource>().volume = 1;
            }
            else
            {
                for (int i = 0; i < _SoundBtn.Length; i++)
                {
                    _SoundBtn[i].GetComponent<GUITexture>().texture = _GSSPic_Off;
                }
                gameObject.GetComponent<AudioSource>().volume = 0;
            }
        }
        else
        {

            PlayBGM();
            if (GlobalManager._SoundEffect == 1)
            {
                for (int i = 0; i < _SoundBtn.Length; i++)
                {
                    _SoundBtn[i].GetComponent<GUITexture>().texture = _SoundPic_On;
                }
                gameObject.GetComponent<AudioSource>().volume = 1;
            }
            else
            {
                for (int i = 0; i < _SoundBtn.Length; i++)
                {
                    _SoundBtn[i].GetComponent<GUITexture>().texture = _SoundPic_Off;
                }
                gameObject.GetComponent<AudioSource>().volume = 0;
            }
        }
    }

    public void Init()
    {
        if (GlobalManager._SoundEffect == 1)
        {
            for (int i = 0; i < _SoundBtn.Length; i++)
            {
                _SoundBtn[i].GetComponent<GUITexture>().texture = _SoundPic_On;
            }
            gameObject.GetComponent<AudioSource>().volume = 1;
        }
        else
        {
            for (int i = 0; i < _SoundBtn.Length; i++)
            {
                _SoundBtn[i].GetComponent<GUITexture>().texture = _SoundPic_On;
            }
            gameObject.GetComponent<AudioSource>().volume = 0;
        }
    }


    public void PlayBGM()
    {
        gameObject.audio.clip = BGM;
        gameObject.audio.volume = PlayerPrefs.GetFloat("BGMVolume");
        gameObject.audio.loop = true;
        gameObject.audio.Play();
    }
    /// <summary>
    /// ∆’Õ®∞¥≈•…˘“Ù
    /// </summary>
    public void BtnSound() {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_BtnEffect);
    }
    public void StartBtnSound() {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_StartBtnEffect);
    }

        
    /// <summary>
    /// ∑≠“≥“Ù–ß
    /// </summary>
    public void BigLevelPageSound() {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_PageSound);
    }

    public void GSSoundCtrl() {
        if (GlobalManager._SoundEffect == 1)
        {
            GlobalManager._SoundEffect = 0;
            _SoundBtn[0].GetComponent<GUITexture>().texture = _GSSPic_Off;
            gameObject.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            GlobalManager._SoundEffect = 1;
            _SoundBtn[0].GetComponent<GUITexture>().texture = _GSSPic_On;
            gameObject.GetComponent<AudioSource>().volume = 1;
        }
        GlobalManager.SaveAllPlayerPrefs();
    }

    /// <summary>
    /// ∞¥≈•◊¥Ã¨«–ªª
    /// </summary>
   public void SoundCtrl() {
        if (GlobalManager._SoundEffect == 1)
        {
            GlobalManager._SoundEffect = 0;
            for (int i = 0; i < _SoundBtn.Length; i++)
            {
                _SoundBtn[i].GetComponent<GUITexture>().texture = _SoundPic_Off;
            }
            gameObject.GetComponent<AudioSource>().volume = 0;
        }
        else
        {
            GlobalManager._SoundEffect = 1;
            for (int i = 0; i < _SoundBtn.Length; i++)
            {
                _SoundBtn[i].GetComponent<GUITexture>().texture = _SoundPic_On;
            }
            gameObject.GetComponent<AudioSource>().volume = 1;
        }
        GlobalManager.SaveAllPlayerPrefs();
    }


    /// <summary>
    ///  §¿˚ §∞‹µØ¥∞…˘“Ù
    /// </summary>
    public void  WinPanel() {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_WinPanelSound);
    }
  

    public void LosePanel()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_LosePanelSound);
    }


    public void MoveSound() {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_MoveSound);
    }
    public void DestorySound() {
        gameObject.GetComponent<AudioSource>().PlayOneShot(_DestorySound);
    }

    /// <summary>
    /// “Ù¡øøÿ÷∆
    /// </summary>
    /// <param name="strDelta"></param>
    void VolumeCtrl(string strDelta) {
        Debug.Log(strDelta);
        if (strDelta == "down")
        {
            float tVolume = -0.2f;
            if (_VolumeRange + tVolume <= 0)
            {
                _VolumeRange = Mathf.RoundToInt(_VolumeRange + tVolume);
            }
            else {
                _VolumeRange += tVolume;
            }
        }
        if (strDelta == "up")
        {
            float tVolume = 0.2f;
            if (_VolumeRange + tVolume >= 1)
            {
                _VolumeRange = Mathf.RoundToInt(_VolumeRange + tVolume);
            }
            else
            {
                _VolumeRange += tVolume;
            }
        }
        gameObject.GetComponent<AudioSource>().volume = _VolumeRange;
        PlayerPrefs.SetFloat("BGMVolume", _VolumeRange);
        PlayerPrefs.Save();
        Debug.Log(_VolumeRange);
    }

}
