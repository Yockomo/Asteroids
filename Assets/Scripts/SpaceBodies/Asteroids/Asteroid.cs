using UnityEngine;

public class Asteroid : MonoBehaviour, IHitable
{
    public void Hit()
    {
        gameObject.SetActive(false);
        transform.position = Vector2.zero;
    }
}
