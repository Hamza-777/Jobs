namespace JobsAPI.Models
{
    public class JobParams
    {
        
        public string? sort { get; set; }

        public string? search { get; set; }
        public int? categoryId { get; set; }
        public int? cityId { get; set; }
        public int? stateId { get; set; }
        private const int maxPageSize = 50;
        public int pageNumber { get; set; } = 1;
        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize ? maxPageSize : value);
            }
        }
    }
}
