using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Archive Image", menuName = "ScriptableObjects/ArchiveImage")]
public class ImageDocument : ArchiveDocument
{
    public Image documentImage;

    public override void SwapIn(DocumentDisplayController controller)
    {
        controller.SetTitle(documentTitle);
        controller.SetContent(documentImage);
    }
}
