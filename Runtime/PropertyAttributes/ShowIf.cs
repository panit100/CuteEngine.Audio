using System;
using UnityEngine;

namespace CuteEngine.Audio.Attributes
{
  [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class | AttributeTargets.Struct, Inherited = true)]
  public class ShowIf : PropertyAttribute
  {
    public string ConditionName { get; private set; }

    public ShowIf(string conditionName)
    {
      ConditionName = conditionName;
    }
  }
}
