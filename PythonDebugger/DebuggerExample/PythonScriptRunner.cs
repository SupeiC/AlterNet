using Alternet.Scripter.Debugger.Python;
using Alternet.Scripter.Python;
using Alternet.Syntax.Lexer;
using Alternet.Syntax.Parsers.Python;
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DebuggerExample
{
    public class PythonScriptRunner
    {
        public bool IsEnabled { get; private set; }
        public string PathToDLL { get; private set; }

        public PythonScriptRunner()
        {
            IsEnabled = CheckIfPythonInstalled();
            if (IsEnabled)
            {
                PythonInit();
            }
        }

        public string GetPythonExample()
        {
            return
@"
import string
import sys
import os

print(""Hello"")
x = 2
y = 3
print(""x + y = "" + str(x + y))
";
        }

        public bool CheckMethodName(string text, string methodName)
        {
            string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("def"))
                {
                    if (methodName == lines[i].Split(new string[] { "def" }, StringSplitOptions.None)[1].Split('(')[0].Trim())
                        return true;
                }
            }

            return false;
        }

        public PythonNETParser GetLexer()
        {
            var parser = new PythonNETParser();
            parser.CodeEnvironment = GetScriptRunner(string.Empty).CodeEnvironment;
            return parser;
        }

        public ScriptDebugger GetScriptDebugger(string text)
        {
            return new ScriptDebugger()
            {
                ScriptRun = GetScriptRunner(text),
            };
        }

        private ScriptRun GetScriptRunner(string text)
        {
            var scriptRunner = new ScriptRun();
            scriptRunner.ScriptSource.FromScriptCode(text);
            scriptRunner.ScriptHost.SkipSyntaxErrorCheck = true;
            scriptRunner.ScriptSource.ReferencedFrameworks = Alternet.Common.DotNet.Framework.Wpf;
            scriptRunner.ScriptSource.Imports.Add("System");
            scriptRunner.ScriptSource.Imports.Add("System.Diagnostics");

            return scriptRunner;
        }

        private bool CheckIfPythonInstalled()
        {
            try
            {
                var path = RunProcess("/C python -c \"import os, sys; print(os.path.dirname(sys.executable))\"");
                if (!string.IsNullOrEmpty(path))
                {
                    path = Path.Combine(path, new DirectoryInfo(path).Name.ToLower() + ".dll");
                    if (File.Exists(path))
                    {
                        PathToDLL = path;
                        return true;
                    }
                }

                path = RunProcess("/C py -0p");
                if (!string.IsNullOrEmpty(path))
                {
                    path = path.Substring(path.IndexOf(":") - 1, (path.IndexOf(".exe") + 4) - (path.IndexOf(":") - 1));
                    path = Path.GetDirectoryName(path);
                    path = Path.Combine(path, new DirectoryInfo(path).Name.ToLower() + ".dll");
                    if (File.Exists(path))
                    {
                        PathToDLL = path;
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Trace.WriteLine("CheckIfPythonInstalled, ex: " + ex.Message);
                return false;
            }
        }

        private void PythonInit()
        {
            if (PythonEngine.IsInitialized)
            {
                ScriptEngine.ShutDownRuntime();
                PythonEngine.Shutdown();
            }

            Runtime.PythonDLL = PathToDLL;
            PythonEngine.Initialize();
            PythonEngine.BeginAllowThreads();
        }

        private string RunProcess(string arguments)
        {
            var proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.Arguments = arguments;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.CreateNoWindow = true;

            proc.Start();
            proc.WaitForExit();

            return proc.StandardOutput.ReadToEnd().Trim();
        }
    }
}
