using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAreaController : MonoBehaviour
{
    public DocumentDisplayController overallController = null;

    public int displayLimit;

    public GameObject resultPrefab = null;

    // List<GameObject> results = new List<GameObject>();

    public void Clear()
    {
        for (int i = 0; i < transform.childCount; i ++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        // results = new List<GameObject>();
    }

    public void ShowResults(List<ArchiveDocument> resultDocuments)
    {
        foreach (ArchiveDocument doc in resultDocuments)
        {
            if (transform.childCount >= displayLimit)
                return;

            GameObject newResult = Instantiate(resultPrefab);
            SearchResultController resultController = newResult.GetComponent<SearchResultController>();
            
            // configure the result
            resultController.SetTitle(doc.GetTitle(overallController));
            resultController.overallController = overallController;
            resultController.targetDoc = doc;
            
            // instantiate the result
            newResult.transform.SetParent(transform, false);
            // results.Add(newResult);
        }
    }


}
