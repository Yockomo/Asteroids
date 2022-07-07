using System.Collections.Generic;
using UnityEngine;

public class AsteroidsPool : SpaceBodiePool
{
    [SerializeField] private int BodiesToCreate;
    [SerializeField] private int RandomPointsForCreate;
    [SerializeField, Range(0, 3)] private float minSpeedOfBodies;
    [SerializeField, Range(0, 3)] private float maxSpeedOfBodies;

    private List<Vector2> randomPointsInScreenArea = new List<Vector2>();
    private List<Vector2> randomDirections = new List<Vector2>()
    {
        Vector2.up, Vector2.down, Vector2.right,Vector2.left,
        new Vector2(1,1), new Vector2(-1,1), new Vector2(1,-1), new Vector2(-1,-1),
    };

    private void FixedUpdate()
    {
        if (GetActiveBodies().Count < 1)
        {
            CreateAsteroids();
        }
    }

    private void CreateAsteroids()
    {
        for (int i = 0; i < BodiesToCreate; i++)
        {
            spaceBodyPool.Get();
        }
    }

    protected override void PoolStartFunction()
    {
        base.PoolStartFunction();
        FillRandomPointsLists();
    }

    private void FillRandomPointsLists()
    {
        var insideCircleRadius = GetRadiusInsideScreen();
        for (int i = 0; i < RandomPointsForCreate; i++)
        {
            randomPointsInScreenArea.Add(Random.insideUnitCircle * insideCircleRadius);
        }
    }

    private float GetRadiusInsideScreen()
    {
        var verticalHalfSize = Camera.main.orthographicSize;
        var horizontalHalfSize = verticalHalfSize * Screen.width / Screen.height;
        return Mathf.Min(verticalHalfSize, horizontalHalfSize);
    }

    protected override SpaceBodyController ActionsOnCreate()
    {
        var randomSpawnPoint = GetRandomPoint(randomPointsInScreenArea);
        var body = Instantiate(spaceBodyPrefab, randomSpawnPoint, Quaternion.identity);
        var asteroid = body.GetComponent<Asteroid>();
        AsteroidEvents(asteroid, true);
        return body;
    }

    private void AsteroidEvents(Asteroid asteroid, bool follow)
    {
        if (follow)
        {
            asteroid.OnHitEvent += spaceBodyPool.Release;
            asteroid.OnDestroyEvent += spaceBodyPool.Release;
        }
        else
        {
            asteroid.OnHitEvent -= spaceBodyPool.Release;
            asteroid.OnDestroyEvent -= spaceBodyPool.Release;
        }
    }

    private Vector2 GetRandomPoint(List<Vector2> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    protected override void ActionsOnGet(SpaceBodyController spaceBody)
    {
        base.ActionsOnGet(spaceBody);
        var randomDirection = GetRandomPoint(randomDirections);
        spaceBody.SetSpeedAndDirection(Random.Range(minSpeedOfBodies, maxSpeedOfBodies), randomDirection);
        spaceBody.StartMoving();
    }

    protected override void ActionsOnRelease(SpaceBodyController spaceBody)
    {
        spaceBody.Stop();
        base.ActionsOnRelease(spaceBody);
    }

    protected override void ActionsOnDestroy(SpaceBodyController spaceBody)
    {
        var asteroid = spaceBody.gameObject.GetComponent<Asteroid>();
        AsteroidEvents(asteroid, false);
        base.ActionsOnDestroy(spaceBody);
    }
}
