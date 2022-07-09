using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IHitable
{
    [SerializeField] private int healthCount;
    [SerializeField] private float invinsibleTime;
    [SerializeField] private float pingingInterval;

    private int currentHealth;
    private SpriteRenderer playersSprite;
    private bool invinsible;

    private void Awake()
    {
        playersSprite = GetComponent<SpriteRenderer>();
        currentHealth = healthCount;
    }

    public void Hit()
    {
        if (!invinsible)
        {
            currentHealth--;
            if (currentHealth < 1)
                Destroy();
            StartCoroutine(InvinsibleTime());
        }
    }

    public void Destroy()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator InvinsibleTime()
    {
        var time = 0f;
        invinsible = true;
        while(time < invinsibleTime)
        {
            playersSprite.enabled = false;
            yield return new WaitForSecondsRealtime(pingingInterval/2);
            playersSprite.enabled = true;
            yield return new WaitForSecondsRealtime(pingingInterval/2);
            time += pingingInterval*2;
        }
        invinsible = false;
    }
}
