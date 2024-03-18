using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public UIManager_Quiz uiManager;

    //private float time = 3;
    //private float decreaseCnt = 0.008f;

    // Start is called before the first frame update
    void Start()
    {

    }

    IEnumerator Sleep()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1);

        countdownText.text = "2";
        yield return new WaitForSeconds(1);

        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        uiManager.FinishCountDown();
        this.gameObject.SetActive(false);

        StopCoroutine(Sleep());
    }

    // Update is called once per frame
    void Update()
    {
        //new WaitForSeconds(10);
        //if (time > 0)
        //{
        //    time -= decreaseCnt;
        //    countdownText.text = ((int)time + 1).ToString();
        //}
        //else
        //{
        //    uiManager.FinishCountDown();
        //    this.gameObject.SetActive(false);
        //}


    }

    public void CountDownStart()
    {
        this.gameObject.SetActive(true);
        StartCoroutine("Sleep");
        //time = 3;
    }
}
