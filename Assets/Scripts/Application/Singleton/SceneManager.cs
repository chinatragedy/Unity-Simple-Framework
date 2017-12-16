using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManager : Singleton<SceneManager>
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

		UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneWasLoaded;
    }
    public void LoadScene(int level)
    {
		// 发送退出当前场景的通知
		Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        PureMVC.Patterns.Facade.Instance.SendNotification(NotiConst.E_ExitScene, scene);

		//加载新场景
		UnityEngine.SceneManagement.SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void LoadScene(string sceneName)
    {
		// 发送退出当前场景的通知
		Scene scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
        PureMVC.Patterns.Facade.Instance.SendNotification(NotiConst.E_ExitScene, scene);

		//加载新场景
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    void OnSceneWasLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnScnenWasLoaded:" + scene.name);

        PureMVC.Patterns.Facade.Instance.SendNotification(NotiConst.E_EnterScene, scene);
    }
}
