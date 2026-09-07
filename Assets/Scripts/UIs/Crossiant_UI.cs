using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class Crossiant_UI : MonoBehaviour
{
    public RawImage crossiantImage;
    public Color unUsableColor;
    void Start()
    {
        crossiantImage.color = unUsableColor;
    }
    public void SetCossiantActive()
    {
        crossiantImage.color = Color.white;
    }
}
