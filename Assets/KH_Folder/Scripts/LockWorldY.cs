using UnityEngine;

public class LockWorldY : MonoBehaviour
{
    public float fixedY; // 고정하고 싶은 월드 Y값 

    Vector3 worldPos;

    void LateUpdate()
    {
        worldPos = transform.position;
        worldPos.y = fixedY;
        transform.position = worldPos;
    }

    public void ZeroYpos()
    {
        transform.position = new Vector3(0,0,0);
    }
}