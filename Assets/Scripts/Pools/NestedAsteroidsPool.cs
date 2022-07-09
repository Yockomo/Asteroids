using UnityEngine;

public class NestedAsteroidsPool : SpaceBodiePool
{
    [Header("Pool for medium asteroids")]
    [SerializeField] private NestedAsteroidsPool nestedPool;
    
    [Header("Asteroids parameters")]
    [SerializeField, Range(0, 3)] private float minSpeedOfBodies;
    [SerializeField, Range(0, 3)] private float maxSpeedOfBodies;

    private Vector3 currentSpawnPosition;
    private float currentSpawnSpeed;
    private Vector3 currentDirection;

    public void CreateAsteroids(SpaceBodyController asteroid)
    {
        if(asteroid != null && asteroid.gameObject.TryGetComponent<NestedAsteroid>(out NestedAsteroid nestedAsteroid))
        {
            currentSpawnPosition = asteroid.transform.position;
            GetAsteroids(nestedAsteroid.CountOfFragments, asteroid);
        }
    }

    private void GetAsteroids(int count, SpaceBodyController asteroid)
    {
        currentSpawnSpeed = Random.Range(minSpeedOfBodies, maxSpeedOfBodies);
        currentDirection = (Quaternion.Euler(0,0,-45) * asteroid.GetDirection()).normalized;

        for (int i =0; i<count; i++)
        {
            spaceBodyPool.Get();
            currentDirection = (Quaternion.Euler(0, 0, 45) * currentDirection).normalized;
        }
    }

    protected override SpaceBodyController ActionsOnCreate()
    {
        var body = Instantiate(spaceBodyPrefab, currentSpawnPosition, Quaternion.identity);
        var asteroid = body.GetComponent<NestedAsteroid>();
        AsteroidEvents(asteroid, true);
        return body;
    }

    protected void AsteroidEvents(NestedAsteroid asteroid, bool follow)
    {
        if (follow)
        {
            if(asteroid.CountOfFragments>0)
                asteroid.OnHitEvent += nestedPool.CreateAsteroids;
            asteroid.OnDestroyEvent += spaceBodyPool.Release;
        }
        else
        {
            if (asteroid.CountOfFragments > 0)
                asteroid.OnHitEvent -= nestedPool.CreateAsteroids;
            asteroid.OnDestroyEvent -= spaceBodyPool.Release;
        }
    }

    protected override void ActionsOnGet(SpaceBodyController spaceBody)
    {
        spaceBody.transform.position = currentSpawnPosition;
        spaceBody.gameObject.SetActive(true);
        activeBodiesInPool.Add(spaceBody);
        spaceBody.SetSpeedAndDirection(currentSpawnSpeed, currentDirection);
        spaceBody.StartMoving();
    }

    protected override void ActionsOnRelease(SpaceBodyController spaceBody)
    {
        base.ActionsOnRelease(spaceBody);
        spaceBody.Stop();
    }

    protected override void ActionsOnDestroy(SpaceBodyController spaceBody)
    {
        var asteroid = spaceBody.gameObject.GetComponent<NestedAsteroid>();
        AsteroidEvents(asteroid, false);
        base.ActionsOnDestroy(spaceBody);
    }
}
