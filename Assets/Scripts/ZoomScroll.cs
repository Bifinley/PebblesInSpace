using UnityEngine;

public class ZoomScroll : MonoBehaviour
{
    // Used Tutorial by Gatsby
    // I dont know how to do this, so this is a learning process.

    private Camera mainCamera;
    private float zoomTarget;

    [SerializeField]
    private float multiplier = 2f, minZoom = 1f, maxZoom = 10f, smoothTime = 0.1f;
    private float velocity = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        zoomTarget = mainCamera.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        zoomTarget -= Input.GetAxisRaw("Mouse ScrollWheel") * multiplier;
        zoomTarget = Mathf.Clamp(zoomTarget, minZoom, maxZoom);
        mainCamera.orthographicSize = Mathf.SmoothDamp(mainCamera.orthographicSize, zoomTarget,ref velocity, smoothTime);
    }
}
