using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform forwardPoint;
    
    [Header("Rotation settings")]
    [SerializeField] private float anglePerRotation;

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
            Move();
        }
        if(inputs.GetCurrentScheme() == InputScheme.Keyboard)
        {
            Rotate(inputs.Rotating);
        }
        else
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Look at mouse Position
        }
    }

    private void Move()
    {
        direction = new Vector3(forwardPoint.position.x, forwardPoint.position.y);
        StartCoroutine(MovePosition(1 / moveSpeed));
    }

    private IEnumerator MovePosition(float duration)
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

    private void Rotate(int direction)
    {
        var angle = direction * anglePerRotation;
        transform.Rotate(Vector3.forward, angle);
    }
}
