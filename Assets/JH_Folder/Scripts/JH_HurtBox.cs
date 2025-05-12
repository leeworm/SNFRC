using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class JH_Hurtbox : MonoBehaviour
{
    // JH_Entity Ŭ���� ���ο� ���ǵ� BodyPart �������� ����ϹǷ� Ÿ���� JH_Entity.BodyPart�� ����մϴ�.
    public JH_Entity.BodyPart bodyPartType = JH_Entity.BodyPart.Body; // �⺻���� Body�� ���� (None�� enum�� �����Ƿ�)
    public JH_Entity ownerEntity; // �� ��Ʈ�ڽ��� ���� ���� Entity ��ũ��Ʈ (Player �Ǵ� Enemy)

    public AudioClip impactSoundClip; // Inspector���� �� ��Ʈ�ڽ��� �¾��� �� ����� ���� Ŭ��
    private AudioSource audioSource;    // ���带 ����� AudioSource ������Ʈ

    void Awake()
    {
        // ownerEntity�� �ν����Ϳ��� �Ҵ���� �ʾҴٸ�, �θ� ������Ʈ���� JH_Entity�� ã���ϴ�.
        if (ownerEntity == null)
        {
            ownerEntity = GetComponentInParent<JH_Entity>();
        }

        if (ownerEntity == null)
        {
            enabled = false; // ownerEntity�� ������ �� ��ũ��Ʈ�� �۵����� �ʵ��� ��Ȱ��ȭ
            return;
        }

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.loop = false;
    }

    // �������� JH_Hitbox ��ũ��Ʈ�� �� ��Ʈ�ڽ��� �浹���� �� ȣ���ϴ� �޼ҵ��Դϴ�.
    // �� �޼ҵ�� �������κ��� ���� ������ ����, �� ��Ʈ�ڽ��� � ���������� ownerEntity���� �����մϴ�.
    public void ProcessHit(float damageAmount)
    {
       

        // �ǰ� ���� ���
        if (audioSource != null && impactSoundClip != null)
        {
            audioSource.PlayOneShot(impactSoundClip);
        }

        if (ownerEntity != null && !ownerEntity.isKnocked)
        {
            ownerEntity.TakeDamage(damageAmount, this.bodyPartType);
        }
        else if (ownerEntity == null)
        {
            Debug.LogError(gameObject.name + "�� Hurtbox�� ownerEntity�� ������� �ʾҽ��ϴ�.");
        }
    }
}
