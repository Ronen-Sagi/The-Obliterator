using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class StageMusicController : MonoBehaviour
{
    private AudioSource musicSource;

    private float stageDuration;
    private float maxVolume;
    private float elapsed;

    void Awake()
    {
        musicSource = GetComponent<AudioSource>();
        musicSource.loop = false;
        musicSource.spatialBlend = 0f; // 2D
    }

    public void StartStageMusic(float durationSeconds, float targetMaxVolume)
    {
        stageDuration = durationSeconds;
        maxVolume = targetMaxVolume;
        elapsed = 0f;

        musicSource.Stop();
        musicSource.time = 0f;
        musicSource.volume = 0f;
        musicSource.Play();
    }

    void Update()
    {
        if (!musicSource.isPlaying) return;

        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / stageDuration);

        musicSource.volume = Mathf.Lerp(0f, maxVolume, t * t);
    }
}