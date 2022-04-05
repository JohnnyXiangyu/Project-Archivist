using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class RedactContext
{
    int visitTimes;
    string text;

    string matchPattern = @"`(?<count>[0-9]+)`(?<text>[^`]+)`/`"; // TODO: backtick might be escaped in regular expression

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
