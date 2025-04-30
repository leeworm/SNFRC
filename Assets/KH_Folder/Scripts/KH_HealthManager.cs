using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class KH_HealthManager : MonoBehaviour
{
    private static KH_HealthManager instance;
    public static KH_HealthManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;
            }
            return instance;
        }
    }

    public int health = 10;
    
    public UnityEngine.UI.Image[] hearts; // 0 1 2 3 4
    public Sprite fullHeart;
    public Sprite halfHeart;
    public Sprite emptyHeart;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(1);
        }
        if(Input.GetKeyDown(KeyCode.P))
        {
            Heal(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UpdateHearts();
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if(health >= (i + 1) * 2) // 2 4 6 8 10
            {
                hearts[i].sprite = fullHeart;
            }
            else if(health == i * 2 + 1) // health % 2 == 1 && 
            {
                hearts[i].sprite = halfHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}