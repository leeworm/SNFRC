using UnityEngine;
using Unity.Cinemachine; // Cinemachine 네임스페이스 추가

public class CinemachineCamera_Stay : MonoBehaviour
{
    public CinemachineCamera virtualCamera; // Virtual Camera를 인스펙터에서 연결
    public float fixedScreenY = 0.2f; // 고정할 Y값 (0.0 ~ 1.0 범위)

    private CinemachinePositionComposer composer;

    void Start()
    {
        if (virtualCamera != null)
        {
        }
    }

    void Update()
    {
        if (composer != null)
        {
            
        }
    }
}
