using UnityEngine;

public class Enemy_ProtoMan : MonoBehaviour
{
    public Animator animator;    
    public Transform firePoint;
    public GameObject attackPrefab;
    public GameObject chargeShotPrefab;
    public Transform player;

    [HideInInspector]
    public EnemyStateMachine stateMachine;
    public void MoveTowardsPlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * Time.deltaTime * 2f); // 여기서 2f는 이동 속도
    }
    private void Awake()
    {
        stateMachine = GetComponent<EnemyStateMachine>();

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator 컴포넌트가 이 오브젝트에 없습니다.");
        }
    }
}
