using UnityEngine;

public class PortalTextureSetUp : MonoBehaviour
{
    public new Camera camera;

    private void Start()
    {
        if (camera.targetTexture != null)
            camera.targetTexture.Release();

        camera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        GetComponent<Renderer>().material.mainTexture = camera.targetTexture;
    }
}
