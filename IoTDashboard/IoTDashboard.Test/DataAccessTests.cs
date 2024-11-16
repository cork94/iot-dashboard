using NSubstitute;
using NSubstitute.Core.Arguments;

namespace IoTDashboard.Test
{
    public class DataAccessTests
    {
        IFileReader _fileReaderFake;
        public DataAccessTests()
        {
            _fileReaderFake = Substitute.For<IFileReader>();
        }

        [Fact]
        public void GetAllDevices_ReadesDevicesFromJson_RetrunsListOfDevices()
        {
            //arange
            IList<Device> expected = GenerateFakeDevices();
            _fileReaderFake.DecerializeFromFile<List<Device>>(Arg.Any<string>()).Returns(expected);
            var dAServicee = new DataAccess(_fileReaderFake);

            //act
            IList<Device> actual = dAServicee.GetAllDevices();

            //Assert
            Assert.Equal(expected.Count, actual.Count);
            for (var i = 0; i < actual.Count; i++)
            {
                {
                    Assert.Equal(actual[i].Id, expected[i].Id);
                    Assert.Equal(actual[i].Name, expected[i].Name);
                    Assert.Equal(actual[i].State, expected[i].State);
                }

            }
        }

        [Fact]
        public void ChangeDeviceState_WhenThereIsNoDeviceWithProvidedID_ThrowArgumentException()
        {
            //arange
            IList<Device> expected = GenerateFakeDevices();
            _fileReaderFake.DecerializeFromFile<List<Device>>(Arg.Any<string>()).Returns(expected);
            var dAServicee = new DataAccess(_fileReaderFake);

            //act

            //Assert
            Assert.Throws<ArgumentException>(() => dAServicee.ChangeDeviceState(5));
        }

        [Fact]
        public void ChangeDeviceState_WhenStateIsRunning_ChangeItToStopped()
        {
            //arange
            int id = 1;
            IList<Device> expected = GenerateFakeDevices();
            _fileReaderFake.DecerializeFromFile<List<Device>>(Arg.Any<string>()).Returns(expected);
            var dAServicee = new DataAccess(_fileReaderFake);

            //act
            dAServicee.ChangeDeviceState(id);
            var device = dAServicee.GetAllDevices().Where(x => x.Id == id).FirstOrDefault();

            //Assert
            Assert.Equal(DeviceState.Stopped, device.State);
        }

        [Fact]
        public void DeleteDevice_WhenNotFound_ThrowArgumentException()
        {
            //arange
            IList<Device> expected = GenerateFakeDevices();
            _fileReaderFake.DecerializeFromFile<List<Device>>(Arg.Any<string>()).Returns(expected);
            var dAServicee = new DataAccess(_fileReaderFake);

            //act

            //Assert
            Assert.Throws<ArgumentException>(() => dAServicee.DeleteDevice(5));
        }

        [Fact]
        public void DeleteDevice_WhenDeleted_ReturnsThatDevice()
        {
            //arange
            int id = 1;
            IList<Device> expected = GenerateFakeDevices();
            _fileReaderFake.DecerializeFromFile<List<Device>>(Arg.Any<string>()).Returns(expected);
            var dAServicee = new DataAccess(_fileReaderFake);

            //act
            var actualDevice = dAServicee.DeleteDevice(id);

            //Assert
            Assert.Equal(id, actualDevice.Id);

        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void AddDevice_WhenNameParameterNotFound_ThrowArgumentException(string name)
        {
            //arange
            IList<Device> expected = GenerateFakeDevices();
            _fileReaderFake.DecerializeFromFile<List<Device>>(Arg.Any<string>()).Returns(expected);
            var dAServicee = new DataAccess(_fileReaderFake);

            //act

            //Assert
            Assert.Throws<ArgumentException>(() => dAServicee.AddDevice(name));
        }

        [Theory]
        [InlineData("Test1", DeviceState.Running)]
        [InlineData("Test2", DeviceState.Stopped)]
        public void AddDevice_WhenDeleted_ReturnsThatDevice(string name, DeviceState state)
        {
            //arange
            IList<Device> expected = GenerateFakeDevices();
            _fileReaderFake.DecerializeFromFile<List<Device>>(Arg.Any<string>()).Returns(expected);
            var dAServicee = new DataAccess(_fileReaderFake);

            //act
            var actualDevice = dAServicee.AddDevice(name,state);

            //Assert
            Assert.Equal(3, actualDevice.Id);
            Assert.Equal(name, actualDevice.Name);
            Assert.Equal(state, actualDevice.State);
        }

        private IList<Device> GenerateFakeDevices()
        {
            IList<Device> result = new List<Device>();
            for (int i = 0; i < 3; i++)
            {
                var tempDevice = new Device()
                {
                    Id = i,
                    Name = $"X{i}",
                    State = DeviceState.Running
                };
                result.Add(tempDevice);
            }

            return result;
        }
    }
}