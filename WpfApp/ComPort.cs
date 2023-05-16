using System.IO.Ports;
using System.Windows;
using System;
using System.Text;

internal class ComPort
{
    private SerialPort Port { get; set; } = null;
    private string PortName { get; set; }
    private int PortBaudRate { get; set; }
    private int PortDataBits { get; set; }
    private Parity PortParity { get; set; }
    private StopBits PortStopBits { get; set; }
    private Handshake PortHandshake { get; set; }

    protected int DefaultTimeout = 500;

    public ComPort() { }

    public ComPort(
        string portName,
        int portBaudRate,
        int portDataBits,
        Parity portParity,
        StopBits portStopBits,
        Handshake portHandshake
    )
    {
        PortName = portName;
        PortBaudRate = portBaudRate;
        PortParity = portParity;
        PortStopBits = portStopBits;
        PortDataBits = portDataBits;
        PortHandshake = portHandshake;
    }

    private bool IsOpened()
    {
        return (Port != null ? Port.IsOpen : false);
    }

    private bool IsClosed()
    {
        return !IsOpened();
    }

    public bool Open()
    {
        try
        {
            Port = new SerialPort(
                PortName,
                PortBaudRate,
                PortParity,
                PortDataBits,
                PortStopBits
            )
            {
                ReadTimeout = DefaultTimeout,
                WriteTimeout = DefaultTimeout,
                Handshake = PortHandshake
            };

            Port.Open();

            return true;
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
        }

        return false;
    }

    public bool Open(
        string portName,
        int portBaudRate,
        int portDataBits,
        Parity portParity,
        StopBits portStopBits,
        Handshake portHandshake
    )
    {
        PortName = portName;
        PortBaudRate = portBaudRate;
        PortParity = portParity;
        PortStopBits = portStopBits;
        PortDataBits = portDataBits;
        PortHandshake = portHandshake;

        return Open();
    }

    public bool Close()
    {
        if (IsOpened())
        {
            try
            {
                Port.Close();

                return true;
            }
            catch (Exception) { }

            return false;
        }

        return true;
    }

    public void Read()
    {
        while (IsOpened())
        {
            try
            {
                string message = Port.ReadLine();

                Console.WriteLine(message);
            }
            catch (TimeoutException) { }
        }
    }

    public void Write(byte[] buffer, int offset, int count)
    {
        if (IsOpened())
        {
            try
            {
                Port.Write(buffer, offset, count);
            }
            catch (Exception) { }
        }
    }

    public void Write(char[] buffer, int offset, int count)
    {
        Write(Encoding.GetEncoding("UTF-8").GetBytes(buffer), offset, count);
    }
}
