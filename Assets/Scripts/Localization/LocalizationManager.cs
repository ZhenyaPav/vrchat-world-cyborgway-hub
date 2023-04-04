
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
namespace Localization
{
    public class LocalizationManager : UdonSharpBehaviour
    {
        private Language language;
        private LocalizationText data;
        public Language Language {
            get => language;
            set {
                language = value;
                LoadLanguage(value);
            }
        }

        private void Initialize(){
            LoadLanguage(default);
            //data = LocalizationText.Parse()
        }
        private void LoadLanguage(Language lang){
            
        }
        public string GetString(string key, string defaultValue = ""){
            if (data==null) return defaultValue;
            else
                return data[key, defaultValue];
        }
    }
}