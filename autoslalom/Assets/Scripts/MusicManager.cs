using UnityEngine;
public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] musicTracks;
    private int currentMusic = 0;
    private void Awake()
    {
        ChangeMusic(1);
        ChangeMusic(0);
    }
    private void Start()
    {
        EventBus.GameEnded += () => ChangeMusic(1);
        EventBus.GameLeaved += () => ChangeMusic(0);
        EventBus.GameStarted += () => ChangeMusic(0);
    }
    private void ChangeMusic(int i)
    {
        if (currentMusic == i)
            return;
        currentMusic = i;
        audioSource.Stop();
        audioSource.clip = musicTracks[i];
        audioSource.Play();
    }
    public void ToggleMusic(bool state)
    {
        audioSource.mute = state;
    }
}