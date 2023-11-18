using SCADA.Industrial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Service
{
    public interface IBaseService<TEntity> where TEntity : class
    {
        DataResult<List<TEntity>> GetAll();

        
    }
}
