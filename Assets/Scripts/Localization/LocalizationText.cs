using System;
using UnityEngine;
using UdonSharp;
namespace Localization {
    [Serializable]
    internal class LocalizationText : UdonSharpBehaviour
    {
        internal const string empty = "ERROR";
        public Language loadedLanguage { get; internal set; }
        public LocalTextData[] data;

        public string this[string key, string defVal = ""]
        {
            get
            {
                for (int i = 0; i < data.Length; i++)
                {
                    if (data[i].key == key)
                        return data[i].text;
                }
                if (string.IsNullOrEmpty( defVal ))
                {
                    return empty;
                }
                else
                {
                    return defVal;
                }
            }
        }
        public static LocalizationText Parse( string json){
            return JsonUtility.FromJson<LocalizationText>(json);
        }
    }
    [Serializable]
    internal class LocalTextData : UdonSharpBehaviour
    {
        public string key;
        public string text;
    }
}