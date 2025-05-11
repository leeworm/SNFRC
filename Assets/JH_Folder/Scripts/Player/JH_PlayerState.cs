using UnityEngine;

public class JH_PlayerState
{
    protected JH_PlayerStateMachine StateMachine;
    protected JH_Player Player;

    protected Rigidbody2D rb;

    private string AnimBool;
    protected float xInput;
    protected float yInput;

    protected float StateTimer;


    public JH_PlayerState(JH_Player _Player, JH_PlayerStateMachine _StateMachine, string _AnimBool)
    {
        this.Player = _Player;
        this.StateMachine = _StateMachine;
        this.AnimBool = _AnimBool;
    }

    public virtual void Enter()
    {
        if (!string.IsNullOrEmpty(AnimBool))
        {
            Player.animator.SetBool(AnimBool, true);
        }
        rb = Player.rb;
       
    } 

    public virtual void Update()
    {


        StateTimer -= Time.deltaTime;

        // 입력을 감지하여 xInput 설정
        xInput = 0f;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            xInput = -1f;
            Player.UpdateEnemyDetection(); // 방향 업데이트
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            xInput = 1f;
            Player.UpdateEnemyDetection(); // 방향 업데이트
        }
        else
        {
            // 입력이 없을 때도 적 감지하여 방향 설정
            Player.UpdateEnemyDetection();
        }

        yInput = 0f; // 기본값 0
        if (Input.GetKey(KeyCode.DownArrow))
        {
            yInput = -1f;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            yInput = 1f;
        }


        //Player.animator.SetFloat("yVelocity", rb.linearVelocityY);
    }
    

    public virtual void Exit()
    {
        if (!string.IsNullOrEmpty(AnimBool))
        {
            Player.animator.SetBool(AnimBool, false);
        }
    }

    public virtual void AnimationFinishTrigger()
    {
      
        StateMachine.ChangeState(Player.IdleState);
    }


}
