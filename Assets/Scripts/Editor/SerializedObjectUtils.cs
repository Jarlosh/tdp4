using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace TDPF.Editor.Tools
{
    public static class SerializedObjectUtils
    {
        public static SerializedProperty FindAutoProperty(this SerializedObject obj, string propertyPath)
        {
            return obj.FindProperty($"<{propertyPath}>k__BackingField");
        }

        public static SerializedProperty FindAutoPropertyRelative(
            this SerializedProperty property,
            string relativePropertyPath)
        {
            return property.FindPropertyRelative($"<{relativePropertyPath}>k__BackingField");
        }

        internal static MethodInfo GetMethod(this SerializedObject obj, string methodName)
        {
            var targetObject = obj.targetObject;
            var flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var method = targetObject.GetType().GetMethod(methodName, flags);
            return method;
        }

        internal static Rect CutRight(this ref Rect rect, float width)
        {
            rect.xMax -= width;
            return new Rect(rect) { x = rect.xMax, width = width };
        }
        
        internal static Rect CutLeft(this ref Rect rect, float width)
        {
            rect.xMin += width;
            return new Rect(rect) { x = rect.x - width, width = width };
        }
        
        internal static Rect CutDown(this ref Rect rect, float height)
        {
            rect.yMax -= height;
            return new Rect(rect) { y = rect.yMax, height = height};
        }
        
        internal static Rect CutTop(this ref Rect rect, float height)
        {
            rect.yMin += height;
            return new Rect(rect) { y = rect.y - height, height = height};
        }
        
        // internal static Rect CutDownSlh(this ref Rect rect, float rows)
        // {
        //     return CutDown(ref rect, rows * EditorGUIUtility.singleLineHeight);
        // }
        
        internal static Rect CutTopSlh(this ref Rect rect, float rows)
        {
            return CutTop(ref rect, rows * EditorGUIUtility.singleLineHeight);
        }
    }
}