using UnityEngine;
using UnityEngine.EventSystems;

public static class MonoExtension
{
    public static void ActiveObj(this GameObject go, bool active)
    {
        if(null == go)
        {
            Debug.LogWarning("GameObject is null!");
            return;
        }

        if (go.activeSelf != active)
        {
            go.SetActive(active);
        }
    }

    public static void ActiveObj(this Transform obj, bool active)
    {
        if (null == obj)
        {
            Debug.LogWarning("Transform is null!");
            return;
        }

        if (obj.gameObject.activeSelf != active)
        {
            obj.gameObject.SetActive(active);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="parent"></param>
    /// <param name="worldPositionStays"> 
    /// If true, the parent-relative position, 
    /// scale and rotation is modified such that the object keeps the same world 
    /// space position, rotation and scale as before.</param>
    public static void SetParent(this GameObject obj, Transform parent, bool worldPositionStays = false)
    {
        if (null == obj)
        {
            Debug.LogWarning("GameObject is null!");
            return;
        }

        obj.transform.SetParent(parent, worldPositionStays);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="parent"></param>
    /// <param name="worldPositionStays"> 
    /// If true, the parent-relative position, 
    /// scale and rotation is modified such that the object keeps the same world 
    /// space position, rotation and scale as before.</param>
    public static void SetParent(this GameObject obj, GameObject parent, bool worldPositionStays = false)
    {
        if (null == parent)
        {
            Debug.LogError("parent is null!");
            return;
        }
        obj.SetParent(parent.transform, worldPositionStays);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="parent"></param>
    /// <param name="worldPositionStays"> 
    /// If true, the parent-relative position, 
    /// scale and rotation is modified such that the object keeps the same world 
    /// space position, rotation and scale as before.</param>
    public static void SetParent(this Transform obj, GameObject parent, bool worldPositionStays = false)
    {
        if (null == parent)
        {
            Debug.LogError("parent is null!");
            return;
        }
        obj.SetParent(parent.transform, worldPositionStays);
    }

    /// <summary>
    /// 查找子对象
    /// </summary>
    /// <param name="parent"></param>
    /// <param name="path"></param>
    /// <returns></returns>
    public static GameObject FindChild(this GameObject parent, string path)
	{
        if (null == parent)
        {
            Debug.LogWarning("Transform parent is null!");
            return null;
        }

        var child = parent.transform.Find(path);
        if(null == child)
        {
            Debug.LogWarning("Transform child is null!");
            return null;
        }

        return child.gameObject;
	}

	/// <summary>
	/// 查找子对象的UI组件
	/// </summary>
	/// <typeparam name="T">UI类型</typeparam>
	/// <param name="parent">父对象</param>
	/// <param name="path">路径</param>
	/// <returns></returns>
	public static T GetComponentInChildren<T>(this GameObject parent, string path) where T : Component
	{
        var obj = parent.FindChild(path);
        if(null == obj)
        {
            Debug.LogWarning("GameObject parent is null!");
            return null;
        }

        var cmpt = obj.GetComponent<T>();
        if (null != cmpt)
		{
			return cmpt;
		}
		else
		{
            Debug.LogError("[" + parent.name + "]找不到子对象的:" + typeof(T) + " UI组件 Path:" + path);
            return null;
		}
	}

	public  static void AddTriggersEvent(this GameObject obj, EventTriggerType eventID, UnityEngine.Events.UnityAction<BaseEventData> action)
	{
		EventTrigger trigger;

        trigger = obj.GetComponent<EventTrigger>();
        if(null == trigger)
		{
			trigger = obj.AddComponent<EventTrigger>();
		}

		//注册进度条触发事件
		if (trigger.triggers.Count == 0)
		{
			trigger.triggers = new System.Collections.Generic.List<EventTrigger.Entry>();
		}

		UnityEngine.Events.UnityAction<BaseEventData> callback = new UnityEngine.Events.UnityAction<BaseEventData>(action);
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = eventID;
		entry.callback.AddListener(callback);
		trigger.triggers.Add(entry);
	}
}
