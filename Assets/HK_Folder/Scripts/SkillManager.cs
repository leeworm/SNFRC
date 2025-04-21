using UnityEngine;

    public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    public Dash_Skill dash;  // �ν����Ϳ��� �Ҵ�

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


