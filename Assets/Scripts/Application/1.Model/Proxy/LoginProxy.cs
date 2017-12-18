using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Threading;
public class LoginProxy : Proxy, IProxy
{
	public new const string NAME = "LoginProxy";
	public UserOV data;
	public LoginProxy() : base(NAME) { }

	//请求登录
	public void SendLoginMsg(UserOV data)
	{
		this.data = data;
		//与服务器通讯，返回消息处理玩之后，如果需要改变试图则调用下面消息
		if (data.Name.Equals("tom") && data.Pwd == 123456)
		{
			Debug.Log("LoginProxy->SendLoginMsg,Model请求登录");

			UserInfoOV uData = new UserInfoOV("Jack", 20);

			//Loom.RunAsync(() =>
			//{
			//	Loom.QueueOnMainThread(() => LoginCallBack(uData), 4);
			//});
			LoginCallBack(uData);
		}
	}

	//登录返回
	private void LoginCallBack(UserInfoOV data) //正规是用Json
	{
		Debug.Log("LoginProxy->LoginCallBack,Model登录回调");
		Facade.SendNotification(NotiConst.V_LoginResult, data);
	}
}