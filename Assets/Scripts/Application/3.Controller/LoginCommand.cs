using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Core;
using PureMVC.Interfaces;
using PureMVC.Patterns;

public class LoginCommand : SimpleCommand
{
	public override void Execute(INotification noti)
	{
		Debug.Log("LoginCommand 执行 Execute");

		switch (noti.Name)
		{
			case NotiConst.E_LOGIN:
				LoginProxy proxy = (LoginProxy)Facade.RetrieveProxy(LoginProxy.NAME);
				UserOV obj = (UserOV)noti.Body;
				proxy.SendLoginMsg(obj);
				break;
			default:
				break;
		}
	}
}