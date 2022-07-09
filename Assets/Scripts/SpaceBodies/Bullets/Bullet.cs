using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private List<int> ignoreLayersIndex;

    public Action<SpaceBodyController> OnHitEvent;

    private float screenDistance;
    private bool timerStarted;

    private void Start()
    {
        CalculateScreenDistance();
    }

    private void FixedUpdate()
    {
        if (!timerStarted)
        {
            timerStarted = true;
            StartCoroutine(TimeOff());
        }
    }

    private void CalculateScreenDistance()
    {
        var rightPoint = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(Camera.main.pixelWidth, 0, Camera.main.nearClipPlane));
        var screenWidth = GetScreenWidthOrHeight(rightPoint);

        var topPoint = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, Camera.main.pixelHeight, Camera.main.nearClipPlane));
        var screenHeight = GetScreenWidthOrHeight(topPoint);

        screenDistance = Math.Min(screenHeight, screenWidth);
    }

    private float GetScreenWidthOrHeight(Vector2 secondPoint)
    {
        var firstPoint = (Vector2) Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        return Vector2.Distance(firstPoint, secondPoint);
    }

    private IEnumerator TimeOff()
    {
        var time = GetComponent<SpaceBodyController>().GetTimeOverDistance(screenDistance);
        yield return new WaitForSecondsRealtime(time);
        timerStarted = false;
        ReleaseThisBullet();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.TryGetComponent<IHitable>(out IHitable hitable) && collision.isActiveAndEnabled
            && !IgnoreObjects(collision))
        {
            hitable.Hit();
            ReleaseThisBullet();
        }
    }

    protected virtual bool IgnoreObjects(Collider2D collider)
    {
        return ignoreLayersIndex.IndexOf(collider.gameObject.layer) != -1;
    }

    public void ReleaseThisBullet()
    {
        if (isActiveAndEnabled)
        {
            timerStarted = false;
            OnHitEvent?.Invoke(GetComponent<SpaceBodyController>());
        }
    }
}
