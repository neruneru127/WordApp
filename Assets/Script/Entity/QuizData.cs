using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class QuizData
{
    public int wordID
    {
        get; set;
    }

    public string Word
    {
        get; set;
    }

    public string Sentence
    {
        get; set;
    }

    public CorrectOptionEnum CorrectOption
    {
        get; set;
    }
    
    public string Option1
    {
        get; set;
    }

    public string Option2
    {
        get; set;
    }
    public string Option3
    {
        get; set;
    }
    public string Option4
    {
        get; set;
    }

    public enum CorrectOptionEnum
    {
        OPTION1,
        OPTION2,
        OPTION3,
        OPTION4
    }

}