using PureMVC.Interfaces;
using PureMVC.Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterSceneCommand : SimpleCommand, ICommand
{
	public override void Execute(INotification note)
	{
		Scene scene = (Scene)note.Body;
		Debug.Log("EnterSceneCommand->Execute : 进入" + scene.name + "场景");

		switch (scene.name)
		{
			case AppConst.Start:
				break;
			case AppConst.Main:

				Debug.Log(" └--注册'00_Main'下的需要的脚本");

				Debug.Log("   └--注册'LoginProxy'");
				// 注册Proxy
				if (!Facade.HasProxy(LoginProxy.NAME))
				{
					Facade.RegisterProxy(new LoginProxy());
				}

				Debug.Log("   └--注册'LoginMediator'");
				// 注册Mediator
				if (!Facade.HasMediator(LoginMediator.NAME))
				{
					Facade.RegisterMediator(new LoginMediator());
				}

				Debug.Log("   └--注册'LoginCommand'");
				// 注册Command
				if (!Facade.HasCommand(NotiConst.E_LOGIN))
				{
					Facade.RegisterCommand(NotiConst.E_LOGIN, typeof(LoginCommand));
				}

				((LoginMediator)Facade.RetrieveMediator(LoginMediator.NAME)).ShowMediator();
				break;

			case AppConst.Game:

				Debug.Log(" └--注册'02_Game'下的监听");

				Debug.Log("   └--注册'GameMediator'");
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
