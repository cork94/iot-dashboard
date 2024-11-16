using IoTDashboard.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IoTDashboard.Views
{
    /// <summary>
    /// Interaction logic for DeviceActionButtonsControl.xaml
    /// </summary>
    public partial class DeviceActionButtonsControl : UserControl
    {
        private DeviceActionButtonsViewModel _viewModel;
        private readonly IDataAccess _dataAccess;

        public DeviceActionButtonsControl(Device device, IDataAccess dataAccess)
        {
            _viewModel = new DeviceActionButtonsViewModel(device);
            _dataAccess = dataAccess;
            DataContext = _viewModel;
            InitializeComponent();

        }

        private void StateBtn_Click(object sender, RoutedEventArgs e)
        {
            _dataAccess.ChangeDeviceState(_viewModel.GetDeviceId());
        }
    }
}
