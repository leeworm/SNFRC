using UnityEngine;
using System;

public class DH_EffectObject : MonoBehaviour
{
    private string effectName;
    private Action<string, GameObject> returnCallback;
    private Animator animator;

    public void Initialize(string name, Action<string, GameObject> onReturn)
    {
        effectName = name;
        returnCallback = onReturn;

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            returnCallback?.Invoke(effectName, gameObject);
        }
    }
}
