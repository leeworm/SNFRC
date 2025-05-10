using UnityEngine;

    public class HK_SkillManager : MonoBehaviour
{
    public static HK_SkillManager instance;

    public HK_Dash_Skill dash;  // �ν����Ϳ��� �Ҵ�

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (dash == null)
        {
            Debug.LogError("Dash_Skill is not assigned in the inspector.");
        }
    }
}


