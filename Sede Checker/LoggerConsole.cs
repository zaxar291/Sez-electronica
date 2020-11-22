using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Sede_Checker.Abstract.Interfaces;

namespace Sede_Checker
{
    public partial class LoggerConsole : Form, ILogger
    {
        public LoggerConsole()
        {
            InitializeComponent();
            ConsoleHistory = new List<string>();
        }

        public List<string> ConsoleHistory { get; }

        public bool IsDebug { get; set; }

        public void Error(string message)
        {
            PrintMessage($"[ERROR] - {message}\n", Color.Crimson);
        }

        public void Exception(Exception exc)
        {
            var message = GetExceptionMessage(exc);
            PrintMessage($"[ERROR] - {message}\n", Color.Firebrick);
        }

        public void Info(string message)
        {
            PrintMessage($"[INFO] - {message}\n", Color.ForestGreen);
        }

        public void Warning(string message)
        {
            
            PrintMessage($"[WARN] - {message}\n", Color.Gold);
        }

        private void SedeMainForm_Load(object sender, EventArgs e)
        {
            Info("Form loaded successfully, initialising adresses...");
            Info("All operations launched");
        }

        private void ContextMenuCopyToClickBoardButton_Click(object sender, EventArgs e)
        {
            var r = ConsoleHistory.Aggregate(string.Empty, (current, line) => current + $"{line}\n");
            Clipboard.SetText(r);
        }

        public bool ConsoleHaveLine(string line)
        {
            foreach (var l in ConsoleHistory.ToArray())
                if (l == line)
                    return true;
            return false;
        }

        private string GetExceptionMessage(Exception exc)
        {
            if (ReferenceEquals(exc, null)) return string.Empty;

            var sb = new StringBuilder();

            sb.Append($"[EXCEPTION MESSAGE] {exc.Message}");
            sb.Append($"[EXCEPTION STACK TRACE] {exc.StackTrace}");

            if (!ReferenceEquals(exc, exc.InnerException))
            {
                var iexc = GetExceptionMessage(exc.InnerException);
                sb.Append($"\r\n[INNER EXCEPTION]\r\n");
                sb.Append(iexc);
            }

            return sb.ToString();
        }

        private void SetDataToRichTextBox(string message, Color color)
        {
            if (ReferenceEquals(richTextBox, null)) return;

            var m = $"[{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}] {message}";

            richTextBox.SelectionStart = richTextBox.TextLength;
            richTextBox.SelectionLength = 0;
            richTextBox.SelectionColor = color;
            richTextBox.AppendText(m);

            richTextBox.SelectionStart = richTextBox.Text.Length;
            richTextBox.ScrollToCaret();
        }

        private void PrintMessage(string message, Color color)
        {
            try
            {
                //if (!IsDebug) return;

                if (richTextBox.InvokeRequired)
                    richTextBox.Invoke(new MethodInvoker(() => { SetDataToRichTextBox(message, color); }));

                else SetDataToRichTextBox(message, color);

                ConsoleHistory.Add(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}