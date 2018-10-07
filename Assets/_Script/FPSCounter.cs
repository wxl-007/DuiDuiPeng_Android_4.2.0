using UnityEngine;
using System.Collections;

// Attach this to a GUIText to make a frames/second indicator.
//
// It calculates frames/second over each updateInterval,
// so the display does not keep changing wildly.
//
// It is also fairly accurate at very low FPS counts (<10).
// We do this not by simply counting frames per interval, but
// by accumulating FPS for each frame. This way we end up with
// correct overall FPS even if the interval renders something like
// 5.5 frames.



public class FPSCounter : MonoBehaviour
{

    public float updateInterval = 1.0f;
    private float accum = 0.0f; // FPS accumulated over the interval
    private float frames = 0f; // Frames drawn over the interval
    private float timeleft; // Left time for current interval
    private float fps = 15.0f; // Current FPS
    private float lastSample;
    private float gotIntervals = 0;

    private float GetFPS() { return fps; }
    private bool HasFPS() { return gotIntervals > 2; }


    void Start()
    {
        timeleft = updateInterval;
        lastSample = Time.realtimeSinceStartup;
    }


    void Update()
    {
        ++frames;
        float newSample = Time.realtimeSinceStartup;
        float deltaTime = newSample - lastSample;
        lastSample = newSample;

        timeleft -= deltaTime;
        accum += 1.0f / deltaTime;

        // Interval ended - update GUI text and start new interval
        if (timeleft <= 0.0)
        {
            // display two fractional digits (f2 format)
            fps = accum / frames;
            //        guiText.text = fps.ToString("f2");
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
            ++gotIntervals;
        }
    }

    void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 160, 10, 150, 40), fps.ToString("f2") + " | QSetting: " + QualitySettings.GetQualityLevel());
    }
}
