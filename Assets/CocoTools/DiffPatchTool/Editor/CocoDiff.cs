using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

namespace CocoTools
{
  public sealed class CocoDiff : EditorWindow
  {
    private GameObject oldFile;
    private GameObject targetFile;
    private string outputFileName;
    private bool isCustomDiffOption = false;
    private bool isForceOverwrite = true;

    private CocoLog cocoLogWindow;

    private const string OUTPUT_EXTENSION = ".hdiff";
    private const string DEFAULT_COMMAND = "-m-6 -SD -c-zstd-21-24 -d";

    private string command = "-m-6 -SD -c-zstd-21-24 -d";

    [MenuItem("coco/Diff tool")]
    private static void Init()
    {
      if (!(GetWindow(typeof(CocoDiff), false, "Diff tool") is CocoDiff window)) return;
      window.Show();
      window.InitLogWindow();
    }

    private void InitLogWindow() => this.cocoLogWindow = new CocoLog();

    private void OnGUI()
    {
      this.oldFile =
        EditorGUILayout.ObjectField("Original File (FBX)", this.oldFile, typeof(GameObject), false, default) as
          GameObject;
      this.targetFile =
        EditorGUILayout.ObjectField("Diff Target File (FBX)", this.targetFile, typeof(GameObject), false, default) as
          GameObject;

      GUILayout.Space(10);
      
      this.isForceOverwrite = EditorGUILayout.Toggle("Overwrite Output File", this.isForceOverwrite);
      this.isCustomDiffOption = EditorGUILayout.Toggle("Custom HDIFF Commands", this.isCustomDiffOption);
      
      if (this.isCustomDiffOption)
        this.command = EditorGUILayout.TextField("", this.command);

      GUILayout.Space(10);

      this.outputFileName = EditorGUILayout.TextField("Output File Name (HDIFF)", this.outputFileName);

      GUI.enabled = ParameterCheck();
      if (GUILayout.Button("Start Diff!"))
      {
        ExecuteProcess();
      }

      GUI.enabled = true;

      var toolDescStyle = EditorStyles.textArea;
      toolDescStyle.richText = true;
      toolDescStyle.normal.textColor = new Color { r = .808f, g = 1, b = 1, a = 1 };
      toolDescStyle.alignment = TextAnchor.MiddleCenter;
      GUILayout.Label("Diff Tool - Please add the Original FBX and New FBX to create the HDIFF file.",toolDescStyle);

      GUILayout.Space(20);
      
      if (this.cocoLogWindow is null) this.cocoLogWindow = new CocoLog();
      this.cocoLogWindow.DrawLogWindow();
    }

    private bool ParameterCheck()
    {
      if (this.oldFile is null) return false;
      if (this.targetFile is null) return false;
      return !string.IsNullOrEmpty(this.outputFileName) && !this.outputFileName.Equals(OUTPUT_EXTENSION);
    }

    private void ExecuteProcess()
    {
      if (!this.outputFileName.EndsWith(OUTPUT_EXTENSION)) this.outputFileName += OUTPUT_EXTENSION;
      var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), AssetDatabase.GetAssetPath(this.oldFile));
      var targetFilePath = Path.Combine(Directory.GetCurrentDirectory(), AssetDatabase.GetAssetPath(this.targetFile));
      var outputPath =  Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(targetFilePath), this.outputFileName);
      var tempFilePath = "";
      
      if (!this.isCustomDiffOption) this.command = DEFAULT_COMMAND;

      if (!this.isForceOverwrite)
      {
        if (File.Exists(outputPath))
        {
          this.cocoLogWindow.AddLog(LogType.ERROR, "Cannot overwrite output file");
          return;
        }
      }
      else
      {
        tempFilePath = Path.Combine(Directory.GetCurrentDirectory(), Path.GetDirectoryName(targetFilePath),
          $"temp_{(long)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds}");
      }   
      
      this.cocoLogWindow.Clear();

      HDiffPatchExporter.RegisterDelegateHdiffz((str) => this.cocoLogWindow.AddLog(LogType.LOG, str));
      HDiffPatchExporter.RegisterErrorDelegateHdiffz((str) => this.cocoLogWindow.AddLog(LogType.ERROR, str));
      
      var options = this.command.Split(' ').ToArray();
      var error = (THDiffResult)HDiffPatchExporter.hdiff_unity(oldFilePath, targetFilePath,
        this.isForceOverwrite ? tempFilePath : outputPath,
        options, options.Length);

      if (this.isForceOverwrite)
      {
        CocoUtils.ForceOverwrite(tempFilePath, outputPath);
      }
      
      if (error == THDiffResult.HDIFF_SUCCESS)
      {
        if (!File.Exists(outputPath)) return;
        this.cocoLogWindow.AddLog(LogType.SUCCESS, $"Successfully created diff file.\n{outputPath}");
        AssetDatabase.Refresh();
      }
      else
      {
        this.cocoLogWindow.AddLog(LogType.ERROR,$"Failed to create diff file : {error}");
      }
    }
  }

  public static partial class HDiffPatchExporter
  {
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HDiffzStringOutput(string str);
    
    [DllImport("hdiffz", EntryPoint = "RegisterDelegate")]
    public static extern void RegisterDelegateHdiffz(HDiffzStringOutput del);

    [DllImport("hdiffz", EntryPoint = "RegisterErrorDelegate")]
    public static extern void RegisterErrorDelegateHdiffz(HDiffzStringOutput del);

    [DllImport("hdiffz")]
    public static extern int hdiff_unity(string oldFileName, string newFileName, string outDiffFileName,
      string[] diffOptions, int diffOptionSize);
  }

  public enum THDiffResult : int
  {
    HDIFF_SUCCESS = 0,
    HDIFF_OPTIONS_ERROR,
    HDIFF_OPENREAD_ERROR,
    HDIFF_OPENWRITE_ERROR,
    HDIFF_FILECLOSE_ERROR,
    HDIFF_MEM_ERROR, // 5
    HDIFF_DIFF_ERROR,
    HDIFF_PATCH_ERROR,
    HDIFF_RESAVE_FILEREAD_ERROR,

    //HDIFF_RESAVE_OPENWRITE_ERROR = HDIFF_OPENWRITE_ERROR
    HDIFF_RESAVE_DIFFINFO_ERROR,
    HDIFF_RESAVE_COMPRESSTYPE_ERROR, // 10
    HDIFF_RESAVE_ERROR,
    HDIFF_RESAVE_CHECKSUMTYPE_ERROR,

    HDIFF_PATHTYPE_ERROR, //adding begin v3.0
    HDIFF_TEMPPATH_ERROR,
    HDIFF_DELETEPATH_ERROR, // 15
    HDIFF_RENAMEPATH_ERROR,

    DIRDIFF_DIFF_ERROR = 101,
    DIRDIFF_PATCH_ERROR,
    MANIFEST_CREATE_ERROR,
    MANIFEST_TEST_ERROR,
  }
}