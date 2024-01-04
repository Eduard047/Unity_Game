using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    [SerializeField] private float followSpeed;

   
    private float currentLookAhead;
    private Vector3 followVelocity = Vector3.zero;

    private void Update()
    {
         // Follow player
        float targetLookAhead = aheadDistance * Mathf.Sign(player.localScale.x);
        currentLookAhead = Mathf.SmoothDamp(currentLookAhead, targetLookAhead, ref followVelocity.x, followSpeed);

        transform.position = new Vector3(
            player.position.x + currentLookAhead,
            transform.position.y,
            transform.position.z
        );
    }
}