using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public static class FormGridHelper
    {
        public static void BindReadOnly(DataGridView grid, DataTable data)
        {
            if (grid == null) return;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoGenerateColumns = true;
            grid.DataSource = data;
        }

        public static void SetupEditableInputGrid(DataGridView grid, params string[] columnHeaders)
        {
            if (grid == null) return;
            grid.Columns.Clear();
            grid.AutoGenerateColumns = false;
            foreach (string header in columnHeaders)
            {
                grid.Columns.Add(header, header);
            }
            grid.AllowUserToAddRows = true;
            grid.AllowUserToDeleteRows = true;
            grid.ReadOnly = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            if (grid.Rows.Count == 0)
            {
                grid.Rows.Add();
            }
        }

        public static long? GetSelectedId(DataGridView grid, params string[] columnNames)
        {
            if (grid?.CurrentRow == null) return null;
            foreach (string name in columnNames)
            {
                if (grid.Columns.Contains(name) && grid.CurrentRow.Cells[name].Value != null
                    && long.TryParse(grid.CurrentRow.Cells[name].Value.ToString(), out long id))
                {
                    return id;
                }
            }
            return null;
        }

        public static string GetCellString(DataGridViewRow row, string columnName)
        {
            if (row == null || !row.DataGridView.Columns.Contains(columnName)) return string.Empty;
            return row.Cells[columnName].Value?.ToString()?.Trim() ?? string.Empty;
        }

        public static bool TryParseDecimal(string text, out decimal value)
        {
            return decimal.TryParse(text, out value);
        }

        public static bool TryParseInt(string text, out int value)
        {
            return int.TryParse(text, out value);
        }
    }
}
