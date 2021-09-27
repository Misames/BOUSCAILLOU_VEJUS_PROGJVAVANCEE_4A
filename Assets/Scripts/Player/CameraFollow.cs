
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float timeOffset;
    public Vector3 positionOffset;
    public Vector3 velocity;
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + positionOffset,
            ref velocity, timeOffset);
    }
}
