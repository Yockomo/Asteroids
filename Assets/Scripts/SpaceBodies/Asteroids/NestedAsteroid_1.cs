using System;
using UnityEngine;

public class NestedAsteroid1 : Asteroid
{
    [SerializeField] private SpaceBodyController nestedAsteroids;
    [SerializeField] private int count;

    public int CountOfFragments { get { return count; } }

    public virtual event Action<SpaceBodyController> OnHitEvent;
    public virtual event Action<SpaceBodyController> OnDestroyEvent;

    public override void Hit()
    {
        OnHitEvent?.Invoke(nestedAsteroids);
        Destroy();
    }

    public override void Destroy()
    {
        OnDestroyEvent?.Invoke(GetComponent<SpaceBodyController>());
    }

    public SpaceBodyController GetNestedAsteroids()
    {
        return nestedAsteroids;
    }
}
