using System.Collections;
using UnityEngine;

public class SpaceBodyController : MonoBehaviour, ITeleportable
{
    public bool IsMoving { get; private set; }
    public bool IsStopped { get; private set; }

    private Vector3 direction = new Vector3(1,1);
    private float speed = 1f;

    private void Start()
    {
        IsMoving = true;
        IsStopped = false;
        SetDirection(direction);
    }

    private void Update()
    {
        if (IsMoving && !IsStopped)
        {
            StartCoroutine(LerpPosition(1));
        }
    }

    private IEnumerator LerpPosition(float duration)
    {
        IsMoving = false;
        var endPoint = transform.position + new Vector3(direction.x, direction.y);
        var time = 0f;
        Vector2 startPosition = transform.position;

        while(time < duration)
        {
            transform.position = Vector2.Lerp (startPosition, endPoint, time/duration);
            time += Time.deltaTime;
            yield return null;
        }
        
        transform.position = endPoint;
        IsMoving = true;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection * speed;
    }

    public void SetSpeed(float value)
    {
        speed = value;
    }

    void ITeleportable.Teleport(Vector2 newPosition)
    {
        Stop();
        transform.position = newPosition;
        StartMoving();
    }

    public void Stop()
    {
        StopAllCoroutines();
        IsStopped = true;
        IsMoving = false;
    }

    public void StartMoving()
    {
        IsStopped = false;
        IsMoving = true;
    }
}
