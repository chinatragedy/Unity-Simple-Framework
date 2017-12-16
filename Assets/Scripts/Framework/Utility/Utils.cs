using System.IO;
using System.Text;
using UnityEngine;
using System;

public static class Utils
{
    #region 网络
    /// <summary>
    /// 网络可用
    /// </summary>
    public static bool IsNetAvailable
    {
        get
        {
            return Application.internetReachability != NetworkReachability.NotReachable;
        }
    }

    /// <summary>
    /// 是否是无线
    /// </summary>
    public static bool IsWifi
    {
        get
        {
            return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }
    #endregion

    #region 加密解密
    /// <summary>
    /// Base64加密，采用utf8编码方式加密
    /// </summary>
    /// <param name="source">待加密的明文</param>
    /// <returns>加密后的字符串</returns>
    public static string EncodeBase64(string source)
	{
		return EncodeBase64(Encoding.UTF8, source);
	}

	/// <summary>
	/// Base64加密
	/// </summary>
	/// <param name="codeName">加密采用的编码方式</param>
	/// <param name="source">待加密的明文</param>
	/// <returns></returns>
	public static string EncodeBase64(Encoding encode, string source)
	{
		string decode = "";
		byte[] bytes = encode.GetBytes(source);
		try
		{
			decode = Convert.ToBase64String(bytes);
		}
		catch (ArgumentNullException e)
		{
			Debug.LogError(e);
			decode = source;
		}
		return decode;
	}

    /// <summary>
	/// Base64解密，采用utf8编码方式解密
	/// </summary>
	/// <param name="result">待解密的密文</param>
	/// <returns>解密后的字符串</returns>
	public static string DecodeBase64(string result)
    {
        return DecodeBase64(Encoding.UTF8, result);
    }

    /// <summary>
    /// Base64解密
    /// </summary>
    /// <param name="codeName">解密采用的编码方式，注意和加密时采用的方式一致</param>
    /// <param name="result">待解密的密文</param>
    /// <returns>解密后的字符串</returns>
    public static string DecodeBase64(Encoding encode, string result)
	{
		string decode = "";
		byte[] bytes = Convert.FromBase64String(result);
		try
		{
			decode = encode.GetString(bytes);
		}
		catch (ArgumentNullException e)
		{
			Debug.LogError(e);
			decode = result;
		}
		return decode;
	}

	/// <summary>
	/// 通过MD5加密字符串
	/// </summary>
	/// <param name="strToEncrypt"></param>
	/// <returns></returns>
	public static string ComputeStringMD5(string strToEncrypt)
	{
		if (string.IsNullOrEmpty(strToEncrypt))
		{
			return "";
		}

		UTF8Encoding ue = new UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		StringBuilder hashString = new StringBuilder();

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString.Append(Convert.ToString(hashBytes[i], 16).PadLeft(2, '0'));
		}

		return hashString.ToString().PadLeft(32, '0');
	}

