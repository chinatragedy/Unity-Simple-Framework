using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;
using System;

public class AppFacade : Facade
{
	#region Accessors

	/// <summary>
	/// Facade Singleton Factory method.  This method is thread safe.
	/// </summary>
	public new static IFacade Instance
	{
		get
		{
			if (m_instance == null)
			{
				lock (m_staticSyncRoot)
				{
					if (m_instance == null)
						m_instance = new AppFacade();
				}
			}

			return m_instance;
		}
	}

	#endregion

	#region Protected & Internal Methods

	protected AppFacade()
	{
		// Protected constructor.
	}

	protected override void InitializeModel()
	{
		Debug.Log("初始化Model");
		base.InitializeModel();
	}

	protected override void InitializeView()
	{
		Debug.Log("初始化View");
		base.InitializeView();

		RegisterMediator(new LoginMediator());
	}

	/// <summary>
	/// Register Commands with the Controller
	/// </summary>
	protected override void InitializeController()
	{
		Debug.Log("初始化Controller");
		base.InitializeController();

		Debug.Log(" └--注册StartupCommand");
		RegisterCommand(NotiConst.E_Startup, typeof(StartupCommand));

		RegisterCommand(NotiConst.E_EnterApp, typeof(EnterAppCommand));
		RegisterCommand(NotiConst.E_ExitApp, typeof(ExitAppCommand));

		RegisterCommand(NotiConst.E_EnterScene, typeof(EnterSceneCommand));
		RegisterCommand(NotiConst.E_ExitScene, typeof(ExitSceneCommand));

		//RegisterCommand(NotiConst.E_LoadScene, typeof(LoadSceneCommand));
	}
	#endregion

	#region Public methods

	/// <summary>
	/// 启动应用的入口
	/// </summary>
	/// <param name="app"></param>
	public void Startup()
	{
		Debug.Log("发送'Startup'消息");
		SendNotification(NotiConst.E_Startup);
		Debug.Log("取消注册'StartupCommand'消息");
		RemoveCommand(NotiConst.E_Startup);
	}

	public static void CheckNetState(Action callback)
	{
		if (Utils.IsNetAvailable)
		{
			if (Utils.IsWifi)
			{
				if (null != callback)
				{
					callback();
				}
			}
			else
			{
				//ShowDialog("当前为3G/4G网络，是否继续?", () =>
				//{
				//	if (null != callback)
				//	{
				//		callback();
				//	}
				//});
			}
		}
		else
		{
			//ShowSingleDialog("当前无网络，请检查网络连接！");
		}
	}

	#endregion
}
