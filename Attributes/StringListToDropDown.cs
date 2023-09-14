using System;
using Code.BlackCubeSubmodule.DebugTools.BlackCubeLogger;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Code.BlackCubeSubmodule.Attributes
{
    public sealed class StringListToDropDown : PropertyAttribute 
    {
        public delegate string[] GetStringList();

        public StringListToDropDown(params string [] list) 
        {
            List = list;
        }

        public StringListToDropDown(Type type, string methodName) 
        {
            var method = type.GetMethod (methodName);
            if (method != null) List = method.Invoke (null, null) as string[];
            else $"NO SUCH METHOD {methodName} FOR {type}".Log();
        }

        public string[] List 
        {
            get;
            private set;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(StringListToDropDown))]
    public sealed class StringInListDrawer : PropertyDrawer 
    {
        // Draw the property inside the given rect
        public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) 
        {
            var stringInList = attribute as StringListToDropDown;
            var list = stringInList.List;
            if (property.propertyType == SerializedPropertyType.String) 
            {
                int index = Mathf.Max (0, Array.IndexOf (list, property.stringValue));
                index = EditorGUI.Popup (position, property.displayName, index, list);

                property.stringValue = list [index];
            } 
            else if (property.propertyType == SerializedPropertyType.Integer) 
            {
                property.intValue = EditorGUI.Popup (position, property.displayName, property.intValue, list);
            } 
            else 
            {
                base.OnGUI (position, property, label);
            }
        }
    }
#endif
}