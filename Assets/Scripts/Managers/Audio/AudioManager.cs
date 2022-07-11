using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioFiles audiofilesContainer;
    [SerializeField] AllEntitiesContainer allEntitiesContainer;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        allEntitiesContainer.PlayersShipControllers.OnSpeedUpEvent += PlaySpeedUpSound;
        allEntitiesContainer.PlayersShipControllers.GetComponent<Shooting>().OnShootEvent += PlayShootingSound;
        FollowAllAsteroidPools(allEntitiesContainer.AsteroidsPools);
    }

    private void FollowAllAsteroidPools(List<AsteroidPool> asteroidPools)
    {
        foreach(var pool in asteroidPools)
        {
            pool.OnDestroyEvent += AsteroidSoundEvents;
        }
    }

    public void PlayShootingSound()
    {
        audioSource.PlayOneShot(audiofilesContainer.ShootingSound);
    }

    public void PlaySpeedUpSound()
    {
        audioSource.PlayOneShot(audiofilesContainer.SpeedUpSound);
    }

    private void AsteroidSoundEvents(SpaceBodyController body)
    {
        body.GetComponent<NestedAsteroid>().OnDestroySoundEvent += PlayExplosionSound;
    }

    public void PlayExplosionSound()
    {
        audioSource.PlayOneShot(audiofilesContainer.ExplosionSound);
    }
}
