using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace Sales_user
{
    public static class SearchQueryHelper
    {
        public static void AddLike(List<string> conditions, List<MySqlParameter> parameters,
            string columnExpression, string value, string paramName)
        {
            if (string.IsNullOrWhiteSpace(value)) return;
            conditions.Add($"{columnExpression} LIKE {paramName}");
            parameters.Add(new MySqlParameter(paramName, "%" + value.Trim() + "%"));
        }

        public static void AddDateFrom(List<string> conditions, List<MySqlParameter> parameters,
            string columnExpression, DateTime? fromDate, string paramName = "@fromDate")
        {
            if (!fromDate.HasValue) return;
            conditions.Add($"{columnExpression} >= {paramName}");
            parameters.Add(new MySqlParameter(paramName, fromDate.Value.Date));
        }

        public static void AddDateTo(List<string> conditions, List<MySqlParameter> parameters,
            string columnExpression, DateTime? toDate, string paramName = "@toDate")
        {
            if (!toDate.HasValue) return;
            conditions.Add($"{columnExpression} < {paramName}");
            parameters.Add(new MySqlParameter(paramName, toDate.Value.Date.AddDays(1)));
        }

        public static void AddStatus(List<string> conditions, List<MySqlParameter> parameters,
            string columnExpression, int? status, string paramName = "@status")
        {
            if (!status.HasValue) return;
            conditions.Add($"{columnExpression} = {paramName}");
            parameters.Add(new MySqlParameter(paramName, status.Value));
        }

        public static int? ParseStatusCombo(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return null;
            if (int.TryParse(text, out int status)) return status;
            string t = text.Trim().ToLowerInvariant();
            if (t.Contains("process") || t.Contains("draft") || t.Contains("pending")) return 0;
            if (t.Contains("complete") || t.Contains("finish") || t.Contains("approved")) return 1;
            if (t.Contains("reject") || t.Contains("cancel")) return 2;
            return null;
        }
    }
}
