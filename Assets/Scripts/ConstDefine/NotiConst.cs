using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotiConst
{
	//Model ---> 以M开头表示数据模型通知，数据模型通知从Proxy发出

	//View ---> 以V开头表示视图通知，视图通知从Mediator发出或View层所关心的事件
	public const string V_LoginResult = "V_LoginResult";

	//Controller ---> 以E开头表示全局事件（Event),每个事件都对应一个以Command结尾的类，是View和Model的纽带

	public const string E_Startup = "E_Startup";	// 启动框架
	public const string E_Shutdown = "E_Shutdown";  // 退出框架

	public const string E_EnterApp = "E_EnterApp";  // 进入App
	public const string E_ExitApp = "E_ExitApp";    // 退出App

	public const string E_EnterScene = "E_EnterScene";  // 进入场景
	public const string E_ExitScene = "E_ExitScene";    // 退出场景

	public const string E_LOGIN = "E_LOGIN";

}
