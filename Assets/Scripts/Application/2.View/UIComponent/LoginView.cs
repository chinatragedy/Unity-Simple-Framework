using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LoginView : BaseView
{
	public InputField inputName;
	public InputField inputPwd;
	public Button btnLogin;
	public Button btnQuit;
	public Text txtTips;

	public Action<UserOV> login_Request;
	public Action login_Quit;

	#region Constructor

	public LoginView() : base("LoginPanel", RootType.ScreenRoot, ViewMode.DoNothing) { }
	public LoginView(string prefabName, RootType type, ViewMode mode) : base(prefabName, type, mode) { }
	#endregion

	#region Override

	protected override void OnStart(GameObject go)
	{
		base.OnStart(go);
		btnLogin = go.FindChild("btnLogin").GetComponent<Button>();
		btnQuit = go.FindChild("btnQuit").GetComponent<Button>();
		txtTips = go.FindChild("txtTip").GetComponent<Text>();
		inputName = go.FindChild("inputName").GetComponent<InputField>();
		inputPwd = go.FindChild("inputPwd").GetComponent<InputField>();

		btnLogin.onClick.AddListener(OnLoginClick);
		btnQuit.onClick.AddListener(OnQuitClick);
	}
	protected override void Show()
	{
		base.Show();

		txtTips.gameObject.SetActive(false);
	}
	protected override void Hide()
	{
		base.Hide();
	}

	public override void Destory()
	{
		base.Destory();
	}
	#endregion

	public void OnLoginClick()
	{
		txtTips.gameObject.SetActive(true);
		txtTips.text = "登录中...";

		if (null != login_Request)
		{
			login_Request(new UserOV(inputName.text, int.Parse(inputPwd.text)));
		}
	}

	public void OnQuitClick()
	{
		if (null != login_Quit)
		{
			login_Quit();
		}
	}

	public void ClearCallback()
	{
		while (login_Request != null)
		{
			login_Request -= login_Request;
		}

		while (login_Quit != null)
		{
			login_Quit -= login_Quit;
		}
	}
}


