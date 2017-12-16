using PureMVC.Patterns;
using PureMVC.Interfaces;
using UnityEngine;

public class ShowViewCommand : SimpleCommand, ICommand
{
	public override void Execute(INotification note)
	{
		var type = (ViewType)note.Body;
		switch (type)
		{
			//case ViewType.Brand:
				//if (!Facade.HasProxy(BrandProxy.NAME))
				//{
				//	Facade.RegisterProxy(new BrandProxy());
				//}
				//if (!Facade.HasMediator(BrandMediator.NAME))
				//{
				//	Facade.RegisterMediator(new BrandMediator());
				//}
				//((BrandMediator)Facade.RetrieveMediator(BrandMediator.NAME)).ShowMediator();
				//((BrandProxy)Facade.RetrieveProxy(BrandProxy.NAME)).Request();
				//break;
			
			default:

				break;
		}

		Debug.Log("OpenViewCommand -----> " + type.ToString());
	}
}
