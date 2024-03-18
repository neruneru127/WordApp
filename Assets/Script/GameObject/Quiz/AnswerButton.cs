using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textObj;
    [SerializeField]
    private UIManager_Quiz uiManager;
    [SerializeField]
    private AudioClip correctSound;
    [SerializeField]
    private AudioClip incorrectSound;

    public bool correctFlg = false;
    private Animator animator;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = this.GetComponent<Animator>();
        audioSource = this.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string text, bool correctFlg)
    {
        textObj.SetText(text);
        this.correctFlg = correctFlg;
    }

    public void OnClicked()
    {
        if (correctFlg)
        {
            audioSource.PlayOneShot(correctSound, GameManager.SEVolume);
            uiManager.SetUINextQuiz();
        }
        else
        {
            audioSource.PlayOneShot(incorrectSound, GameManager.SEVolume);
            animator.SetBool("StartAnimation", true);
        }
        
    }

    public void StopAnimation()
    {
        animator.SetBool("StartAnimation", false);
        uiManager.IncorrectAnswer();
        this.gameObject.SetActive(false);

    }

    public void ResetText()
    {
        textObj.SetText("");
    }
}
