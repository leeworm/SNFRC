using UnityEngine;

public class Enemy_Bass_AI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 3f;
    public float decisionInterval = 1.0f;

    private float timer;
    private EnemyStateMachine stateMachine;

    void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            MakeDecision();
            timer = decisionInterval;
        }
    }

    void MakeDecision()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        /*float hpRatio = GetComponent<EnemyHealth>().CurrentHP / GetComponent<EnemyHealth>().MaxHP;

        if (distance > detectionRange)
        {
            stateMachine.ChangeState("Idle");
        }
        else if (distance > attackRange)
        {
            stateMachine.ChangeState("Move");
        }
        else
        {
            // Ã¼·ÂÀÌ ³·À¸¸é Å± È®·ü Áõ°¡
            int r = Random.Range(0, 100);
            if (r < (hpRatio < 0.5f ? 40 : 20))
            {
                stateMachine.ChangeState("Kick");
            }
            else
            {
                // °ø°Ý 1 ¶Ç´Â 2
                stateMachine.ChangeState(Random.Range(0, 2) == 0 ? "Attack1" : "Attack2");
            }
        }*/
    }
}
