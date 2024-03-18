using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class SentenceLabel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textMP;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetText(string sentence, string word)
    {
        // 単語のハイライト
        try
        {
            var startReg = new Regex("^" + word + " ");
            var endReg = new Regex(" " + word + "$");
            var allReg = new Regex("^" + word + "$");
            if (startReg.IsMatch(sentence))
            {
                // 文頭の単語を変更
                sentence = startReg.Replace(sentence, "<color=red>" + word + "</color> ");
            }
            else if(endReg.IsMatch(sentence))
            {
                // 文頭の単語を変更
                sentence = endReg.Replace(sentence, " <color=red>" + word + "</color>");
            }
            else if(allReg.IsMatch(sentence))
            {
                sentence = allReg.Replace(sentence, " <color=red>" + word + "</color>");
            }
            else
            {
                // 文中の一致を変更(スペース等で単語を区切っているため、それを利用する)
                var reg = new Regex(" " + word + "[\\.\\,\\s,\\!,\\?]");
                var match = reg.Match(sentence);

                var startIndex = sentence.IndexOf(match.Value);
                var endIndex = startIndex + match.Value.Length - 1;

                sentence = sentence.Insert(endIndex, "</color>");
                sentence = sentence.Insert(startIndex, "<color=red>");
            }

            textMP.SetText(sentence);
        }
        catch
        {
            Debug.LogError("ハイライト表示に失敗 sentense:" + sentence + " word:" + word);
        }
    
    }
    public void ResetText()
    {
        textMP.SetText("");
    }
}
