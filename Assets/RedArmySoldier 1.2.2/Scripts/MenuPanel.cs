using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    [SerializeField] public CanvasController canvasController;

    [SerializeField] public bool isOpen = false;
    [SerializeField] public float swapDistance = 1120f;
    [SerializeField] public float swapDuration = 0.7f;

    public void MenuButtonClick()
    {
            StartCoroutine(OpenCloseMenu());
    }
    private IEnumerator OpenCloseMenu()
    {
        Vector2 startPosition = transform.localPosition;
        Vector2 targetPosition = startPosition + new Vector2(isOpen ? -swapDistance : swapDistance, 0);
        isOpen = !isOpen;
        float elapsed = 0f;

        while (elapsed < swapDuration)
        {
            float t = elapsed / swapDuration;
            float smoothStep = t * t * (3f - 2f * t);
            transform.localPosition = Vector2.Lerp(startPosition, targetPosition, smoothStep);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = targetPosition;
    }
}
