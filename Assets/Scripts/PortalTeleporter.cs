using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
public class PortalTeleporter : MonoBehaviour
{
    public Transform receiver;
    public GameObject[] toEnable;

    private Transform player;
    private bool playerIsOverlapping = false;
    private MSCameraController firstPersonController;

    private void Start()
    {
        player = Camera.main.transform.parent ? Camera.main.transform.parent : Camera.main.transform;
        firstPersonController = player.GetComponent<MSCameraController>();
    }

    private void Update()
    {
         if (playerIsOverlapping)
        {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            // If this is true: The player has moved across the portal
            if (dotProduct < 0f)
            {
                // Teleport him!
                float rotationDiff = -Quaternion.Angle(transform.rotation, receiver.rotation);
                rotationDiff += 180;
                player.Rotate(Vector3.up, rotationDiff);

                Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
                firstPersonController.enabled = false;
                Invoke("EnableController", 0.05f);
                player.position = receiver.position + positionOffset;

                if (toEnable.Length != 0) SetToEnable(!toEnable[0].activeSelf);

                playerIsOverlapping = false;
            }
        }
    }

    private void EnableController()
    {
        firstPersonController.enabled = true;
    }

    private void SetToEnable(bool enable)
    {
        foreach(GameObject gameObject in toEnable)
        {
            gameObject.SetActive(enable);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            playerIsOverlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "MainCamera")
        {
            playerIsOverlapping = false;
        }
    }
}
