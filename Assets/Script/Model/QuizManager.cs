using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class QuizManager
{
    // 全単語データ
    private List<WordData> wordDataList;
    // 今回の単語グループ
    private WordGroupData groupData;

    private List<int> wordIDList;

    // 出題中の単語ID
    private int currentWordID = -1;
    // 不正解フラグ
    private bool inCorrectFlg = false;

    private int roopCnt = 0;
    private List<int> quizHistory = new List<int>();

    public QuizManager(List<WordData> wordDataList,
        WordGroupData groupData)
    {
        this.wordDataList = wordDataList;
        this.groupData = groupData;
        this.wordIDList = new List<int>(groupData.WordList).OrderBy(e => 
            Guid.NewGuid()).ToList();
    }


    public QuizData GetNextQuizData()
    {
        var random = new System.Random();

        currentWordID = ChooseNextQuiz();

        var copyWordList = new List<int>(groupData.WordList);
        copyWordList.Remove(currentWordID);


        var wordData = wordDataList.Where(e => e.ID.Equals(currentWordID)).First();

        var quizData = new QuizData();
        quizData.wordID = wordData.ID;
        quizData.Word = wordData.Word;
        quizData.Sentence = wordData.Sentence;
        quizData.CorrectOption = (QuizData.CorrectOptionEnum)random.Next(
            Enum.GetValues(typeof(QuizData.CorrectOptionEnum)).Length);


        // 正解選択肢の設定
        switch (quizData.CorrectOption)
        {
            case QuizData.CorrectOptionEnum.OPTION1:
                quizData.Option1 = wordData.Translate;
                break;
            case QuizData.CorrectOptionEnum.OPTION2:
                quizData.Option2 = wordData.Translate;
                break;
            case QuizData.CorrectOptionEnum.OPTION3:
                quizData.Option3 = wordData.Translate;
                break;
            case QuizData.CorrectOptionEnum.OPTION4:
                quizData.Option4 = wordData.Translate;
                break;
        }

        // 不正解選択肢の設定

        if (quizData.Option1 == null)
        {
            var randIndex = random.Next(0, copyWordList.Count);
            var wrongWordData = wordDataList.Where(e => 
              e.ID.Equals(copyWordList[randIndex])).First();
            copyWordList.RemoveAt(randIndex);

            quizData.Option1 = wrongWordData.Translate;
        }

        if (quizData.Option2 == null)
        {
            var randIndex = random.Next(0, copyWordList.Count);
            var wrongWordData = wordDataList.Where(e =>
              e.ID.Equals(copyWordList[randIndex])).First();
            copyWordList.RemoveAt(randIndex);

            quizData.Option2 = wrongWordData.Translate;
        }

        if (quizData.Option3 == null)
        {
            var randIndex = random.Next(0, copyWordList.Count);
            var wrongWordData = wordDataList.Where(e =>
              e.ID.Equals(copyWordList[randIndex])).First();
            copyWordList.RemoveAt(randIndex);

            quizData.Option3 = wrongWordData.Translate;
        }

        if (quizData.Option4 == null)
        {
            var randIndex = random.Next(0, copyWordList.Count);
            var wrongWordData = wordDataList.Where(e =>
              e.ID.Equals(copyWordList[randIndex])).First();
            copyWordList.RemoveAt(randIndex);

            quizData.Option4 = wrongWordData.Translate;
        }

        wordDataList.Where(e => 
            e.ID.Equals(currentWordID)).First().TotalCnt += 1;

        inCorrectFlg = false;

        return quizData;
    }

    /// <summary>
    /// 規定回数まではすべての問題を出題する.
    /// その後、苦手な問題が出やすいように出題する.
    /// 
    /// </summary>
    /// <returns></returns>
    private int ChooseNextQuiz() 
    {
        if (roopCnt < Init.LEAST_QUIZ_CNT)
        {
            var index = wordIDList.FindIndex(e => e.Equals(currentWordID));

            index++;
            if (index == wordIDList.Count)
            {
                index = 0;
                roopCnt++;
            }

            return wordIDList[index];
        }

        var currentID = GetQuizID();

        while (quizHistory.Contains(currentID))
        {
            currentID = GetQuizID();
        }

        quizHistory.Add(currentID);
        if (quizHistory.Count > 5)
        {
            quizHistory.RemoveAt(0);
        }

        return currentID;

    }

    private int GetQuizID()
    {
        // 正答率が高いほど、Listのindexが小さくなるようにソート
        wordDataList.Sort((a, b) => b.CorrectParsent - a.CorrectParsent);

        var random = new System.Random();
        var val = 0;
        for (int i = 0; i < 5; i++)
        {
            val += random.Next(wordDataList.Count / 5);
        }

        if (val >= wordDataList.Count)
        {
            val = wordDataList.Count - 1;
        }

        return wordDataList[val].ID;
    }

    /// <summary>
    /// 終了条件を満たしているか確認を行う.
    /// 
    /// 終了条件:
    ///   1.全ての単語が規定回数以上出題されていること
    ///   2.全ての単語が規定パーセント以上正解していること
    /// 
    /// 
    /// </summary>
    /// <returns>今回の問題の結果,nullの場合継続</returns>
    public List<WordData> CheckFinish()
    {
        foreach (var wordData in wordDataList)
        {
            if (wordData.TotalCnt < Init.LEAST_QUIZ_CNT 
                || wordData.CorrectParsent < Init.LEAST_CORRECT_PARSENT)
            {
                return null;
            }
        }

        return wordDataList;
    }

    /// <summary>
    /// 回答を間違えた際に呼び出す.
    /// 複数回呼び出した場合でも初回のみカウント.
    /// 
    /// </summary>
    public void IncorrectAnswer()
    {
        if (!inCorrectFlg)
        {
            wordDataList.Where(e =>
                e.ID.Equals(currentWordID)).First().IncorrectCnt += 1;

            inCorrectFlg = true;
        }
    }

}