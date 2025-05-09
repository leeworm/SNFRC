using System;
using UnityEngine;

public class Enemy_Bass : MonoBehaviour
{
    public Animator animator;
    public GameObject rapidShotPrefab;
    public Transform firePoint;

    [HideInInspector] public Transform player;
    [HideInInspector] public EnemyStateMachine stateMachine;
    [Header("���� ����")]
    public int maxJumps = 1;
    [HideInInspector] public int jumpCount = 0;
    [HideInInspector] public float linearVelocityX = 0f;
    void Awake()
    {
        if (animator == null)
            animator = GetComponent<Animator>();

        stateMachine = GetComponent<EnemyStateMachine>();
    }

    void Update()
    {
        stateMachine?.currentState?.Update();
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ��� �޼���
    public void AnimationFinishTrigger()
    {
        stateMachine?.currentState?.AnimationFinishTrigger();
    }

    internal void SetVelocity(Vector2 zero)
    {
        throw new NotImplementedException();
    }
}

