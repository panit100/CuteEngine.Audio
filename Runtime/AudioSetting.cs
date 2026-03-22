using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace CuteEngine.Audio
{
    [CreateAssetMenu(fileName = "NewAudioSetting", menuName = "CuteEngine/Audio/AudioSetting")]
    public class AudioSetting : ScriptableObject
    {
        private const string DEFAULTSETTINGPATH = "CuteEngine/Audio/AudioSetting.asset"; //TODO user can change it in Editor

        [SerializeField] float defaultMasterVolume = 0.8f;

        [SerializeField] AudioMixer mainMixer;
        [SerializeField] AudioMixerGroup masterGroup;
        [SerializeField] AudioMixerGroup musicGroup;
        [SerializeField] AudioMixerGroup sfxGroup;
        [SerializeField] AudioMixerGroup uiGroup;
        [SerializeField] AudioMixerGroup voiceGroup;


        [Header("Audio List")]
        [SerializeField] private List<AudioData> audioList = new List<AudioData>();
        private Dictionary<string, AudioData> audioDict;

        public float DefaultMasterVolume => defaultMasterVolume;

        public AudioMixer MainMixer => mainMixer;
        public AudioMixerGroup MasterGroup => masterGroup;
        public AudioMixerGroup MusicGroup => musicGroup;
        public AudioMixerGroup SfxGroup => sfxGroup;
        public AudioMixerGroup UiGroup => uiGroup;
        public AudioMixerGroup VoiceGroup => voiceGroup;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void AutoInitialize()
        {
            var setting = Resources.Load<AudioSetting>(DEFAULTSETTINGPATH);
            if (setting != null)
            {
                AudioManager.Init(setting);
            }
            else
            {
                var newSetting = CreateAudioSetting();
                AudioManager.Init(newSetting);
            }
        }

        private static AudioSetting CreateAudioSetting()
        {
            AudioSetting tmpSetting = ScriptableObject.CreateInstance<AudioSetting>();
            string path = AssetDatabase.GenerateUniqueAssetPath(DEFAULTSETTINGPATH);
            AssetDatabase.CreateAsset(tmpSetting, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            return tmpSetting;
        }

        public void Initialize()
        {
            audioDict = new Dictionary<string, AudioData>();
            foreach (AudioData data in audioList)
            {
                string audioName = data.Name;

                if (string.IsNullOrEmpty(data.Name))
                {
                    if (data.Clip != null)
                    {
                        audioName = data.Name;
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

        public AudioData GetAudioData(string name, out AudioMixerGroup audioMixerGroup)
        {
            if (audioDict == null) Initialize();

            if (audioDict.TryGetValue(name, out AudioData data))
            {
                audioMixerGroup = GetAudioMixerGroup(data.GroupType);
                return data;
            }

            AudioLogger.LogError($"Audio with name : {name} not found in database");
            audioMixerGroup = null;
            return null;
        }

        private AudioMixerGroup GetAudioMixerGroup(AudioGroupType type)
        {
            if (mainMixer == null)
                return null;

            return type switch
            {
                AudioGroupType.Master => masterGroup,
                AudioGroupType.Music => musicGroup,
                AudioGroupType.SFX => sfxGroup,
                AudioGroupType.UI => uiGroup,
                AudioGroupType.Voice => voiceGroup,
                _ => masterGroup,
            };

        }

        public void SetMainMixer(AudioMixer audioMixer)
        {
            mainMixer = audioMixer;
        }

        public void SetMasterGroup(AudioMixerGroup audioMixerGroup)
        {
            masterGroup = audioMixerGroup;
        }

        public void SetMusicGroup(AudioMixerGroup audioMixerGroup)
        {
            musicGroup = audioMixerGroup;
        }

        public void SetSfxGroup(AudioMixerGroup audioMixerGroup)
        {
            sfxGroup = audioMixerGroup;
        }

        public void SetUiGroup(AudioMixerGroup audioMixerGroup)
        {
            uiGroup = audioMixerGroup;
        }

        public void SetVoiceGroup(AudioMixerGroup audioMixerGroup)
        {
            voiceGroup = audioMixerGroup;
        }
    }
}
