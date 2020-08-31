using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<Action> actions = new List<Action>();
        public bool CanBeCompiled { get; set; }
        void OnGUI()
        {
            foreach (Action acc in actions) acc();
            if (CanBeCompiled)
            {
                CanBeCompiled = false;
                var script = CodeBox.text.Split("\n".ToCharArray()).OfType<string>().ToList();
                script.ForEach((string command) =>
                {
                    Regex rectEx = new Regex("(([0-9]*), ([0-9]*), ([0-9]*), ([0-9]*))");
                    Match rectMatch = rectEx.Match(command);
                    Regex commandEx = new Regex("^([a-zA-Z]*)");
                    Match commandMatch = commandEx.Match(command);
                    Regex stringEx = new Regex("\"(.*?)\"");
                    Match stringMatch = stringEx.Match(command);
                    List<string> rects = rectMatch.Value.Split(",".ToCharArray()).OfType<string>().ToList();
                    List<float> parsedRects = new List<float>();
                    switch (commandMatch.Value)
                    {
                        case "Label":
                            
                            foreach (string rect in rects)
                            {
                                parsedRects.Add(float.Parse(rect.Replace(",", "").Trim()));
                            }
                            actions.Add(() => GUI.Label(new Rect(parsedRects[0], parsedRects[1], parsedRects[2], parsedRects[3]), stringMatch.Value.RemoveQuotes()));
                            break;
                        case "Box":
                            foreach (string rect in rects)
                            {
                                parsedRects.Add(float.Parse(rect.Replace(",", "").Trim()));
                            }
                            actions.Add(() => GUI.Box(new Rect(parsedRects[0], parsedRects[1], parsedRects[2], parsedRects[3]), stringMatch.Value.RemoveQuotes()));
                            break;
                    }
                });
            }
        }
        void Start()
        {
            
            CanBeCompiled = false;
            CompileButton.onClick.AddListener(CompileTScript);
            ClearUIButton.onClick.AddListener(() => actions.Clear());
        }
        public void CompileTScript()
        {
            CanBeCompiled = true;
        }
    }
}

