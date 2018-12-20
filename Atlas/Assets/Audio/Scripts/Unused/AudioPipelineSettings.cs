using UnityEngine;
using UnityEditor;

//[CreateAssetMenu(menuName = "Audio Events/Audio Pipeline Configuration")]
public class AudioPipelineSettings : ScriptableObject
{
    public string ClassName;

    public string DictionaryName;

    [Tooltip("Name of the enum that will be generated")]
    public string EnumName;

    [Tooltip("Type of labeled assets to search")]
    public string Label;

    public string NameSpace;

    [Tooltip("Full paths of the folders that will be scanned to find assets")]
    public string[] AssetFolders;
}
