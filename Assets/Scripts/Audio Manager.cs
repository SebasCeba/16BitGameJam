using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip background;
    public AudioClip selected;
    public AudioClip[] placementSfx;
    public AudioClip[] jumpSfx;
    public AudioClip walkSfx;
    public AudioClip deathSfx;
    public AudioClip collectedSfx;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
    public void PlaySfx(AudioClip clip)
    {
        if(clip != null)
            sfxSource.PlayOneShot(clip); // Play the specified sound effect clip once without interrupting any currently playing sound effects
    }
    public void StopSfx()
    {
        sfxSource.Stop(); // Stop the currently playing sound effect on the sfxSource
    }
    public void PlayRandomJumpSfx()
    {
        if(jumpSfx.Length > 0)
        {
            int index = Random.Range(0, jumpSfx.Length);
            PlaySfx(jumpSfx[index]);
        }
    }
    public void PlayRandomPlacementSfx()
    {
        if (placementSfx.Length > 0)
        {
            int index = Random.Range(0, placementSfx.Length);
            PlaySfx(placementSfx[index]);
        }
    }
}
