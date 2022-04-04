using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchResultController : MonoBehaviour
{
    public TextMeshProUGUI textBox = null;
    public ArchiveDocument targetDoc = null;
    public DocumentDisplayController overallController = null;

    public void SetTitle(string inTitle)
    {
        textBox.text = inTitle;
    }

    public void SwapIn()
    {
        targetDoc.SwapIn(overallController);
    }
}
