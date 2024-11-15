using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static int GetOrDefault(this Dictionary<string, int> dict, string key, int defaultValue = 0)
    {
        if (dict.ContainsKey(key))
        {
            return dict[key];
        }
        else
        {
            return defaultValue;
        }
    }

    public static void SetOrCreate(this Dictionary<string, int> dict, string key, int value = 0)
    {
        if (dict.ContainsKey(key))
        {
            dict[key] = value;
        }
        else
        {
            dict.Add(key, value);
        }
    }
}
