using UnityEngine;
using DG.Tweening;

public class Sonic : MonoBehaviour
{
    [SerializeField]public int Damage = 50;

    private Animator anim;
    private SpriteRenderer sr;

    private Vector3 targetPoint;    // 목표 위치
    private Vector3 originalPos;     // 시작 위치

    public float moveDuration = 0.2f;  // 한 쪽 방향 이동 시간
    public float riseDuration = 1f;


    void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        originalPos = transform.position;

        // 시퀀스 생성
        Sequence seq = DOTween.Sequence();
        
        // 스핀 준비하고 발사
        seq.AppendCallback(() => anim.SetBool("Spin", true));
        seq.AppendInterval(1f);
        seq.AppendCallback(() => anim.SetBool("Spin", false));

        // 왔다 갔다 3번
        for (int i = 0; i < 3; i++)
        {
            targetPoint = KH_GameManager.Instance.koopa.transform.position;
            seq.AppendCallback(() => Flip());

            seq.Append(transform.DOMove(targetPoint, moveDuration).SetEase(Ease.InOutSine));
            seq.Append(transform.DOMove(originalPos, moveDuration).SetEase(Ease.InOutSine));
            seq.AppendInterval(0.1f);
        }

        // 황금 소닉 변신
        seq.AppendCallback(() => anim.SetBool("Change", true));
        seq.AppendInterval(0.8f);
        seq.AppendCallback(() => anim.SetBool("Change", false));

        // 왔다 갔다
        for (int i = 0; i < 2; i++)
        {
            targetPoint = KH_GameManager.Instance.koopa.transform.position;
            seq.AppendCallback(() => Flip());

            seq.Append(transform.DOMove(targetPoint, moveDuration).SetEase(Ease.InOutSine));
            seq.Append(transform.DOMove(originalPos, moveDuration).SetEase(Ease.InOutSine));
            seq.AppendInterval(0.2f);
        }

        // 올라 가기 전에 딜레이
        seq.AppendInterval(0.1f);
        seq.AppendCallback(() => anim.SetBool("Up", true));

        // 위로 올라가기
        seq.Append(transform.DOMoveY(transform.position.y + 20f, riseDuration).SetEase(Ease.OutSine));
        
        seq.AppendCallback(() => Destroy(gameObject));
    }

    private void Flip()
    {
        if(transform.position.x > targetPoint.x)
            sr.flipX = true;
        else if(transform.position.x > targetPoint.x)
            sr.flipX = false;
    }

    void Update()
    {
        targetPoint = KH_GameManager.Instance.koopa.transform.position;
    }

}
