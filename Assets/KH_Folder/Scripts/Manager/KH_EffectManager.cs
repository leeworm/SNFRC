using System.Collections.Generic;
using UnityEngine;

public class KH_EffectManager : MonoBehaviour
{
    [SerializeField] private GameObject[] effectArray;
    Dictionary<string, GameObject> effectDictionary = new Dictionary<string, GameObject>();

    private static KH_EffectManager instance;
    public static KH_EffectManager Instance
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

        foreach (GameObject obj in effectArray)
        {
            effectDictionary.Add(obj.name, obj);
        }
    }

    public void PlayEffect(string name, Vector3 _vec)
    {
        if (effectDictionary.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained Eff_Dic");
            return;
        }
        Instantiate(effectDictionary[name], _vec, Quaternion.identity);  
    }
}
