/**
 * BuildTools.cs
 * Created by: Joao Borks
 * Created on: 30/14/17 (dd/mm/yy)
 */

using UnityEditor;
using System.Diagnostics;

public static class BuildTools
{
    [MenuItem("Tools/Run Server")]
    public static void RunServerMenu()
    {
        Process p = new Process();
        p.StartInfo.FileName = GetPath("serverPath");
        p.StartInfo.Arguments = "servermode";
        p.Start();
    }

    [MenuItem("Tools/Run Game")]
    public static void RunGameMenu()
    {
        Process p = new Process();
        p.StartInfo.FileName = GetPath("gamePath");
        p.Start();
    }

    public static string GetPath(string itemName)
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
}