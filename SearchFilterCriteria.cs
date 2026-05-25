using System;

namespace Sales_user
{
    public class SearchFilterCriteria
    {
        public string Keyword { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Category { get; set; }
        public string StyleNumber { get; set; }
        public string Color { get; set; }
        public string Unit { get; set; }
        public string Size { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? Status { get; set; }

        public bool HasAnyFilter =>
            !string.IsNullOrWhiteSpace(Keyword) ||
            !string.IsNullOrWhiteSpace(Name) ||
            !string.IsNullOrWhiteSpace(Phone) ||
            !string.IsNullOrWhiteSpace(Email) ||
            !string.IsNullOrWhiteSpace(Category) ||
            !string.IsNullOrWhiteSpace(StyleNumber) ||
            !string.IsNullOrWhiteSpace(Color) ||
            !string.IsNullOrWhiteSpace(Unit) ||
            !string.IsNullOrWhiteSpace(Size) ||
            FromDate.HasValue ||
            ToDate.HasValue ||
            Status.HasValue;
    }
}
