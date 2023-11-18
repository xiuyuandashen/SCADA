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
    public class StorageAreaService : IStorageAreaService
    {
        public DataResult<List<StorageAreaDto>> GetAll()
        {
            DataResult<List<StorageAreaDto>> result = new DataResult<List<StorageAreaDto>>();
            try
            {
                using(SCADAModels context = new SCADAModels())
                {
                    List<StorageArea> storageAreas = context.StorageArea.ToList();
                    List<StorageAreaDto> storageAreaDtos = AutoMapperConfiguration.GetMapper().Map<List<StorageAreaDto>>(storageAreas);
                    result.SUCCESS(data: storageAreaDtos);
                }

            }catch(Exception ex)
            {
                result.ERROR(Message: ex.Message);
                return result;
            }
            return result;
        }
    }
}
