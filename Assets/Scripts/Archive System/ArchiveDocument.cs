using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text.RegularExpressions;

public class ArchiveDocument : ScriptableObject
{
    public string documentName = "";

    public virtual string GetSearchIndex()
    {
        return documentName;
    }
}
