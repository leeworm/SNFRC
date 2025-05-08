using DG.Tweening;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }

    public float duration = 0.2f;
    public float strength = 0.2f;
    public int vibrato = 10;
    public float randomness = 90f;

    private Vector3 originalPosition;

    void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        originalPosition = transform.position;
    }

    public void Shake()
    {
        transform.position = originalPosition;

        transform.DOShakePosition(duration, new Vector3(strength, strength, 0f), vibrato, randomness, false, true)
            .OnComplete(() => transform.position = originalPosition);
    }
}
