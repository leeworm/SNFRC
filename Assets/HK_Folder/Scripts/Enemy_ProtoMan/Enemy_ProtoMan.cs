using UnityEngine;

public class Enemy_ProtoMan : MonoBehaviour
{

    public Transform player;
    public Animator animator;
    public float moveSpeed = 2f;
    public GameObject chargeShotPrefab;
    public Transform firePoint;
    public EnemyStateMachine stateMachine { get; private set; }

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }

    private void Start()
    {
        stateMachine = GetComponent<EnemyStateMachine>();
        stateMachine.ChangeState(new ProtoManIdleState(this));
    }
    public void AnimationFinishTrigger()
    {
        stateMachine.currentState.AnimationFinishTrigger(); 
    }

}
