using System;
using System.Windows.Forms;

namespace Sales_user
{
    public static class CreateFormHelper
    {
        public static void WireCancel(Button cancelButton, Form form)
        {
            cancelButton.Click += (s, e) => form.Close();
        }

        public static void ShowSaveResult(Form form, int savedCount, string entityName)
        {
            if (savedCount > 0)
            {
                MessageBox.Show($"{savedCount} {entityName} record(s) saved.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                form.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("No valid rows to save. Fill at least one complete row.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
