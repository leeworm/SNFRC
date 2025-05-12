using System.Collections;
using UnityEngine;

public class KoopaLaser : MonoBehaviour
{
    private SpriteRenderer sr;

    public float colorChangeSpeed = 0.05f;
    public float alpha = 1;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        StartCoroutine(Rainbow());
    }

    void Update()
    {
        
    }

    IEnumerator Rainbow()
    {
        while(true)
        {
            sr.color = new Color(255,0,0,alpha);
            yield return new WaitForSeconds(colorChangeSpeed);
            sr.color = new Color(0,255,0,alpha);
            yield return new WaitForSeconds(colorChangeSpeed);
            sr.color = new Color(0,0,255,alpha);
            yield return new WaitForSeconds(colorChangeSpeed);
        }
    }
}
