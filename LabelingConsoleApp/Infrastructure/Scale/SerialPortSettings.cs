using System.IO.Ports;

namespace LabelingConsole.Infrastructure.Scale
{
    public class SerialPortSettings
    {
        public string PortName { get; set; } = "COM4";
        public int BaudRate { get; set; } = 9600;
        public int DataBits { get; set; } = 8;
        public Parity Parity { get; set; } = Parity.None;
        public StopBits StopBits { get; set; } = StopBits.One;

        public SerialPortSettings(string portName)
        {
            PortName = portName;
        }
    }
}
