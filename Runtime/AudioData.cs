using CuteEngine.Audio.Attributes;
using UnityEngine;

namespace CuteEngine.Audio
{
    [System.Serializable]
    public class AudioData
    {
        public string name;
        public AudioClip clips;
        public bool randomPitch = false;
        [ShowIf("randomPitch")][MinMaxFloat(-3f, 3f)] public Vector2 pitchRange = new Vector2(-3f, 3f);

        public AudioClip GetClip()
        {
            //TODO
            if (clips == null) return null;
            return clips;
        }
    }
}
