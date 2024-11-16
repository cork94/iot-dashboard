using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoTDashboard.ViewModel
{
    public class DeviceActionButtonsViewModel
    {
        private Device _device;

        public DeviceActionButtonsViewModel(Device device)
        {
            _device = device;
        }

        public int GetDeviceId() => _device.Id;

        public string StateButtonName
        {
            get {
                    if(_device.State == DeviceState.Running)
                    {
                        return "Stop";
                    }
                    else
                    {
                        return "Start";
                    }
                }
            set { _device.Name = value; }
        }
    }
}
