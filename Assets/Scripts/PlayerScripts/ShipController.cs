using System.Collections;
using UnityEngine;

public class ShipController : MonoBehaviour, ITeleportable
{
    [Header("Movement settings")]
    [SerializeField] private float maxMoveSpeed;
    [SerializeField] private Transform forwardPoint;
    
    [Header("Rotation settings")]
    [SerializeField] private float anglePerRotationKeyboard;
    [SerializeField] private float rotationSpeed;

    public bool IsMoving { get; set; }
    public bool IsStopped { get; set; }

    private InputSystem inputs;
    private Shooting shootingSystem;
    private float currentSpeed;

    private void Start()
    {
        inputs = GetComponent<InputSystem>();
        shootingSystem = GetComponent<Shooting>();
    }

    private void Update()
    {
        DetermineShipRotationAndMovement();
    }

    private void DetermineShipRotationAndMovement()
    {
        shootingSystem.Shoot();

        if (inputs.Moving && !IsMoving)
        {
            SpeedUp();
        }
        if (!IsMoving && !IsStopped)
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


    private void SpeedUp()
    {
        if (currentSpeed < maxMoveSpeed)
        {
            currentSpeed += 0.01f * maxMoveSpeed;
        }
    }

    private void Move()
    {
        StartCoroutine(MoveToPosition(0.1f));
    }

    private IEnumerator MoveToPosition(float duration)
    {
        IsMoving = true;
        var endPoint = transform.position + (forwardPoint.position - transform.position)*currentSpeed;
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

    public void Stop()
    {
        StopAllCoroutines();
        IsStopped = true;
    }

    public void StartMoving()
    {
        IsStopped = false;
        IsMoving = false;
    }

    public void Teleport(Vector2 newPosition)
    {
       Stop();
       transform.position = newPosition;
       StartMoving(); 
    }
}
