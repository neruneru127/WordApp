using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayVoice : MonoBehaviour
{
    public UIManager_Quiz uiManager;

    private AudioSource audioSource;
    private bool finishedFirstPlay = false;

    private Coroutine coroutine;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!this.gameObject.activeSelf && coroutine != null)
        //{
        //    StopCoroutine(coroutine);
        //}
    }

    public void SetAudio(int wordID)
    {
        finishedFirstPlay = false;

        var voicePath = Init.VOICE_DIRECTORY + Init.PATH_SEPARATOR + wordID + Init.VOICE_EXTENSION;


        coroutine = StartCoroutine(LoadToAudioClipAndPlay(voicePath));
        
    }

    //�t�@�C���̓ǂݍ��݁i�_�E�����[�h�j�ƍĐ�
    IEnumerator LoadToAudioClipAndPlay(string path)
    {
        if (!File.Exists(path))
        {
            // �t�@�C�������݂��Ȃ�
            Debug.Log("File not found. " + path);
            uiManager.finishedPlayVoice();
            yield break;
        }

        // ���[�J���t�@�C�����擾
        using (var www = new WWW("file://" + path))
        {
            while (!www.isDone)
                yield return null;

            AudioClip audioClip = www.GetAudioClip(false, true);
            if (audioClip.loadState != AudioDataLoadState.Loaded)
            {
                // ���[�h�Ɏ��s
                Debug.Log("Failed to load AudioClip.");
                uiManager.finishedPlayVoice();
                yield break;
            }

            // ���[�h����
            audioSource.clip = audioClip;
            PlayAudio();
        }
    }

    public void PlayAudio()
    {
        audioSource.volume = GameManager.VoiceVolume;
        audioSource.Play();

        StartCoroutine(Checking());
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }

    private IEnumerator Checking()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            if (!audioSource.isPlaying)
            {
                if (!finishedFirstPlay)
                {
                    uiManager.finishedPlayVoice();
                }

                finishedFirstPlay = true;
                break;
            }
        }
    }
}
