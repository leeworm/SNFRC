using UnityEngine;

public class KH_GameManager : MonoBehaviour
{
    public GameObject GroundTilemap;

    public Koopa koopa; // Koopa 스크립트

    public GameObject DamageRangeX;
    public GameObject DamageRangeY;

    private static KH_GameManager instance;
    public static KH_GameManager Instance
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
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    
    public void PhaseChange()
    {
        //GroundTilemap.SetActive(false);
        Destroy(GroundTilemap, 0.001f);
    }

    /// Phase2 시작
    public void StartPhase2()
    {
        koopa.phaseState = PhaseState.Phase2;
    }

    public void SetActive_DamageRangeX(bool _isTrue)
    {
        if(koopa.phaseState == PhaseState.Phase1)
            DamageRangeX.transform.position = new Vector3(koopa.transform.position.x,0,0);
        else if(koopa.phaseState == PhaseState.Phase2)
            DamageRangeX.transform.position = new Vector3(koopa.transform.position.x,-196.9f,0);

        DamageRangeX.SetActive(_isTrue);
    }
    public void SetActive_DamageRangeY(bool _isTrue)
    {
        DamageRangeY.transform.position = new Vector3(0,koopa.transform.position.y,0);

        DamageRangeY.SetActive(_isTrue);
    }
}
