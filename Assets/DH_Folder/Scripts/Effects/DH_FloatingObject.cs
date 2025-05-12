using DG.Tweening;
using UnityEngine;

public class DH_FloatingObject : MonoBehaviour
{
    public float floatHeight = 0.25f; // 위아래 이동 범위
    public float floatDuration = 1f;  // 왕복 시간

    private void Start()
    {
        Vector3 upPos = transform.localPosition + Vector3.up * floatHeight;

        transform.DOLocalMove(upPos, floatDuration)
                 .SetEase(Ease.InOutSine)
                 .SetLoops(-1, LoopType.Yoyo);
    }
}
