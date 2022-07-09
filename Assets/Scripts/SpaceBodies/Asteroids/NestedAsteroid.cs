using System;
using UnityEngine;

public class NestedAsteroid : Asteroid
{
    [SerializeField] private SpaceBodyController nestedAsteroids;
    [SerializeField] private int count;

    public int CountOfFragments { get { return count; } }

    public virtual event Action<SpaceBodyController> OnHitEvent;
    public virtual event Action<SpaceBodyController> OnDestroyEvent;

    public override void Hit()
    {
        OnHitEvent?.Invoke(GetComponent<SpaceBodyController>());
        Destroy();
    }

    public override void Destroy()
    {
        if(gameObject.activeSelf)
            OnDestroyEvent?.Invoke(GetComponent<SpaceBodyController>());
    }

    public SpaceBodyController GetNestedAsteroids()
    {
        return nestedAsteroids;
    }
}
