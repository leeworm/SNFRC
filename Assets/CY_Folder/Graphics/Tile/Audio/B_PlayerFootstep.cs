using UnityEngine;

public class PlayerFootstep : MonoBehaviour
{
    public AudioClip[] footstepClips;
    public AudioSource audioSource;
    public float interval = 0.25f;

    private float lastStepTime = 0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Mathf.Abs(rb.linearVelocity.x) > 0.1f && Time.time - lastStepTime > interval)
        {
            lastStepTime = Time.time;
            PlayRandomFootstep();
        }
    }

    void PlayRandomFootstep()
    {
        if (footstepClips.Length == 0) return;
        int index = Random.Range(0, footstepClips.Length);
        audioSource.PlayOneShot(footstepClips[index]);
    }
}
