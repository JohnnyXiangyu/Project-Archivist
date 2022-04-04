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
    ArchiveLoader archiveLoader = null;

    // canvases
    [SerializeField]
    Canvas menuCanvas = null;
    [SerializeField]
    Canvas searchCanvas = null;
    [SerializeField]
    Canvas viewCanvas = null;

    // view canvas
    [SerializeField]
    TextMeshProUGUI viewTextDisplay = null;
    [SerializeField]
    Image viewImageDisplay = null;

    // search canvas
    [SerializeField]
    ResultAreaController searchResultArea = null;

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
        this.viewTextDisplay?.gameObject.SetActive(true);
        this.viewTextDisplay.text = textContent;
    }

    public void SetContent(Sprite imageContent)
    {
        TurnOffAllDisplays();
        viewImageDisplay.gameObject.SetActive(true);
        viewImageDisplay.sprite = imageContent;
    }

    private void TurnOffAllDisplays()
    {
        viewTextDisplay?.gameObject.SetActive(false);
        viewImageDisplay?.gameObject.SetActive(false);
    }

    private void TurnOffAllCanvases()
    {
        menuCanvas?.gameObject.SetActive(false);
        searchCanvas?.gameObject.SetActive(false);
        viewCanvas?.gameObject.SetActive(false);
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
                viewCanvas?.gameObject.SetActive(true);
                break;
        }
    }

    public void InitiateSearch(string query, int resultLimit = -1)
    {
        List<ArchiveDocument> searchResult = archiveLoader.Search(query, resultLimit); // this proxy function is only here due to mediator pattern ... so stupid 
        searchResultArea.ShowResults(searchResult);
    }
}
