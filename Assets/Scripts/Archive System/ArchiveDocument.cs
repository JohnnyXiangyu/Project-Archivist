using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public abstract class ArchiveDocument : ScriptableObject
{
    public string documentName = "";

    public virtual string GetSearchIndex()
    {
        return documentName;
    }

    public abstract void SwapIn(DocumentDisplayController controller);
}
