using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AssetBundleManager : Singleton<AssetBundleManager>
{
    /// <summary>
    /// 资源和bundle的映射(资源在哪个bundle里)
    /// </summary>
    static Dictionary<string, string> assetToBundleDic = new Dictionary<string, string>();
    /// <summary>
    /// 所有AssetBundle
    /// </summary>
    static Dictionary<string, AssetBundle> assetBundleDic = new Dictionary<string, AssetBundle>();

    /// <summary>
    /// 已经加载过的资源
    /// </summary>
    static Dictionary<string, LoadedAsset> loadedAssetBundleDic = new Dictionary<string, LoadedAsset>();

    /// <summary>
    /// 初始化AssetBundleManager
    /// </summary>
    /// <param name="bundles">所有的AssetBundle</param>
    public void Init(List<AssetBundle> bundles)
    {
        for (int i = 0; i < bundles.Count; i++)
        {
            AssetBundle bundle = bundles[i];
            string[] assets = bundle.GetAllAssetNames();
            for (int j = 0; j < assets.Length; j++)
            {
                assetToBundleDic.Add(assets[j], bundle.name);
            }
            assetBundleDic.Add(bundle.name, bundles[i]);
        }
    }

    /// <summary>
    /// 获取AssetBundle
    /// </summary>
    /// <param name="assetName">资源名</param>
    /// <returns></returns>
    private AssetBundle GetAssetBundle(string assetName)
    {
        assetName = assetName.ToLower();
        if (!assetToBundleDic.ContainsKey(assetName))
        {
            Debug.LogError("can't find " + assetName + " in any assetbundle!");
            return null;
        }
        string bundleName = assetToBundleDic[assetName];
        if (!assetBundleDic.ContainsKey(bundleName))
        {
            Debug.LogError("can't find " + bundleName + "in assetBundleDic");
            return null;
        }

        return assetBundleDic[bundleName];
    }

    /// <summary>
    /// 同步加载资源并且返回资源的实例
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public GameObject LoadAssetAndInstantiate(string assetName)
    {
        UnityEngine.Object g = LoadAsset<UnityEngine.Object>(assetName);
        return CreateGameObj(assetName, g);
    }

    /// <summary>
    /// 异步加载资源并且返回资源的实例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="assetName"></param>
    /// <param name="onComplete"></param>
    public void LoadAssetAsyncAndInstantiate(string assetName, Action<GameObject> onComplete)
    {
        LoadAssetAsync<UnityEngine.Object>(assetName, (g) =>
        {
            onComplete(CreateGameObj(assetName, g));
        });
    }

    private GameObject CreateGameObj(string assetName, UnityEngine.Object obj)
    {
        if (obj == null)
        {
            Debug.LogError(assetName + " instantiate fail!");
            return null;
        }

        AddToCache(assetName, obj);

        GameObject gobj = Instantiate(obj) as GameObject;
        BundleGameObject script = gobj.AddComponent<BundleGameObject>();
        script.assetName = assetName;
        return gobj;
    }

    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="assetName">资源名</param>
    /// <returns></returns>
    public T LoadAsset<T>(string assetName) where T : UnityEngine.Object
    {
        //先从缓存里里取
        if(loadedAssetBundleDic.ContainsKey(assetName))
        {
            return (T)loadedAssetBundleDic[assetName].obj;
        }

        AssetBundle bundle = GetAssetBundle(assetName);

        if (bundle == null) return null;

        T asset = bundle.LoadAsset<T>(assetName);

        if(asset == null)
        {
            Debug.LogError(assetName + " load fail! " + bundle.name + " don't have" + assetName);
            return null;
        }
        return asset;
    }

    /// <summary>
    /// 异步加载资源
    /// </summary>
    /// <typeparam name="T">资源类型</typeparam>
    /// <param name="assetName">资源名</param>
    /// <param name="onComplete">加载完成回调</param>
    public void LoadAssetAsync<T>(string assetName, Action<T> onComplete) where T : UnityEngine.Object
    {
        //先从缓存里里取
        if (loadedAssetBundleDic.ContainsKey(assetName))
        {
            onComplete((T)loadedAssetBundleDic[assetName].obj);
            return;
        }

        StartCoroutine(AssetAsyncLoader<T>(assetName, onComplete));
    }

    IEnumerator AssetAsyncLoader<T>(string assetName, Action<T> onComplete) where T : UnityEngine.Object
    {
        AssetBundle bundle = GetAssetBundle(assetName);

        if (bundle == null) yield break;

        AssetBundleRequest request = bundle.LoadAssetAsync(assetName);
        yield return request;

        if (request.isDone)
        {
            if (onComplete != null)
                onComplete((T)request.asset);
        }
    }

    /// <summary>
    /// 加载到缓存
    /// </summary>
    void AddToCache(string assetName, UnityEngine.Object obj)
    {
        //TODO: image.sprite = AssetBundleManager.Instance.LoadAsset<Sprite>("assets/test/3/3.png");
        //像这种不依赖GameObject的还不能做到自己清除内存，因为挂不上BundleGameObject脚本
        if (loadedAssetBundleDic.ContainsKey(assetName))
        {
            loadedAssetBundleDic[assetName].referenceCount++;
        }
        else
        {
            LoadedAsset asset = new LoadedAsset(obj);
            loadedAssetBundleDic.Add(assetName, asset);
        }
    }

    /// <summary>
    /// 从缓存卸载
    /// </summary>
    public static void DeleteFromCache(string assetName)
    {
        if (loadedAssetBundleDic.ContainsKey(assetName))
        {
            loadedAssetBundleDic[assetName].referenceCount--;
            if(loadedAssetBundleDic[assetName].referenceCount <= 0)
            {
                loadedAssetBundleDic.Remove(assetName);
                Resources.UnloadUnusedAssets();
            }
        }
    }
}

public class LoadedAsset
{
    public int referenceCount;
    public UnityEngine.Object obj;

    public LoadedAsset(UnityEngine.Object _obj)
    {
        obj = _obj;
        referenceCount = 1;
    }
}

public class BundleGameObject : MonoBehaviour
{
    public string assetName;

    void OnDestroy()
    {
        AssetBundleManager.DeleteFromCache(assetName);
    }
}
