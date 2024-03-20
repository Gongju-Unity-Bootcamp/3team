using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ResourcesManager : MonoBehaviour
{

    public Dictionary<string, GameObject> Prefabs { get; private set; }
    public Dictionary<string, AudioClip> AudioClips { get; private set; }
    public Dictionary<string, AnimationClip> AnimClips { get; private set; }
    public Dictionary<string, Sprite> Sprite { get; private set; }

    public Dictionary<string, VideoClip> Video { get; private set; }


    public void Init()
    {
        Prefabs = new Dictionary<string, GameObject>();
        AudioClips = new Dictionary<string, AudioClip>();
        AnimClips = new Dictionary<string, AnimationClip>();
        Sprite = new Dictionary<string, Sprite>();
    }

    public GameObject LoadPrefab(string path) => Load(Prefabs, string.Concat(Define.Path.PREFAB, path));
    public AudioClip LoadAudioClip(string path) => Load(AudioClips, string.Concat(Define.Path.AUDIOCLIP, path));
    public AnimationClip LoadAinmclip(string path) => Load(AnimClips, string.Concat(Define.Path.ANIM, path));
    public Sprite LoadSprite(string path) => Load(Sprite, string.Concat(Define.Path.SPRITE, path));
    public VideoClip LoadVideo(string path) => Load(Video, string.Concat(Define.Path.VIDEO, path));

    private T Load<T>(Dictionary<string, T>dic, string path) where T : Object
    {
        if(false == dic.ContainsKey(path))
        {
            T resource = Resources.Load<T>(path);
            dic.Add(path, resource);
            return dic[path];
        }
        return dic[path];
    }
    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject prefab = LoadPrefab(path);

        Debug.Assert(prefab != null);

        return Instantiate(prefab, parent);
    }

    public GameObject Instantiate(GameObject prefab, Transform parent = null)
    {
        GameObject go = Object.Instantiate(prefab, parent);

        go.name = prefab.name;

        return go;
    }

}
