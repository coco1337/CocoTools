using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CocoTools
{
  public sealed class CocoPatch : EditorWindow
  {
    private GameObject oldFile;
    private Object hdiff;
    private string outputFileName = "";

    private CocoLog cocoLogWindow;

    private const string OUTPUT_EXTENSION = ".fbx";

    //private bool isForceOverwrite = true;
    private bool isForceOverwriteOriginal = false;

    [MenuItem("coco/Patch Tool")]
    private static void Init()
    {
      if (!(GetWindow(typeof(CocoPatch), false, "Patch Tool") is CocoPatch window)) return;
      window.Show();
      window.InitLogWindow();
    }

    private void InitLogWindow() => this.cocoLogWindow = new CocoLog();

    private void OnGUI()
    {
      GUILayoutOption[] defaultLayoutOption = { GUILayout.Width(position.width) };

      this.oldFile =
        EditorGUILayout.ObjectField("Original File (FBX)", this.oldFile, typeof(GameObject), false, default) as
          GameObject;
      this.hdiff =
        EditorGUILayout.ObjectField("Diff File (HDIFF)", this.hdiff, typeof(Object), false, default);

      GUILayout.Space(10);

      this.isForceOverwriteOriginal = EditorGUILayout.Toggle("Overwrite Original FBX", this.isForceOverwriteOriginal);

      GUILayout.Space(10);

      // this.command = EditorGUILayout.TextField("Patch command", this.command);
      GUI.enabled = !this.isForceOverwriteOriginal;
      this.outputFileName = EditorGUILayout.TextField("Output File Name (FBX)", this.outputFileName);

      GUI.enabled = ParameterCheck();
      if (GUILayout.Button("Start Patch!"))
      {
        ExecuteProcess();
      }

      GUI.enabled = true;

      var toolDescStyle = EditorStyles.textArea;
      toolDescStyle.richText = true;
      toolDescStyle.normal.textColor = new Color { r = .808f, g = 1, b = 1, a = 1 };
      toolDescStyle.alignment = TextAnchor.MiddleCenter;

      GUILayout.Label("Patch Tool - Please add the Original FBX and the HDIFF file to create the patched FBX.",
        toolDescStyle);

      GUILayout.Space(20);

      if (this.cocoLogWindow is null) this.cocoLogWindow = new CocoLog();
      this.cocoLogWindow.DrawLogWindow();
    }

    private bool ParameterCheck()
    {
      if (this.oldFile is null) return false;
      if (this.hdiff is null) return false;
      return this.isForceOverwriteOriginal ||
             !string.IsNullOrEmpty(this.outputFileName) && !this.outputFileName.Equals(OUTPUT_EXTENSION);
    }

    private void ExecuteProcess()
    {
      if (!this.isForceOverwriteOriginal && !this.outputFileName.EndsWith(OUTPUT_EXTENSION))
        this.outputFileName += OUTPUT_EXTENSION;
      var oldModelPath = Path.Combine(Directory.GetCurrentDirectory(), AssetDatabase.GetAssetPath(this.oldFile));
      var hdiffPath = Path.Combine(Directory.GetCurrentDirectory(), AssetDatabase.GetAssetPath(this.hdiff));
      var outputPath = this.isForceOverwriteOriginal
        ? Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(hdiffPath),
          $"temp_{(long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds}")
        : Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(hdiffPath),
          this.outputFileName);

      this.cocoLogWindow.Clear();

      HDiffPatchExporter.RegisterDelegateHpatchz((str) => this.cocoLogWindow.AddLog(LogType.LOG, str));
      HDiffPatchExporter.RegisterErrorDelegateHpatchz((str) => this.cocoLogWindow.AddLog(LogType.ERROR, str));

      //var options = this.command.Split(' ');
      var optionList = new List<string>();
      //if (this.isForceOverwrite) optionList.Add("-f");
      //if (this.isForceOverwriteOriginal) optionList.Add("-m");
      var error = (THPatchResult)HDiffPatchExporter.hpatch_unity(optionList.Count, optionList.ToArray(),
        oldModelPath, hdiffPath, outputPath);

      if (this.isForceOverwriteOriginal)
      {
        CocoUtils.ForceOverwrite(outputPath, oldModelPath);
        outputPath = oldModelPath;
      }

      if (error == THPatchResult.HPATCH_SUCCESS)
      {
        if (!File.Exists(outputPath)) return;
        this.cocoLogWindow.AddLog(LogType.SUCCESS, $"Successfully created patched file. {outputPath}");
        AssetDatabase.Refresh();
      }
      else
      {
        this.cocoLogWindow.AddLog(LogType.ERROR, $"Failed to create patched file : {error}");
      }
    }
  }

  public static partial class HDiffPatchExporter
  {
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HPatchzStringOutput(string str);

    [DllImport("hpatchz", EntryPoint = "RegisterDelegate")]
    public static extern void RegisterDelegateHpatchz(HPatchzStringOutput del);

    [DllImport("hpatchz", EntryPoint = "RegisterErrorDelegate")]
    public static extern void RegisterErrorDelegateHpatchz(HPatchzStringOutput del);

    [DllImport("hpatchz")]
    public static extern int hpatch_unity(int optionCount, string[] options, string oldPath,
      string diffFileName,
      string outNewPath);
  }

  public enum THPatchResult : int
  {
    HPATCH_SUCCESS = 0,
    HPATCH_OPTIONS_ERROR = 1,
    HPATCH_OPENREAD_ERROR,
    HPATCH_OPENWRITE_ERROR,
    HPATCH_FILEREAD_ERROR,
    HPATCH_FILEWRITE_ERROR, // 5 //see 24
    HPATCH_FILEDATA_ERROR,
    HPATCH_FILECLOSE_ERROR,
    HPATCH_MEM_ERROR, //see 22
    HPATCH_HDIFFINFO_ERROR,
    HPATCH_COMPRESSTYPE_ERROR, // 10
    HPATCH_HPATCH_ERROR,
    HPATCH_PATHTYPE_ERROR, //adding begin v3.0
    HPATCH_TEMPPATH_ERROR,
    HPATCH_DELETEPATH_ERROR,
    HPATCH_RENAMEPATH_ERROR, // 15
    HPATCH_SPATCH_ERROR,
    HPATCH_BSPATCH_ERROR,
    HPATCH_VCPATCH_ERROR,

    HPATCH_DECOMPRESSER_OPEN_ERROR = 20,
    HPATCH_DECOMPRESSER_CLOSE_ERROR,
    HPATCH_DECOMPRESSER_MEM_ERROR,
    HPATCH_DECOMPRESSER_DECOMPRESS_ERROR,
    HPATCH_FILEWRITE_NO_SPACE_ERROR,
  }
}