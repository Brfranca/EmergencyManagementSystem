using EmergencyManagementSystem.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergencyManagementSystem.Service.Interfaces
{
    public interface IRestBase<TModel> where TModel : class
    {
        Result Register(TModel model);
        Result Delete(TModel model);
        Result<TModel> Find(IFilter model);
        Result Update(TModel model);
    }
}
