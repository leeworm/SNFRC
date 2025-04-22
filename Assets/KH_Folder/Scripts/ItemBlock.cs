using UnityEngine;
using DG.Tweening;

public class ItemBlock : MonoBehaviour
{
    private SpriteRenderer sr; // 스프라이트 렌더러
    public Sprite basicblockSprite; // 일반 블럭 스프라이트
    public GameObject coinPrefab; // 코인 프리팹

    [SerializeField] private float blockBounceHeight = 0.2f; // 블록이 들썩이는 높이
    [SerializeField] private float blockBounceDuration = 0.1f; // 블록이 들썩이는 시간
    
    [SerializeField] private float coinBounceHeight = 3f; // 코인이 들썩이는 높이
    [SerializeField] private float coinBounceDuration = 0.1f; // 코인이 들썩이는 시간

    private Vector3 originalPosition; // 블록의 원래 위치
    private Vector3 coinPosition; // 블록의 원래 위치

    private bool isNoItemBlock = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // 블록의 원래 위치 저장
        originalPosition = transform.position;
        coinPosition = transform.position + Vector3.up * 1f; // 코인의 원래 위치
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isNoItemBlock)
        {
            sr.sprite = basicblockSprite; // 스프라이트 변경

            // DOTween을 사용하여 블록을 위로 들썩이게 하고 다시 원위치로 이동
            transform.DOMoveY(originalPosition.y + blockBounceHeight, blockBounceDuration)
                .OnComplete(() => transform.DOMoveY(originalPosition.y, blockBounceDuration));

            GameObject coin = Instantiate(coinPrefab, coinPosition, Quaternion.identity); // 코인 생성
            
            coin.transform.DOMoveY(coinPosition.y + coinBounceHeight, coinBounceDuration)
                .OnComplete(() => coin.transform.DOMoveY(coinPosition.y, coinBounceDuration)
                    .OnComplete(() => Destroy(coin)));

            isNoItemBlock = false; // 아이템 블록이 아님을 표시
        }
    }
}
