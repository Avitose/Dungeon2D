using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float boundX = 0.3f;
    public float boundY = 0.15f;

    private Vector3 delts = Vector3.zero;
    private Vector3 destination;
    private Transform lookAt;

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    private void FixedUpdate()
    {
        delts = Vector3.zero;

        var deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if (transform.position.x < lookAt.position.x)
                delts.x = deltaX - boundX;
            else
                delts.x = deltaX + boundX;
        }

        var deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
                delts.y = deltaY - boundY;
            else
                delts.y = deltaY + boundY;
        }

        delts.z = 0f;
        destination = Vector3.Lerp(transform.position, transform.position + delts, 0.2f);
        destination.z = -1f;
        transform.position = destination;
    }
}