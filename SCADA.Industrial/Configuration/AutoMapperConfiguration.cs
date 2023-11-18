using AutoMapper;
using AutoMapper.Configuration.Conventions;
using SCADA.Industrial.Models;
using SCADA.Industrial.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Configuration
{
    public static class AutoMapperConfiguration
    {

        public static IMapper Mapper { get; set; } = null;

        public static IMapper ConfigMapper()
        {
            // 创建映射配置
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Devices, DevicesDto>().ReverseMap();
                cfg.CreateMap<StorageArea, StorageAreaDto>().ReverseMap();
                cfg.CreateMap<MonitorValues, MonitorValuesDto>().ReverseMap();
            });

            // 初始化映射
            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        public  static IMapper GetMapper()
        {
            if (Mapper == null)
            {
                lock (typeof(AutoMapperConfiguration))
                {
                    if(Mapper == null)
                    {
                        Mapper = ConfigMapper();
                    }
                    return Mapper;
                }
            }
            return Mapper;

        }
    }
}
