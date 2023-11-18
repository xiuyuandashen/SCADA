using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Models
{
    public class DataResult<T>
    {
        public string Message { get; set; }

        public bool Status { get; set; }

        public T Result { get; set; }


        public DataResult<T> SUCCESS(string Message)
        {
            this.Status= true;
            this.Message= Message;
            return this;
        }

        public DataResult<T> SUCCESS(T data)
        {
            this.Status = true;
            this.Result= data;
            return this;
        }

        public DataResult<T> SUCCESS(string Message,T data)
        {
            this.Status = true;
            this.Message = Message;
            this.Result = data;
            return this;
        }

        public DataResult<T> ERROR(string Message)
        {
            this.Status = false;
            this.Message = Message;
            return this;
        }
    }

    public class DataResult: DataResult<string>
    {
        
    }
}