    /// <summary>
    /// 计算文件的MD5值
    /// </summary>
    /// <param name="path">文件地址</param>
    /// <returns></returns>
    public static string ComputeFileMD5(string path)
    {
        try
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(fs);
            fs.Close();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }
        catch (Exception ex)
        {
            throw new Exception("Compute file MD5 fail, error:" + ex.Message);
        }
    }

    /// <summary>
    /// 将字符串编码成一个唯一的长整型数
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
	public static long GetInt64HashCode(string target)
    {
        var s1 = target.Substring(0, target.Length / 2);
        var s2 = target.Substring(target.Length / 2);

        var x = ((long)s1.GetHashCode()) << 0x20 | s2.GetHashCode();

        return x;
    }
    #endregion

    #region Time
    /// <summary>
    /// 获取时间戳
    /// </summary>
    /// <returns></returns>
    public static long GetTimestamp()
    {
        TimeSpan ts = new TimeSpan(DateTime.UtcNow.Ticks - new DateTime(1970, 1, 1, 0, 0, 0).Ticks);
        return (long)ts.TotalMilliseconds;
    }

    /// <summary>
    /// 格式化时间戳（显示成00:00:00）
    /// </summary>
    /// <param name="ticks">毫秒(ms)</param>
    /// <param name="showSecond">显示秒，默认显示</param>
    public static string FormatTimestamp(float ticks, bool showSecond = true)
    {
        int hour = 0;
        int minute = 0;
        int second = 0;

        second = (int)(ticks * 0.001f);

        if (second >= 60)
        {
            minute = second / 60;
            second = second % 60;
        }
        if (minute >= 60)
        {
            hour = minute / 60;
            minute = minute % 60;
        }

        if (showSecond)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", hour, minute, second);
        }
        else
        {
            return string.Format("{0:00}:{1:00}", hour, minute);
        }
    }
	#endregion

	#region File Option
	private static string ConfigPath
	{
		get
		{
			if (Application.platform == RuntimePlatform.WindowsEditor)
				return Application.dataPath + "/Resources/InterConfigFile/";
			return Application.persistentDataPath;
		}
	}

	/// <summary>
	/// 创建交互片配置文件
	/// </summary>
	/// <param name="sName"></param>
	/// <param name="nDate"></param>
	public static void SaveConfigFile(string sName, string nDate)
	{
		if (!sName.Contains(".txt")) sName += ".txt";
		SaveFile(ConfigPath + sName, nDate, true);
	}

	/// <summary>
	/// 读取交互片配置文件
	/// </summary>
	/// <param name="sName"></param>
	/// <returns></returns>
	public static string LoadConfigFile(string sName)
	{
		if (!sName.Contains(".txt")) sName += ".txt";
		return LoadFile(ConfigPath + sName);
	}


	/// <summary>
	/// 保存文件
	/// </summary>
	/// <param name="path">文件创建目录</param>
	/// <param name="name">文件的名称</param>
	/// <param name="info">写入的内容</param>
	/// <param name="isCover">是否覆盖之前的信息</param>
	public static void SaveFile(string path, string info, bool isCover = false)
	{
		//文件流信息
		StreamWriter sw = null;
		try
		{
			FileInfo fi = new FileInfo(path);

			if (isCover)
			{
				sw = fi.CreateText();
			}
			else
			{
				if (fi.Exists) sw = fi.AppendText();
				else sw = fi.CreateText();
			}

			//以行的形式写入信息
			sw.WriteLine(info);
			//关闭流
			sw.Close();
			//销毁流
			sw.Dispose();
		}
		catch (Exception ex)
		{
			Debug.LogWarning(ex.ToString());
		}
	}

	/// <summary>
	/// 读取文件
	/// </summary>
	/// <param name="path">读取文件的路径</param>
	public static string LoadFile(string path)
	{
		//使用流的形式读取
		StreamReader sr = null;
		try
		{
			sr = File.OpenText(path);
		}
		catch (System.Exception e)
		{
			//路径与名称未找到文件则直接返回空
			Debug.LogError(e);
			return null;
		}
		string line = sr.ReadToEnd();
		//关闭流
		sr.Close();
		//销毁流
		sr.Dispose();
		return line;
	}

	/// <summary>
	/// 删除文件
	/// </summary>
	/// <param name="path">删除文件的路径</param>
	public static void DeleteFile(string path)
	{
		File.Delete(path);
	}
	#endregion

	#region File Path
	/// <summary>
	/// 获取不同平台的流式加载路径
	/// </summary>
	/// <returns></returns>
	public static string GetDataPath()
    {
        string path = string.Empty;

        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = "jar:file://" + Application.dataPath + "!/assets/";
                break;
            case RuntimePlatform.IPhonePlayer:
                path = Application.dataPath + "/Raw/";
                break;
            default:
                path = Application.dataPath + "/StreamingAssets/";
                break;

        }
        return path;
    }

    /// <summary>
    /// 获取不同平台的持久化数据存储路径
    /// </summary>
    /// <returns></returns>
    public static string GetPersistentPath()
    {
        if (Application.isMobilePlatform)
        {
            return Application.persistentDataPath + "/AssetBundles/";
        }
        else
        {
            int i = Application.dataPath.LastIndexOf('/');
            return Application.dataPath.Substring(0, i + 1) + "AssetBundles/";
        }
    }

    public static string GetPlatformName()
    {
        string path = string.Empty;
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                path = "Android";
                break;
            case RuntimePlatform.IPhonePlayer:
                path = "iOS";
                break;
            case RuntimePlatform.WindowsEditor:
                path = "Windows";
                break;
            case RuntimePlatform.OSXEditor:
                path = "OSX";
                break;
            case RuntimePlatform.WebGLPlayer:
                path = "WebGL";
                break;
        }
        return path;
    }
	#endregion

	#region Tool Functions

	#endregion
}
