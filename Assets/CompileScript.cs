using SimpleFileBrowser;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SMMMGUIEdior
{
    public static class Ext
    {
        public static string RemoveQuotes(this string input)
        {
            return Regex.Replace(input, "\"+", "");
        }
    }
    public class CompileScript : MonoBehaviour
    {
        public TextMeshProUGUI CodeBox;
        public Button CompileButton;
        public Button ClearUIButton;
        public Button ExportButton;
        public Dictionary<int, string> inputBoxes = new Dictionary<int, string>();
        public List<Action> actions = new List<Action>();
        void OnGUI()
        {
            foreach (Action acc in actions) acc();
        }
        void Start()
        {
            CompileButton.onClick.AddListener(CompileTScript);
            ClearUIButton.onClick.AddListener(() => actions.Clear());
            ExportButton.onClick.AddListener(Export);
            FileBrowser.SetFilters(true, new FileBrowser.Filter("IMGUI File", ".imgui"));
            FileBrowser.SetDefaultFilter(".imgui");
        }
        public void Export()
        {
            StartCoroutine(ShowLoadDialogCoroutine());
        }

        IEnumerator ShowLoadDialogCoroutine()
        {
            yield return FileBrowser.WaitForSaveDialog(false, false, null, "Export Script", "Export");
            Debug.Log(FileBrowser.Success);
            if (FileBrowser.Success)
            {
                FileBrowserHelpers.WriteBytesToFile(FileBrowser.Result[0], Encoding.UTF8.GetBytes(CodeBox.text));
            }
        }

        public void CompileTScript()
        {
            var script = CodeBox.text.Split("\n".ToCharArray()).OfType<string>().ToList();
            script.ForEach((string command) =>
            {

                Regex commandEx = new Regex("^([a-zA-Z]*)");
                Match commandMatch = commandEx.Match(command);
                Regex rectEx = new Regex("(([0-9]*), ([0-9]*), ([0-9]*), ([0-9]*))");
                Match rectMatch = rectEx.Match(command);
                List<string> rects = rectMatch.Value.Split(",".ToCharArray()).OfType<string>().ToList();
                List<float> parsedRects = new List<float>();
                Regex stringEx = new Regex("\"(.*?)\"");
                Match stringMatch = stringEx.Match(command);
                foreach (string rect in rects)
                {
                    parsedRects.Add(float.Parse(rect.Replace(",", "").Trim()));
                }
                switch (commandMatch.Value)
                {
                    case "Label":
                        actions.Add(() => GUI.Label(new Rect(parsedRects[0], parsedRects[1], parsedRects[2], parsedRects[3]), stringMatch.Value.RemoveQuotes()));
                        break;
                    case "Box":
                        actions.Add(() => GUI.Box(new Rect(parsedRects[0], parsedRects[1], parsedRects[2], parsedRects[3]), stringMatch.Value.RemoveQuotes()));
                        break;
                    case "Button":
                        actions.Add(() => GUI.Button(new Rect(parsedRects[0], parsedRects[1], parsedRects[2], parsedRects[3]), stringMatch.Value.RemoveQuotes()));
                        break;
                    case "Input":
                        int dictCount = inputBoxes.Count + 1;
                        inputBoxes.Add(dictCount, stringMatch.Value.RemoveQuotes());
                        actions.Add(() =>
                        {
                            inputBoxes[dictCount] = GUI.TextField(new Rect(parsedRects[0], parsedRects[1], parsedRects[2], parsedRects[3]), inputBoxes[dictCount]);
                        });
                        break;
                }
            });
        }
    }
}

