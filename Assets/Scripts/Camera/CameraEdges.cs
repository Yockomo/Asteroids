using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]

public class CameraEdges : MonoBehaviour
{
    private EdgeCollider2D edgeCollider;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = GetComponent<Camera>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        var edges = GetCameraEdges();
        edges.Add(edges[0]);
        edgeCollider.SetPoints(edges);
    }

    public List<Vector2> GetCameraEdges()
    {
        var edgeRadius = edgeCollider.edgeRadius;

        var leftBottomPoint = (Vector2) mainCamera.ScreenToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        var leftTopPoint = (Vector2) mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelHeight, mainCamera.nearClipPlane));
        var rightBottomPoint = (Vector2) mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, 0, mainCamera.nearClipPlane));
        var rightTopPoint = (Vector2) mainCamera.ScreenToWorldPoint(new Vector3(mainCamera.pixelWidth, mainCamera.pixelHeight, mainCamera.nearClipPlane));

        leftBottomPoint = new Vector2(leftBottomPoint.x + edgeRadius,leftBottomPoint.y + edgeRadius);
        leftTopPoint = new Vector2(leftTopPoint.x + edgeRadius,leftTopPoint.y - edgeRadius);
        rightTopPoint = new Vector2(rightTopPoint.x-edgeRadius, rightTopPoint.y - edgeRadius);
        rightBottomPoint = new Vector2(rightBottomPoint.x-edgeRadius, rightBottomPoint.y+edgeRadius);

        var currentCameraEdges = new List<Vector2>() {
            leftBottomPoint, leftTopPoint, rightTopPoint, rightBottomPoint};

        return currentCameraEdges;
    }
}
