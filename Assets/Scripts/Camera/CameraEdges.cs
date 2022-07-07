using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]

public class CameraEdges : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;
    private Camera mainCamera;
    private List<Vector2> cameraEdgePoints;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        edgeCollider.SetPoints(GetCameraEdges());
        //cameraEdgePoints = GetCameraEdges().GetRange(0, 4);
    }

    private List<Vector2> GetCameraEdges()
    {
        var leftBottomPoint = (Vector2) mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        var leftTopPoint = (Vector2) mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelHeight, mainCamera.nearClipPlane));
        var rightBottomPoint = (Vector2) mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, mainCamera.nearClipPlane));
        var rightTopPoint = (Vector2) mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight, mainCamera.nearClipPlane));

        var currentCameraEdges = new List<Vector2>() {
            leftBottomPoint, leftTopPoint, rightTopPoint, rightBottomPoint, leftBottomPoint};

        return currentCameraEdges;
    }

    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    var collisionPoint = collision.transform.position;
    //    if(IsCollisionOnLine(collisionPoint,EdgePoint.LeftBottomPoint, EdgePoint.LeftTopPoint))
    //    {
            
    //        //collision.transform.position = new Vector2(collisionPoint.x + )
    //    }
        
    //}

    //private bool IsCollisionOnLine(Vector2 collisionPoint, EdgePoint firstPoint, EdgePoint secondPoint)
    //{
    //    var lineStart = cameraEdgePoints[(int)firstPoint];
    //    var lineEnd = cameraEdgePoints[(int)secondPoint];
    //    var doPointBelongsToLine = (collisionPoint.x - lineStart.x) * (lineEnd.y - lineStart.y) - (lineEnd.x - lineStart.x) * (collisionPoint.y - lineStart.y);
    //    return doPointBelongsToLine == 0;
    //}

    //private Vector2 GetOppositeEdgePoint(Vector2 currentPoint)
    //{

    //    return new Vector2();
    //}
}

//public enum EdgePoint
//{
//    LeftBottomPoint = 0,
//    LeftTopPoint =1,
//    RightTopPoint =2,
//    RightBottomPoint =3,
//}
