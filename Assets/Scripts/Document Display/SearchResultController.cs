using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SearchResultController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public TextMeshProUGUI textBox = null;
    public ArchiveDocument targetDoc = null;
    public DocumentDisplayController overallController = null;
    public Image background = null;

    public Color textColor = Color.white;

    // event interface
    public void OnPointerClick(PointerEventData eventData)
    {
        SwapIn();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color tempColor = background.color;
        tempColor.a = 1;
        background.color = tempColor;

        textBox.color = Color.black;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color tempColor = background.color;
        tempColor.a = 0;
        background.color = tempColor;

        textBox.color = textColor;
    }

    // interface for other components
    public void SetTitle(string inTitle)
    {
        textBox.text = inTitle;
        textBox.color = textColor;
    }

    public void SwapIn()
    {
        targetDoc.SwapIn(overallController);
    }
}
