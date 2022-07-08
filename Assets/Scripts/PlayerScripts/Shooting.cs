using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform forwardPoint;

    private InputSystem input;

    private void Start()
    {
        input = GetComponent<InputSystem>();
    }

    private void FixedUpdate()
    {
        if(input.Shooting)
        {
            var shootDirection = forwardPoint.position - transform.position;
            bulletPool.GetBullet(shootDirection.normalized);
        }
    }
}
