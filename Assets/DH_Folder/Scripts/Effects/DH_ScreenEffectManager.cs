using System.Collections;
using UnityEngine;

public class DH_ScreenEffectManager : MonoBehaviour
{
    public GameObject blackFadePrefab;
    public GameObject customEffectPrefab;

    public static DH_ScreenEffectManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void PlayEffectWithBlackScreen(float fadeIn = 0.5f, float hold = 1f, float fadeOut = 0.5f)
    {
        StartCoroutine(ShowEffectSequence(fadeIn, hold, fadeOut));
    }

    private IEnumerator ShowEffectSequence(float fadeIn, float hold, float fadeOut)
    {
        // 검정 배경 생성
        GameObject black = Instantiate(blackFadePrefab);
        black.name = "BlackScreenFade";
        SetToScreen(black);
        SpriteRenderer blackSR = black.GetComponent<SpriteRenderer>();
                
        // 페이드 인 (덮기)
        // yield return StartCoroutine(FadeAlpha(blackSR, 0f, 0f, fadeIn));

        // 연출 프리팹 등장
        GameObject effect = Instantiate(customEffectPrefab);
        effect.name = "CustomScreenEffect";
        SetToScreen(effect);

        // 연출 보여줌
        yield return StartCoroutine(FadeAlpha(blackSR, 0f, 0f, fadeOut));
        
        // 연출 프리팹 유지 시간 (흑배경은 뒤에서 기다리는 중)
        yield return new WaitForSeconds(hold);

        // 검은 화면 다시 페이드 인 (덮기)
        yield return StartCoroutine(FadeAlpha(blackSR, 0f, 1f, fadeIn));

        // 정리: 프리팹 제거 + 검은 화면 제거
        Destroy(effect);
        yield return StartCoroutine(FadeAlpha(blackSR, 1f, 0f, fadeOut));
        Destroy(black);
    }

    private IEnumerator FadeAlpha(SpriteRenderer sr, float from, float to, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            float alpha = Mathf.Lerp(from, to, time / duration);
            sr.color = new Color(0f, 0f, 0f, alpha);
            time += Time.deltaTime;
            yield return null;
        }
        sr.color = new Color(0f, 0f, 0f, to);
    }

    public void ShowScreenPrefab(GameObject prefab, float fadeInDuration = 1f, float holdDuration = 1f, float fadeOutDuration = 1f)
    {
        GameObject go = Instantiate(prefab);
        go.name = "FullScreenEffectPrefab";

        Camera cam = Camera.main;
        go.transform.position = cam.transform.position + new Vector3(0, 0, 10); // 플레이어보다 뒤

        // 크기 자동 조절
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        if (sr != null && sr.sprite != null)
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;

            Vector2 size = sr.sprite.bounds.size;
            go.transform.localScale = new Vector3(width / size.x, height / size.y, 1f);

            // 시작은 투명 (MPB로만 조절할 거면 이건 사실 필요 없음)
            StartCoroutine(FadeInHoldOutWithMPB(sr, fadeInDuration, holdDuration, fadeOutDuration));
        }
        else
        {
            Debug.LogWarning("프리팹에 SpriteRenderer가 없습니다! 페이드 처리를 건너뜁니다.");
        }
    }

    private void SetToScreen(GameObject go, float scale = 1f)
    {
        Camera cam = Camera.main;
        go.transform.position = cam.transform.position + new Vector3(0, 0, 10); // 뒤로

        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        if (sr != null && sr.sprite != null)
        {
            float height = 2f * cam.orthographicSize;
            float width = height * cam.aspect;
            Vector2 size = sr.sprite.bounds.size;
            go.transform.localScale = new Vector3(
                width / size.x,
                height / size.y * 1.25f,
                1f);
        }
    }

    private IEnumerator FadeInHoldOutWithMPB(SpriteRenderer sr, float fadeIn, float hold, float fadeOut)
    {
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        float time = 0f;

        // 페이드 인
        while (time < fadeIn)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / fadeIn);
            sr.GetPropertyBlock(mpb);
            mpb.SetColor("_Color", new Color(0f, 0f, 0f, alpha));  // ✅ 핵심!
            sr.SetPropertyBlock(mpb);

            time += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(hold);

        // 페이드 아웃
        time = 0f;
        while (time < fadeOut)
        {
            float alpha = Mathf.Lerp(1f, 0f, time / fadeOut);
            sr.GetPropertyBlock(mpb);
            mpb.SetColor("_Color", new Color(0f, 0f, 0f, alpha));  // ✅ 여기도
            sr.SetPropertyBlock(mpb);

            time += Time.deltaTime;
            yield return null;
        }

        Destroy(sr.gameObject);
    }
}
