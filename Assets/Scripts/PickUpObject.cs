using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private GameObject pickedUpObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (pickedUpObject == null)
            {
                Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    pickedUpObject = hit.collider.gameObject;
                    float oldDistance = Vector3.Distance(Camera.main.transform.position, pickedUpObject.transform.position);
                    SetObjectPosition(pickedUpObject.transform.position - Camera.main.transform.position);
                    RescaleObject(pickedUpObject, oldDistance, Vector3.Distance(Camera.main.transform.position, pickedUpObject.transform.position));
                    pickedUpObject.GetComponent<Rigidbody>().isKinematic = true;
                }
            }
            else
            {
                pickedUpObject.transform.parent = null;
                pickedUpObject.GetComponent<Rigidbody>().isKinematic = false;
                pickedUpObject = null;
            }
        }
    }

    private void SetObjectPosition(Vector3 direction)
    {
        pickedUpObject.transform.position = Camera.main.transform.position + (direction.normalized * 2);
        pickedUpObject.transform.parent = Camera.main.transform;
    }

    private void RescaleObject(GameObject go, float oldDistance, float newDistance)
    {
        float oldFrustumHeight = 2.0f * oldDistance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float newFrustumHeight = 2.0f * newDistance * Mathf.Tan(Camera.main.fieldOfView * 0.5f * Mathf.Deg2Rad);

        float scaleFactor = newFrustumHeight / oldFrustumHeight;
        go.transform.localScale = go.transform.localScale * scaleFactor;
    }
}
    