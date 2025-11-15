using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;


    private void FixedUpdate()
    {
        transform.position += transform.forward * (_moveSpeed * Time.deltaTime);
    }
}