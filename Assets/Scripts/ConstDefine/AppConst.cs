using UnityEngine;

public static class AppConst
{
	/// <summary>
	/// 应用包名
	/// </summary>
	public const string PackageName = "com.cmcm.talkingTest";
	#region settings
	/// <summary>
	/// 设置标准分辨率为1334 x 750(iPhone7分辨率)
	/// </summary>
	public static readonly Vector2 DefaultResolution = new Vector2(1334, 750);
	public static readonly Vector3 DefaultPosition = new Vector3(0, 0, -10);
	public static readonly Vector3 DefaultScale = Vector3.one;
	#endregion

	// 资源路径
	public const string SoundDir = @"Assets/BundleAssets/Audio/";

	// PlayerPrefs
	#region PlayerPerfs
	//public const string HasGuide = "HasGuide";
	#endregion

	public const int GameFrameRate = 90;

	public const bool DebugMode = false;                       //调试模式-用于内部测试

	public const string Md5File = "files.txt";                 // 存储资源名和对应的MD5值的文件
	public const string ExtName = ".unity3d";                   //素材扩展名

	public const bool UpdateMode = false; // 是否开启更新模式
	public const string WebUrl = "http://localhost:45000/vrhotcast/";      //资源更新地址

	// 场景名称
	public const string Start = "00_Start";
	public const string Main = "01_Main";
	public const string Game = "02_Game";


}

public enum ViewType
{
	None = 0,
}