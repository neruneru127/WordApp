using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WordResult : MonoBehaviour
{
    public TextMeshProUGUI wordText;
    public TextMeshProUGUI translateText;
    public TextMeshProUGUI parsentText;

    public int WordID
    {
        get; set;
    }


    // Start is called before the first frame update
    void Start()
    {
        
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
}
