using System;
using System.Linq;
using System.Reflection;
using TDPF.Editor.Tools;
using TDPF.FuckUp.DialogueSystem;
using UnityEditor;
using UnityEngine;

namespace TDPF.Editor
{
    [CustomPropertyDrawer(typeof(DialogueStack.Item))]
    public class DialogueStackItemDrawer : PropertyDrawer
    {
        private float Slh => EditorGUIUtility.singleLineHeight;

        private SerializedProperty SettingsProp(SerializedProperty prop) =>
            prop.FindAutoPropertyRelative(nameof(DialogueStack.Item.Settings));

        private float GetHeight(SerializedProperty prop) => EditorGUI.GetPropertyHeight(prop);

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var prop = SettingsProp(property);
            return Slh * 4 + (prop.isExpanded ? GetHeight(prop) : 0);
        }

        public override bool CanCacheInspectorGUI(SerializedProperty property) => false;

        public override void OnGUI(Rect rect, SerializedProperty property, GUIContent label)
        {
            var settingsProp = SettingsProp(property);
            var charProp = property.FindAutoPropertyRelative(nameof(DialogueStack.Item.Character));
            var textProp = property.FindAutoPropertyRelative(nameof(DialogueStack.Item.Text));
            var leftOffset = rect.width / 32;
            var rightOffset = 4; //px

            rect.xMin += leftOffset;
            rect.xMax -= rightOffset;

            var coefficient = 1f / 5;
            var space = 5f;
            var content = new GUIContent(label);
            // var labelWidth = EditorStyles.label.CalcSize(content).x;

            var foldoutWidth = coefficient * rect.width;
            var characterRect = rect.CutTopSlh(1);
            var propRect = characterRect.CutRight((1f - coefficient) * characterRect.width);
            var foldoutRect = propRect.CutRight(foldoutWidth);
            var labelRect = characterRect;
            propRect.CutLeft(space);
            var colorRect = propRect.CutLeft(10f);
            propRect.CutLeft(space);

            foldoutRect.CutLeft(space);
            settingsProp.isExpanded = EditorGUI.Foldout(foldoutRect, settingsProp.isExpanded, "Settings");

            if (settingsProp.isExpanded)
            {
                var settingsRect = rect.CutTop(GetHeight(settingsProp));
                EditorGUI.PropertyField(settingsRect, settingsProp, new GUIContent(settingsProp.displayName), true);
            }

            var character = charProp.objectReferenceValue as CharacterItem;
            if (character != null)
            {
                EditorGUI.DrawRect(colorRect, character.Color);
            }

            EditorGUI.LabelField(labelRect, content);
            EditorGUI.PropertyField(propRect, charProp, GUIContent.none);
            rect.y -= Slh / 2;
            EditorGUI.PropertyField(rect, textProp, GUIContent.none);
        }
    }
}