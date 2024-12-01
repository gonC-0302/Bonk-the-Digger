using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundData", menuName = "Scriptable Objects/SoundDataSO")]
public class SoundDataSO : ScriptableObject
{
    public List<SoundData> soundDatasList = new List<SoundData>();

    [Serializable]
    public class SoundData
    {
        public SoundType type;
        public AudioClip clip;
    }
}
