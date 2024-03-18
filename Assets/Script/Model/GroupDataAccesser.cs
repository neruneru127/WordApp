using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

public static class GroupDataAccesser
{
    static GroupDataAccesser()
    {
        if (!Directory.Exists(Init.ROOT_PATH))
        {
            Directory.CreateDirectory(Init.ROOT_PATH);
        }

        if (!File.Exists(Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_GROUP))
        {
            using FileStream fs = File.Create(Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_GROUP);
        }

    }

    public static List<WordGroupData> Read()
    {
        var groupDataList = new List<WordGroupData>();
        using (StreamReader sr = new StreamReader(
            Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_GROUP))
        {
            var line = "";

            while ((line = sr.ReadLine()) != null)
            {
                var splitStr = line.Split(',');
                var groupData = new WordGroupData();
                groupData.GroupName = splitStr[0];
                groupData.PassedTestFlg = System.Convert.ToBoolean(splitStr[1]);
                groupData.WordList = splitStr.Skip(2).Select(e => int.Parse(e)).ToList();

                groupDataList.Add(groupData);
            }
        }

        return groupDataList;

    }

    public static void Write(List<WordGroupData> groupDataList)
    {
        using (StreamWriter sw = new StreamWriter(
             Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_GROUP))
        {
           foreach(var groupData in groupDataList)
            {
                var sb = new StringBuilder();
                foreach(var wordID in groupData.WordList)
                {
                    sb.Append(wordID + Init.DATA_SEPARATOR);
                }

                sb.Remove(sb.Length - Init.DATA_SEPARATOR.Length,
                    Init.DATA_SEPARATOR.Length);

                sw.WriteLine(
                    groupData.GroupName + Init.DATA_SEPARATOR
                    + groupData.PassedTestFlg + Init.DATA_SEPARATOR
                    + sb.ToString());

            }


        }
    }
}
