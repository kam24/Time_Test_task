using Newtonsoft.Json;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;


public class TimeController : MonoBehaviour
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void PrintString(string str);
#else
    private static void PrintString(string str) 
    { 
        Debug.Log(str);
    }
#endif

    public void GetMoscowTime()
    {
        StartCoroutine(GetText());
    }

    private IEnumerator GetText()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://worldtimeapi.org/api/timezone/Europe/Moscow");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Time jsonObject = JsonConvert.DeserializeObject<Time>(www.downloadHandler.text);
            PrintString(jsonObject.DateTime.TimeOfDay.ToString());
        }
    }
}

public class Time
{
    [JsonProperty("datetime")]
    public DateTime DateTime { get; set; }
}

