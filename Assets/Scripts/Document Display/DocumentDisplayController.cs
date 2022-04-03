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
    Canvas textContentCanvas = null;
    [SerializeField]
    Canvas imageContentCanvas = null;

    public int swapCount { get; private set; }

    public void SetTitle(string title)
    {
        swapCount++;
        titleBox.text = title;
    }

    public void SetContent(string textContent)
    {
        TurnOffAllCanvas();
        textContentCanvas.gameObject.SetActive(true);

        // TODO: put content in text content canvas
    }

    public void SetContent(Image imageContent)
    {
        TurnOffAllCanvas();
        imageContentCanvas.gameObject.SetActive(true);

        // TODO: put content in image content canvas
    }

    private void TurnOffAllCanvas()
    {
        textContentCanvas.gameObject.SetActive(false);
        imageContentCanvas.gameObject.SetActive(false);
    }
}
