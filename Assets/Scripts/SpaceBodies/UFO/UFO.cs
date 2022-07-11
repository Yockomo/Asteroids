using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class UFO : MonoBehaviour, IHitable, IScorable
{
    [Header("Target to Shoot")]
    [SerializeField] private Transform playersShipTransform;

    [Header("Enemy bullet pool")]
    [SerializeField] private BulletPool uFOBulletPool;
    
    [Header("UFO characteristics")]
    [SerializeField] private float uFOMoveSpeed;
    [SerializeField] private float atackCooldown;
    [SerializeField] private float spawnCooldown;
    [SerializeField] private int valueForKilling;

    private SpaceBodyController ufoController;
    private Collider2D ufoCollider;
    private SpriteRenderer ufoRenderer;
    private float screenHeight;
    private float screenWidth;
    
    private bool needToRespawn;
    private bool isAtackCooldown;
    
    private List<Vector2> directionsToMove = new List<Vector2>() { Vector2.left, Vector2.right };

    public int Value { get {return valueForKilling; } }

    public event Action<SpaceBodyController> OnDestroyEvent;

    private void OnDestroy()
    {
        OnDestroyEvent = null;
    }

    private void Start()
    {
        GetComponents();
        screenHeight = ScreenSizeParameters.verticalHalfSize * 0.8f;
        screenWidth = ScreenSizeParameters.horizontalHalfSize;
        StartMoving();
    }

    void Update()
    {
        if (CanAtack())
        {
            Shoot();        
        }
        else if(needToRespawn)
        {
            StartCoroutine(Respawn());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<ShipController>(out ShipController playersShip))
        {
            playersShip.gameObject.GetComponent<IHitable>().Hit();
            this.Hit();
        }
    }

    private void GetComponents()
    {
        ufoController = GetComponent<SpaceBodyController>();
        ufoCollider = GetComponent<Collider2D>();
        ufoRenderer = GetComponent<SpriteRenderer>();
    }

    private void StartMoving()
    {
        EnableUfo(true);
        transform.position = GetRandomSpawnPosition();
        var randomDirection = directionsToMove[Random.Range(0, 2)];
        ufoController.SetSpeedAndDirection(uFOMoveSpeed, randomDirection);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        var randomHorizontalPoint = Random.Range(0, 1) == 0 ? -screenWidth : screenWidth;
        var randomVerticalPoint = Random.Range(-screenHeight, screenHeight);
        return new Vector2(randomHorizontalPoint, randomVerticalPoint);
    }

    private bool CanAtack()
    {
        return ufoController.enabled;
    }

    private void Shoot()
    {
        if (!isAtackCooldown)
        {
            StartCoroutine(GetBullet());
        }
    }

    private IEnumerator GetBullet()
    {
        isAtackCooldown = true;
        var direction = playersShipTransform.position - transform.position;
        uFOBulletPool.GetBullet(direction.normalized);
        yield return new WaitForSecondsRealtime(atackCooldown);
        isAtackCooldown = false;    
    }

    private IEnumerator Respawn()
    {
        needToRespawn = false;
        yield return new WaitForSecondsRealtime(spawnCooldown);
        StartMoving();
    }

    public void Hit()
    {
        Destroy();
    }

    public void Destroy()
    {
        OnDestroyEvent?.Invoke(GetComponent<SpaceBodyController>());
        needToRespawn = true;
        EnableUfo(false);
    }

    private void EnableUfo(bool value)
    {
        ufoController.enabled = value;
        ufoCollider.enabled = value;
        ufoRenderer.enabled = value;    
    }
}
