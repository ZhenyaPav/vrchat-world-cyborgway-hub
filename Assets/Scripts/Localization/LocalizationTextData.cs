using System;
using UnityEngine;
using UdonSharp;
using System.Collections.Generic;
namespace Localization {
    [Serializable, AddComponentMenu("")]
    public class LocalizationTextData : UdonSharpBehaviour
    {
        public const string DEFAULT_LANGUAGE = "English";

        private readonly char[] LINE_SEPARATORS = new char[] { '\r', '\n' },
                                KEY_VALUE_SEPARATORS = new char[] { ',' };
        private const string EMPTY = "ERROR";
        public string[] AvailableLanguages { get; private set; } = new string[]{
            DEFAULT_LANGUAGE
        };
        
        [SerializeField]
        private TextAsset[] textAssets;

        /// <summary>
        /// Because creating objects in Udon sucks.
        /// </summary> 
        private string[] keys, values;
        public string this[string key, string defVal = ""]
        {
            get
            {
                for (int i = 0; i < keys.Length; i++)
                {
                    if (keys[i] == key)
                        return values[i];
                }
                if (string.IsNullOrEmpty( defVal ))
                {
                    return EMPTY;
                }
                else
                {
                    return defVal;
                }
            }
        }
        private void Start(){
            PopulateLanguageList();
            LoadLanguage(AvailableLanguages[1]);
        }
        private void PopulateLanguageList(){
            AvailableLanguages = new string[textAssets.Length];
            for (int i = 0; i < textAssets.Length; i++)
            {
                AvailableLanguages[i] = textAssets[i].name;
            }
        }
        private void LoadLanguage(string lang){
            foreach (var json in textAssets)
            {
                if (string.Equals(json.name, lang, StringComparison.InvariantCultureIgnoreCase)){
                    
                    ParseData(json.text);
                    break;
                }
            }
        }
        private void ParseData(string text){
            // Split the text into lines
            string[] lines = text.Split(LINE_SEPARATORS, StringSplitOptions.RemoveEmptyEntries);

            // Initialize the data array with the correct size
            keys = new string[lines.Length];
            values = new string[lines.Length];
            // Parse each line as a key-value pair and add it to the data array
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(KEY_VALUE_SEPARATORS, StringSplitOptions.RemoveEmptyEntries);
                keys[i] = parts[0].Trim();
                values[i] = parts[1].Trim();
            }
        }

    }
}