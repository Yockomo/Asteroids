using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LargeAsteroidsPool : AsteroidPool
{
    [Header("Large asteroids settings")]
    [SerializeField] private int bodiesToCreate;
    [SerializeField] private int randomPointsForCreate;
    [SerializeField] private int increaseCountPerSpawn;

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
        for (int i = 0; i < bodiesToCreate; i++)
        {
            spaceBodyPool.Get();
        }
        bodiesToCreate += increaseCountPerSpawn;
    }

    protected override void PoolStartFunction()
    {
        base.PoolStartFunction();
        FillRandomPointsLists();
    }

    private void FillRandomPointsLists()
    {
        var listOfYPoints = GetRandomPoints(ScreenSizeParameters.verticalHalfSize, randomPointsForCreate, 0.9f);
        var listOfXPoints = GetRandomPoints(ScreenSizeParameters.horizontalHalfSize, randomPointsForCreate, 0.9f);

        for (int i = 0; i < randomPointsForCreate; i++)
        {
            var xPoint = GetRandomPointFromList(listOfXPoints);
            var yPoint = GetRandomPointFromList(listOfYPoints);
            randomPointsInScreenArea.Add(new Vector2(xPoint,yPoint));
        }
    }

    private T GetRandomPointFromList<T>(List<T> list)
    {
        return list[Random.Range(0, list.Count)];
    }

    private List<float> GetRandomPoints(float borders, int count, float bordersPercent)
    {
        bordersPercent = bordersPercent > 0.9f ? 0.9f : bordersPercent;
        var listOfPoints = new List<float>();
        for(int i = 0; i<count;i++)
        {
        var randomMinusPoint = Random.Range(-borders * bordersPercent, -borders);
        listOfPoints.Add(randomMinusPoint);
        var randomPlusPoint = Random.Range(borders * bordersPercent, borders);
        listOfPoints.Add(randomPlusPoint);
        }
        return listOfPoints;
    }

    protected override SpaceBodyController ActionsOnCreate()
    {
        var randomSpawnPoint = GetRandomPointFromList(randomPointsInScreenArea);
        var body = Instantiate(spaceBodyPrefab, randomSpawnPoint, Quaternion.identity);
        var asteroid = body.GetComponent<NestedAsteroid>();
        AsteroidEvents(asteroid, true);
        return body;
    }

    protected override void ActionsOnGet(SpaceBodyController spaceBody)
    {
        base.ActionsOnGet(spaceBody);
        var randomDirection = GetRandomPointFromList(randomDirections);
        spaceBody.SetSpeedAndDirection(Random.Range(minSpeedOfBodies, maxSpeedOfBodies), randomDirection);
        spaceBody.StartMoving();
    }
}
