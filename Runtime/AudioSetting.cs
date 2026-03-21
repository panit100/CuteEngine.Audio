using UnityEngine;

namespace CuteEngine.Audio
{
  public static class AudioSetting
  {
    static float globalVolume = 1;
    static bool isMuted = false;
    public static bool GetMute => isMuted;
    public static float GetVolume => globalVolume;

    //TODO maybe implement save load setting data form prefab or json

    public static void SetMute(bool mute)
    {
      isMuted = mute;
    }

    public static void SetVolume(float volume)
    {
      globalVolume = Mathf.Clamp(volume, 0, 1);
    }
  }
}
