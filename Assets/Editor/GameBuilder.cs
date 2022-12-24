using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;

namespace Assets.Editor
{
    public class GameBuilder : MonoBehaviour
    {
        [MenuItem("Build/Build win64")]
        public static void PerformWin64Build()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = new[] { "Assets/Scenes/SampleScene.unity" };
            buildPlayerOptions.locationPathName = "build/win64/Match3.app";
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.options = BuildOptions.None;

            BuildReport buildReport = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary buildSummary = buildReport.summary;

            if (buildSummary.result == BuildResult.Succeeded)
                Debug.Log("Build successful: " + buildSummary.totalSize + " bytes");

            if (buildSummary.result == BuildResult.Failed)
                Debug.Log("Build failed.");
        }
    }
}