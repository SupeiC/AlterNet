using Alternet.Scripter.Debugger;
using Alternet.Scripter.Debugger.Python;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DebuggerExample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool _isRunning, _isRunningStopped;
        int _logMessageCount;
        ExecutionPosition _executionPosition;
        PythonScriptRunner _pythonScriptRunner;

        public MainWindow()
        {
            InitializeComponent();

            Trace.WriteLine("Start");
            test();
            Trace.WriteLine("End");

            _pythonScriptRunner = new PythonScriptRunner();
            if (!_pythonScriptRunner.IsEnabled)
            {
                MessageBox.Show("Python is not installed", "Wraning", MessageBoxButton.OK);
                Environment.Exit(0);
            }

            SetRunning(false);
            SetRunningStopped(false);

            textEditor.Text = _pythonScriptRunner.GetPythonExample4();
            textEditor.Lexer = _pythonScriptRunner.GetLexer();

            debugEditor.FileName = "sample.py";
            debugEditor.SingleEditorDebuggingUI = true;
            debugEditor.Text = _pythonScriptRunner.GetPythonExample4();
            debugEditor.Lexer = _pythonScriptRunner.GetLexer();
            debugEditor.Debugger = RenewDebugBreakPoints(null);
            
        }

        private void test()
        {
            for (int i = 0; i < 3; i++)
            {
                var output = i * 2;
                Trace.WriteLine("output: " + output);
            }
        }

        #region Toolbar

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SetRunning(true);
                SetRunningStopped(false);

                debugEditor.Debugger = _pythonScriptRunner.GetScriptDebugger(textEditor.Text);

                // work
                debugEditor.Debugger.ScriptRun.Run();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Run ex: " + ex.Message + "\nStackTrace: " + ex.StackTrace, "Error", MessageBoxButton.OK);
            }
        }

        private void btnRunDebug_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_isRunning)
                {
                    debugEditor.Debugger.Continue();
                }
                else
                {
                    _logMessageCount = 0;
                    SetRunning(true);
                    SetRunningStopped(false);

                    debugEditor.Debugger = RenewDebugBreakPoints(debugEditor.Debugger.Breakpoints.Breakpoints.ToArray());

                    // not work
                    debugEditor.Debugger.StartDebugging(new StartDebuggingOptions());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("RunDebug ex: " + ex.Message + "\nStackTrace: " + ex.StackTrace, "Error", MessageBoxButton.OK);
            }
        }

        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await debugEditor.Debugger.StopDebuggingAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("DebugStop ex: " + ex.Message + "\nStackTrace: " + ex.StackTrace, "Error", MessageBoxButton.OK);
            }
        }

        private void btnStepInto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                debugEditor.Debugger.StepInto();
            }
            catch (Exception ex)
            {
                MessageBox.Show("StepOver ex: " + ex.Message + "\nStackTrace: " + ex.StackTrace, "Error", MessageBoxButton.OK);
            }
        }

        private void btnStepOver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                debugEditor.Debugger.StepOver();
            }
            catch (Exception ex)
            {
                MessageBox.Show("StepOver ex: " + ex.Message + "\nStackTrace: " + ex.StackTrace, "Error", MessageBoxButton.OK);
            }
        }

        private void btnStepOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                debugEditor.Debugger.StepOut();
            }
            catch (Exception ex)
            {
                MessageBox.Show("StepOver ex: " + ex.Message + "\nStackTrace: " + ex.StackTrace, "Error", MessageBoxButton.OK);
            }
        }

        private void SetRunning(bool running)
        {
            _isRunning = running;
        }

        private void SetRunningStopped(bool stopped)
        {
            _isRunningStopped = stopped;
            Application.Current.Dispatcher.Invoke(() =>
            {
                btnStop.IsEnabled = stopped;
                btnStepOver.IsEnabled = stopped;
                btnStepInto.IsEnabled = stopped;
                btnStepOut.IsEnabled = stopped;
            });
        }

        #endregion

        #region Debugger

        private ScriptDebugger RenewDebugBreakPoints(Breakpoint[] points)
        {
            var debugger = _pythonScriptRunner.GetScriptDebugger(debugEditor.Text);
            debugger.StateChanged += Debugger_StateChanged;
            debugger.ExecutionStopped += Debugger_ExecutionStopped;
            debugger.DebuggingStopped += Debugger_DebuggingStopped;
            debugger.LogMessageReceived += Debugger_LogMessageReceived;

            foreach (var point in points ?? new Breakpoint[] { })
                debugger.Breakpoints.AddBreakpoint(point.FilePath, point.LineNumber);

            return debugger;
        }

        private void Debugger_LogMessageReceived(object sender, LogMessageReceivedEventArgs e)
        {
            _logMessageCount++;
            if (_logMessageCount % 2 == 0)
                return;

            var message = e.Message.Substring(0, e.Message.Length - 1);
            Trace.WriteLine("[Debugger_LogMessageReceived]" + message);
            MessageBox.Show(message, "LogMessageReceived", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Debugger_DebuggingStopped(object sender, DebuggingStoppedEventArgs e)
        {
            Trace.WriteLine("[Debugger_DebuggingStopped]" + e.ToString());
            SetRunning(false);
            SetRunningStopped(false);
            ClearExecutionPosition();
        }

        private void Debugger_ExecutionStopped(object sender, ExecutionStoppedEventArgs e)
        {
            Trace.WriteLine($"[Debugger_ExecutionStopped] {e.StopReason}/{e.Position.Line}/{e.Position.File}");
            switch (e.StopReason)
            {
                case ExecutionStopReason.BreakpointHit:
                    SetRunningStopped(true);
                    _executionPosition = e.Position;
                    Application.Current.Dispatcher.Invoke(() => debugEditor.ExecutionStopped(_executionPosition));
                    break;
                case ExecutionStopReason.Exception:
                case ExecutionStopReason.UnhandledException:
                    Trace.WriteLine($"\t{e.Exception.Message}");
                    break;
            }
        }

        private void Debugger_StateChanged(object sender, DebuggerStateChangedEventArgs e)
        {
            Trace.WriteLine($"[Debugger_StateChanged] old/new state: {e.OldState}/{e.NewState}");
        }

        private void ClearExecutionPosition()
        {
            if (_executionPosition != null)
            {
                debugEditor.ClearDebugStyles(_executionPosition);
                _executionPosition = null;
            }
        }

        #endregion
    }
}
