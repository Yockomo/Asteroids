using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform forwardPoint;

    public bool IsMoving { get; set; }

    private InputSystem inputs;
    private Vector3 direction;

    private void Start()
    {
        inputs = GetComponent<InputSystem>();
    }

    private void FixedUpdate()
    {
        if(inputs.Moving && !IsMoving)
        {
            direction = new Vector3(forwardPoint.position.x, forwardPoint.position.y);
            StartCoroutine(LerpPosition(1/moveSpeed));
        }
    }

    private IEnumerator LerpPosition(float duration)
    {
        IsMoving = true;
        var endPoint = transform.position + (direction - transform.position);
        var time = 0f;
        Vector2 startPosition = transform.position;

        while (time < duration)
        {
            transform.position = Vector2.Lerp(startPosition, endPoint, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.position = endPoint;
        IsMoving = false;
    }
}
