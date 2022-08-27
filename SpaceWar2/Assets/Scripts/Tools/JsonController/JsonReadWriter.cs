using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonReadWriter
{
    /// <summary>
    /// Tには、[System.Serializable]が設定されている事　MonoBehaviourを継承していないこと
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static void WriteData<T>(T saveContent,string saveFileName)
    {
        StreamWriter writer;
        string jsonString = JsonUtility.ToJson(saveContent);

        writer = new StreamWriter(Application.dataPath + "/" + saveFileName + ".json",false);
        writer.Write(jsonString);
        writer.Flush();
        writer.Close();
    }
    /// <summary>
    /// Tには、[System.Serializable]が設定されている事 MonoBehaviourを継承していないこと
    /// fileNameは拡張子を省略すること
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static T ReadData<T>(string fileName)
    {
        if (File.Exists(Application.dataPath + "/" + fileName + ".json"))
        {
            string dataString = "";
            StreamReader reader;
            reader = new StreamReader(Application.dataPath + "/" + fileName + ".json");
            dataString = reader.ReadToEnd();
            reader.Close();
            return JsonUtility.FromJson<T>(dataString);
        }
        else
        {
            //参考　https://ponyoth.hateblo.jp/entry/2015/09/06/110231
            return default(T);//勝手に規定値を入れてくれる
            
        }

    }
}
