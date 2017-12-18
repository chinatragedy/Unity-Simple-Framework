using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;
using System;

public class StartupCommand : MacroCommand, ICommand
{
	public override void Execute(INotification note)
	{
		Debug.Log("StartupCommand执行Execute");

		Debug.Log(" └-设置相机、UI、单例");
		SetupSingleton();
		SetupCamera();
		SetupUIRoot();

		Debug.Log(" └-开始加载Main场景");
		SceneManager.Instance.LoadScene(AppConst.Main);
	}


	/// <summary>
	/// 设置摄像机
	/// </summary>
	private void SetupCamera()
	{
		Camera mainCamera = GameObject.FindObjectOfType(typeof(Camera)) as Camera;
		if (mainCamera == null)
		{
			mainCamera = new GameObject("MainCamera").AddComponent<Camera>();
			mainCamera.gameObject.tag = "MainCamera";
			mainCamera.transform.position = AppConst.DefaultPosition;
			GameObject.DontDestroyOnLoad(mainCamera);
		}
	}

	/// <summary>
	/// 设置全局单例
	/// </summary>
	private void SetupSingleton()
	{
		GameObject singleton = GameObject.Find("Singleton");
		if (null == singleton)
		{
			singleton = new GameObject("Singleton");

			singleton.AddComponent<GameManager>();
			singleton.AddComponent<SceneManager>();
			singleton.AddComponent<InputManager>();
			singleton.AddComponent<Log2File>();
			singleton.AddComponent<SoundManager>();

#if UNITY_EDITOR
			singleton.AddComponent<ShowFPS>();
#endif
			GameObject.DontDestroyOnLoad(singleton);
		}
	}

	/// <summary>
	/// 设置UI的根节点
	/// </summary>
	private void SetupUIRoot()
	{
		UIRoot uiroot = GameObject.FindObjectOfType(typeof(UIRoot)) as UIRoot;
		if (uiroot == null)
		{
			uiroot = new GameObject("UIRoot").AddComponent<UIRoot>();
			GameObject.DontDestroyOnLoad(uiroot.gameObject);
		}
	}
}
