using Game.Item.PlantSeed;
using Plants.Plant;
using UnityEditor;
using UnityEngine;

namespace Game.Item.Editor
{
    [CustomPropertyDrawer(typeof(Reproduction))]
    [CustomPropertyDrawer(typeof(PlantContainer))]
    [CustomPropertyDrawer(typeof(SoilType))]
    public class EnumMaskFielDrawner : PropertyDrawer
    {
        public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
        {
            _property.intValue = EditorGUI.MaskField( _position, _label, _property.intValue, _property.enumNames );
        }
    }
}
