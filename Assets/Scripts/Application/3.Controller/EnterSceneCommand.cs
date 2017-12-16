using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSceneCommand : SimpleCommand, ICommand
{
	public override void Execute(INotification note)
	{
		Scene scene = (Scene)note.Body;
		Debug.Log("EnterSceneCommand---> Scene name : " + scene.name);

		switch (scene.name)
		{
			case AppConst.Start:
				break;
			case AppConst.Main:
				// 注册Proxy
				if (!Facade.HasProxy(LoginProxy.NAME))
				{
					Facade.RegisterProxy(new LoginProxy());
				}
				// 注册Mediator
				if (!Facade.HasMediator(LoginMediator.NAME))
				{
					Facade.RegisterMediator(new LoginMediator());
				}
				// 注册Command
				if (!Facade.HasCommand(NotiConst.E_LOGIN))
				{
					Facade.RegisterCommand(NotiConst.E_LOGIN, typeof(LoginCommand));
				}

				((LoginMediator)Facade.RetrieveMediator(LoginMediator.NAME)).ShowMediator();
				break;

			case AppConst.Game:
				// 注册Mediator
				if (!Facade.HasMediator(GameMediator.NAME))
				{
					Facade.RegisterMediator(new GameMediator());
				}
				((GameMediator)Facade.RetrieveMediator(GameMediator.NAME)).ShowMediator();
				break;

				//// 注册Proxy
				//// 注册Mediator
				//// 注册Command
		}
	}
}
