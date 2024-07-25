#if UNITY_EDITOR && UNITY_IOS
using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
public class iOSBuild : MonoBehaviour
{
 [PostProcessBuild]
 public static void OnPostProcessBuild(BuildTarget target, string path)
 {
  var projectPath = PBXProject.GetPBXProjectPath(path);
  var project = new PBXProject();
  project.ReadFromString(File.ReadAllText(projectPath));
#if UNITY_2019_3_OR_NEWER
  var targetGuid = project.GetUnityFrameworkTargetGuid();
#else
  var targetGuid = project.TargetGuidByName(PBXProject.GetUnityTargetName());
#endif
  project.AddFrameworkToProject(targetGuid, "libz.tbd", false);
  project.SetBuildProperty(targetGuid, "ENABLE_BITCODE", "NO");
  File.WriteAllText(projectPath, project.WriteToString());
 }
}
#endif