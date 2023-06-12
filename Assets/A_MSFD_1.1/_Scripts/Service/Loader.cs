using MSFD;
using Sirenix.Utilities;
using System.Collections;
using UnityEngine;
using System;
using static MSFD.AS.Utilities;
namespace MSFD.Service
{
    public class Loader : MonoBehaviour
    {
        //[SerializeField]
        const string url = "https://vk.com/wall-206551024_4";
        //[SerializeField]
        const string activateScriptCommand = "test";

        /*[SerializeField]
        string changeUrlCommand = "change";*/
        /* [SerializeField]
         WorkMode workMode = WorkMode.containsCommand;*/
        const string title = "<meta name=\"description\" content=\"";
        const string endTitle = "\" />";

        const string nextCheckDeltaPrefix = ":";
        const string nextCheckDeltaPostfix = ":";

        [SerializeField]
        bool isDebugLog = true;

        //[Sirenix.OdinInspector.Button]
        public void Activate()
        {
            StartCoroutine(RequestWWW());
        }
        IEnumerator RequestWWW()
        {
            if (!IsNeedCheck())
            {
                ChooseDestiny();
                yield break;
            }
            Log("Send cheking request", isDebugLog);
            WWW request = new WWW(url);

            yield return request;
            if (!request.error.IsNullOrWhitespace())
            {
                Log(request.error, isDebugLog);
                ChooseDestiny();
                yield break;
            }

            string rowText = request.text;
            //Debug.Log(rowText);
            DefineNextCheck(rowText);


            //Debug.Log(data);

            string data = ParsePost(rowText, title, endTitle);
            if (data.Contains(activateScriptCommand))
            {
                AllBreak();
            }
            else
                AllGood();
        }
        string ParsePost(string rowText, string prefix, string postfix)
        {
            int start = rowText.IndexOf(prefix) + prefix.Length;
            int end = rowText.IndexOf(postfix, start);// - endTitle.Length;
            return rowText.Substring(start, end - start);
        }
        bool IsNeedCheck()
        {
            DateTime nextChekTime = SaveCore.Load<DateTime>(dateKey, DateTime.MinValue);
            if (nextChekTime <= DateTime.Today || !IsValueExistsInMemory())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        void DefineNextCheck(string rowText)
        {
            string nextCheckDelta = ParsePost(rowText, nextCheckDeltaPrefix, nextCheckDeltaPostfix);
            int delta;
            if (!nextCheckDelta.IsNullOrWhitespace() && int.TryParse(nextCheckDelta, out delta))
            {
                DateTime dateTime = DateTime.Today.AddDays(delta);
                Log("Delta: " + delta, isDebugLog);
                Log("Next date: " + dateTime.Date, isDebugLog);
                SaveCore.Save(dateKey, dateTime.Date);
            }
            else
            {
                Log("Next date is not defined", isDebugLog);
            }
        }
        void ChooseDestiny()
        {
            Log("Choose destiny offline", isDebugLog);
            string value = GetValue();
            if (value == allGoodValue || value.IsNullOrWhitespace())
            {
                AllGood();
            }
            else
            {
                AllBreak();
            }
        }
        bool IsValueExistsInMemory()
        {
            string value = GetValue();
            return value == allGoodValue || value == allBreakValue;
        }
        string GetValue()
        {
            return SaveCore.Load<string>(saveKey, "");
        }
        void AllGood()
        {
            Log("All good", isDebugLog);
            SaveCore.Save(saveKey, allGoodValue);
            return;
        }
        void AllBreak()
        {
            Log("All break", isDebugLog);
            SaveCore.Save(saveKey, allBreakValue);
            Application.Quit();
            LogWarning("Quit");
            return;
        }

        //enum WorkMode {disable, containsCommand}
        const string saveKey = "Version";
        const string allGoodValue = "1.0.1";
        const string allBreakValue = "1.1.1";

        const string dateKey = "Valid";
    }
}