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
        // �P��̃n�C���C�g
        try
        {
            var startReg = new Regex("^" + word + " ");
            var endReg = new Regex(" " + word + "$");
            var allReg = new Regex("^" + word + "$");
            if (startReg.IsMatch(sentence))
            {
                // �����̒P���ύX
                sentence = startReg.Replace(sentence, "<color=red>" + word + "</color> ");
            }
            else if(endReg.IsMatch(sentence))
            {
                // �����̒P���ύX
                sentence = endReg.Replace(sentence, " <color=red>" + word + "</color>");
            }
            else if(allReg.IsMatch(sentence))
            {
                sentence = allReg.Replace(sentence, " <color=red>" + word + "</color>");
            }
            else
            {
                // �����̈�v��ύX(�X�y�[�X���ŒP�����؂��Ă��邽�߁A����𗘗p����)
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
            Debug.LogError("�n�C���C�g�\���Ɏ��s sentense:" + sentence + " word:" + word);
        }
    
    }
    public void ResetText()
    {
        textMP.SetText("");
    }
}
