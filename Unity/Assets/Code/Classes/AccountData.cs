using System;
using UnityEngine;

[Serializable]
public class AccountData
{
    public string Name;
    public int Number;

    public AccountData(string name, int number)
    {
        Name = name;
        Number = number;
    }
}