using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : SpaceBodiePool
{
    [SerializeField] private Transform shootPosition;

    [SerializeField] private float bulletSpeed;

    private void CreateAsteroids()
    {
        spaceBodyPool.Get();
    }

    protected override void PoolStartFunction()
    {
        spaceBodyPool = new ObjectPool<SpaceBodyController>(createFunc: () => ActionsOnCreate(), actionOnGet: (body) => ActionsOnGet(body),
        actionOnRelease: (body) => ActionsOnRelease(body), actionOnDestroy: (body) => ActionsOnDestroy(body), defaultCapacity: 10, maxSize: 50);
    }

    protected override SpaceBodyController ActionsOnCreate()
    {
        var body = Instantiate(spaceBodyPrefab, shootPosition.position, Quaternion.identity);
        var bullet = body.GetComponent<Bullet>();
        BulletEvents(bullet, true);
        return body;
    }

    private void BulletEvents(Bullet bullet, bool follow)
    {
        if (follow)
        {
            bullet.OnHitEvent += spaceBodyPool.Release;
        }
        else
        {
            bullet.OnHitEvent -= spaceBodyPool.Release;
        }
    }

    protected override void ActionsOnGet(SpaceBodyController spaceBody)
    {
        //base.ActionsOnGet(spaceBody);
        //var direction
        //    spaceBody.SetSpeedAndDirection(bulletSpeed, randomDirection);
        //spaceBody.StartMoving();
    }

    protected override void ActionsOnRelease(SpaceBodyController spaceBody)
    {
        spaceBody.Stop();
        base.ActionsOnRelease(spaceBody);
    }

    protected override void ActionsOnDestroy(SpaceBodyController spaceBody)
    {
        //var asteroid = spaceBody.gameObject.GetComponent<Asteroid>();
        //AsteroidEvents(asteroid, false);
        //base.ActionsOnDestroy(spaceBody);
    }
}