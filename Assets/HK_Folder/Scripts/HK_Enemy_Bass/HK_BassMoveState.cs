using UnityEngine;

public class HK_BassMoveState : HK_IEnemyState
{
    private HK_Enemy_Bass bass;
    private float speed = 2.5f;

    public HK_BassMoveState(HK_Enemy_Bass bass)
    {
        this.bass = bass;
    }

    public void Enter()
    {
        bass.animator.SetBool("isMoving", true);  // �̵� �ִϸ��̼� ����
    }

    public void Update()
    {
        if (bass.player == null) return;

        // �÷��̾���� ������ ����Ͽ� �̵�
        Vector2 direction = (bass.player.position - bass.transform.position).normalized;
        bass.transform.position += (Vector3)(new Vector2(direction.x, 0) * speed * Time.deltaTime);

        // ���� �÷��̾���� �Ÿ��� ����� ��������� �̵��� ���߰� �ٸ� ���·� ��ȯ�� �� ����
        float distance = Vector2.Distance(bass.transform.position, bass.player.position);
        if (distance <= bass.attackRange)  // ���� ���� �̳��� �������� ���
        {
            bass.stateMachine.ChangeState(new HK_BassRapidFireState(bass));  // ���÷� ���� ���·� ��ȯ
        }
    }

    public void Exit()
    {
        bass.animator.SetBool("isMoving", false);  // �̵� �ִϸ��̼� ����
    }

    public void AnimationFinishTrigger() { }
}
