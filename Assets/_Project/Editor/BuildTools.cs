/**
 * BuildTools.cs
 * Created by: Joao Borks
 * Created on: 30/14/17 (dd/mm/yy)
 */

using UnityEditor;
using UnityEngine;
using Process = System.Diagnostics.Process;

public static class BuildTools
{
    [MenuItem("Tools/Run Server")]
    public static void RunServerMenu()
    {
        Process p = new Process();
        p.StartInfo.FileName = GetFilePath("serverPath");
        p.StartInfo.Arguments = "servermode";
        p.Start();
    }

    [MenuItem("Tools/Run Game")]
    public static void RunGameMenu()
    {
        Process p = new Process();
        p.StartInfo.FileName = GetFilePath("gamePath");
        p.StartInfo.Arguments = "gamemode";
        p.Start();
    }

    [MenuItem("Tools/Quick Build")]
    public static void QuickBuildMenu()
    {
        BuildPlayerOptions opts = new BuildPlayerOptions
        {
            options = BuildOptions.Development,
            locationPathName = GetSaveFile("buildPath"),
            target = BuildTarget.StandaloneWindows
        };
        BuildPipeline.BuildPlayer(opts);
    }

    [MenuItem("Tools/Clear Editor Prefs", false, 22)]
    public static void ClearPrefsMenu()
    {
        EditorPrefs.DeleteKey("gamePath");
        EditorPrefs.DeleteKey("serverPath");
        EditorPrefs.DeleteKey("buildPath");
    }

    public static string GetFilePath(string itemName)
    {
        var path = EditorPrefs.GetString(itemName);
        if (string.IsNullOrEmpty(path))
            path = EditorUtility.OpenFilePanel("Select game executable", "C:/Users/Joao Borks/Desktop", "exe");
        if (string.IsNullOrEmpty(path))
            throw new System.Exception("Operation cancelled");
        else
            EditorPrefs.SetString(itemName, path);
        return path;
    }

    public static string GetSaveFile(string itemName)
    {
        var path = EditorPrefs.GetString(itemName);
        if (string.IsNullOrEmpty(path))
            path = EditorUtility.SaveFilePanel("Select folder to save", "C:/Users/Joao Borks/Desktop", "newTest", "exe");
        if (string.IsNullOrEmpty(path))
            throw new System.Exception("Operation cancelled");
        else
            EditorPrefs.SetString(itemName, path);
        return path;
    }
}