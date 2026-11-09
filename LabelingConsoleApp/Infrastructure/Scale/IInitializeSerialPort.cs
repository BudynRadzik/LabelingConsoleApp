using System.IO.Ports;


namespace LabelingConsole.Infrastructure.Scale
{
    public interface IInitializeSerialPort
    {
        SerialPort? SerialPortInit(SerialPortSettings serialPortSettings);
    }
}
