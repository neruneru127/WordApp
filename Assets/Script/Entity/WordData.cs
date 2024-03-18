using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordData
{
    // primary key
    public int ID
    {
        get; set;
    }

    public int TotalCnt
    {
        get; set;
    }

    public int IncorrectCnt
    {
        get; set;
    }

    public string Word
    {
        get; set;
    }

    public string Translate
    {
        get; set;
    }

    public string Sentence
    {
        get; set;
    }

    public DateTime CreateDateTime
    {
        get; set;
    }


    public int CorrectParsent
    {
        get
        {
            if (TotalCnt == 0)
            {
                return 0;
            }
            return (int)((1 - (float)IncorrectCnt / TotalCnt) * 100);
        }
    }

    public WordData CreateInitializedCopy()
    {
        WordData wordData = new WordData();
        wordData.ID = ID;
        wordData.Word = Word;
        wordData.Translate = Translate;
        wordData.Sentence = Sentence;
        wordData.CreateDateTime = CreateDateTime;

        wordData.TotalCnt = 0;
        wordData.IncorrectCnt = 0;

        return wordData;
    }

    public void MergeScore(int totalCnt, int incorrectCnt)
    {
        TotalCnt += totalCnt;
        IncorrectCnt += incorrectCnt;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        WordData c = (WordData)obj;
        return (this.ID == c.ID);
    }

    //Equals‚ªtrue‚ð•Ô‚·‚Æ‚«‚É“¯‚¶’l‚ð•Ô‚·
    public override int GetHashCode()
    {
        return this.ID;
    }
}
