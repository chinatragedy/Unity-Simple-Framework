using UnityEngine;
using System.Collections.Generic;
using System;
using UObject = UnityEngine.Object;

public abstract class BaseView
{
	private static Dictionary<string, BaseView> viewDic = new Dictionary<string, BaseView>();
	private static List<string> viewList = new List<string>();

	private GameObject viewObject;

	public string ViewName { get; private set; }
	public RootType RootType { get; private set; }
	public ViewMode Mode { get; private set; }
	public bool IsActive
	{
		get
		{
			return null != viewObject && viewObject.activeSelf;
		}
	}

	protected BaseView(string prefabName, RootType type, ViewMode mode)
	{
		//ViewName = this.GetType().ToString();
		ViewName = prefabName;
		RootType = type;
		Mode = mode;

		CreateUI();
	}

	~BaseView() { }

	public static BaseView ShowView<T>() where T : BaseView, new()
	{
		Type t = typeof(T);
		string viewName = t.ToString();
		BaseView view = null;
		if (!viewDic.TryGetValue(viewName, out view))
		{
			view = new T();
			viewDic[viewName] = view;
		}

		if (ViewMode.StackView == view.Mode)
		{
			Push(view);
		}
		view.Show();

		return view;
	}

	public static void HideView<T>() where T : BaseView
	{
		Type t = typeof(T);
		string viewName = t.ToString();
		BaseView view = null;
		if (viewDic.TryGetValue(viewName, out view))
		{
			view.Hide();
		}
		else
		{
			Debug.LogWarning(viewName + " is invalid view!");
		}
	}

	public static void HideAll()
	{
		foreach (var item in viewDic.Values)
		{
			if (item.IsActive)
			{
				item.Hide();
			}
		}
	}

	/*
    public static BaseView ShowView(string viewName)
    {
        BaseView view = null;
        if (!viewDic.TryGetValue(viewName, out view))
        {
            view = Activator.CreateInstance(Type.GetType(viewName)) as BaseView;
            viewDic[viewName] = view;
        }

        view.Show();

        return view;
    }

    public static void HideView(string viewName)
    {
        BaseView view = null;
        if (viewDic.TryGetValue(viewName, out view))
        {
            view.Hide();
        }
        else
        {
            Debug.LogWarning(viewName + " is invalid view!");
        }
    }
    */

	protected virtual void OnStart(GameObject go) { }

	protected virtual void Show()
	{
		if (null != viewObject)
		{
			if (!viewObject.activeSelf)
			{
				viewObject.SetActive(true);
			}
		}
		else
		{
			CreateUI();
		}
	}

	protected virtual void Hide()
	{
		if (null != viewObject)
		{
			if (viewObject.activeSelf)
			{
				viewObject.SetActive(false);
			}
		}
		else
		{
			Debug.LogError("viewObject is null!");
		}
	}

	public virtual void Destory()
	{
		if (null != this.viewObject)
		{
			UObject.Destroy(this.viewObject);
			this.viewObject = null;
		}
	}

	private void CreateUI()
	{
		string path = string.Format("Assets/BundleAssets/Prefabs/UI/{0}.prefab", this.ViewName);
#if UNITY_EDITOR && !TEST
		var prefab = UnityEditor.AssetDatabase.LoadMainAssetAtPath(path) as GameObject;
		GameObject obj = UObject.Instantiate(prefab);
		if (null != obj)
		{
			UIRoot.SetRoot(obj.transform, this.RootType);
			AnchorUI(obj);
			this.viewObject = obj;
			OnStart(obj);
		}
		else
		{
			Debug.LogWarning(path + " is invalid path!");
		}

#elif !ASYNC
        var obj = AssetBundleManager.Instance.LoadAssetAndInstantiate(path);

        if (null != obj)
        {
            UIRoot.SetRoot(obj.transform, this.RootType);
            AnchorUI(obj);
            this.viewObject = obj;
            OnStart(obj);
        }
        else
        {
            Debug.LogWarning(path + " is invalid path!");
        }
#else
        AssetBundleManager.Instance.LoadAssetAsyncAndInstantiate(path, (obj) =>
        {
            if (null != obj)
            {
                UIRoot.SetRoot(obj.transform, this.RootType);
                AnchorUI(obj);
                this.viewObject = obj;
                OnStart(obj);
            }
            else
            {
                Debug.LogWarning(path + " is invalid path!");
            }
        });
#endif
	}

	private void AnchorUI(GameObject go)
	{
		if (null == go)
		{
			return;
		}

		Vector3 anchorPos = Vector3.zero;
		Vector2 sizeDel = Vector2.zero;
		Vector3 scale = Vector3.one;

		RectTransform rt = go.GetComponent<RectTransform>();
		if (null != rt)
		{
			anchorPos = rt.anchoredPosition;
			sizeDel = rt.sizeDelta;
			scale = rt.localScale;
		}
		else
		{
			anchorPos = go.transform.localPosition;
			scale = go.transform.localScale;
		}

		if (null != rt)
		{
			go.transform.localPosition = anchorPos;
			rt.anchoredPosition = anchorPos;
			rt.sizeDelta = sizeDel;
			rt.localScale = scale;
		}
		else
		{
			go.transform.localPosition = anchorPos;
			go.transform.localScale = scale;
		}
	}

	private static void Push(BaseView view)
	{
		if (view.Mode != ViewMode.StackView)
		{
			return;
		}

		string oldName = "";
		if (viewList.Count > 0)
		{
			oldName = viewList[viewList.Count - 1];
		}

		if (!string.Equals(view.ViewName, oldName))
		{
			BaseView oldView = null;
			if (viewDic.TryGetValue(oldName, out oldView))
			{
				oldView.Hide();
			}
		}

		viewList.Remove(view.ViewName);
		viewList.Add(view.ViewName);
	}
}

/// <summary>
/// 界面模式
/// </summary>
public enum ViewMode
{
	DoNothing,
	StackView,
}