using UnityEngine;

public class Log2File : Singleton<Log2File>
{
    private string logPath = "";

	void Start ()
    {
        Application.logMessageReceived += LogHandler;
    }
	
	private void LogHandler(string condition, string stackTrace, LogType type)
    {
        if (type == LogType.Exception || type == LogType.Error)
        {
            string log = string.Format("Date:{0}\nLogType:{1}\nCondition:\n{2}\nStackTrace:\n{3}", 
                                        System.DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") ,type.ToString(), condition, stackTrace);
            Utils.SaveFile(GetLogPath(), log);
        }
    }

    private string GetLogPath()
    {
        if (string.IsNullOrEmpty(logPath))
        {
            logPath = string.Format("{0}/vr_hotcast_{1}.txt", Application.persistentDataPath, System.DateTime.Now.ToString("yyyyMMdd"));
        }

        return logPath;
    }
}
