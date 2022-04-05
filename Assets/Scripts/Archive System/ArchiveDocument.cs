using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public abstract class ArchiveDocument : ScriptableObject
{
    public string documentTitle = "";

    public virtual string GetTitle(DocumentDisplayController controller)
    {
        return Redact(controller.swapCount, documentTitle);
    }

    public virtual string GetSearchIndex()
    {
        return documentTitle;
    }

    public virtual void SwapIn(DocumentDisplayController controller)
    {
        controller.visitMap.Add(this);
    }

    protected string Redact(int visitTimes, string text)
    {
        // convert `xx` into tags
        RedactContext context = new RedactContext(visitTimes, text);
        return context.Redact();
    }
}
