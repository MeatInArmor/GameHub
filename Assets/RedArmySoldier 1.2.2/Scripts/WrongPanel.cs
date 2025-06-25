using System.Collections;
using TMPro;
using UnityEngine;

public class WrongPanel : MonoBehaviour
{
    [SerializeField] public TMP_Text wrongText;
    [SerializeField] public float timeout;


    public void OnEnable()
    {
        StartCoroutine(ClosePanel());

    }
    public void PanelOn()
    {
        gameObject.SetActive(true);
    }
    public IEnumerator ClosePanel()
    {
        yield return new WaitForSeconds(timeout);
        gameObject.SetActive(false);
    }
}
