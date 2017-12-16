using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleView : BaseView // : BaseView
{

	#region Constructor

	public SampleView() : base("SampleView", RootType.ScreenRoot, ViewMode.DoNothing) { }
	public SampleView(string prefabName, RootType type, ViewMode mode) : base(prefabName, type, mode) { }
	#endregion

	#region Override

	protected override void OnStart(GameObject go)
	{
		base.OnStart(go);
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

	// 清空注册的委托
	public void ClearCallback()
	{

	}
}
