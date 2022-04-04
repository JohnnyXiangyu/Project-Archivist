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

    
    HashSet<char> removedCharacters = new HashSet<char>() { ',', '.', '/', '(', ')', '[', ']', '!', '@', '#', '$', '%', '^', '&', '*', ' ' };

    Dictionary<string, ArchiveRecord> allDocuments = new Dictionary<string, ArchiveRecord>();
    Dictionary<string, List<ArchiveDocument>> searchIndex = new Dictionary<string, List<ArchiveDocument>>();

    public List<ArchiveDocument> Search(string query, int limit = -1)
    {
        List<ArchiveDocument> result = new List<ArchiveDocument>();

        List<string> keywords = ExtractWords(query);

        foreach (string word in keywords)
        {
            if (searchIndex.ContainsKey(word))
            {
                foreach (ArchiveDocument document in searchIndex[word])
                {
                    if (limit > 0 && result.Count > limit)
                        return result;

                    result.Add(document);
                }
            }
        }

        return result;
    }

    // Start is called before the first frame update
    void Start()
    {
        // load all archive documents
        var docs = Resources.LoadAll("Documents.Prod", typeof(ArchiveDocument)).Cast<ArchiveDocument>().ToArray();
        foreach (var doc in docs)
        {
            ArchiveRecord newRecord = new ArchiveRecord();
            newRecord.doc = doc;
            newRecord.fullText = doc.GetSearchIndex();

            allDocuments.Add(newRecord.fullText, newRecord);

            Debug.Log(doc.documentTitle);

            // remove symbols, break up words, insert into search index
            List<string> extractedWords = ExtractWords(doc.GetSearchIndex());
            foreach (string word in extractedWords)
            {
                if (searchIndex.ContainsKey(word))
                {
                    searchIndex[word].Add(doc);
                }
                else
                {
                    searchIndex.Add(word, new List<ArchiveDocument>() { doc });
                }
            }
        }
    }

    List<string> ExtractWords(string source)
    {
        List<string> words = new List<string>();
        words.Add("");

        foreach (char c in source)
        {
            if (!removedCharacters.Contains(c))
            {
                words[words.Count - 1] += c;
            }
            else if (words[words.Count - 1] != "")
            {
                words.Add("");
            }
        }

        return words;
    }
}
