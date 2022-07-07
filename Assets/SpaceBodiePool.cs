using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class SpaceBodiePool : MonoBehaviour
{
    [Header("Pool parameters")]
    [SerializeField] protected SpaceBodyController spaceBodyPrefab;

    protected ObjectPool<SpaceBodyController> spaceBodyPool;
    protected List<SpaceBodyController> bodiesInPool = new List<SpaceBodyController>();

    private void Start()
    {
        PoolStartFunction();
    }

    protected virtual void PoolStartFunction()
    {
        spaceBodyPool = new ObjectPool<SpaceBodyController>(createFunc: () => ActionsOnCreate(), actionOnGet: (body) => ActionsOnGet(body),
        actionOnRelease: (body) => ActionsOnRelease(body), actionOnDestroy: (body) => ActionsOnDestroy(body), defaultCapacity: 10, maxSize: 50);
    }

    protected virtual SpaceBodyController ActionsOnCreate()
    {
        var body = Instantiate(spaceBodyPrefab, Vector3.zero, Quaternion.identity);
        return body;
    }

    protected virtual void ActionsOnGet(SpaceBodyController spaceBody)
    {
        spaceBody.gameObject.SetActive(true);
        bodiesInPool.Add(spaceBody);
    }

    protected virtual void ActionsOnRelease(SpaceBodyController spaceBody)
    {
        bodiesInPool.Remove(spaceBody);
        spaceBody.gameObject.SetActive(false);
    }

    protected virtual void ActionsOnDestroy(SpaceBodyController spaceBody)
    {
        Destroy(spaceBody);
    }

    protected virtual List<SpaceBodyController> GetActiveBodies()
    {
        return bodiesInPool;
    }
}
