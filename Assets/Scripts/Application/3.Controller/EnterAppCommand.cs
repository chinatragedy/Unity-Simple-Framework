using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterAppCommand : SimpleCommand, ICommand
{
	public override void Execute(INotification note)
	{
		// 横屏
		Screen.orientation = ScreenOrientation.LandscapeLeft;
		//// 竖屏
		//Screen.orientation = ScreenOrientation.Portrait;

		Debug.Log("EnterAppCommand");
	}
}
