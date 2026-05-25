using System.Drawing;
using System.Windows.Forms;

namespace Sales_user
{
    public static class InputPrompt
    {
        public static string Show(string title, string prompt, string defaultValue = "")
        {
            using (Form form = new Form())
            using (TextBox textBox = new TextBox())
            using (Button ok = new Button())
            using (Button cancel = new Button())
            using (Label label = new Label())
            {
                form.Text = title;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterParent;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ClientSize = new Size(360, 110);

                label.Text = prompt;
                label.SetBounds(12, 12, 330, 20);
                textBox.SetBounds(12, 36, 330, 24);
                textBox.Text = defaultValue;
                ok.Text = "OK";
                ok.SetBounds(200, 72, 70, 28);
                ok.DialogResult = DialogResult.OK;
                cancel.Text = "Cancel";
                cancel.SetBounds(278, 72, 70, 28);
                cancel.DialogResult = DialogResult.Cancel;

                form.Controls.AddRange(new Control[] { label, textBox, ok, cancel });
                form.AcceptButton = ok;
                form.CancelButton = cancel;

                return form.ShowDialog() == DialogResult.OK ? textBox.Text.Trim() : null;
            }
        }
    }
}
