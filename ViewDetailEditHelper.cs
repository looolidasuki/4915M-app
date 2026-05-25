using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Sales_user
{
    public class ViewDetailEditHelper
    {
        private readonly Control[] _fields;
        private readonly Button _btnEdit;
        private readonly Button _btnSave;
        private readonly Button _btnCancel;
        private readonly Func<bool> _saveAction;
        private readonly Action _reloadAction;
        private readonly Dictionary<Control, string> _snapshot = new Dictionary<Control, string>();
        private bool _isEditing;

        public bool IsEditing => _isEditing;

        public ViewDetailEditHelper(
            Control[] editableFields,
            Button btnEdit,
            Button btnSave,
            Button btnCancel,
            Func<bool> saveAction,
            Action reloadAction)
        {
            _fields = editableFields ?? new Control[0];
            _btnEdit = btnEdit;
            _btnSave = btnSave;
            _btnCancel = btnCancel;
            _saveAction = saveAction;
            _reloadAction = reloadAction;
        }

        public void Initialize()
        {
            TakeSnapshot();
            SetReadOnly(true);
            if (_btnSave != null) _btnSave.Enabled = false;

            if (_btnEdit != null)
                _btnEdit.Click += BtnEdit_Click;
            if (_btnSave != null)
                _btnSave.Click += BtnSave_Click;
            if (_btnCancel != null)
                _btnCancel.Click += BtnCancel_Click;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            TakeSnapshot();
            SetReadOnly(false);
            _isEditing = true;
            if (_btnSave != null) _btnSave.Enabled = true;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            RestoreSnapshot();
            SetReadOnly(true);
            _isEditing = false;
            if (_btnSave != null) _btnSave.Enabled = false;
            _reloadAction?.Invoke();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_saveAction != null && _saveAction())
            {
                TakeSnapshot();
                SetReadOnly(true);
                _isEditing = false;
                if (_btnSave != null) _btnSave.Enabled = false;
            }
        }

        private void TakeSnapshot()
        {
            _snapshot.Clear();
            foreach (Control c in _fields)
            {
                if (c is TextBox tb) _snapshot[c] = tb.Text;
                else if (c is ComboBox cb) _snapshot[c] = cb.Text;
                else if (c is DateTimePicker dtp) _snapshot[c] = dtp.Value.ToString("yyyy-MM-dd");
            }
        }

        private void RestoreSnapshot()
        {
            foreach (var kv in _snapshot)
            {
                if (kv.Key is TextBox tb) tb.Text = kv.Value;
                else if (kv.Key is ComboBox cb) cb.Text = kv.Value;
                else if (kv.Key is DateTimePicker dtp && DateTime.TryParse(kv.Value, out DateTime dt))
                    dtp.Value = dt;
            }
        }

        private void SetReadOnly(bool readOnly)
        {
            foreach (Control c in _fields)
            {
                if (c is TextBox tb) tb.ReadOnly = readOnly;
                else if (c is ComboBox cb) cb.Enabled = !readOnly;
                else if (c is DateTimePicker dtp) dtp.Enabled = !readOnly;
            }
        }
    }
}
