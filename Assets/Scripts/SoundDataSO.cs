using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "SoundDataSO", menuName = "Create  SoundDataSO")]
public class SoundDataSO : ScriptableObject
{
    /// <summary>
    /// BGM ‚ÌŽí—Þ
    /// </summary>
    public enum BgmType
    {
        Main,
        Boss,
        GameClear,
        GameOver
    }


    [Serializable]
    public class BgmData
    {
        public int no;
        public BgmType bgmType;
        public float volume = 0.3f;
        public AudioClip bgmAudioClip;
    }

    public List<BgmData> bgmDataList = new List<BgmData>();

}
