using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class huhu : MonoBehaviour
{
    public void run() {
        Process process = new Process();
        
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.CreateNoWindow = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.Start();

        process.StandardInput.WriteLine("ipconfig");
        process.StandardInput.Flush();
        process.StandardInput.Close();

        string output = process.StandardOutput.ReadToEnd();
        UnityEngine.Debug.Log(output);
        // process.WaitForExit();

        System.Console.ReadKey(); 
    }
}
