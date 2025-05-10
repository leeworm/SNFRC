using DG.Tweening;
using UnityEngine;

public class KH_CameraShake : MonoBehaviour
{
    public static KH_CameraShake Instance { get; private set; }

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
    public void Shake(float _duration, float _strength, int _vibrato, float _randomness)
    {
        transform.position = originalPosition;

        transform.DOShakePosition(_duration, new Vector3(_strength, _strength, 0f), _vibrato, _randomness, false, true)
            .OnComplete(() => transform.position = originalPosition);
    }

    public void CameraDown(Vector3 targetPosition, float duration)
    {
        // DOTween을 사용하여 카메라를 targetPosition으로 이동
        //transform.DOMove(targetPosition, duration).SetEase(Ease.Linear);
    }
}
