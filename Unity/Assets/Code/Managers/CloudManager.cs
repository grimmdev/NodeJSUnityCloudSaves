using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CloudManager : MonoBehaviour
{
    public static CloudManager i;

    public string UniqueIdentifier;

    public string AppAddress;
    public string SaveEndpoint;
    public string LoadEndpoint;

    private void Awake()
    {
        i = this;

        UniqueIdentifier = SystemInfo.deviceUniqueIdentifier;
    }

    public void Save(string json)
    {
        StartCoroutine(InternalSave(json));
    }

    public void Load(Action<string> callback)
    {
        StartCoroutine(InternalLoad(callback));
    }

    private IEnumerator InternalSave(string json)
    {
        WWWForm form = new WWWForm();
        form.AddField("guid", UniqueIdentifier);
        form.AddField("data", json);

        UnityWebRequest www = UnityWebRequest.Post(AppAddress + SaveEndpoint, form);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogWarning("Error Saving to Cloud:" + www.error);
        }
        else
        {
            Debug.Log("Cloud Saved Successfully.");
        }
    }

    private IEnumerator InternalLoad(Action<string> callback)
    {
        UnityWebRequest www = UnityWebRequest.Get(AppAddress + LoadEndpoint + "/" + UniqueIdentifier);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.LogWarning("Error Loading from Cloud:" + www.error);
        }
        else
        {
            Debug.Log("Cloud Loaded Successfully.");
            if (callback != null)
                callback.Invoke(www.downloadHandler.text);
        }
    }
}
