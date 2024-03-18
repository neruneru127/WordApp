using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordSelect : MonoBehaviour
{

    public TextMeshProUGUI wordText;
    public TextMeshProUGUI translateText;
    public TextMeshProUGUI parsentText;

    private GameManager gameManager;

    public int WordID
    {
        get; set;
    }

    // Start is called before the first frame update
    void Start()
    {
        var scrollContent = this.transform.parent.GetComponent<ScrollContent>();
        gameManager = scrollContent.GetGameManager();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string word, string translate, string parsent)
    {
        wordText.SetText(word);
        translateText.SetText(translate);
        parsentText.SetText(parsent);
    }

    public void PlayClickAudio()
    {
        gameManager.PlayClickAudio();
    }
}
