using System;
using UnityEngine;

public class Asteroid : MonoBehaviour, IHitable
{
    public Action<SpaceBodyController> OnHitEvent;
    public Action<SpaceBodyController> OnDestroyEvent;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable)
            && !IgnoreObjects(collision))
        {
            hitable.Hit();
            this.Destroy();
        }
    }

    protected virtual bool IgnoreObjects(Collision2D collision)
    {
        return collision.gameObject.TryGetComponent<Asteroid>(out Asteroid asteroid);
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
