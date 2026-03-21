using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip selected;
    public AudioClip placementSfx;
    public AudioClip jumpSfx;
    public AudioClip walkSfx;
    public AudioClip deathSfx;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySfx(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
