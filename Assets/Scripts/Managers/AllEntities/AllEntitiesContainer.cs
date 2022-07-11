using System.Collections.Generic;
using UnityEngine;

public class AllEntitiesContainer : MonoBehaviour
{
    [SerializeField] private List<AsteroidPool> asteroidsPools;
    public List<AsteroidPool> AsteroidsPools { get { return asteroidsPools; } }

    [SerializeField] private List<SpaceBodiePool> bulletsPools;
    public List<SpaceBodiePool> BulletsPools { get { return bulletsPools; } }

    [SerializeField] private ShipController shipController;
    public ShipController PlayersShipControllers { get { return shipController; } }

    [SerializeField] private UFO uFO;
    public UFO UFO { get { return uFO; } }
}
