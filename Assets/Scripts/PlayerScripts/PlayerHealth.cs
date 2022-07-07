using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHitable
{
    [SerializeField] private int healthCount;
    [SerializeField] private float invinsibleTime;
    [SerializeField] private float pingingInterval;

    private int currentHealth;
    private Collider2D playersCollider;
    private SpriteRenderer playersSprite;

    private void Awake()
    {
        playersCollider = GetComponent<Collider2D>();
        playersSprite = GetComponent<SpriteRenderer>();
        currentHealth = healthCount;
    }

    public void Hit()
    {
        currentHealth--;
        if (currentHealth < 1)
            Destroy();
        StartCoroutine(InvinsibleTime());
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator InvinsibleTime()
    {
        var time = 0f;
        playersCollider.enabled = false;
        
        while(time < invinsibleTime)
        {
            playersSprite.enabled = false;
            yield return new WaitForSecondsRealtime(pingingInterval/2);
            playersSprite.enabled = true;
            yield return new WaitForSecondsRealtime(pingingInterval/2);
            time += pingingInterval*2;
        }

        playersCollider.enabled = true;
    }
}
