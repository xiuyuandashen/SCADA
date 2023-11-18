using SCADA.Industrial.Configuration;
using SCADA.Industrial.Models;
using SCADA.Industrial.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Service
{
    public class MonitorValuesService : IMonitorValuesService
    {
        public DataResult<List<MonitorValuesDto>> GetAll()
        {
            DataResult<List<MonitorValuesDto>> result = new DataResult<List<MonitorValuesDto>>();
            try
            {
                using (SCADAModels context = new SCADAModels())
                {
                    List<MonitorValues> monitorValues = context.MonitorValues.ToList();
                    List<MonitorValuesDto> monitorValuesDtos = AutoMapperConfiguration.GetMapper().Map<List<MonitorValuesDto>>(monitorValues);
                    result.SUCCESS(data: monitorValuesDtos);
                }

            }
            catch (Exception ex)
            {
                result.ERROR(Message: ex.Message);
                return result;
            }
            return result;
        }
    }
}
