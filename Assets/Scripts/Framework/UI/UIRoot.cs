using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Root类型
/// </summary>
public enum RootType
{
	/// <summary>
	/// 没有根节点
	/// </summary>
	NoRoot,
	/// <summary>
	/// 2D UI根节点
	/// </summary>
	ScreenRoot,
	/// <summary>
	/// 3D UI根节点
	/// </summary>
	WorldRoot,
	/// <summary>
	/// 跟踪根节点，VR模式下视野的中心点
	/// </summary>
	TraceRoot,
	/// <summary>
	/// 跟随根节点，VR模式下，跟随头部移动（在竖直方向上不跟随，只在水平方向上跟随）
	/// </summary>
	FollowRoot,
}

public class UIRoot : Singleton<UIRoot>
{
	#region fields
	//private Transform headObj;
	//private Vector3 direction;
	//private float currentYaw = 0;
	//private float previousYaw = 0;

	//private bool triggerState;
	#endregion

	#region properties
	/// <summary>
	/// 2D UI的Root
	/// </summary>
	public static GameObject ScreenRoot { get; private set; }
	/// <summary>
	/// 3D UI的Root
	/// </summary>
	public static GameObject WorldRoot { get; private set; }
	/// <summary>
	/// 视线追踪的Root
	/// </summary>
	public static GameObject TraceRoot { get; private set; }
	/// <summary>
	/// 视线跟随的Root
	/// </summary>
	public static GameObject FollowRoot { get; private set; }

	/// <summary>
	/// 水平方向最大活动角度
	/// </summary>
	//public static float MaxYaw { private get; set; }

	//public static bool LockYaw { private get; set; }

	//public static float LookAngle { private get; set; }

	//public static bool Recenter { private get; set; }
	#endregion

	#region events
	public static event Action<bool> OnTriggerAngle;
	#endregion

	#region override functions
	protected override void Awake()
	{
		base.Awake();
		InitRoot();

		//LookAngle = 30f;// 默认30度触发低头显示控制栏
	}

	void LateUpdate()
	{
		//UpdateRoot();
	}

	protected override void OnDestroy()
	{
		//this.headObj = null;
	}
	#endregion

	#region public functions

	//public void SetHead(Transform head)
	//{
	//	this.headObj = head;
	//}

	public static void SetRoot(Transform target, RootType type)
	{
		if (RootType.ScreenRoot == type)
		{
			target.SetParent(ScreenRoot);
		}
		else if (RootType.WorldRoot == type)
		{
			target.SetParent(WorldRoot);
		}
		else if (RootType.TraceRoot == type)
		{
			target.SetParent(TraceRoot);
		}
		else if (RootType.FollowRoot == type)
		{
			target.SetParent(FollowRoot);
		}
	}
	#endregion

	#region private functions
	private void InitRoot()
	{
		gameObject.name = "UIRoot";
		gameObject.layer = LayerMask.NameToLayer("UI");

		ScreenRoot = CreateRoot("ScreenRoot", RenderMode.ScreenSpaceCamera, 0);

		WorldRoot = CreateRoot("WorldRoot", RenderMode.WorldSpace, 0);
		WorldRoot.transform.localPosition = AppConst.DefaultPosition;
		WorldRoot.transform.localScale = AppConst.DefaultScale;

		FollowRoot = CreateRoot("FollowRoot", RenderMode.WorldSpace, 0);
		TraceRoot = CreateRoot("TraceRoot", RenderMode.WorldSpace, 500);

		SetupEventSytem(gameObject);
	}

	private GameObject CreateRoot(string name, RenderMode mode, int sort)
	{
		GameObject go = new GameObject(name);
		go.transform.SetParent(transform);
		go.layer = LayerMask.NameToLayer("UI");

		AddRectTransform(go);

		AddCanvas(go, mode, sort);

		AddScaler(go, mode);

		if (sort < 500)
		{
			go.AddComponent<GraphicRaycaster>();
		}

		return go;
	}

	private void AddRectTransform(GameObject go)
	{
		RectTransform rt = go.AddComponent<RectTransform>();
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, 0);
		rt.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, 0, 0);
		rt.anchorMin = Vector2.zero;
		rt.anchorMax = Vector2.one;
		rt.sizeDelta = AppConst.DefaultResolution;
	}

	private void AddCanvas(GameObject go, RenderMode mode, int sort)
	{
		Canvas can = go.AddComponent<Canvas>();
		can.renderMode = mode;
		can.pixelPerfect = true;
		can.overrideSorting = true;
		can.sortingOrder = sort;
	}

	private void AddScaler(GameObject go, RenderMode mode)
	{
		CanvasScaler cs = go.AddComponent<CanvasScaler>();
		if (RenderMode.ScreenSpaceCamera == mode)
		{
			cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
			cs.referenceResolution = AppConst.DefaultResolution;
			cs.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
		}
	}
	private void SetupEventSytem(GameObject root)
	{
		var eventSystem = new GameObject("EventSystem");
		eventSystem.SetParent(root);
		eventSystem.layer = LayerMask.NameToLayer("UI");
		eventSystem.AddComponent<EventSystem>();
#if UNITY_EDITOR
		eventSystem.AddComponent<StandaloneInputModule>();
#endif
	}
	//private void UpdateRoot()
	//{
	//    if (null == headObj)
	//    {
	//        return;
	//    }

	//    if (LockYaw)
	//    {
	//        direction = headObj.rotation * Vector3.forward;
	//    }
	//    else
	//    {
	//        direction = headObj.forward;
	//    }

	//    float deviceYaw = (Mathf.Rad2Deg * Mathf.Atan2(direction.x, direction.z) + 360) % 360;
	//    float devicePitch = Mathf.Rad2Deg * Mathf.Asin(direction.y);

	//    float deltaYaw = Mathf.DeltaAngle(deviceYaw, previousYaw);
	//    float targetYaw = currentYaw + deltaYaw;

	//    previousYaw = deviceYaw;
	//    Quaternion deviceRotation = Quaternion.AngleAxis(deviceYaw, Vector3.up);

	//    currentYaw = Mathf.Clamp(targetYaw, -MaxYaw, MaxYaw);

	//    if (LockYaw)
	//    {
	//        FollowRoot.transform.position = headObj.position;
	//        FollowRoot.transform.localRotation = Quaternion.AngleAxis(-devicePitch, Vector3.right);
	//    }
	//    else
	//    {
	//        FollowRoot.transform.position = headObj.position;
	//        FollowRoot.transform.localRotation = deviceRotation * Quaternion.AngleAxis(currentYaw, Vector3.up);
	//    }

	//    TraceRoot.transform.position = headObj.position;
	//    TraceRoot.transform.localRotation = deviceRotation * Quaternion.AngleAxis(-devicePitch, Vector3.right);

	//    TriggerAngle(-devicePitch);
	//}

	//private void TriggerAngle(float angle)
	//{
	//	bool trigger = angle > LookAngle;

	//	if (triggerState != trigger && null != OnTriggerAngle)
	//	{
	//		OnTriggerAngle(trigger);

	//		triggerState = trigger;

	//		if (Recenter && !LockYaw)
	//		{
	//			Recenter = false;
	//			currentYaw = 0;
	//		}

	//		Debug.Log("trigger----------" + trigger);
	//	}
	//}
	#endregion
}