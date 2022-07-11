using System;
using System.Collections;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private BulletPool bulletPool;
    [SerializeField] private Transform forwardPoint;
    [SerializeField] private float intervalBetweenShots;

    private InputSystem input;
    private bool cooldown;

    public event Action OnShootEvent;

    private void OnDestroy()
    {
        OnShootEvent = null;
    }

    private void Start()
    {
        input = GetComponent<InputSystem>();
    }

    public void Shoot()
    {
        if (input.Shooting && !cooldown)
        {
            OnShootEvent?.Invoke();
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
