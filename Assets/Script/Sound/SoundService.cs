using System;
using System.Collections;
using UnityEngine;
using Utility;

namespace Sound
{
    public class SoundService : MonoGenericSingleton<SoundService>
    {
        [SerializeField] private AudioSource audioEffects;
        [SerializeField] private AudioSource soundEffect;
        [SerializeField] private AudioSource backgroundMusic;
        [SerializeField] private AudioSource MusicEffect;
        [SerializeField] private Sounds[] audioList;

        private bool hasSoundPlayed = false;
        private bool hasMusicPlayed = false;
        private bool hasMusicEffectPlayed = false;
        private Coroutine soundCoroutine;

        public void PlaySoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);

            if (clip != null)
            {
                soundEffect.loop = loopSound;
                soundEffect.PlayOneShot(clip);
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        public void PlaySoundEffect(SoundType soundType, bool loopSound = false)
        {
            if (!hasSoundPlayed)
            {
                stopCoroutine(soundCoroutine);
                AudioClip clip = GetSoundClip(soundType);
                
                if (clip != null)
                {
                    audioEffects.loop = loopSound;
                    soundCoroutine = StartCoroutine(PlaySoundOnce(clip));
                }
                else
                {
                    Debug.LogError("No Audio Clip got selected");
                }
            }
        }

        public void PlayMusic(SoundType soundType, bool loopSound = false)
        {
            if (!hasMusicPlayed)
            {
                stopCoroutine(soundCoroutine);
                AudioClip clip = GetSoundClip(soundType);

                if (clip != null)
                {
                    backgroundMusic.loop = loopSound;
                    soundCoroutine = StartCoroutine(PlayMusicOnce(clip));
                }
                else
                {
                    Debug.LogError("No Audio Clip got selected");
                }
            }
        }

        public void PlayMusicEffect(SoundType soundType, bool loopSound = false)
        {
            if (!hasMusicEffectPlayed)
            {
                stopCoroutine(soundCoroutine);
                AudioClip clip = GetSoundClip(soundType);

                if (clip != null)
                {
                    MusicEffect.loop = loopSound;
                    soundCoroutine = StartCoroutine(PlayMusicEffectOnce(clip));
                }
                else
                {
                    Debug.LogError("No Audio Clip got selected");
                }
            }
        }

        private IEnumerator PlaySoundOnce(AudioClip clip)
        {
            hasSoundPlayed = true;
            audioEffects.PlayOneShot(clip);
            yield return new WaitForSeconds(clip.length);
            hasSoundPlayed = false;
        }

        private IEnumerator PlayMusicOnce(AudioClip clip)
        {
            hasMusicPlayed = true;
            backgroundMusic.clip = clip;
            backgroundMusic.Play();
            yield return new WaitForSeconds(clip.length);
            hasMusicPlayed = false;
        }

        private IEnumerator PlayMusicEffectOnce(AudioClip clip)
        {
            hasMusicEffectPlayed = true;
            MusicEffect.clip = clip;
            MusicEffect.Play();
            yield return new WaitForSeconds(clip.length);
            hasMusicEffectPlayed = false;
        }

        public void StopSoundEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                audioEffects.clip = clip;
                audioEffects.Stop();
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        public void StopMusicEffects(SoundType soundType, bool loopSound = false)
        {
            AudioClip clip = GetSoundClip(soundType);
            if (clip != null)
            {
                MusicEffect.clip = clip;
                MusicEffect.Stop();
            }
            else
            {
                Debug.LogError("No Audio Clip got selected");
            }
        }

        private AudioClip GetSoundClip(SoundType soundType)
        {
            Sounds sound = Array.Find(audioList, item => item.SoundType == soundType);
            if (sound != null)
            {
                return sound.Audio;
            }
            return null;
        }

        public void stopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }    
        }
    }
}