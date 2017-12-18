using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{
	private void Start()
	{
		Debug.Log("<======游戏启动======>");
		AppFacade facade = (AppFacade)AppFacade.Instance;
		facade.Startup();
	}
}
