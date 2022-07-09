using UnityEngine;

public class EdgesDetector : MonoBehaviour
{
    private float verticalHalfSize;
    private float horizontalHalfSize;
    private Vector3 teleportPosition;

    private void Start()
    {
        verticalHalfSize = ScreenSizeParameters.verticalHalfSize;
        horizontalHalfSize = ScreenSizeParameters.horizontalHalfSize;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<ITeleportable>(out ITeleportable teleporter) 
            && DoNeedToTeleport(collision.transform))
        {
           teleporter.Teleport(teleportPosition);
        }
    }

    private bool DoNeedToTeleport(Transform transform)
    {
        teleportPosition = transform.position;

        if (transform.position.y >= verticalHalfSize)
        {
            teleportPosition = new Vector2(transform.position.x, -verticalHalfSize);
        }
        else if (transform.position.y <= -verticalHalfSize)
        {
            teleportPosition = new Vector2(transform.position.x, verticalHalfSize);
        }
        if (transform.position.x >= horizontalHalfSize)
        {
            teleportPosition = new Vector2(-horizontalHalfSize, transform.position.y);
        }
        else if (transform.position.x <= -horizontalHalfSize)
        {
            teleportPosition = new Vector2(horizontalHalfSize, transform.position.y);
        }

        return teleportPosition != transform.position;
    }
}

