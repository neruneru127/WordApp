using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class WordDataAccesser
{
    static WordDataAccesser()
    {
        if (!Directory.Exists(Init.ROOT_PATH))
        {
            Directory.CreateDirectory(Init.ROOT_PATH);
        }


        if (!File.Exists(Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_WORD))
        {
            using FileStream fs = File.Create(Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_WORD);
        }

    }

    public static List<WordData> Read()
    {
        var wordDataList = new List<WordData>();
        using (StreamReader sr = new StreamReader(
            Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_WORD))
        {   
            var line = "";

            while ((line = sr.ReadLine()) != null)
            {
                var splitStr = line.Split(',');
                WordData wordData = new WordData();
                wordData.ID = int.Parse(splitStr[0]);
                wordData.CreateDateTime = DateTime.Parse(splitStr[1]);
                wordData.TotalCnt = int.Parse(splitStr[2]);
                wordData.IncorrectCnt = int.Parse(splitStr[3]);
                wordData.Word = splitStr[4];
                wordData.Translate = splitStr[5];
                wordData.Sentence = string.Join(',', splitStr.Skip(6).ToArray());

                wordDataList.Add(wordData);
            }
        }

        return wordDataList;
        
    }

    public static void Write(List<WordData> wordDataList)
    {
        using (StreamWriter sw = new StreamWriter(
             Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_WORD))
        {
            foreach (var wordData in wordDataList)
            {
                sw.WriteLine(
                    wordData.ID + Init.DATA_SEPARATOR
                    + wordData.CreateDateTime + Init.DATA_SEPARATOR
                    + wordData.TotalCnt + Init.DATA_SEPARATOR
                    + wordData.IncorrectCnt + Init.DATA_SEPARATOR
                    + wordData.Word + Init.DATA_SEPARATOR
                    + wordData.Translate + Init.DATA_SEPARATOR
                    + wordData.Sentence);
            }


        }
    }
}
