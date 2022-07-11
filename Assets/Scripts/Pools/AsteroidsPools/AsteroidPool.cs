using System;
using UnityEngine;

public class AsteroidPool : SpaceBodiePool
{
    [Header("Pool for nested asteroids")]
    [SerializeField] protected NestedAsteroidsPool nestedPool;

    [Header("Asteroids parameters")]
    [SerializeField, Range(0, 3)] protected float minSpeedOfBodies;
    [SerializeField, Range(0, 3)] protected float maxSpeedOfBodies;

    public event Action<SpaceBodyController> OnDestroyEvent;

    private void Start()
    {
        PoolStartFunction();
    }

    protected override SpaceBodyController ActionsOnCreate()
    {
        var body = Instantiate(spaceBodyPrefab, Vector3.zero, Quaternion.identity);
        var asteroid = body.GetComponent<NestedAsteroid>();
        AsteroidEvents(asteroid, true);
        return body;
    }

    protected override void ActionsOnDestroy(SpaceBodyController spaceBody)
    {
        var asteroid = spaceBody.gameObject.GetComponent<NestedAsteroid>();
        AsteroidEvents(asteroid, false);
        Destroy(spaceBody);
    }

    protected virtual void AsteroidEvents(NestedAsteroid asteroid, bool follow)
    {
        if (follow)
        {
            if (AsteroidHaveFragments(asteroid))
                asteroid.OnHitEvent += nestedPool.CreateAsteroids;
            asteroid.OnDestroyEvent += spaceBodyPool.Release;
            asteroid.OnDestroyEvent += OnDestroyEvent;
        }
        else
        {
            if (AsteroidHaveFragments(asteroid))
                asteroid.OnHitEvent -= nestedPool.CreateAsteroids;
            asteroid.OnDestroyEvent -= spaceBodyPool.Release;
            asteroid.OnDestroyEvent -= OnDestroyEvent;
        }
    }

    protected bool AsteroidHaveFragments(NestedAsteroid asteroid)
    {
        return asteroid.CountOfFragments > 0;
    }
}
