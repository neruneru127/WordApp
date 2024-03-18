using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour
{
    public UIManager_Quiz uiManager;
    public Image bar;
    public Slider slider;

    private BAR_STATE barState = 0;
    private bool isProsessing = true;

    private enum BAR_STATE
    {
        GOOD,
        NORMAL,
        BAD
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
 

    }

    IEnumerator Timer()
    {
        while(slider.value > 0 && isProsessing)
        {
            slider.value -= 0.004f;

            if (slider.value < 0.6 && barState == BAR_STATE.GOOD)
            {
                uiManager.reduceAnswerButton();
                bar.color = Color.yellow;
                barState = BAR_STATE.NORMAL;
            }

            if (slider.value < 0.3 && barState == BAR_STATE.NORMAL)
            {
                uiManager.reduceAnswerButton();
                bar.color = Color.red;
                barState = BAR_STATE.BAD;
            }

            yield return new WaitForSeconds(0.01f);
        }

        if (isProsessing)
        {
            uiManager.TimeUp();

        }

        StopCoroutine(Timer());
    }

    public void ResetTime()
    {
        slider.value = 1;
        bar.color = Color.green;
        barState = BAR_STATE.GOOD;

        ControlTimer(false);
    }


    public void ControlTimer(bool isEnable)
    {
        isProsessing = isEnable;
        if (isEnable)
        {
            StartCoroutine("Timer");
        }
    }
}
