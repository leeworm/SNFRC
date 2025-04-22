using UnityEngine;

public class Enemy_Bass : MonoBehaviour
{
    public EnemyStateMachine stateMachine;
    public Transform player;

    public float moveSpeed = 3.5f;
    public GameObject rapidShotPrefab;
    public Transform firePoint;

    [HideInInspector] public int jumpCount = 0;
    public int maxJumps = 2;

    private void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        stateMachine.ChangeState(new BassIdleState(this));
    }
}
