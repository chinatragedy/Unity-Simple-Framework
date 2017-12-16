using System.Collections;
using System.Collections.Generic;
using PureMVC.Patterns;
using UnityEngine;
using PureMVC.Interfaces;

public class SampleMediator : Mediator, IMediator
{
	public new const string NAME = "SampleMediator";

	private SampleView SampleView
	{
		get
		{
			//ViewComponent = BaseView.ShowView<SampleView>();
			return (SampleView)ViewComponent;
		}
	}
	#region Constructor

	public SampleMediator() : base(NAME) { }
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

	// 注册与UI Component的交互
	private void RegisterCallback()
	{
		//SampleView.Print += CallBack1;
	}

	private void CallBack1()
	{
		Debug.Log("SampleMediator.CallBack1()");
	}
}

#region This is a Pattern. Convenient for developer to use.

//public new const string NAME = "";

//private XxxView XxxView
//{
//	get
//	{
//		//ViewComponent = BaseView.ShowView<XxxView>();
//		return (XxxView)ViewComponent;
//	}
//}
//#region Constructor

//public SampleMediator() : base(NAME) { }
//#endregion

//#region Override

//public override void OnRegister()
//{
//	base.OnRegister();
//}

//// 需要监听的消息在这里添加
//public override IList<string> ListNotificationInterests()
//{
//	IList<string> list = new List<string>();
//	return list;
//}

//// 收到所监听的消息，做对应的处理
//public override void HandleNotification(INotification notification)
//{
//	switch (notification.Name)
//	{
//		default:
//			break;
//	}
//}

//public override void OnRemove()
//{
//	base.OnRemove();
//}
//#endregion

//public void ShowMediator()
//{
//	RegisterCallback();
//}

//// 注册对UI脚本里操作的监听
//private void RegisterCallback()
//{
//}
#endregion
