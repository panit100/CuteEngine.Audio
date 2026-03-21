using System;
using CuteEngine.Audio.Attributes;
using UnityEditor;
using UnityEngine;

namespace CuteEngine.Audio.Editor
{
  [CustomPropertyDrawer(typeof(ShowIf))]
  public class ShowIfDrawer : PropertyDrawer
  {
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
      ShowIf condition = (ShowIf)attribute;
      bool enable = GetConditionResult(property, condition.ConditionName);

      if (enable)
      {
        EditorGUI.PropertyField(position, property, label, true);
      }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
      ShowIf condition = (ShowIf)attribute;
      bool enable = GetConditionResult(property, condition.ConditionName);
      return enable ? EditorGUI.GetPropertyHeight(property, label, true) : -EditorGUIUtility.standardVerticalSpacing;
    }

    private bool GetConditionResult(SerializedProperty property, string conditionName)
    {
      string path = property.propertyPath;

      string conditionPath = path.Replace(property.name, conditionName);

      SerializedProperty conditionProperty = property.serializedObject.FindProperty(conditionPath);

      if (conditionProperty != null && conditionProperty.propertyType == SerializedPropertyType.Boolean)
      {
        return conditionProperty.boolValue;
      }

      return true;
    }
  }
}
