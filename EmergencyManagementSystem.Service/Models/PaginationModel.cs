using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace EmergencyManagementSystem.Service.Models
{
    public class PaginationModel<T>
    {
        public DataPagination DataPagination { get; set; }
        public List<T> Models { get; set; }

        public PaginationModel(List<T> list, DataPagination dataPagination)
        {
            Models = list;
            DataPagination = dataPagination;
        }
    }

    public class DataPagination : IPagedList
    {
        public int PageCount { get; set; }

        public int TotalItemCount { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public bool IsFirstPage { get; set; }

        public bool IsLastPage { get; set; }

        public int FirstItemOnPage { get; set; }

        public int LastItemOnPage { get; set; }
    }
}
