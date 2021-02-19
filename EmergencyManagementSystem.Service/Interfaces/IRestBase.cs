using EmergencyManagementSystem.Service.Models;
using System.Collections.Generic;
using X.PagedList;

namespace EmergencyManagementSystem.Service.Interfaces
{
    public interface IRestBase<TModel> where TModel : class
    {
        Result Register(TModel model);
        Result Delete(TModel model);
        Result<TModel> Find(IFilter model);
        Result Update(TModel model);
        PagedList<TModel> FindPaginated(IFilter filter);
        Result<List<TModel>> FindAll(IFilter emergencyFilter);
    }
}
