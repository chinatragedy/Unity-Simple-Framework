using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections;
using System.Collections.Generic;

public class LoginMediator : Mediator, IMediator
{
	public new const string NAME = "LoginMediator";

	private LoginView LoginView
	{
		get
		{
			ViewComponent = BaseView.ShowView<LoginView>();
			return (LoginView)ViewComponent;
		}
	}
	#region Constructor

	public LoginMediator() : base(NAME) { }
	#endregion

	#region Override

	public override void OnRegister()
	{
		base.OnRegister();
	}

	// 需要监听的消息在这里添加
	public override IList<string> ListNotificationInterests()
	{
		IList<string> list = new List<string>();
		list.Add(NotiConst.V_LoginResult);
		return list;
	}

	// 收到所监听的消息，做对应的处理
	public override void HandleNotification(INotification notification)
	{
		switch (notification.Name)
		{
			case NotiConst.V_LoginResult:

				UnityEngine.Debug.Log("LoginMediator->HandleNotification,处理'V_LoginResult'消息");
				LoginView.txtTips.text = "Login Success!";
				UserInfoOV info = (UserInfoOV)notification.Body;
				BaseView.HideView<LoginView>();
				SceneManager.Instance.LoadScene(AppConst.Game);
				break;
			default:
				break;
		}
	}

	public override void OnRemove()
	{
		base.OnRemove();
	}
	#endregion

	public void ShowMediator()
	{
		RegisterCallback();
	}

	// 注册对UI脚本里操作的监听
	private void RegisterCallback()
	{
		LoginView.ClearCallback();

		LoginView.login_Request += (data) =>
		{
			UnityEngine.Debug.Log("发送'E_LOGIN'消息");
			Facade.SendNotification(NotiConst.E_LOGIN, data);
		};

		LoginView.login_Quit += () =>
		{
			UnityEngine.Application.Quit();
		};
	}
}
