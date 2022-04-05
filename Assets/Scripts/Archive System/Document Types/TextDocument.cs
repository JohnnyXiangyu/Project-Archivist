using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

[CreateAssetMenu(fileName = "Archive Document", menuName = "ScriptableObjects/ArchiveDocument")]
public class TextDocument : ArchiveDocument
{
    [TextArea(30, 50)]
    public string documentText = "";

    [TextArea(1, 5)]
    public string tagOverride = "";

    private string Redact(int visitTimes, string text)
    {
        // convert `xx` into tags
        RedactContext context = new RedactContext(visitTimes, text);
        return context.Redact();
    }

    public override string GetSearchIndex()
    {
        return (tagOverride == "")? documentTitle + " " + documentText : tagOverride;
    }

    public override void SwapIn(DocumentDisplayController controller)
    {
        base.SwapIn(controller);
        controller.SetTitle(GetName(controller.swapCount));
        controller.SetContent(GetContent(controller.swapCount));
    }

    public string GetName(int visitTimes)
    {
        return Redact(visitTimes, documentTitle);
    }

    public string GetContent(int visitTimes)
    {
        return Redact(visitTimes, documentText);
    }

    class RedactContext
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
}
