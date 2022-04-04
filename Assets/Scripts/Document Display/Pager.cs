using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pager : MonoBehaviour
{
    public TextMeshProUGUI displayBox;
    public TextMeshProUGUI indicatorBox;

    public void RefreshPage()
    {
        displayBox.ForceMeshUpdate();
        displayBox.pageToDisplay = 1;
        UpdateIndicator();
    }

    private void UpdateIndicator()
    {
        indicatorBox.text = "page " + displayBox.pageToDisplay + " of " + displayBox.textInfo.pageCount;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && displayBox.pageToDisplay < displayBox.textInfo.pageCount)
        {
            displayBox.pageToDisplay++;
            UpdateIndicator();
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && displayBox.pageToDisplay > 1)
        {
            displayBox.pageToDisplay--;
            UpdateIndicator();
        }
    }
}
