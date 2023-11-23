using AutoMapper;
using SCADA.Industrial.Configuration;
using SCADA.Industrial.Models;
using SCADA.Industrial.Models.Dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCADA.Industrial.Service
{
    public class DeviceService : IDeviceService
    {
        public DataResult<List<DevicesDto>> GetAll()
        {
            DataResult<List<DevicesDto>> result = new DataResult<List<DevicesDto>>();
            try
            {
                using (SCADAModels context = new SCADAModels())
                {
                    List<Devices> devices = context.Devices.ToList();
                    IMapper mapper = AutoMapperConfiguration.GetMapper();
                    List<DevicesDto> devicesDtos = mapper.Map<List<DevicesDto>>(devices);

                    foreach (var deviceItem in devicesDtos)
                    {
                        List<MonitorValues> monitorValues = context.MonitorValues.Where(f => f.d_id == deviceItem.D_ID).ToList();
                        List<MonitorValuesDto> monitorValuesDtos = mapper.Map<List<MonitorValuesDto>>(monitorValues);
                        deviceItem.MonitorValuesList = new ObservableCollection<MonitorValuesDto>();
                        foreach (var monitorValue in monitorValuesDtos)
                        {
                            // 创建告警回调 每次当前监控值改变时触发该回调
                            monitorValue.ValueStateChanged = (state, msg) =>
                            {

                                WarningMessageMode warningMessageMode = deviceItem.ErrorMessagesList.FirstOrDefault(f => f.ValueId.Equals(monitorValue.VALUE_ID));
                                if (warningMessageMode != null)
                                {
                                    // 如果存在告警记录，删除 每个监控值仅保留最新告警
                                    deviceItem.ErrorMessagesList.Remove(warningMessageMode);
                                }

                                if (state != Base.MonitorValuesState.OK)
                                {
                                    // 存在数据异常告警
                                    deviceItem.IsWaring = true;

                                    deviceItem.ErrorMessagesList.Add(new WarningMessageMode { ValueId = monitorValue.VALUE_ID, Message = msg });
                                }

                                bool exitsWarning = deviceItem.ErrorMessagesList.Count > 0;
                                if (exitsWarning != deviceItem.IsWaring)
                                {
                                    deviceItem.IsWaring = exitsWarning;
                                }
                            };

                            deviceItem.MonitorValuesList.Add(monitorValue);

                        }
                    }
                    result.SUCCESS(data: devicesDtos);
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
