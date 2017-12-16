using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviour
{
	private void Start()
	{
		AppFacade facade = (AppFacade)AppFacade.Instance;
		facade.Startup();
	}
}
