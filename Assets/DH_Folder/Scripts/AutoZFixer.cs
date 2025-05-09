using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AutoZFixer : MonoBehaviour
{
    [SerializeField] private float zOffset = -0.1f; // 타일보다 앞에 나오게 Z 고정
    [SerializeField] private bool lockZPosition = true;
    [SerializeField] private int sortingOrderOverride = 10;

    private SpriteRenderer sr;
    private Vector3 lastPosition;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        // Sorting Order 보정
        if (sortingOrderOverride >= 0)
            sr.sortingOrder = sortingOrderOverride;
    }

    void LateUpdate()
    {
        if (lockZPosition)
        {
            Vector3 pos = transform.position;

            // Y값이 바뀌면 따라감, Z는 고정
            if (pos != lastPosition)
            {
                transform.position = new Vector3(pos.x, pos.y, zOffset);
                lastPosition = transform.position;
            }
        }
    }
}
