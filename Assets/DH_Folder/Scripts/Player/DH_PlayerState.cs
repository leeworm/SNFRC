using UnityEngine;

public class DH_PlayerState
{
    protected DH_PlayerStateMachine stateMachine;
    public DH_Player player;

    public Rigidbody2D rb;

    protected float xInput;
    protected float yInput;
    private string animBoolName;

    protected float stateTimer;
    protected bool triggerCalled = false;

    public DH_PlayerState(DH_Player _player, DH_PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;            // 플레이어 객체 저장
        this.stateMachine = _stateMachine; // 상태 머신 저장
        this.animBoolName = _animBoolName; // 애니메이션 트리거 변수 이름 저장
    }

    public virtual void Enter()
    {
        player.anim.SetBool(animBoolName, true); // 애니메이션 트리거 활성화
        rb = player.rb;
        triggerCalled = false;
        //Debug.Log($"{animBoolName} 상태 진입"); // 상태 진입 로그 출력
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime; // 상태 타이머 업데이트

        xInput = GetArrowKeyHorizontalInput(); // 수평 입력 값 가져오기
        yInput = GetArrowKeyVerticalInput(); // 수직 입력 값 가져오기
        player.anim.SetFloat("yVelocity", rb.linearVelocity.y); // y축 속도 애니메이션 변수 설정
        //Debug.Log($"{animBoolName} 상태 업데이트"); // 상태 업데이트 로그 출력
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false); // 애니메이션 트리거 비활성화
        Debug.Log($"{animBoolName} 상태 종료"); // 상태 종료 로그 출력
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }

    public static float GetArrowKeyHorizontalInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
            return -1f;
        if (Input.GetKey(KeyCode.RightArrow))
            return 1f;
        return 0f;
    }

    public static float GetArrowKeyVerticalInput()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            return 1f;
        if (Input.GetKey(KeyCode.DownArrow))
            return -1f;
        return 0f;
    }
}