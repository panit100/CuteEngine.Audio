using System;
using UnityEngine;

namespace CuteEngine.Audio
{
    [Serializable]
    public class AudioHandle
    {
        public string ID { get; private set; }
        public AudioSource Source { get; private set; }
        public AudioClip Clip { get; private set; }
        public bool IsActive => Source != null && (Source.isPlaying || IsPause);
        public bool IsPause { get; private set; }

        public AudioHandle(string id, AudioClip clip, AudioSource source)
        {
            ID = id;
            Clip = clip;
            Source = source;
        }

        public void Play(bool loop = false)
        {
            if (Source == null) return;

            Source.clip = Clip;
            Source.loop = loop;
            Source.Play();
            IsPause = false;
        }

        public void Stop()
        {
            if (Source == null) return;

            Source.Stop();
            IsPause = false;
        }

        public void Pause()
        {
            if (Source == null) return;
            if (!Source.isPlaying || IsPause) return;

            Source.Pause();
            IsPause = true;
        }

        public void Resume()
        {
            if (Source == null) return;
            if (!IsPause) return;

            Source.UnPause();
            IsPause = false;
        }
    }
}
