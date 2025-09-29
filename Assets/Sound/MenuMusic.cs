using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioClip menuMusic;   // Kéo file nhạc nền vào đây
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (menuMusic != null)
        {
            audioSource.clip = menuMusic;
            audioSource.loop = true;       // Lặp lại
            audioSource.playOnAwake = true; // Tự chạy khi mở game
            audioSource.Play();
        }
    }
}
