using System;
using CuteEngine.Audio.Attributes;
using UnityEditor;
using UnityEngine;

namespace CuteEngine.Audio.Editor
{
    [CustomPropertyDrawer(typeof(MinMaxFloat))]
    public class MinMaxFloatDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Vector2)
            {
                MinMaxFloat attr = (MinMaxFloat)attribute;
                Vector2 range = property.vector2Value;

                float min = range.x;
                float max = range.y;

                label = EditorGUI.BeginProperty(position, label, property);
                position = EditorGUI.PrefixLabel(position, label);

                float fieldWidth = 40f;
                float spacing = 5f;
                float sliderWidth = position.width - (fieldWidth * 2) - (spacing * 2);

                Rect leftFieldRect = new Rect(position.x, position.y, fieldWidth, position.height);
                Rect sliderRect = new Rect(position.x + fieldWidth + spacing, position.y, sliderWidth, position.height);
                Rect rightFieldRect = new Rect(position.x + fieldWidth + spacing + sliderWidth + spacing, position.y, fieldWidth, position.height);

                EditorGUI.BeginChangeCheck();

                min = EditorGUI.FloatField(leftFieldRect, min);
                EditorGUI.MinMaxSlider(sliderRect, ref min, ref max, attr.Min, attr.Max);
                max = EditorGUI.FloatField(rightFieldRect, max);

                if (EditorGUI.EndChangeCheck())
                {
                    min = Mathf.Clamp(min, attr.Min, max);
                    max = Mathf.Clamp(max, min, attr.Max);
                    property.vector2Value = new Vector2(min, max);
                }

                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.LabelField(position, label.text, "Use MinMax with Vector2 only.");
            }
        }
    }
}
