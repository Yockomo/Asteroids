using UnityEngine;

[RequireComponent(typeof(SpaceBodyController))]

public class Asteroid : MonoBehaviour, IHitable
{
    [SerializeField, Range(0, 5)] private float speedValue;
    [SerializeField] private Vector2 direction;

    private SpaceBodyController controller;

    private void Start()
    {
        Constructor(speedValue, direction);
    }

    public virtual void Constructor(float speed, Vector2 direction)
    {
        controller = GetComponent<SpaceBodyController>();
        controller.SetDirection(direction);
        controller.SetSpeed(speed);
    }

    public void Hit()
    {
        gameObject.SetActive(false);
        transform.position = Vector2.zero;
    }
}
