using UnityEngine;

public class HK_Enemy_ProtoMan : MonoBehaviour
{
    public Animator animator;    
    public Transform firePoint;
    public GameObject attackPrefab;
    public GameObject chargeShotPrefab;
    public Transform player;

    [HideInInspector]
    public HK_EnemyStateMachine stateMachine;
    public void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * Time.deltaTime * 2f); // ���⼭ 2f�� �̵� �ӵ�
    }
    private void Awake()
    {
        stateMachine = GetComponent<HK_EnemyStateMachine>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator ������Ʈ�� �� ������Ʈ�� �����ϴ�.");
        }
    }
}
