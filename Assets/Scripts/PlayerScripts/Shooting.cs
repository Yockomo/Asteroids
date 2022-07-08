using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform forwardPoint;
    [SerializeField] private float intervalBetweenShots;

    private InputSystem input;
    private bool cooldown;

    private void Start()
    {
        input = GetComponent<InputSystem>();
    }

    private void FixedUpdate()
    {
        if(input.Shooting && !cooldown)
        {
            cooldown = true;
            var shootDirection = forwardPoint.position - transform.position;
            bulletPool.GetBullet(shootDirection.normalized);
            StartCoroutine(CooldownBetweenShots());
        }
    }

    private IEnumerator CooldownBetweenShots()
    {
        yield return new WaitForSecondsRealtime(intervalBetweenShots);
        cooldown = false;
    }
}
