using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dummy_TextLoader : MonoBehaviour
{
    public int visitCount = 0;
    public TextMeshProUGUI titleBox = null;
    public TextMeshProUGUI textBox = null;
    public TextDocument document = null;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        titleBox.text = document.GetName(visitCount);
        textBox.text = document.GetContent(visitCount);
    }
}
