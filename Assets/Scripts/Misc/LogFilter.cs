using UnityEngine;
using System.Collections;

public class LogFilter : MonoBehaviour
{
	//Error = 0,
	//Assert = 1,
	//Warning = 2,
	//Log = 3,
	//Exception = 4
	public LogType logtype = LogType.Log;

	private void Awake()
	{
		Debug.logger.filterLogType = logtype;
	}
}
