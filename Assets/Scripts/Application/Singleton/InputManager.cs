using UnityEngine;
using System;

public class InputManager : Singleton<InputManager>
{
	private bool isAddListener = false;
    
	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Escape))
		{
			AppEscape();
		}
	}

	public void AppEscape()
	{
        //MessageDispatcher.SendMessage("AppEscape");
        AppFacade.Instance.SendNotification(NotiConst.E_ExitApp, false);
    }
}
