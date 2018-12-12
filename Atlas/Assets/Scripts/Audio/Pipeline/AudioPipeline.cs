#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class AudioPipeline
{
    private static AudioPipelineSettings GetSettings()
    {
        string[] guids = AssetDatabase.FindAssets("AudioPipelineSettingsObj");

        if (guids.Length == 0)
            return null;

        return AssetDatabase.LoadAssetAtPath<AudioPipelineSettings>(AssetDatabase.GUIDToAssetPath(guids[0]));
    }

    private static string NormalizePath(string name)
    {
        string stripped;
        if (name.EndsWith(".asset"))
        {
           stripped = name.Remove(name.Length - ".asset".Length);
        }
        else
        {
            stripped = name;
        }

        System.Text.StringBuilder builder = new System.Text.StringBuilder(stripped.Normalize().ToUpper());
        for (int i = 0; i < builder.Length; ++i)
        {
            if (!char.IsLetterOrDigit(builder[i]))
            {
                builder[i] = '_';
            }
        }

        return (builder.ToString());
    }

    [MenuItem("Audio Events/Generate Enumerations")]
    public static void Go()
    {
        AudioPipelineSettings settings = GetSettings();
        if (settings == null)
        {
            Debug.LogWarning("Audio pipeline configuration not found! Generate one using Assets/Audio Events/Audio Pipeline Configuration");
            return;
        }

        string filePathAndName = "Assets/Scripts/Audio/" + settings.ClassName + ".cs"; //The folder Scripts/Enums/ is expected to exist
        string[] soundEvents = AssetDatabase.FindAssets("l:" + settings.Label, settings.AssetFolders);

        if (File.Exists(filePathAndName))
        {
            File.Delete(filePathAndName);
        }

        using (StreamWriter streamWriter = new StreamWriter(filePathAndName))
        {
            // Imports
            streamWriter.WriteLine("using System.Collections.Generic;");
            streamWriter.WriteLine("using UnityEditor;");
            streamWriter.WriteLine();

            // Namespace
            streamWriter.WriteLine("namespace " + settings.NameSpace + " {");

            // Class declaration
            streamWriter.WriteLine("\tpublic class " + settings.ClassName);
            streamWriter.WriteLine("\t{");

            // Enumeration
            streamWriter.WriteLine("\t\tpublic enum " + settings.EnumName);
            streamWriter.WriteLine("\t\t{");
            for (int i = 0; i < soundEvents.Length; i++)
            {
                if (i < soundEvents.Length - 1)
                {
                    streamWriter.WriteLine("\t\t\t" + NormalizePath(AssetDatabase.GUIDToAssetPath(soundEvents[i])) + ",");
                }
                else
                {
                    streamWriter.WriteLine("\t\t\t" + NormalizePath(AssetDatabase.GUIDToAssetPath(soundEvents[i])));
                }
            }
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine();

            // Paths
            streamWriter.WriteLine("\t\tstring[] paths = new[]");
            streamWriter.WriteLine("\t\t{");
            for (int i = 0; i < soundEvents.Length; i++)
            {
                if (i < soundEvents.Length - 1)
                {
                    streamWriter.WriteLine("\t\t\t\"" + AssetDatabase.GUIDToAssetPath(soundEvents[i]) + "\",");
                }
                else
                {
                    streamWriter.WriteLine("\t\t\t\"" + AssetDatabase.GUIDToAssetPath(soundEvents[i]) + "\"");
                }
            }
            streamWriter.WriteLine("\t\t};");
            streamWriter.WriteLine();


            // Dictionnary
            streamWriter.WriteLine("\t\tpublic Dictionary<" + settings.EnumName + ", AudioEvent> " + settings.DictionaryName + " = new Dictionary<" + settings.EnumName + ", AudioEvent>();");
            streamWriter.WriteLine();

            // Constructor
            streamWriter.WriteLine("\t\tpublic " + settings.ClassName + "()");
            streamWriter.WriteLine("\t\t{");
            for (int i = 0; i < soundEvents.Length; i++)
            {
                streamWriter.WriteLine("\t\t\t" + settings.DictionaryName + ".Add(" + settings.EnumName + "." + NormalizePath(AssetDatabase.GUIDToAssetPath(soundEvents[i])) + ",  AssetDatabase.LoadAssetAtPath<AudioEvent>(paths[" + i + "]));");
            }
            streamWriter.WriteLine("\t\t}");
            streamWriter.WriteLine("\t}");

            // End 
            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }
}
#endif
