using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "NewSoundData", menuName = "Audio/Data") ]
public class AudioData : ScriptableObject
{
    public string clipName;
    public AudioClip clipSource;
    public float volume = 1.0f;
    public bool loop = false;
}
