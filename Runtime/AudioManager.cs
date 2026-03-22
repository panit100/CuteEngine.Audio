using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace CuteEngine.Audio
{
    public static class AudioManager
    {
        private static Transform parent;
        private static AudioUpdater updater;
        private static Dictionary<string, AudioHandle> audioHandles = new Dictionary<string, AudioHandle>();
        private static Stack<AudioSource> sourcePool = new Stack<AudioSource>();

        private static AudioSetting setting;

        public static void Init(AudioSetting settingIn)
        {
            setting = settingIn;
            CreateAudioRoot();
        }

        private static void CreateAudioRoot()
        {
            if (parent == null)
            {
                parent = new GameObject("Audio_Root").transform;
                Object.DontDestroyOnLoad(parent.gameObject);

                updater = parent.gameObject.AddComponent<AudioUpdater>();
            }
        }

        private static AudioSource GetAudioSource()
        {
            CreateAudioRoot();

            if (sourcePool.Count > 0)
            {
                var source = sourcePool.Pop();
                source.gameObject.SetActive(true);
                return source;
            }

            GameObject go = new GameObject("AudioSource");
            go.transform.SetParent(parent);
            AudioSource audioSource = go.AddComponent<AudioSource>();
            return audioSource;
        }

        private static void AddHandle(string id, AudioHandle handle)
        {
            if (audioHandles.ContainsKey(id))
            {
                AudioLogger.LogWarning($"AudioHandle with ID {id} already exists.");
                return;
            }

            audioHandles.Add(id, handle);
        }

        public static void RemoveHandle(string id)
        {
            if (!audioHandles.TryGetValue(id, out AudioHandle audioHandle))
            {
                AudioLogger.LogWarning($"AudioHandle with ID {id} not found.");
                return;
            }

            audioHandle.Stop();
            audioHandle.Source.gameObject.SetActive(false);
            sourcePool.Push(audioHandle.Source);
            audioHandles.Remove(id);

            if (updater != null) updater.StopTrack(id);
        }

        public static AudioHandle GetAudioHandle(string id)
        {
            if (audioHandles.TryGetValue(id, out AudioHandle audioHandle))
            {
                return audioHandle;
            }
            return null;
        }

        public static string Play(string name, bool loop = false)
        {
            string id = System.Guid.NewGuid().ToString();
            return Play(id, name, loop);
        }

        public static string Play(string id, string name, bool loop = false)
        {
            if (audioHandles.TryGetValue(id, out AudioHandle existingHandle))
            {
                existingHandle.Play(loop);
                return id;
            }

            AudioData audioData = setting.GetAudioData(name, out AudioMixerGroup audioMixerGroup);
            if (audioData == null)
            {
                AudioLogger.LogError($"Audio with name : {name} not found in database");
                return "";
            }

            AudioClip clip = audioData.Clip;
            AudioSource audioSource = GetAudioSource();

            //TODO Init AudioSource
            audioSource.clip = clip;
            audioSource.spatialBlend = 0; // 0 For 2D, 1 For 3D
            audioSource.outputAudioMixerGroup = audioMixerGroup;

            AudioHandle audioHandle = new AudioHandle(id, clip, audioSource);
            AddHandle(id, audioHandle);

            audioHandle.Play(loop);

            if (!loop && updater != null)
            {
                float realDuration = clip.length / Mathf.Max(0.1f, Mathf.Abs(audioSource.pitch));
                updater.Track(id, realDuration);
            }

            return id;
        }

        public static void Resume(string id)
        {
            if (!audioHandles.TryGetValue(id, out AudioHandle audioHandle))
            {
                AudioLogger.LogWarning($"AudioHandle with ID {id} not found.");
                return;
            }
            audioHandle.Resume();
        }

        public static void Stop(string id)
        {
            RemoveHandle(id);
        }

        public static void Pause(string id)
        {
            if (!audioHandles.TryGetValue(id, out AudioHandle audioHandle))
            {
                AudioLogger.LogWarning($"AudioHandle with ID {id} not found.");
                return;
            }
            audioHandle.Pause();
        }
    }
}
