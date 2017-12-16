using UnityEngine;

public class ShowFPS : MonoBehaviour
{
    public float updateInterval = 0.5F;
    private float lastInterval;
    private int frames = 0;
    private float fps;

    void Start()
    {
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
		DontDestroyOnLoad (gameObject);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 200, 100), "FPS:" + fps.ToString("f2"));
    }

    void Update()
    {
        ++frames;

        if (Time.realtimeSinceStartup > lastInterval + updateInterval)
        {
            fps = frames / (Time.realtimeSinceStartup - lastInterval);

            frames = 0;

            lastInterval = Time.realtimeSinceStartup;
        }
    }
}