using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchBarAutoSubmit : MonoBehaviour
{
    public DocumentDisplayController documentDisplayController;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SubmitSearch();
        }
    }

    public void SubmitSearch()
    {
        documentDisplayController.InitiateSearch(GetComponent<TMP_InputField>().text);
    }
}
