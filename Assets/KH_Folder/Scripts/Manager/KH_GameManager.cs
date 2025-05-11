using UnityEngine;

public class KH_GameManager : MonoBehaviour
{
    // 부서질 땅 타일맵
    public GameObject GroundTilemap;

    // Koopa 스크립트
    public Koopa koopa; 

    // 데미지 영역
    public GameObject DamageRangeX;
    public GameObject DamageRangeY;

    // 파이프 위치
    public class TelepotPipe
    {
        public Vector3 vec;
        public bool havePipe = false;
    }

    public TelepotPipe[] telepotPipe;

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

    void Start()
    {
        telepotPipe = new TelepotPipe[2];
        
        telepotPipe[0] = new TelepotPipe();
        telepotPipe[1] = new TelepotPipe();
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

    public int SetTelepotPipe(Vector3 _vec, bool _havePipe)
    {
        for(int i = 0; i < 2; i++)
        {
            if(telepotPipe[i].havePipe == false) // 비어 있다면
            {
                telepotPipe[i].vec = _vec;
                telepotPipe[i].havePipe = _havePipe;
                return i;
            }
        }
        return -1;
    }

    public void EmptyTelepotPipe(int _pipeNum, bool _havePipe)
    {
        telepotPipe[_pipeNum].havePipe = _havePipe;
    }

    public void StartTelepotPipe()
    {
        if(telepotPipe[0].havePipe && telepotPipe[1].havePipe)
        {

        }
    }
}
