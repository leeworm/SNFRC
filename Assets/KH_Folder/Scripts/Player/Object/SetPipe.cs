using UnityEngine;

public class SetPipe : MonoBehaviour
{
    [SerializeField] private float plusPos = 1;
    [SerializeField] private GameObject pipePrefab;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.X))
        {
            KH_SoundManager.Instance.PlaySFXSound("marioPipe");

            Instantiate(pipePrefab, transform.position, Quaternion.identity);
            Debug.Log("파이프 생성");
            Destroy(gameObject); // 파이프 생성 후 오브젝트 삭제
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0, plusPos, 0);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0, -plusPos, 0);
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-plusPos, 0, 0);
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(plusPos, 0, 0);
        }
    }
}
