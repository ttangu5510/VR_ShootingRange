using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get 
        { 
            if (instance == null)
            {
                GameObject go = new GameObject("AudioManager");
                instance = go.AddComponent<AudioManager>();
                DontDestroyOnLoad(go);
            }
            return instance; 
        }
        set {  instance = value; }
    }
    [Header("Set DB")]
    [SerializeField] AudioDataBase audioDB;

    [Header("Set AudioSource")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource ambientSource;

    [Header("Set AudioClip")]
    [SerializeField] AudioData defaultBGM;
    [SerializeField] AudioData defaultAmbient;

    private Dictionary<string, AudioData> audioDict;
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
            
        audioDict = new Dictionary<string, AudioData>();
        foreach(AudioData data in audioDB.audioList)
        {
            audioDict.Add(data.clipName, data);
        }
        // 이것도 가능
        // audioDict = audioDB.audioList.ToDictionary(s => s.clipName, s => s);
    }
    private void Start()
    {
        PlayAmbient(defaultAmbient);
    }
    public void PlayClip(string name, Vector3 pos)
    {
        if (!audioDict.TryGetValue(name, out AudioData data))
        {
            Debug.LogError($"사운드 파일이 없음 : {name}");
            return;
        }
        GameObject go = new GameObject($"AudioClip_{name}");
        go.transform.position = pos;
        AudioSource audioClip = go.AddComponent<AudioSource>();
        audioClip.clip = data.clipSource;
        audioClip.volume = data.volume;
        audioClip.loop = data.loop;
        audioClip.spatialBlend = 1;
        audioClip.Play();

        if (!data.loop)
        {
            Destroy(go, data.clipSource.length);
        }
    }
    public void PlayBGM(AudioData bgm)
    {
        if (bgmSource.isPlaying && bgmSource.clip == bgm.clipSource) return;
        
        bgmSource.clip = bgm.clipSource;
        bgmSource.volume = bgm.volume;
        bgmSource.loop = bgm.loop;
        bgmSource.Play();
    }

    public void PlayAmbient(AudioData ambient)
    {
        if (ambientSource.isPlaying && bgmSource.clip == ambient.clipSource) return;

        ambientSource.clip = ambient.clipSource;
        ambientSource.volume = ambient.volume;
        ambientSource.loop = ambient.loop;
        ambientSource.Play();
    }
}
