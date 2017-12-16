using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameView : BaseView
{
	public Action game_Back;
	public Action game_Boom;

	public Button btnBoom;
	public Button btnBack;

	public GameView() : base("GamePanel", RootType.ScreenRoot, ViewMode.DoNothing) { }

	#region Override

	protected override void OnStart(GameObject go)
	{
		base.OnStart(go);
		btnBoom = go.FindChild("btnBoom").GetComponent<Button>();
		btnBack = go.FindChild("btnBack").GetComponent<Button>();

		btnBoom.onClick.AddListener(OnBoomClick);
		btnBack.onClick.AddListener(OnBackClick);

	}
	protected override void Show()
	{
		base.Show();
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

	private void OnBackClick()
	{
		if (null != game_Back)
		{
			game_Back();
		}
	}
	private void OnBoomClick()
	{
		if (null != game_Boom)
		{
			game_Boom();
		}
	}

	// 清空注册的委托
	public void ClearCallback()
	{
		while (null != game_Back)
		{
			game_Back -= game_Back;
		}
		while (null != game_Boom)
		{
			game_Boom -= game_Boom;
		}
	}
}
