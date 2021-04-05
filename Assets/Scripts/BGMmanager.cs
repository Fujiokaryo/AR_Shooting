using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;
public class BGMmanager : MonoBehaviour
{
    public static BGMmanager instance;

    public SoundDataSO soundDataSO;
    public const float CROSS_FADE_TIME = 2.0f;

    public float bgmVolume = 0.1f;

    public AudioSource[] bgmSources = new AudioSource[2];

    private bool isCrossFading;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        for(int i = 0; i < bgmSources.Length; i++)
        {
            bgmSources[i] = gameObject.AddComponent<AudioSource>();
        }

        bgmSources[1].volume = 0;
    }

    public void PlayBGM(SoundDataSO.BgmType newBgmType, bool loopFlg = true)
    {
        SoundDataSO.BgmData newBgmData = null;
        foreach (SoundDataSO.BgmData bgmData in soundDataSO.bgmDataList.Where(x => x.bgmType == newBgmType))
        {
            newBgmData = bgmData;
            break;
        }

        if (newBgmData == null)
        {
            return;
        }

        if (bgmSources[0].clip != null && bgmSources[0].clip == newBgmData.bgmAudioClip)
        {
            return;
        }
        else if (bgmSources[1].clip != null && bgmSources[1].clip == newBgmData.bgmAudioClip)
        {
            return;
        }

        if (bgmSources[0].clip == null && bgmSources[1].clip == null)
        {
            bgmSources[0].loop = loopFlg;
            bgmSources[0].clip = newBgmData.bgmAudioClip;
            bgmSources[0].volume = newBgmData.volume;
            bgmSources[0].Play();
        }
        else
        {
            StartCoroutine(CrossFadeChangeBGM(newBgmData, loopFlg));
        }
    }

    private IEnumerator CrossFadeChangeBGM(SoundDataSO.BgmData bgmData, bool loopFlg)
    {
        isCrossFading = true;

        if(bgmSources[0].clip != null)
        {
            bgmSources[1].DOFade(bgmData.volume, CROSS_FADE_TIME).SetEase(Ease.Linear);
            bgmSources[1].clip = bgmData.bgmAudioClip;
            bgmSources[1].loop = loopFlg;
            bgmSources[1].Play();
            bgmSources[0].DOFade(0, CROSS_FADE_TIME).SetEase(Ease.Linear);

            yield return new WaitForSeconds(CROSS_FADE_TIME);
            bgmSources[0].Stop();
            bgmSources[0].clip = null;       
        }
        else
        {
            bgmSources[0].DOFade(bgmData.volume, CROSS_FADE_TIME).SetEase(Ease.Linear);
            bgmSources[0].clip = bgmData.bgmAudioClip;
            bgmSources[0].loop = loopFlg;
            bgmSources[0].Play();
            bgmSources[1].DOFade(0, CROSS_FADE_TIME).SetEase(Ease.Linear);

            yield return new WaitForSeconds(CROSS_FADE_TIME);
            bgmSources[1].Stop();
            bgmSources[1].clip = null;
        }
    }
}
