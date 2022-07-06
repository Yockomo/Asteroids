using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;


    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.W))
            MoveForward();
    }

    private void MoveForward()
    {
        var nextPoint = Vector2.Lerp(transform.position, transform.forward, Time.deltaTime * moveSpeed);
        Debug.Log(nextPoint);
        transform.Translate(nextPoint);
    }
}
