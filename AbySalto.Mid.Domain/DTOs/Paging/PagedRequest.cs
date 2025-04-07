namespace AbySalto.Mid.Domain.DTOs.Paging
{
    /// <summary>
    /// Represents a paged request with arbitary type primary query. Subclass for additional data.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PagedRequest<T>
    {
        public PagedRequest()
        {
        }

        public PagedRequest(int pageSize, int page)
        {
            PageSize = pageSize;
            Page = page;
        }

        public string? Includes { get; set; } = string.Empty;

        public int PageSize { get; set; }

        public int Page { get; set; }

        public long? FromId { get; set; }

        public DateTime? UpdatedAfter { get; set; }

        public string OrderByKey { get; set; }

        public bool IsDescending { get; set; } = false;

        public bool IsFullSize { get; set; } = false;

        public T? Query { get; set; }

        // json string because Swashbuckle does not support Dictionary in FromQuery yet
        public string? Filter { get; set; }

        private void EnsureValidPagination()
        {
            if (PageSize <= 0)
            {
                PageSize = 10;
            }

            if (PageSize > 10000)
            {
                PageSize = 10000;
            }

            if (Page <= 0)
            {
                Page = 1;
            }

            if (Page > 100000)
            {
                Page = 100000;
            }
        }

        public int ItemsToSkip()
        {
            EnsureValidPagination();
            return (Page - 1) * PageSize;
        }
    }
}
