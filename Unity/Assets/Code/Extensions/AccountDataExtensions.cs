using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AccountDataExtensions
{
    public static AccountData Deserialize(this string jsonString)
    {
        return JsonUtility.FromJson<AccountData>(jsonString);
    }

    public static string Serialize(this AccountData accountData)
    {
        return JsonUtility.ToJson(accountData);
    }
}
