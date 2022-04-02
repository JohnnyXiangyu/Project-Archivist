using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

[CreateAssetMenu(fileName = "Archive Document", menuName = "ScriptableObjects/ArchiveDocument")]
public class ArchiveDocument : ScriptableObject
{
    public string documentName = "";

    [TextArea(30, 50)]
    public string documentText = "";

    private string Redact(int visitTimes, string text)
    {
        // convert `xx` into tags
        RedactContext context = new RedactContext(visitTimes, text);
        return context.Redact();
    }

    public string GetName(int visitTimes)
    {
        return Redact(visitTimes, documentName);
    }

    public string GetContent(int visitTimes)
    {
        return Redact(visitTimes, documentText);
    }

    class RedactContext
    {
        int visitTimes;
        string text;

        string matchPattern = @"`(?<count>[0-9]+)`(?<text>.+)`/`"; // TODO: backtick might be escaped in regular expression

        public RedactContext(int visitCount, string inText)
        {
            visitTimes = visitCount;
            text = inText;
        }

        public string Redact()
        {
            return Regex.Replace(text, matchPattern, RedactEvaluation);
        }

        private string RedactEvaluation(Match match)
        {
            string count = match.Groups["count"].Value;
            int countReq = int.Parse(count);

            if (countReq <= visitTimes)
            {
                string result = "<font=\"Redacted-Regular SDF\">" + match.Groups["text"].Value + "</font>";
                return result;
            }
            else
            {
                return match.Groups["text"].Value;
            }
        }
    }
}