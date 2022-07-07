using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IHitable
{
    [SerializeField] private LayerMask ignoreLayer;
    
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
        var firstPoint = (Vector2)Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        return Vector2.Distance(firstPoint, secondPoint);
    }

    private IEnumerator TimeOff()
    {
        var time = GetComponent<SpaceBodyController>().GetTimeOverDistance(screenDistance);
        yield return new WaitForSecondsRealtime(time);
        timerStarted = false;
        Hit();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.TryGetComponent<IHitable>(out IHitable hitable) 
            && collision.gameObject.layer != ignoreLayer )
        {
            hitable.Hit();
            Hit();
        }
    }

    public void Hit()
    {
        OnHitEvent?.Invoke(GetComponent<SpaceBodyController>());
    }

    public void Destroy()
    {
        return;
    }
}
