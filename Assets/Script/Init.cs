using System;
using System.IO;
using UnityEditor;
using UnityEngine;

public static class Init
{
    public const string FILE_PATH_WORD = "WordData.txt";
    public const string FILE_PATH_GROUP = "GroupData.txt";
    public const string FILE_PATH_CONFIG = "config.config";

    public static char PATH_SEPARATOR = Path.DirectorySeparatorChar;

    public static string ROOT_PATH = Application.persistentDataPath;
    public static string VOICE_DIRECTORY = ROOT_PATH + PATH_SEPARATOR + "Voice";
    public const string VOICE_EXTENSION = ".mp3";

    // TODO: Configに入れるのも一考
    public const int DEFAULT_WORD_CNT = 50;
    public const int LEAST_QUIZ_CNT = 5;
    public const int LEAST_CORRECT_PARSENT = 30;

    public const string CREATE_GROUP_STR = "新規作成";


    public const string CONFIG_SEPARETE_CHAR = "=";
    public const string CONFIG_VOICE_VOLUME = "VoiceVolume";
    public const string CONFIG_SE_VOLUME = "SEVolume";

    public const string DATA_SEPARATOR = ",";

}