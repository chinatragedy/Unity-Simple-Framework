using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    protected override void Awake()
    {
        InitSystemSettings();
    }

    private void InitSystemSettings()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // 0 for no sync, 1 for panel refresh rate, 2 for 1/2 panel rate
        QualitySettings.vSyncCount = 0;
        // VSync must be disabled
        Application.targetFrameRate = AppConst.GameFrameRate;
    }
}