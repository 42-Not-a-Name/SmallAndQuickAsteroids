using UnityEngine;

public class paralax : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private float parallaxFactor = 0.5f;

    private Vector3 lastCamPosition;

    private void Start()
    {
        lastCamPosition = cam.position;
    }

    private void LateUpdate()
    {
        Vector3 delta = cam.position - lastCamPosition;

        transform.position += new Vector3(
            delta.x * parallaxFactor,
            delta.y * parallaxFactor,
            0
        );

        lastCamPosition = cam.position;
    }
}