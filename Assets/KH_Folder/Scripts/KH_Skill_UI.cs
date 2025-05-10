using UnityEngine;
using UnityEngine.UI;

public class KH_Skill_UI : MonoBehaviour
{
    [SerializeField] public KH_Player player;

    [Header("UI")]
    public Image CoolTimeImage_Pipe;
    public Image CoolTimeImage_MushRoom;

    

    void Update()
    {
        if(player.setPipeTimer > 0)
        {
            CoolTimeImage_Pipe.fillAmount = player.setPipeTimer / player.setPipeCoolTime;
        }
        else
        {
            CoolTimeImage_Pipe.fillAmount = 0;
        }
        if(player.mushRoomTimer > 0)
        {
            CoolTimeImage_MushRoom.fillAmount = player.mushRoomTimer / player.mushRoomCoolTime;
        }
        else
        {
            CoolTimeImage_MushRoom.fillAmount = 0;
        }
    }
}
