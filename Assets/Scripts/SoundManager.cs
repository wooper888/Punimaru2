using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //BGMのスピーカー
    [SerializeField] AudioSource audioSourceBGM = default;

    //BGMの音源
    [SerializeField] AudioClip[] audioClips = default;

    //SEのスピーカー
    [SerializeField] AudioSource audioSourceSE = default;

    //SEの音源
    [SerializeField] AudioClip[] seClips = default;

    //列挙型で書く(BGM)
    public enum BGM
    {
        TitleSceneBGM,
        GameSceneBGM
    }

    //列挙型で書く(SE)
    public enum SE
    {
        Touch,
        Destroy,
        Countdown,
        Bomb
    }

    //シングルトン
    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SoundManager.instance.PlayBGM(SoundManager.BGM.TitleSceneBGM);
    }

    //BGMの設定
    public void PlayBGM(BGM bgm)
    {
        audioSourceBGM.clip = audioClips[(int)bgm];
        audioSourceBGM.Play();
    }

    //SEの設定
    public void PlaySE(SE se)
    {
        audioSourceSE.PlayOneShot(seClips[(int)se]);
    }

    //音の設定
    public void OnSoundButton()
    {
        audioSourceBGM.mute = !audioSourceBGM.mute;
        audioSourceSE.mute = !audioSourceSE.mute;

    }
}
