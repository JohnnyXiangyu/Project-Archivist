using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatCollector : MonoBehaviour
{
    public ArchiveLoader loader = null;
    public DocumentDisplayController controller = null;

    public TextMeshProUGUI textBox = null;

    private void OnEnable()
    {
        string statMessage = "STATISTICS\n\n";

        // visit count
        statMessage += ">> TOTAL VISITS: " + controller.swapCount + "\n";
        // different document count
        statMessage += ">> DOCUMENTS RETRIEVED: " + controller.visitMap.Count + "\n";

        textBox.text = statMessage;
    }
}
