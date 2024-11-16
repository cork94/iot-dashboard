using IoTDashboard.Views;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IoTDashboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IList<Device> devices;
        IList<UIElement> displayedIoTElements;
        IDataAccess dataAccess;
        public MainWindow()
        {
            IFileReader fileReader = new FileReaderWrapper();
            devices = new List<Device>();
            dataAccess = new DataAccess(fileReader);
            displayedIoTElements = new List<UIElement>();
            InitializeComponent();
            ReadAndDisplayDevices();
        }

        private void refreshBtn_Click(object sender, RoutedEventArgs e)
        {
            removeOldElements();
            ReadAndDisplayDevices();
        }

        private void removeOldElements()
        {
            var elemntCount = displayedIoTElements.Count;
            List<int> rowIndex = new List<int>();
            if (elemntCount > 0)
            {
                foreach (var ele in displayedIoTElements)
                {
                    int index = Grid.GetRow(ele);
                    rowIndex.Add(index);
                    iotListGrid.Children.Remove(ele);
                }
            }
            List<int> clearRowIndex = rowIndex.Distinct().OrderByDescending(x=>x).ToList();
            if (clearRowIndex.Count > 0)
            {
                foreach (var ele in clearRowIndex)
                {
                    iotListGrid.RowDefinitions.RemoveAt(ele);
                }
            }
        }

        private void ReadAndDisplayDevices()
        {
            if(devices.Count > 0)
            {
                devices.Clear();
            }
            devices = dataAccess.GetAllDevices();
            for (var i = 0; i < devices.Count; i++)
            {
                RowDefinition tempRowDef= new RowDefinition();
                tempRowDef.Height = GridLength.Auto;
                iotListGrid.RowDefinitions.Add(tempRowDef);

                Label idLabel = new Label();
                idLabel.FontSize = 12;
                idLabel.Content = devices[i].Id;
                Grid.SetColumn(idLabel, 0);
                Grid.SetRow(idLabel, i + 1);
                displayedIoTElements.Add(idLabel);
                iotListGrid.Children.Add(idLabel);

                Label nameLabel = new Label();
                nameLabel.FontSize = 12;
                nameLabel.Content = devices[i].Name;
                Grid.SetColumn(nameLabel,1);
                Grid.SetRow(nameLabel, i + 1);
                displayedIoTElements.Add(nameLabel);
                iotListGrid.Children.Add(nameLabel);

                Label stateLabel = new Label();
                stateLabel.FontSize = 12;
                stateLabel.Content = devices[i].State;
                Grid.SetColumn(stateLabel, 2);
                Grid.SetRow(stateLabel, i + 1);
                displayedIoTElements.Add(stateLabel);
                iotListGrid.Children.Add(stateLabel);

                var deviceActionBtns = new DeviceActionButtonsControl(devices[i]);
                Grid.SetColumn(deviceActionBtns, 3);
                Grid.SetRow(deviceActionBtns, i + 1);
                displayedIoTElements.Add(deviceActionBtns);
                iotListGrid.Children.Add(deviceActionBtns);
            }
        }

        private void AddActionButtons()
        {
            Button editButton = new Button();
            editButton.FontSize = 10;
            editButton.Content = "EDIT";

            Button deleteButton = new Button();
            editButton.FontSize = 10;
            editButton.Content = "Delete";
        }
    }
}