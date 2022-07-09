using UnityEngine;

public class NestedAsteroid : Asteroid
{
    [SerializeField] private Asteroid nestedAsteroids;
    [SerializeField] private int count;
}
