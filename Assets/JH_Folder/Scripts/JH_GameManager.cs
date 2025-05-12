using UnityEngine;
using UnityEngine.UI; // UI 사용을 위해 필요
using TMPro; // TextMeshPro 사용을 위해 필요

public class JH_GameManager : MonoBehaviour
{
    public static JH_GameManager Instance { get; private set; } // 싱글톤 패턴

    [Header("카운트다운 UI")]
    public Image countdownImage; // TextMeshPro 대신 Image 컴포넌트
    public Sprite[] numberSprites; // 숫자 스프라이트 배열 (3,2,1)
    public Sprite fightSprite;   // Fight! 스프라이트
    public Sprite winsprite;
    public float countdownDuration = 3f;
    private float currentCountdown;

    [Header("오브젝트 활성화 설정")]
    public GameObject[] activateOnStart;    // 게임 시작 시 활성화할 오브젝트들
    public GameObject[] deactivateOnStart;

    [Header("게임 상태")]
    public bool isGameStarted = false;
    public static bool IsGamePaused { get; private set; } = true;
    private bool isVictoryAnimationPlaying = false;

    [Header("플레이어 참조")]
    public JH_Player player;    // Inspector에서 플레이어 할당
    public JH_Entity enemy;     // Inspector에서 적 할당

    [Header("아이템 설정")]
    public GameObject errorCodePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        isGameStarted = false;
        IsGamePaused = true;
        currentCountdown = countdownDuration;

        // 게임 시작 전 오브젝트들 비활성화
        foreach (GameObject obj in activateOnStart)
        {
            if (obj != null) obj.SetActive(false);
        }

        // 적의 KO 이벤트 구독
        if (enemy != null)
        {
            enemy.OnKnockced += HandleEnemyKO;
        }

        foreach (GameObject obj in activateOnStart)
        {
            if (obj != null) obj.SetActive(false);
        }

        StartCountdown();
    }

    private void HandleEnemyKO()
    {
        IsGamePaused = true;
        isVictoryAnimationPlaying = true;

        // 적의 위치에 아이템 생성
        if (enemy != null && errorCodePrefab != null)
        {
            Vector3 spawnPosition = enemy.transform.position + Vector3.up * 0.5f;
            Instantiate(errorCodePrefab, spawnPosition, Quaternion.identity);
        }

        // Win 이미지 표시
        if (countdownImage != null && winsprite != null)
        {
            countdownImage.gameObject.SetActive(true);
            countdownImage.sprite = winsprite;
            countdownImage.SetNativeSize();
        }

        // 플레이어 승리 모션
        if (player != null && player.animator != null)
        {
            player.animator.SetTrigger("Victory");
        }

        Invoke("OnVictoryAnimationComplete", 2f);
    }

    private void OnVictoryAnimationComplete()
    {
        isVictoryAnimationPlaying = false;
        IsGamePaused = false;  // 움직임 다시 활성화

        // Win 이미지 숨기기
        if (countdownImage != null)
        {
            countdownImage.gameObject.SetActive(false);
        }
    }

    public bool IsVictoryAnimationPlaying()
    {
        return isVictoryAnimationPlaying;
    }

    private void OnDestroy()
    {
        // 이벤트 구독 해제
        if (enemy != null)
        {
            enemy.OnKnockced -= HandleEnemyKO;
        }
    }

    void StartCountdown()
    {
        if (countdownImage != null)
        {
            countdownImage.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (!isGameStarted && currentCountdown > 0)
        {
            currentCountdown -= Time.deltaTime;

            if (countdownImage != null)
            {
                // 현재 카운트다운 숫자 (3,2,1)
                int currentNumber = Mathf.CeilToInt(currentCountdown);

                // 배열 범위 체크 (0부터 시작하므로 -1)
                if (currentNumber > 0 && currentNumber <= numberSprites.Length)
                {
                    countdownImage.sprite = numberSprites[currentNumber - 1];
                    countdownImage.SetNativeSize(); // 이미지 원본 크기로 설정
                }
            }

            if (currentCountdown <= 0)
            {
                StartGame();
            }
        }
    }

    void StartGame()
    {
        isGameStarted = true;
        IsGamePaused = false;

        // Fight! 이미지 표시
        if (countdownImage != null && fightSprite != null)
        {
            countdownImage.sprite = fightSprite;
            countdownImage.SetNativeSize();
            Invoke("HideCountdownImage", 1f);
        }

        foreach (GameObject obj in deactivateOnStart)
        {
            if (obj != null) obj.SetActive(false);
        }

        foreach (GameObject obj in activateOnStart)
        {
            if (obj != null) obj.SetActive(true);
        }
    }

    void HideCountdownImage()
    {
        if (countdownImage != null)
        {
            countdownImage.gameObject.SetActive(false);
        }
    }
    // 다른 스크립트에서 게임 시작 여부 확인을 위한 메서드
    public bool IsGameStarted()
    {
        return isGameStarted;
    }

    public static bool IsPaused()
    {
        return IsGamePaused;
    }

}