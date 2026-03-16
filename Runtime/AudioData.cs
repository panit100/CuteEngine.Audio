using UnityEngine;

namespace CuteEngine.Audio
{
    [System.Serializable]
    public class AudioData
    {
        public string name;
        public AudioClip[] clips;

        public AudioClip GetClip()
        {
            //TODO
            if (clips == null || clips.Length == 0) return null;
            return clips[0];
        }
    }
}
