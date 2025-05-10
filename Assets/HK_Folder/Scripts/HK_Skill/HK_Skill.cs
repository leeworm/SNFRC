using UnityEngine;

public class HK_Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown;
    protected float cooldownTimer;


    protected HK_Player player;


    protected virtual void Start()
    {
        player = HK_PlayerManager.instance.player;
    }


    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }


    public virtual bool CanUseSkill()
    {
        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }


        Debug.Log("HK_Skill is on cooldown");

        return false;
    }


    public virtual void UseSkill()
    {
        //��ų���
    }



}
