using System;
using UnityEngine;

public class NestedAsteroidsPool : AsteroidPool
{
    [SerializeField] private int angleOfSerparation;

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
        currentSpawnSpeed = UnityEngine.Random.Range(minSpeedOfBodies, maxSpeedOfBodies);
        currentDirection = (Quaternion.Euler(0,0,-angleOfSerparation) * asteroid.GetDirection()).normalized;

        for (int i =0; i<count; i++)
        {
            spaceBodyPool.Get();
            currentDirection = (Quaternion.Euler(0, 0, angleOfSerparation) * currentDirection).normalized;
        }
    }

    protected override SpaceBodyController ActionsOnCreate()
    {
        var body = Instantiate(spaceBodyPrefab, currentSpawnPosition, Quaternion.identity);
        var asteroid = body.GetComponent<NestedAsteroid>();
        AsteroidEvents(asteroid, true);
        return body;
    }

    protected override void ActionsOnGet(SpaceBodyController spaceBody)
    {
        spaceBody.transform.position = currentSpawnPosition;
        spaceBody.gameObject.SetActive(true);
        activeBodiesInPool.Add(spaceBody);
        spaceBody.SetSpeedAndDirection(currentSpawnSpeed, currentDirection);
        spaceBody.StartMoving();
    }
}
