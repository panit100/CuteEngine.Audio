using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace CuteEngine.Audio.Editor
{
    public class AudioSystemCreator
    {
        private const string MIXER_TEMPLATE_GUID = "2f055dfb852ba7149af6285e3e54f70a";
        private const string MIXER_TEMPLATE_PATH = "Packages/com.cute.engine.audio/Templates/DefaultMixerTemplate.mixer";

        private const string DEFAULT_FOLDER = "Assets/Resources/CuteEngine/Audio";
        private const string DEFAULT_SETTING_PATH = DEFAULT_FOLDER + "/AudioSetting.asset";
        private const string DEFAULT_MIXER_PATH = DEFAULT_FOLDER + "/MainMixer.mixer";

        //TODO Change DEFAULTSETTINGPATH to match DEFAULT_SETTING_PATH

        [MenuItem("CuteEngine/Setup Audio System")]
        public static AudioSetting CreateAudioSettingWithMixer()
        {
            string templatePath = AssetDatabase.GUIDToAssetPath(MIXER_TEMPLATE_GUID);

            if (string.IsNullOrEmpty(templatePath))
            {
                templatePath = MIXER_TEMPLATE_PATH;
            }

            if (!Directory.Exists(DEFAULT_FOLDER))
            {
                Directory.CreateDirectory(DEFAULT_FOLDER);
                AssetDatabase.Refresh();
            }

            string uniqueMixerPath = AssetDatabase.GenerateUniqueAssetPath(DEFAULT_MIXER_PATH);

            AssetDatabase.CopyAsset(templatePath, uniqueMixerPath);
            AssetDatabase.Refresh();

            AudioMixer newMixer = AssetDatabase.LoadAssetAtPath<AudioMixer>(uniqueMixerPath);

            AudioSetting tmpSetting = ScriptableObject.CreateInstance<AudioSetting>();

            tmpSetting.SetMainMixer(newMixer);
            tmpSetting.SetMasterGroup(FindGroup(newMixer, "Master"));
            tmpSetting.SetMusicGroup(FindGroup(newMixer, "Music"));
            tmpSetting.SetSfxGroup(FindGroup(newMixer, "SFX"));
            tmpSetting.SetUiGroup(FindGroup(newMixer, "UI"));
            tmpSetting.SetVoiceGroup(FindGroup(newMixer, "Voice"));

            string uniqueSettingPath = AssetDatabase.GenerateUniqueAssetPath(DEFAULT_SETTING_PATH);
            AssetDatabase.CreateAsset(tmpSetting, uniqueSettingPath);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            AudioLogger.Log($"Audio System Initialized Successfully!");
            AudioLogger.Log($"Setting: {uniqueSettingPath} | Mixer: {uniqueMixerPath}");

            Selection.activeObject = tmpSetting;

            return tmpSetting;
        }

        private static AudioMixerGroup FindGroup(AudioMixer mixer, string name)
        {
            AudioMixerGroup[] groups = mixer.FindMatchingGroups(name);
            return groups.Length > 0 ? groups[0] : null;
        }
    }
}
