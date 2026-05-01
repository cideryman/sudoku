using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

public static class Program
{
    public static void Main()
    {
        string appDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        if (String.IsNullOrEmpty(appDirectory))
        {
            appDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }

        string htmlPath = Path.Combine(appDirectory, "index.html");

        if (!File.Exists(htmlPath))
        {
            ShowMessage(
                "Cannot find index.html.\n\nPlace Sudoku.exe and index.html in the same folder, then run Sudoku.exe again.",
                "Sudoku");
            return;
        }

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = htmlPath;
            startInfo.UseShellExecute = true;
            Process.Start(startInfo);
        }
        catch (Exception error)
        {
            ShowMessage("Cannot open the browser.\n\n" + error.Message, "Sudoku");
        }
    }

    private static void ShowMessage(string text, string caption)
    {
        MessageBox(IntPtr.Zero, text, caption, 0x00000010);
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBox(IntPtr hWnd, string text, string caption, uint type);
}
