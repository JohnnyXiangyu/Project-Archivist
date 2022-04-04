using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DocumentDisplayController : MonoBehaviour
{
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
    TextMeshProUGUI viewTitleBox = null;
    [SerializeField]
    TextMeshProUGUI viewTextDisplay = null;
    [SerializeField]
    Image viewImageDisplay = null;
    [SerializeField]
    Pager viewPager = null;
    [SerializeField]
    RectTransform imageReferencePort;

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
        currentState = DisplayState.VIEW;
        Refresh();

        swapCount++;

        if (viewTitleBox)
            viewTitleBox.text = title;
    }

    public void SetContent(string textContent)
    {
        TurnOffAllDisplays();
        viewTextDisplay?.gameObject.SetActive(true);
        viewTextDisplay.text = textContent;
        viewPager.RefreshPage();
    }

    public void SetContent(Sprite imageContent)
    {
        TurnOffAllDisplays();
        viewImageDisplay.gameObject.SetActive(true);
        viewImageDisplay.sprite = imageContent;

        // image resize
        viewImageDisplay.SetNativeSize();
        float xFactor = 1;
        float yFactor = 1;
        if (viewImageDisplay.rectTransform.sizeDelta.x > imageReferencePort.sizeDelta.x)
            xFactor = viewImageDisplay.rectTransform.sizeDelta.x / imageReferencePort.sizeDelta.x;
        if (viewImageDisplay.rectTransform.sizeDelta.y > imageReferencePort.sizeDelta.y)
            yFactor = viewImageDisplay.rectTransform.sizeDelta.y / imageReferencePort.sizeDelta.y;

        Vector2 tempsize = viewImageDisplay.rectTransform.sizeDelta;

        tempsize.x /= Mathf.Max(xFactor, yFactor);
        tempsize.y /= Mathf.Max(xFactor, yFactor);
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

    public void GoToSearch()
    {
        currentState = DisplayState.SEARCH;
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

    public void InitiateSearch(string query)
    {
        searchResultArea.Clear();

        List<ArchiveDocument> searchResult = archiveLoader.Search(query); // this proxy function is only here due to mediator pattern ... so stupid 
        if (searchResult != null)
            searchResultArea.ShowResults(searchResult);
        else
            Debug.Log("didn't find anything");
    }

    private void Start()
    {
        currentState = DisplayState.MENU;
        Refresh();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GoBack();
        }
    }
}
