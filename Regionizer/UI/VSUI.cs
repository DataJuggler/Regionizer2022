using DataJuggler.Regionizer.UI.Forms;
using Microsoft;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DataJuggler.Regionizer.UI
{
    public static class VSUI
    {
        public static void ShowBlazorComponentsForm()
        {
            ThreadHelper.JoinableTaskFactory.Run(async delegate
            {
                // Switch to VS UI thread
                await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

                // Get VS owner window from the global service provider
                IVsUIShell uiShell =
                    (IVsUIShell)ServiceProvider.GlobalProvider.GetService(typeof(SVsUIShell));
                Assumes.Present(uiShell);

                IntPtr ownerHwnd;
                uiShell.GetDialogOwnerHwnd(out ownerHwnd);

                Rectangle screenBounds = Screen.PrimaryScreen.WorkingArea;

                BlazorComponentsForm form = new BlazorComponentsForm();
                form.Width = 620;
                form.Height = 762;
                form.Location = new Point(screenBounds.Right - 620, screenBounds.Top + 80);
                form.ShowInTaskbar = false;
                form.StartPosition = FormStartPosition.Manual;

                // Modeless, owned by VS so activation/clicks behave
                form.Show(new Win32Window(ownerHwnd));
            });
        }
    }
}