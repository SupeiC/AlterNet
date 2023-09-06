
using Python.Runtime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Sample
{
    public class ViewModel : INotifyPropertyChanged
    {
        public Alternet.Scripter.Debugger.Python.ScriptDebugger Debugger { get; private set; }
        public Alternet.Syntax.Parsers.Python.PythonNETParser Lexer { get; private set; }
        public CustomTextWriter Writer { get; private set; }

        public ViewModel()
        {
            if (!CheckIfPythonInstalled())
                throw new Exception("Python is not installed!");
            if (!SetPythonPath())
                throw new Exception("Cannot set python path!");

            Debugger = new Alternet.Scripter.Debugger.Python.ScriptDebugger();
            Debugger.ScriptRun = new Alternet.Scripter.Python.ScriptRun();
            Debugger.ScriptRun.ScriptSource.ReferencedFrameworks = Alternet.Common.DotNet.Framework.Wpf;

            Lexer = new Alternet.Syntax.Parsers.Python.PythonNETParser();
            Lexer.CodeEnvironment = Debugger.ScriptRun.CodeEnvironment;

            Writer = new CustomTextWriter();
        }

        public void Execute(bool debugMode, Action<string> action, string text)
        {
            Writer.SetAction(action);
            using (Py.GIL())
            {
                dynamic sys = Py.Import("sys");
                sys.stdout = Writer;
                sys.stderr = Writer;
            }

            Debugger.ScriptRun.ScriptSource.FromScriptCode(text);
            if (!Debugger.ScriptRun.Compile())
                throw new Exception(string.Join("\r\n", Debugger.ScriptRun.ScriptHost.CompilerErrors.Select(x => x.ToString()).ToArray()));

            var methodName = "ex1";
            var methodArgs = new object[] { 3.5, 1.5, action };
            if (!debugMode)
            {
                Debugger.ScriptRun.RunFunction(methodName, methodArgs);
            }
            else
            {
                Debugger.StartDebugging(new Alternet.Scripter.Debugger.StartDebuggingOptions()
                {
                    MethodName = methodName,
                    MethodArgs = methodArgs,
                });
            }
        }

        public bool CheckIfPythonInstalled()
        {
            try
            {
                var path = RunProcess("/C python -c \"import os, sys; print(os.path.dirname(sys.executable))\"");
                path = Path.Combine(path, new DirectoryInfo(path).Name.ToLower() + ".dll");
                if (File.Exists(path))
                {
                    Runtime.PythonDLL = path;
                    return true;
                }

                path = RunProcess("/C py -0p");
                path = path.Substring(path.IndexOf(":") - 1, (path.IndexOf(".exe") + 4) - (path.IndexOf(":") - 1));
                path = Path.GetDirectoryName(path);
                path = Path.Combine(path, new DirectoryInfo(path).Name.ToLower() + ".dll");
                if (File.Exists(path))
                {
                    Runtime.PythonDLL = path;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SetPythonPath()
        {
            try
            {
                var pythonPath = RunProcess($"/C python -c \"import sys; print('{System.IO.Path.PathSeparator}'.join(sys.path))\"");
                Environment.SetEnvironmentVariable("PYTHONPATH", pythonPath, EnvironmentVariableTarget.Process);
                PythonEngine.PythonPath = Environment.GetEnvironmentVariable("PYTHONPATH", EnvironmentVariableTarget.Process);
                PythonEngine.Initialize();
                PythonEngine.BeginAllowThreads();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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

        /// <summary>
        /// Property changed event
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Trigger when property changed
        /// </summary>
        /// <param name="propertyName">Caller member name</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
