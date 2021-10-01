using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Vector3 positionOffset, velocity;
    [SerializeField] float timeOffset;

    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + positionOffset,
            ref velocity, timeOffset);
    }
}
