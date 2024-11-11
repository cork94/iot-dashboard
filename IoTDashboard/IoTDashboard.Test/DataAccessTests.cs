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
            IList<Device> expected = new List<Device>();
            for (int i = 0; i < 3; i++)
            {
                var tempDevice = new Device()
                {
                    Id = i,
                    Name = $"X{i}",
                    State = DeviceState.Unknown
                };
                expected.Add(tempDevice);
            }
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
    }
}