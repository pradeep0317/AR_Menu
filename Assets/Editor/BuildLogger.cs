using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System.IO;

public class BuildLogger : IPreprocessBuildWithReport, IPostprocessBuildWithReport
{
    private static string logFilePath;
    private static StreamWriter logWriter;

    public int callbackOrder => 0;

    public void OnPreprocessBuild(BuildReport report)
    {
        logFilePath = Path.Combine(Application.dataPath, "../BuildConsoleLog.txt");
        logWriter = new StreamWriter(logFilePath, false);
        logWriter.AutoFlush = true;

        Application.logMessageReceived += HandleLog;

        Debug.Log(" Build started...");
        Debug.Log($"Saving build logs to: {logFilePath}");
    }

    public void OnPostprocessBuild(BuildReport report)
    {
        Debug.Log(" Build finished.");
        Application.logMessageReceived -= HandleLog;

        logWriter?.Flush();
        logWriter?.Close();
        logWriter = null;
    }

    private static void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (logWriter == null) return;

        string logEntry = $"[{type}] {logString}";

        if (type == LogType.Warning || type == LogType.Error || type == LogType.Exception)
        {
            logEntry += $"\nStack Trace:\n{stackTrace}";
        }

        logWriter.WriteLine(logEntry);
    }
}
