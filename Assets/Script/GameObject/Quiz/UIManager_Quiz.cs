using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

/// <summary>
/// UI全体の監視を行う
/// 各オブジェクトの情報設定、画面更新を担うクラス
/// </summary>
public class UIManager_Quiz : IUIManager
{

    public AnswerButton button1;
    public AnswerButton button2;
    public AnswerButton button3;
    public AnswerButton button4;
    public SentenceLabel sentenceLabel;
    public TextMeshProUGUI groupText;
    public TimeBar timeBar;
    public PlayVoice playVoice;
    public GameManager gameManager;
    public CountDown countDown;

    private QuizManager quizManager;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToActive(List<WordData> wordDataList, WordGroupData groupData)
    {
        this.gameObject.SetActive(true);
        sentenceLabel.ResetText();
        button1.ResetText();
        button2.ResetText();
        button3.ResetText();
        button4.ResetText();
        timeBar.ResetTime();

        countDown.CountDownStart();

        quizManager = new QuizManager(wordDataList, groupData);
        groupText.text = groupData.GroupName;

    }

    public void FinishCountDown()
    {
        SetUINextQuiz();
    }

    public void ToInactive()
    {
        this.gameObject.SetActive(false);
    }


    public void SetUINextQuiz()
    {
        var resultData = quizManager.CheckFinish();
        if (resultData != null)
        {
            gameManager.SendQuizToQuizResult(resultData);
            return;
        }

        var quizData = quizManager.GetNextQuizData();

        button1.gameObject.SetActive(true);
        button1.SetText(quizData.Option1,
            quizData.CorrectOption == QuizData.CorrectOptionEnum.OPTION1 ? true : false);

        button2.gameObject.SetActive(true);
        button2.SetText(quizData.Option2,
            quizData.CorrectOption == QuizData.CorrectOptionEnum.OPTION2 ? true : false);

        button3.gameObject.SetActive(true);
        button3.SetText(quizData.Option3,
            quizData.CorrectOption == QuizData.CorrectOptionEnum.OPTION3 ? true : false);

        button4.gameObject.SetActive(true);
        button4.SetText(quizData.Option4,
            quizData.CorrectOption == QuizData.CorrectOptionEnum.OPTION4 ? true : false);


        sentenceLabel.SetText(quizData.Sentence, quizData.Word);

        timeBar.ResetTime();

        playVoice.SetAudio(quizData.wordID);
    }

    /// <summary>
    /// 不正解時に呼び出し.
    /// 
    /// </summary>
    public void IncorrectAnswer()
    {
        quizManager.IncorrectAnswer();
    }


    public void TimeUp()
    {
        IncorrectAnswer();
        SetUINextQuiz();
    }


    public void reduceAnswerButton()
    {
        var activeButtonList = new List<AnswerButton>();

        if (button1.gameObject.activeSelf)
        {
            activeButtonList.Add(button1);
        }
        if (button2.gameObject.activeSelf)
        {
            activeButtonList.Add(button2);
        }
        if (button3.gameObject.activeSelf)
        {
            activeButtonList.Add(button3);
        }
        if (button4.gameObject.activeSelf)
        {
            activeButtonList.Add(button4);
        }


        var random = new System.Random();

        while(true)
        {
            if (activeButtonList.Count < 2)
            {
                break;
            }

            var index = random.Next(0, activeButtonList.Count);
            if (!activeButtonList[index].correctFlg)
            {
                activeButtonList[index].gameObject.SetActive(false);
                break;
            }
        }
    }

    public void finishedPlayVoice()
    {
        timeBar.ControlTimer(true);
    }

    public override void SortExecute()
    {
        throw new System.NotImplementedException();
    }
}
