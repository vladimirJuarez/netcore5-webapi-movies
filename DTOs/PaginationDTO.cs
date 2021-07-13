namespace back_end.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; } = 1;
        private int _recordsPerPage = 10;
        private readonly int _maxNumberRecordsPerPage = 50;

        public int RecordsPerPage
        {
            get
            {
                return _recordsPerPage;
            }
            set
            {
                _recordsPerPage = (value > _maxNumberRecordsPerPage)
                    ? _maxNumberRecordsPerPage
                    : value;
            }
        }
    }
}