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
    Canvas menuCanvas = null;
    [SerializeField]
    Canvas searchCanvas = null;
    [SerializeField]
    Canvas displayCanvas = null;

    [SerializeField]
    TextMeshProUGUI textDisplay = null;
    [SerializeField]
    Image imageDisplay = null;

    [SerializeField]
    TMP_InputField inputField = null;

    enum DisplayState
    {
        MENU,
        SEARCH,
        VIEW,
        LENGTH
    }

    DisplayState currentState = DisplayState.MENU;

    public int swapCount { get; private set; }

    public void SetTitle(string title)
    {
        swapCount++;

        if (titleBox)
            titleBox.text = title;
    }

    public void SetContent(string textContent)
    {
        TurnOffAllDisplays();
        this.textDisplay?.gameObject.SetActive(true);
        this.textDisplay.text = textContent;
    }

    public void SetContent(Sprite imageContent)
    {
        TurnOffAllDisplays();
        imageDisplay.gameObject.SetActive(true);
        imageDisplay.sprite = imageContent;
    }

    private void TurnOffAllDisplays()
    {
        textDisplay?.gameObject.SetActive(false);
        imageDisplay?.gameObject.SetActive(false);
    }

    private void TurnOffAllCanvases()
    {
        menuCanvas?.gameObject.SetActive(false);
        searchCanvas?.gameObject.SetActive(false);
        displayCanvas?.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        currentState = (currentState == 0) ? currentState : currentState - 1;
        Refresh();
    }

    public void Refresh()
    {
        TurnOffAllCanvases();

        switch (currentState)
        {
            case DisplayState.MENU:
                menuCanvas?.gameObject.SetActive(true);
                break;
            case DisplayState.SEARCH:
                searchCanvas?.gameObject.SetActive(true);
                break;
            case DisplayState.VIEW:
                displayCanvas?.gameObject.SetActive(true);
                break;
        }
    }

    private void Update()
    {
        if (currentState == DisplayState.SEARCH)
            inputField?.Select();
    }
}
