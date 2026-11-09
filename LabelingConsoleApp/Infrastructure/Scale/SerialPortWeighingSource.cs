using LabelingConsole.Abstractions;

namespace LabelingConsole.Infrastructure.Scale
{
    public class SerialPortWeighingSource : IWeightSource
    {
        private readonly IInitializeSerialPort _initializeSerialPort;
        private readonly SerialPortSettings _serialPortSettings;
        public SerialPortWeighingSource(IInitializeSerialPort initializeSerialPort, SerialPortSettings serialPortSettings)
        {
            _initializeSerialPort = initializeSerialPort;
            _serialPortSettings = serialPortSettings;
        }
        
            
        
        public bool TryReadWeight(out decimal weight, out string? error)
        {
            weight = 0;
            error = null;
            try
            {
                var serialPort = _initializeSerialPort.SerialPortInit(_serialPortSettings);
                 if (serialPort is null || !serialPort.IsOpen)
                 {
                    error = "Nie udało się otworzyć portu szeregowego.";
                    return false;
                 }

                
                string? data = serialPort.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(data))
                {
                    weight = 0;
                    error = null;
                    return true;
                }

                 if (decimal.TryParse(data, System.Globalization.NumberStyles.Any,
                                System.Globalization.CultureInfo.InvariantCulture,
                                out decimal result))
                 {
                     weight = result;
                     error = null;
                     return true;
                 }
                 else
                 {
                     weight = 0;
                     error = $"Nie udało się odczytać wagi z danych: {data}";
                     return false;
                 }
            }
            catch (TimeoutException)
            {
                weight = 0;
                error = null;
                return true;
            }
            catch (Exception ex)
            {
                weight = 0;
                error = $"Wystąpił błąd podczas odczytu wagi: {ex.Message}";
                return false;
            }
        }
    }
}
