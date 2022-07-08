using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : SpaceBodiePool
{
    [SerializeField] private Transform shootingPointTransform;

    [SerializeField] private float bulletSpeed;

    private Vector2 currentDirection;

    public void GetBullet(Vector2 shootDirection)
    {
        currentDirection = shootDirection;
        spaceBodyPool.Get();
    }

    protected override SpaceBodyController ActionsOnCreate()
    {
        var body = Instantiate(spaceBodyPrefab, shootingPointTransform.position, Quaternion.identity);
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
        spaceBody.transform.position = shootingPointTransform.position;
        spaceBody.SetSpeedAndDirection(bulletSpeed, currentDirection);
        spaceBody.gameObject.SetActive(true);
        spaceBody.StartMoving();
    }

    protected override void ActionsOnRelease(SpaceBodyController spaceBody)
    {
        base.ActionsOnRelease(spaceBody);
        spaceBody.Stop();
        spaceBody.gameObject.GetComponent<Bullet>().StopAllCoroutines();
    }

    protected override void ActionsOnDestroy(SpaceBodyController spaceBody)
    {
        var bullet = spaceBody.gameObject.GetComponent<Bullet>();
        BulletEvents(bullet, false);
        base.ActionsOnDestroy(spaceBody);
    }
}