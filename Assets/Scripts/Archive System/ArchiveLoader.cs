using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArchiveLoader : MonoBehaviour
{    
    HashSet<char> removedCharacters = new HashSet<char>() { ',', '.', '/', '(', ')', '[', ']', '!', '@', '#', '$', '%', '^', '&', '*', ' ' };

    Dictionary<string, HashSet<ArchiveDocument>> searchIndex = new Dictionary<string, HashSet<ArchiveDocument>>();
    Dictionary<ArchiveDocument, HashSet<string>> reverseIndex = new Dictionary<ArchiveDocument, HashSet<string>>();

    public List<ArchiveDocument> archiveAll = new List<ArchiveDocument>();

    public List<ArchiveDocument> Search(string query)
    {
        List<ArchiveDocument> result = null;

        List<string> keywords = ExtractWords(query);

        // THIS IS AND SEARCH
        foreach (string word in keywords)
        {
            if (result == null)
            {
                if (searchIndex.ContainsKey(word))
                {
                    foreach (ArchiveDocument document in searchIndex[word])
                    {
                        result = new List<ArchiveDocument>(searchIndex[word]);
                    }
                }
            }
            else
            {
                result.RemoveAll(x => !reverseIndex[x].Contains(word));
            }
        }

        return result;
    }

    void Start()
    {
        // load all archive documents
        // var docs = Resources.LoadAll("Documents.Prod", typeof(ArchiveDocument)).Cast<ArchiveDocument>().ToArray();
        foreach (var doc in archiveAll)
        {
            List<string> extractedWords = ExtractWords(doc.GetSearchIndex());

            // remove symbols, break up words, insert into search index
            foreach (string word in extractedWords)
            {
                if (searchIndex.ContainsKey(word))
                {
                    searchIndex[word].Add(doc);
                }
                else
                {
                    searchIndex.Add(word, new HashSet<ArchiveDocument>() { doc });
                }
            }

            // insert reverse index
            HashSet<string> wordSet = new HashSet<string>(extractedWords);
            reverseIndex.Add(doc, wordSet);
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

        for (int i = 0; i < words.Count; i++)
        {
            words[i] = words[i].ToLower();
        }

        return words;
    }
}
