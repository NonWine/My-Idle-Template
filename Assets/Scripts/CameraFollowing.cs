using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private float speedLerp;
    [SerializeField] private Vector3 startCamPos;
    private Transform player;

    void LateUpdate()
    {

        player = Player.Instance.transform;
        FollowToPlayer();
    }

    private void FollowToPlayer()
    {
        if (transform.position != player.position)
           transform.position = Vector3.MoveTowards(transform.position,player.position + startCamPos , speedLerp);
    }

}
