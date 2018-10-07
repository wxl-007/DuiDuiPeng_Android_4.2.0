using UnityEngine;

[ExecuteInEditMode]
public class LoadingComponent : MonoBehaviour
{
    private AsyncOperation asy;
    public Texture2D backgroundImage;
    public string levelName;
    public AudioClip loadingAudio;
    public float loadingBarHeight = 25f;
    public Texture2D loadingBarImage;
    public Vector2 loadingBarPosition;
    public string loadingText = "Loading Now...";
    public Color loadingTextColor;
    public FontStyle loadingTextFont;
    public int loadingTextSize = 5;
    public bool normalized;
    public bool showProgressPercentage = true;
    public float startDelay = 1.0f;
    private GUIStyle style;
    private GUIStyle textStyle;

    private void Start()
    {
        if (Application.isPlaying)
        {
            if (loadingAudio)
            {
                if (GetComponent<AudioSource>() == null)
                {
                    gameObject.AddComponent("AudioSource");
                }


                GetComponent<AudioSource>().loop = true;
                GetComponent<AudioSource>().playOnAwake = false;
                GetComponent<AudioSource>().clip = loadingAudio;
            }
            gameObject.AddComponent("GUITexture");
        }
    }

    private void Update()
    {
        if (normalized)
        {
            loadingBarPosition.x = Mathf.Clamp(loadingBarPosition.x, 0f, 1f);
            loadingBarPosition.y = Mathf.Clamp(loadingBarPosition.y, 0f, 1f);
        }
    }

    private void LoadNextLevel()
    {
        transform.position = new Vector3(0, 0, 100);
        transform.localScale = Vector3.zero;

        guiTexture.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
        guiTexture.texture = backgroundImage;
        if (loadingAudio)
        {
            audio.Play();
        }
        DontDestroyOnLoad(this);
        asy = Application.LoadLevelAsync(levelName);
    }


    private void OnGUI()
    {
        if (asy != null)
        {
            if (!asy.isDone)
            {
                style = new GUIStyle();
                style.normal.background = loadingBarImage;
                style.alignment = TextAnchor.MiddleCenter;
                style.normal.textColor = loadingTextColor;
                style.fontStyle = loadingTextFont;
                style.fontSize = loadingTextSize;
                textStyle = new GUIStyle();
                textStyle.alignment = TextAnchor.MiddleCenter;
                textStyle.fontStyle = loadingTextFont;
                textStyle.normal.textColor = loadingTextColor;
                textStyle.fontSize = loadingTextSize;


                if (!normalized)
                {
                    GUI.Label(new Rect(Screen.width/2, loadingBarPosition.y - 20, 50, 50), loadingText, textStyle);

                    if (showProgressPercentage)
                    {
                        GUI.Box(
                            new Rect(loadingBarPosition.x, loadingBarPosition.y,
                                     ((Screen.width - (loadingBarPosition.x*2))*(asy.progress*100f))/100f,
                                     loadingBarHeight),
                            (Mathf.RoundToInt(asy.progress*100f)).ToString() + "%", style);
                        Debug.Log("1: " + asy.progress * 100f);
                    }
                    else
                    {
                        GUI.Box(
                            new Rect(loadingBarPosition.x, loadingBarPosition.y,
                                     ((Screen.width - (loadingBarPosition.x*2))*(asy.progress*100f))/100f,
                                     loadingBarHeight),
                            "", style);
                        Debug.Log("1: " + asy.progress * 100f);
                    }
                }
                else
                {
                    GUI.Label(new Rect(Screen.width/2, (loadingBarPosition.y*Screen.height) - 50, 50, 50),
                              loadingText, textStyle);
                    if (showProgressPercentage)
                    {
                        GUI.Box(
                            new Rect(loadingBarPosition.x*Screen.width, loadingBarPosition.y*Screen.height,
                                     ((Screen.width - (loadingBarPosition.x*Screen.width*2))*(asy.progress*100f))/
                                     100f,
                                     loadingBarHeight),
                            (Mathf.RoundToInt(asy.progress*100f)).ToString() + "%", style);
                        Debug.Log("1: " + asy.progress * 100f);
                    }
                    else
                    {
                        GUI.Box(
                            new Rect(loadingBarPosition.x*Screen.width, loadingBarPosition.y*Screen.height,
                                     ((Screen.width - (loadingBarPosition.x*Screen.width*2))*(asy.progress*100f))/
                                     100f,
                                     loadingBarHeight),
                            "", style);

                    }
                }
            }
            else
            {
                if (!normalized)
                {
                    GUI.Label(new Rect(Screen.width/2, loadingBarPosition.y - 50, 50, 50), loadingText, textStyle);
                    if (showProgressPercentage)
                    {
                        GUI.Box(
                            new Rect(loadingBarPosition.x, loadingBarPosition.y,
                                     ((Screen.width - (loadingBarPosition.x*2))*(asy.progress*100f))/100f,
                                     loadingBarHeight),
                            (Mathf.RoundToInt(asy.progress*100f)).ToString() + "%", style);
                    }
                    else
                    {
                        GUI.Box(
                            new Rect(loadingBarPosition.x, loadingBarPosition.y,
                                     ((Screen.width - (loadingBarPosition.x*2))*(asy.progress*100f))/100f,
                                     loadingBarHeight),
                            "", style);
                    }
                }
                else
                {
                    GUI.Label(new Rect(Screen.width/2, (loadingBarPosition.y*Screen.height) - 50, 50, 50),
                              loadingText, textStyle);
                    if (showProgressPercentage)
                    {
                        GUI.Box(
                            new Rect(loadingBarPosition.x*Screen.width, loadingBarPosition.y*Screen.height,
                                     ((Screen.width - loadingBarPosition.x*Screen.width*2)*(asy.progress*100f))/100f,
                                     loadingBarHeight),
                            (Mathf.RoundToInt(asy.progress*100f)).ToString() + "%", style);
                    }
                    else
                    {
                        GUI.Box(
                            new Rect(loadingBarPosition.x*Screen.width, loadingBarPosition.y*Screen.height,
                                     ((Screen.width - loadingBarPosition.x*Screen.width*2)*(asy.progress*100f))/100f,
                                     loadingBarHeight),
                            "", style);
                    }
                }
                Destroy(gameObject, startDelay);
            }
        }
    }
}