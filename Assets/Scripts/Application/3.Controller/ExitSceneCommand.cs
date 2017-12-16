using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine.SceneManagement;
using UnityEngine;
public class ExitSceneCommand : SimpleCommand, ICommand
{
	public override void Execute(INotification note)
	{
		Scene scene = (Scene)note.Body;
		Debug.Log("ExitSceneCommand---> Scene name : " + scene.name);

		switch (scene.name)
		{

		}
	}
}
