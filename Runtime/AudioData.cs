using CuteEngine.Audio.Attributes;
using UnityEngine;

namespace CuteEngine.Audio
{
    public enum AudioGroupType
    {
        Master,
        Music,
        SFX,
        UI,
        Voice,
    }

    [System.Serializable]
    public class AudioData
    {
        [SerializeField] string name;
        [SerializeField] AudioClip clip;
        [SerializeField] AudioGroupType groupType = AudioGroupType.Master;
        [SerializeField] bool randomPitch = false;
        [ShowIf("randomPitch")][MinMaxFloat(-3f, 3f)][SerializeField] Vector2 pitchRange = new Vector2(-3f, 3f);

        public string Name => name;
        public AudioClip Clip => clip;
        public bool RandomPitch => randomPitch;
        public Vector2 PitchRange => pitchRange;
        public AudioGroupType GroupType => groupType;
    }
}
