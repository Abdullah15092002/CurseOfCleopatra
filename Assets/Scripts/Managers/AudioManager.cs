using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource sfxSource;

        [Header("AudioClip’ler")]
        public AudioClip backgroundMusic;
        public AudioClip CoinCollectMusic;


        void Awake()
        {
            musicSource.volume = 0.4f;
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void PlaySFX(AudioClip clip)
        {
            sfxSource.PlayOneShot(clip);
        }
        public void PlayBackgroundMusic()
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
        public void StopBackgroundMusic()
        {
            if (musicSource != null)
            {
                musicSource.Stop();
            }
        }
        public void VolumeOffBackgroundMusıc()
        {
            musicSource.volume = 0;
        }
        public void VolumeOnnBackgroundMusıc()
        {
            musicSource.volume = 0.4f;
        }
    }
}
