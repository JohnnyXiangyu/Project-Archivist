using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultAreaController : MonoBehaviour
{
    public DocumentDisplayController overallController = null;

    public int displayLimit { get; private set; }

    public GameObject resultPrefab = null;

    List<GameObject> results = new List<GameObject>();

    public void Clear()
    {
        foreach (var child in results)
        {
            Destroy(child);
        }
    }

    public void ShowResults(List<ArchiveDocument> resultDocuments)
    {
        foreach (ArchiveDocument doc in resultDocuments)
        {
            if (results.Count >= displayLimit)
                return;

            GameObject newResult = Instantiate(resultPrefab);
            SearchResultController resultController = newResult.GetComponent<SearchResultController>();
            
            // configure the result
            resultController.SetTitle(doc.documentTitle);
            resultController.overallController = overallController;
            resultController.targetDoc = doc;
            
            // instantiate the result
            newResult.transform.SetParent(transform);
            results.Add(newResult);
        }
    }
}
