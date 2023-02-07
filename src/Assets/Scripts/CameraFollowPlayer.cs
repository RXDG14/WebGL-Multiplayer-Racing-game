using UnityEngine;

public class CameraFollowPlayer: MonoBehaviour
{
    [SerializeField] float yPos;
    [SerializeField] float zPos;
    [SerializeField] float smoothTime;
    [SerializeField] Transform player;
    Vector3 currentVelocity = Vector3.zero;

    void LateUpdate()
    {
        if(player != null)
        {
            //transform.position = player.transform.position + new Vector3(0, yPos, zPos);// * Time.deltaTime;
            Vector3 targetPosition = player.transform.position + new Vector3(0, yPos, zPos);
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition,ref currentVelocity,smoothTime);
        }
    }
}