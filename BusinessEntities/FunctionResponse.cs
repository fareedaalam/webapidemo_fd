using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
  public  class FunctionResponse
    {
        public StatusType Status { get; set; }
        public string Message { get; set; }
        public List<object> Data { get; set; }

        public FunctionResponse()
        {
            Status = StatusType.ERROR;
            Message = "";
            Data = new List<object>();
        }

        public enum StatusType
        {
            SUCCESS,
            ERROR,
            WARNING,
            NO_RECORD,
            CRITICAL_ERROR,
            UNAUTHORIZED,
            DUPLICATE
        }
       
    }
}
