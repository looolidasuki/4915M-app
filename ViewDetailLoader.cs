using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public static class ViewDetailLoader
    {
        public static long ResolveRecordId(long? passedId, DataTable list, string idColumnName, long fallbackId)
        {
            if (passedId.HasValue && passedId.Value > 0) return passedId.Value;
            if (list != null && list.Rows.Count > 0 && list.Columns.Contains(idColumnName))
                return Convert.ToInt64(list.Rows[0][idColumnName]);
            return fallbackId;
        }

        public static void ShowSavedMessage(bool ok, string entityName)
        {
            if (ok)
                MessageBox.Show($"{entityName} updated successfully.", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show($"Failed to update {entityName}.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
