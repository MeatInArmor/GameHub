using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragablePanel : MonoBehaviour
{
    [SerializeField] public CanvasController canvasController;
    [SerializeField] public RectTransform rt;


    [SerializeField] public bool isOpen = false;
    [SerializeField] public Vector2[] coordinates = new Vector2[2];

    [SerializeField] public float swapDuration = 0.7f;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }
    public void OpenCloseButtonClick()
    {
            StartCoroutine(OpenCloseInventory());
    }

    private IEnumerator OpenCloseInventory()
    {
        isOpen = !isOpen;
        Vector2 startPosition = rt.anchoredPosition;
        Vector2 targetPosition = coordinates[Convert.ToInt32(isOpen)]; 
        float elapsed = 0f;

        while (elapsed < swapDuration)
        {
            float t = elapsed / swapDuration;
            float smoothStep = t * t * (3f - 2f * t);
            rt.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, smoothStep);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rt.anchoredPosition = targetPosition;
    }
}
