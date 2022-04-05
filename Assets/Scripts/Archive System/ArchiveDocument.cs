using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public abstract class ArchiveDocument : ScriptableObject
{
    public string documentTitle = "";

    public virtual string GetSearchIndex()
    {
        return documentTitle;
    }

    public virtual void SwapIn(DocumentDisplayController controller)
    {
        controller.visitMap.Add(this);
    }
}
