using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ConfigAccesser
{
    static ConfigAccesser()
    {
        if (!Directory.Exists(Init.ROOT_PATH))
        {
            Directory.CreateDirectory(Init.ROOT_PATH);
        }

        if (!File.Exists(Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_CONFIG))
        {
            using FileStream fs = File.Create(Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_CONFIG);
        }

    }

    public static ConfigData Read()
    {
        var configData = new ConfigData();
        using (StreamReader sr = new StreamReader(
            Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_CONFIG))
        {
            if (!sr.EndOfStream)
            {
                configData.VoiceVolume = float.Parse(sr.ReadLine().Split(Init.CONFIG_SEPARETE_CHAR)[1]);
            }

            if (!sr.EndOfStream)
            {
                configData.SEVolume = float.Parse(sr.ReadLine().Split(Init.CONFIG_SEPARETE_CHAR)[1]);
            }
        }

        return configData;

    }


    public static void Write(ConfigData configData)
    {
        using (StreamWriter sw = new StreamWriter(
             Init.ROOT_PATH + Init.PATH_SEPARATOR + Init.FILE_PATH_CONFIG))
        {
            sw.WriteLine(Init.CONFIG_VOICE_VOLUME + Init.CONFIG_SEPARETE_CHAR + configData.VoiceVolume);
            sw.WriteLine(Init.CONFIG_SE_VOLUME + Init.CONFIG_SEPARETE_CHAR + configData.SEVolume);
        
        }
    }
}