using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Sede_Checker.Abstract.Interfaces;

namespace Sede_Checker.Implementation.FormLogger
{
    internal class FormLogger : ILogger
    {
        public FormLogger(ref RichTextBox p, bool debug = true)
        {
            LogPlace = p;

            IsDebug = debug;

            LogCount = 0;

            ConsoleHistory = new List<string>();
        }

        private RichTextBox LogPlace { get; }

        private int LogCount { get; set; }

        public List<string> ConsoleHistory { get; }

        public bool IsDebug { get; set; }

        public void Error(string message)
        {
            PrintMessage($"[ERROR] - {message}\n", Color.Red);
        }

        public void Exception(Exception exc)
        {
            var message = GetExceptionMessage(exc);
            PrintMessage($"[ERROR] - {message}\n", Color.Red);
        }

        public void Info(string message)
        {
            PrintMessage($"[INFO] - {message}\n", Color.Green);
        }

        public void Warning(string message)
        {
            PrintMessage($"[WARN] - {message}\n", Color.Gold);
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
                sb.Append($"[INNER EXCEPTION]\r\n");
                sb.Append(iexc);
            }

            return sb.ToString();
        }

        private void SetDataToRichTextBox(RichTextBox richTextBox, string message, Color color)
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
                if (!IsDebug) return;

                if (LogPlace.InvokeRequired)
                    LogPlace.Invoke(new MethodInvoker(() => { SetDataToRichTextBox(LogPlace, message, color); }));
                
                else SetDataToRichTextBox(LogPlace, message, color);

                ConsoleHistory.Add(message);

                LogCount++;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}