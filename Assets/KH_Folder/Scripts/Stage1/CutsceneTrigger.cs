using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BackgroundMusic.Instance.BGM_Change();
            KH_SoundManager.Instance.PlaySFXSound("marioFlag");

            collision.transform.SetParent(transform);
            collision.transform.GetComponent<KH_Player>().Hang();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }


}