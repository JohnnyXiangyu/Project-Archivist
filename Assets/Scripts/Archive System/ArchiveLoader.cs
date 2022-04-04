using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArchiveLoader : MonoBehaviour
{    
    HashSet<char> removedCharacters = new HashSet<char>() { ',', '.', '/', '(', ')', '[', ']', '!', '@', '#', '$', '%', '^', '&', '*', ' ' };

    Dictionary<string, List<ArchiveDocument>> searchIndex = new Dictionary<string, List<ArchiveDocument>>();
    Dictionary<ArchiveDocument, HashSet<string>> reverseIndex = new Dictionary<ArchiveDocument, HashSet<string>>();

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
        var docs = Resources.LoadAll("Documents.Prod", typeof(ArchiveDocument)).Cast<ArchiveDocument>().ToArray();
        foreach (var doc in docs)
        {
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

        return words;
    }
}
