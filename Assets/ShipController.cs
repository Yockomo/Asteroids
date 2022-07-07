using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Movement settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform forwardPoint;
    
    [Header("Rotation settings")]
    [SerializeField] private float anglePerRotationKeyboard;
    [SerializeField] private float rotationSpeed;

    public bool IsMoving { get; set; }

    private InputSystem inputs;
    private Vector3 direction;

    private void Start()
    {
        inputs = GetComponent<InputSystem>();
    }

    private void FixedUpdate()
    {
        DetermineShipRotationAndMovement();
    }

    private void DetermineShipRotationAndMovement()
    {
        if (inputs.Moving && !IsMoving)
        {
            Move();
        }
        if (inputs.GetCurrentScheme() == InputScheme.Keyboard)
        {
            Rotate(inputs.Rotating);
        }
        else
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RotateToMousePosition(mousePosition);
        }
    }

    private void Move()
    {
        direction = new Vector3(forwardPoint.position.x, forwardPoint.position.y);
        StartCoroutine(MoveToPosition(1 / moveSpeed));
    }

    private IEnumerator MoveToPosition(float duration)
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
        var angle = direction * anglePerRotationKeyboard;
        transform.Rotate(Vector3.forward, angle);
    }

    private void RotateToMousePosition(Vector3 mousePosition)
    {
        Vector2 direction = mousePosition - transform.position;
        var angle = Vector2.SignedAngle(Vector2.up, direction);
        var targetRotation = new Vector3(0, 0, angle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), rotationSpeed * Time.deltaTime);
    }
}
