using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyForms.Data
{
    public interface IDataRepository
    {
        IEnumerable<dynamic> GetFormsNumber();
    }
}
