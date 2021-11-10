using UnityEngine;

public class AntiPerspective : MonoBehaviour
{
    private float originalFrustumHeight;
    private Vector3 originalScale;

    private void Start()
    {
        originalFrustumHeight = 2.0f * Vector3.Distance(Camera.main.gameObject.transform.position, transform.position) * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        originalScale = transform.localScale;
    }

    private void Update()
    {
        float newFrustumHeight = 2.0f * Vector3.Distance(Camera.main.gameObject.transform.position, transform.position) * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

        float scaleFactor = newFrustumHeight / originalFrustumHeight;
        gameObject.transform.localScale = originalScale * scaleFactor;
    }
}
