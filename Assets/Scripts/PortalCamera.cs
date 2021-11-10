using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform portal;
    public Transform otherPortal;

    private Transform playerCamera;

    private void Start()
    {
        playerCamera = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.position = portal.TransformPoint(otherPortal.InverseTransformPoint(playerCamera.position));
        transform.rotation = portal.transform.rotation * Quaternion.Inverse(otherPortal.transform.rotation) * playerCamera.transform.rotation;
    }
}
