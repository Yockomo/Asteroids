using System;
using UnityEngine;

public class Asteroid : MonoBehaviour, IHitable
{
    public Action<SpaceBodyController> OnHitEvent;
    public Action<SpaceBodyController> OnDestroyEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth health))
        {
            health.Hit();
            this.Destroy();
        }
    }

    public void Hit()
    {
        OnHitEvent?.Invoke(GetComponent<SpaceBodyController>());
    }

    public void Destroy()
    {
        OnDestroyEvent?.Invoke(GetComponent<SpaceBodyController>());
    }
}
