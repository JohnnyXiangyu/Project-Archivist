using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArchiveLoader : MonoBehaviour
{
    struct ArchiveRecord
    {
        public ArchiveDocument doc;
        public string fullText;
    }

    Dictionary<string, ArchiveRecord> allDocuments = new Dictionary<string, ArchiveRecord>();

    // Start is called before the first frame update
    void Start()
    {
        // load all archive documents
        var docs = Resources.LoadAll("Documents.Prod", typeof(ArchiveDocument)).Cast<ArchiveDocument>().ToArray();
        foreach (var doc in docs)
        {
            ArchiveRecord newRecord = new ArchiveRecord();
            newRecord.doc = doc;
            newRecord.fullText = doc.documentName + " " + doc.documentText;

            allDocuments.Add(newRecord.fullText, newRecord);

            Debug.Log(doc.documentName);
        }
    }
}
