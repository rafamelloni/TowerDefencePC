using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip clipTest;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Application.targetFrameRate = 120;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); // Evita duplicados
            return;
        }

        if (musicSource == null)
        {
            Debug.LogError("MusicManager: No se ha asignado un AudioSource en el Inspector.");
        }
    }

    private void Start()
    {
        PlayMusic(clipTest, 1f , true);
    }

    public void PlayMusic(AudioClip musicClip, float volume = 1f, bool loop = true)
    {
        if (musicSource == null || musicClip == null) {
            print("retorno");
            return;
        } 


        AudioSource audioSource = Instantiate(musicSource, transform.position, Quaternion.identity);
        audioSource.clip = musicClip;
        audioSource.volume = volume;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource == null) return;
        musicSource.Stop();
    }

    public void ChangeMusic(AudioClip newMusicClip, float volume = 1f, bool loop = true)
    {
        if (musicSource == null || newMusicClip == null) return;

        musicSource.Stop();
        PlayMusic(newMusicClip, volume, loop);
    }

    public void SetMusicVolume(float volume)
    {
        if (musicSource == null) return;
        musicSource.volume = Mathf.Clamp01(volume); // Limita entre 0 y 1
    }
}
