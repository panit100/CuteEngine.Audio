using UnityEngine;

namespace CuteEngine.Audio.Attributes
{
  public class MinMaxFloat : PropertyAttribute
  {
    public float Min { get; private set; }
    public float Max { get; private set; }

    public MinMaxFloat(float min, float max)
    {
      Min = min;
      Max = max;
    }
  }
}
