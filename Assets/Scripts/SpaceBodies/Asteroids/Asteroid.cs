using UnityEngine;

public class Asteroid : MonoBehaviour, IHitable, IScorable
{
    [SerializeField] private int valueForKilling;

    public int Value { get { return valueForKilling; } }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHitable>(out IHitable hitable)
            && !IgnoreObjects(collision))
        {
            hitable.Hit();
            Destroy();
        }
    }

    protected virtual bool IgnoreObjects(Collision2D collision)
    {
        return collision.gameObject.TryGetComponent<Asteroid>(out Asteroid asteroid);
    }

    public virtual void Hit()
    {
    }

    public virtual void Destroy()
    {
    }
}
