using UnityEngine;

public class TEST : MonoBehaviour
{
    public Vector3 GLOBALPOSITION;
    public Vector3 LOCALPOSITION;

    void Update()
    {
        GLOBALPOSITION = transform.position;
        LOCALPOSITION = transform.localPosition;
    }
}
