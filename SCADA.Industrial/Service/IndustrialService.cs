using SCADA.Communication;
using SCADA.Industrial.Base;
using SCADA.Industrial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Service
{
    public class IndustrialService : IIndustrialService
    {
        public DataResult<List<SerialInfo>> GetAll()
        {
            throw new NotImplementedException();
        }

        public DataResult<SerialInfo> InitSerialInfo()
        {
            SerialInfo info = new SerialInfo();
            DataResult<SerialInfo> result= new DataResult<SerialInfo>();
            try
            {
                info.PortName = GlobalConfiguration.PortName;
                info.BaudRate = GlobalConfiguration.BaudRate;
                info.Parity = GlobalConfiguration.Parity;
                info.DataBit = GlobalConfiguration.DataBit;
                info.StopBits= GlobalConfiguration.StopBits;
                result.Status = true;
                result.Result = info;
            }
            catch(Exception ex)
            {
                result.Status = false;
                result.Message = ex.Message;
                result.Result = null;
                return result;
            }

            return result;
        }

        
    }
}
