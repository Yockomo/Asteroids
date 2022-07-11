using System;
using UnityEngine;

public class NestedAsteroid : Asteroid
{
    [SerializeField] private SpaceBodyController nestedAsteroids;
    [SerializeField] private int count;

    public int CountOfFragments { get { return count; } }

    public  event Action<SpaceBodyController> OnHitEvent;
    public  event Action<SpaceBodyController> OnDestroyEvent;
    public event Action OnDestroySoundEvent;

    public override void Hit()
    {
        OnHitEvent?.Invoke(GetComponent<SpaceBodyController>());
        Destroy();
    }

    public override void Destroy()
    {
        if(gameObject.activeSelf)
        {
            OnDestroyEvent?.Invoke(GetComponent<SpaceBodyController>());
            OnDestroySoundEvent?.Invoke();
        }
    }

    public SpaceBodyController GetNestedAsteroids()
    {
        return nestedAsteroids;
    }
}
