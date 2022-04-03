using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocumentDisplayController : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI titleBox = null;

    [SerializeField]
    TextMeshProUGUI textContentCanvas = null;
    [SerializeField]
    Image imageContentCanvas = null;

    public int swapCount { get; private set; }

    public void SetTitle(string title)
    {
        swapCount++;
        if (titleBox)
            titleBox.text = title;
    }

    public void SetContent(string textContent)
    {
        TurnOffAllCanvas();
        textContentCanvas?.gameObject.SetActive(true);
        textContentCanvas.text = textContent;
    }

    public void SetContent(Sprite imageContent)
    {
        TurnOffAllCanvas();
        imageContentCanvas.gameObject.SetActive(true);
        imageContentCanvas.sprite = imageContent;
    }

    private void TurnOffAllCanvas()
    {
        textContentCanvas?.gameObject.SetActive(false);
        imageContentCanvas?.gameObject.SetActive(false);
    }
}
