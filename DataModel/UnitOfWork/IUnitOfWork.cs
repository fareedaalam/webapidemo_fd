using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.UnitOfWork
{
    public interface IUnitOfWork
    {
        // Save method.
        Int32 Save();
       // IEnumerable ExecuteReader(String StoreProcedurePName, SqlParameter[] parameters = null);
      //  string ExecuteReader(String StoreProcedurePName, SqlParameter[] parameters = null);
    }
}
