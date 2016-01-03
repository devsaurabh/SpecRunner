using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace NSpecRunner.GUI.Framework
{
    public static class Extentions
    {
        [DebuggerNonUserCode]
        public static void InvokeAction(this Form form, Action action)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(action);
            }
            else
            {
                action();
            }
        }

        [DebuggerNonUserCode]
        public static void SetTitle(this Form form, string title = null)
        {
            if (!string.IsNullOrWhiteSpace(title)) form.Text = string.Format("{0} - {1}", title, ApplicationConstants.APPLICATION_NAME);
            else form.Text = ApplicationConstants.APPLICATION_NAME;
            
        }

        [DebuggerNonUserCode]
        public static void AppendText(this RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        [DebuggerNonUserCode]
        public static void AppendLine(this RichTextBox box, int space, string text, Color color)
        {
            box.AppendText("\n");
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;

            box.AppendText(text.PadLeft(space + text.Length, ' '));
            box.SelectionColor = box.ForeColor;
        }
    }
}
