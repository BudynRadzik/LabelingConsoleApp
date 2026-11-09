using System.Diagnostics;
using System.IO.Ports;

namespace LabelingConsole.Infrastructure.Scale
{
    public class InitializeSerialPort: IInitializeSerialPort
    {
        private SerialPort? _serialPort;
        public SerialPort? SerialPortInit(SerialPortSettings serialPortSettings)
        {
            if (_serialPort != null && _serialPort.IsOpen)
                return _serialPort;

            try
            {
                _serialPort = new SerialPort
                {
                    PortName = serialPortSettings.PortName,// Ustaw odpowiedni port COM
                    BaudRate = serialPortSettings.BaudRate, // Ustaw odpowiednią prędkość transmisji
                    DataBits = serialPortSettings.DataBits, // Ustaw odpowiednią liczbę bitów danych
                    Parity = serialPortSettings.Parity, // Ustaw odpowiednią parzystość
                    StopBits = serialPortSettings.StopBits, // Ustaw odpowiednią liczbę bitów stopu
                    Handshake = Handshake.None, // Ustaw odpowiedni tryb kontroli przepływu
                    ReadTimeout = 1000, // Ustaw odpowiedni czas oczekiwania na odczyt danych
                };
                _serialPort.Open();
                return _serialPort;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[Scale] Błąd inicjalizacji portu {serialPortSettings.PortName}: {ex.Message}");
                _serialPort = null;
                return null;
            }
        }
    }
}
