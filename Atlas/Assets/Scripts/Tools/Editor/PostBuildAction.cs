#if UNITY_EDITOR
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace Tools.Editor
{
    public static class PostBuildAction
    {
        [PostProcessBuild]
        static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            string path = Path.GetDirectoryName(pathToBuiltProject) + "/" + Path.GetFileNameWithoutExtension(pathToBuiltProject)+"_Data";

            FileUtil.CopyFileOrDirectory(ConfigFileManager.fullConfigFilePath, path + ConfigFileManager.configFilePath);
        }

    }
}
#endif