using System.Collections.Generic;
using UnityEngine;

namespace CuteEngine.Audio
{
    [CreateAssetMenu(fileName = "NewAudioDatabase", menuName = "CuteEngine/Audio/AudioDatabase")]
    public class AudioDatabase : ScriptableObject
    {
        [Header("Audio List")]
        [SerializeField] private List<AudioData> audioList = new List<AudioData>();

        private Dictionary<string, AudioData> audioDict;

        public void Initialize()
        {
            audioDict = new Dictionary<string, AudioData>();
            foreach (AudioData data in audioList)
            {
                string audioName = data.name;

                if (string.IsNullOrEmpty(data.name))
                {
                    if (data.clips != null)
                    {
                        audioName = data.name;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (!audioDict.ContainsKey(audioName))
                {
                    audioDict.Add(audioName, data);
                }
                else
                {
                    AudioLogger.LogWarning($"Found duplicate audio name : {audioName}");
                }
            }
        }

        public AudioData GetAudioData(string name)
        {
            if (audioDict == null) Initialize();

            if (audioDict.TryGetValue(name, out AudioData data))
            {
                return data;
            }

            AudioLogger.LogError($"Audio with name : {name} not found in database");
            return null;
        }
    }
}
