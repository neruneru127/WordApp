using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager_Menu : IUIManager
{
    // 問題画面の時間停止用
    public TimeBar timeBar;
    public PlayVoice playVoice;
    public GameManager gameManager;
    public Slider voiceVolumeSlider;
    public Slider seVolumeSlider;
    public AudioClip voiceCheckClip;
    public AudioClip seCheckClip;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        // 初期化なので、閉じる際の処理は行わない
        this.gameObject.SetActive(false);

        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetInitialVolume (float voiceVolume, float seVolume)
    {
        voiceVolumeSlider.value = voiceVolume;
        seVolumeSlider.value = seVolume;

    }


    public void ToActive()
    {
        if (timeBar.isActiveAndEnabled)
        {
            timeBar.ControlTimer(false);
        }

        if (playVoice.isActiveAndEnabled)
        {
            playVoice.StopAudio();
        }
        this.gameObject.SetActive(true);
    }

    public void ToInactive()
    {
        if (timeBar.isActiveAndEnabled)
        {
            timeBar.ControlTimer(true);
        }

        // Configを書き込む
        gameManager.WriteConfig();

        this.gameObject.SetActive(false);
    }

    public void ReturnTitleButtonOnClicked()
    {
        gameManager.ReturnTitle();
    }

    public void VoiceVolumeChanged()
    {
        GameManager.VoiceVolume = voiceVolumeSlider.value;

    }

    public void VoiceVolumeCheck()
    {
        audioSource.PlayOneShot(voiceCheckClip, GameManager.VoiceVolume);
    }

    public void SEVolumeChanged()
    {
        GameManager.SEVolume = seVolumeSlider.value;
    }

    public void SEVolumeCheck()
    {
        audioSource.PlayOneShot(seCheckClip, GameManager.SEVolume);
    }

    public override void SortExecute()
    {
        throw new System.NotImplementedException();
    }
}
