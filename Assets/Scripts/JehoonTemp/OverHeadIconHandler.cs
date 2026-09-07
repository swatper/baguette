using System.Collections;
using UnityEngine;

public class OverHeadIconHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [Tooltip("머리위 바게트")]
    [SerializeField] private GameObject OverHeadBread;
    [Tooltip("머리위 크로아상")]
    [SerializeField] private GameObject OverHeadCroissan;
    [Tooltip("머리위 유로")]
    [SerializeField] private GameObject OverHeadEuro;

    public bool isBreadShown = false;
    public bool isCroissanShown = false;
    public bool isEuroShown = false;
    /// <summary>
    /// ShowBreadCoroutine을 실행한다.
    /// </summary>
    public void StartShowBread()
    {
        StartCoroutine(ShowBreadCoroutine());
    }


    public void StartShowEuro()
    {
        StartCoroutine(ShowEuroCoroutine());
    }

    public void StartShowCroissan()
    {
        StartCoroutine(ShowCroissanCoroutine());
    }

    private IEnumerator ShowEuroCoroutine()
    {
        OverHeadEuro.SetActive(true);
        isEuroShown = true;
        yield return new WaitForSeconds(1f);

        OverHeadEuro.SetActive(false);
        isEuroShown = false;
    }
    private IEnumerator ShowBreadCoroutine()
    {
        OverHeadBread.SetActive(true);
        isBreadShown = true;
        yield return new WaitForSeconds(1f);

        OverHeadBread.SetActive(false);
        isBreadShown = false;
    }

    private IEnumerator ShowCroissanCoroutine()
    {
        OverHeadCroissan.SetActive(true);
        isCroissanShown = true;
        yield return new WaitForSeconds(1f);

        OverHeadCroissan.SetActive(false);
        isCroissanShown = false;
    }
}
