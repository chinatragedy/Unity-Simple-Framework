using UnityEngine;
using System.Collections.Generic;

public class SoundManager : Singleton<SoundManager>
{
    #region 常量
    #endregion

    #region 事件
    #endregion

    #region 字段
    private Dictionary<string, AudioClip> audioDic;

    private AudioSource bgSound;// 背景音乐
    private AudioSource effectSound;// 音效
    #endregion

    #region 属性
    /// <summary>
    /// 音乐声音大小
    /// </summary>
    public float BgVolume
    {
        get { return bgSound.volume; }
        set { bgSound.volume = value; }
    }

    /// <summary>
    /// 音效声音大小
    /// </summary>
    public float EffectVolume
    {
        get { return effectSound.volume; }
        set { effectSound.volume = value; }
    }
    #endregion

    #region 方法
    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="audioName"></param>
    public void PlayBgSound(string audioName)
    {
        string oldName;
        if (bgSound.clip == null)
        {
            oldName = "";
        }
        else
        {
            oldName = bgSound.clip.name;
        }

        if (!string.Equals(oldName, audioName))
        {
            // 加载音乐
            AudioClip clip = LoadAudio(name);

            // 播放
            if (null != clip)
            {
                bgSound.clip = clip;
                bgSound.Play();
            }
        }
    }

    /// <summary>
    /// 停止背景音乐
    /// </summary>
    public void StopBgSound()
    {
        bgSound.Stop();
    }
    
    /// <summary>
    /// 播放音效
    /// </summary>
    /// <param name="audioName"></param>
    //public void PlayEffect(string audioName)
    public void PlayEffect(string audioName)
    {
        // 加载音频
        AudioClip clip = LoadAudio(audioName);
        
        // 播放
        effectSound.PlayOneShot(clip);
    }
    #endregion

    #region Unity回调
    protected override void Awake()
    {
        base.Awake();

        audioDic = new Dictionary<string, AudioClip>();

        bgSound = this.gameObject.AddComponent<AudioSource>();
        bgSound.playOnAwake = false;
        bgSound.loop = true;

        effectSound = this.gameObject.AddComponent<AudioSource>();
    }
    #endregion

    #region 事件回调
    #endregion

    #region 帮助方法
    private AudioClip LoadAudio(string name)
    {
        // 音乐文件路径
        string path;
        if (string.IsNullOrEmpty(AppConst.SoundDir))
        {
            path = "";
        }
        else
        {
            path = System.IO.Path.Combine(AppConst.SoundDir, name);
        }

        path += ".wav";

        AudioClip clip = null;
        if (!audioDic.TryGetValue(path, out clip))
        {
#if UNITY_EDITOR && !TEST
            clip = (AudioClip)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));
#else
            clip = AssetBundleManager.Instance.LoadAsset<AudioClip>(path);
#endif
            audioDic[path] = clip;
        }
        
        return clip;
    }
#endregion
}
