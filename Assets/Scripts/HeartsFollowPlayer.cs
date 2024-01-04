using UnityEngine;

public class HeartsFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform target; // Персонаж або камера, до якої ми хочемо прикріпити серця
    [SerializeField] private Vector3 offset = new Vector3(1.4f, 3f, 0f); // Зміщення серць відносно персонажа або камери

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
