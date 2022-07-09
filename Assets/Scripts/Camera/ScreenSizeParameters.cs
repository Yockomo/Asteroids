using UnityEngine;

public static class ScreenSizeParameters
{
    public static float verticalHalfSize { get { return Camera.main.orthographicSize; } }
    public static float horizontalHalfSize { get { return verticalHalfSize * Screen.width / Screen.height; } }
}

