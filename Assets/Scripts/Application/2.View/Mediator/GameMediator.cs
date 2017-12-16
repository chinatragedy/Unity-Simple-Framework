using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class GameMediator : Mediator, IMediator
{
	public new const string NAME = "GameMediator";

	private GameView GameView
	{
		get
		{
			ViewComponent = BaseView.ShowView<GameView>();
			return (GameView)ViewComponent;
		}
	}
	#region Constructor

	public GameMediator() : base(NAME) { }
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
		return list;
	}

	// 收到所监听的消息，做对应的处理
	public override void HandleNotification(INotification notification)
	{
		switch (notification.Name)
		{
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
		GameView.ClearCallback();

		GameView.game_Back += () =>
		{
			BaseView.HideView<GameView>();
			SceneManager.Instance.LoadScene(AppConst.Main);
		};

		GameView.game_Boom += () =>
		{
			//TODO:
			GameObject.Find("boom").GetComponent<ParticleSystem>().Play();
		};
	}
}
