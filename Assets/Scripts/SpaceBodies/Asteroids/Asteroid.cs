using System;
using UnityEngine;

public class Asteroid : MonoBehaviour, IHitable
{
    public Action<SpaceBodyController> OnHitEvent;
    public Action<SpaceBodyController> OnDestroyEvent;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable)
            && CorrectObjects(collision))
        {
            hitable.Hit();
            this.Destroy();
        }
    }

    protected virtual bool CorrectObjects(Collision2D collision)
    {
        return collision.gameObject.TryGetComponent<ShipController>(out ShipController playersShip);
    }

    public void Hit()
    {
        if(enabled)
            OnHitEvent?.Invoke(GetComponent<SpaceBodyController>());
    }

    public void Destroy()
    {
        if(enabled)
         OnDestroyEvent?.Invoke(GetComponent<SpaceBodyController>());
    }
}
