using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace CocoTools
{
  public static class CocoUtils
  {
    public static void ForceOverwrite(string assetPath, string originalAssetPath)
    {
      FileUtil.ReplaceFile(assetPath, originalAssetPath);

      if (assetPath.StartsWith(Directory.GetCurrentDirectory()))
        assetPath = assetPath.Substring(Directory.GetCurrentDirectory().Length + 1);
      if (originalAssetPath.StartsWith(Directory.GetCurrentDirectory()))
        originalAssetPath = originalAssetPath.Substring(Directory.GetCurrentDirectory().Length + 1);
      AssetDatabase.DeleteAsset(assetPath);
      AssetDatabase.ImportAsset(originalAssetPath);
    }
  }

  #region Log

  public enum LogType
  {
    LOG,
    WARNING,
    ERROR,
    SUCCESS,
  };

  public sealed class CocoLog
  {
    private List<(LogType LogType, string Log)> logList = new List<(LogType LogType, string Log)>();
    private Vector2 scrollPosition = new Vector2();

    public void Clear() => this.logList.Clear();
    public void AddLog(LogType type, string log) => this.logList.Add((type, log));

    public void DrawLogWindow()
    {
      EditorGUILayout.BeginVertical(GUI.skin.box);
      var style = new GUIStyle(GUI.skin.label) { alignment = TextAnchor.MiddleCenter };
      EditorGUILayout.LabelField("LOG", style);

      using (new EditorGUILayout.HorizontalScope())
      {
        if (GUILayout.Button("Copy"))
        {
          if (this.logList.Count > 0)
          {
            EditorGUIUtility.systemCopyBuffer = this.logList.Aggregate("", (acc, element) => acc + element.Log);
            EditorUtility.DisplayDialog("Copy logs", "Successfully copied to clipboard!", "Ok");
          }
        }

        if (GUILayout.Button("Clear"))
          Clear();
      }

      this.scrollPosition = EditorGUILayout.BeginScrollView(this.scrollPosition, GUI.skin.box);

      foreach (var logElement in this.logList)
      {
        var textStyle = new GUIStyle()
        {
          wordWrap = true,
          richText = true,
        };

        switch (logElement.LogType)
        {
          case LogType.ERROR:
            textStyle.normal.textColor = Color.red;
            EditorGUILayout.LabelField($"[ERROR] {logElement.Log}", textStyle);
            break;
          case LogType.SUCCESS:
            textStyle.normal.textColor = new Color { r = .808f, g = 1, b = 1, a = 1 };
            EditorGUILayout.LabelField($"[SUCCESS] {logElement.Log}", textStyle);
            break;
          default:
            textStyle.normal.textColor = Color.gray;
            EditorGUILayout.LabelField($"{logElement.Log}", textStyle);
            break;
        }
      }

      EditorGUILayout.EndScrollView();
      EditorGUILayout.EndVertical();
    }
  }

  #endregion
}