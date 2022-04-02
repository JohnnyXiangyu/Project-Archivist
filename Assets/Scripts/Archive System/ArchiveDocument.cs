using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Archive Document", menuName = "ScriptableObjects/ArchiveDocument")]
public class ArchiveDocument : ScriptableObject
{
    public string documentName = "";
    public string documentText = "";
}
