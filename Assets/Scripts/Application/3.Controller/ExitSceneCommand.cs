using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine.SceneManagement;
using UnityEngine;
public class ExitSceneCommand : SimpleCommand, ICommand
{
	public override void Execute(INotification note)
	{
		Scene scene = (Scene)note.Body;
		Debug.Log("ExitSceneCommand->Execute : 从" + scene.name + "场景退出");

		switch (scene.name)
		{

		}
	}
}
