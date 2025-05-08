using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    AudioSource audioSource;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private AudioClip explosionSound;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayShotSound()
    {
        if (shotSound != null)
        {
            audioSource.PlayOneShot(shotSound);
        }
        else
        {
            Debug.LogError("Shot sound not assigned!");
        }
    }

    public void PlayExplosionSound()
    {
        if (explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
        else
        {
            Debug.LogError("Explosion sound not assigned!");
        }
    }
}
