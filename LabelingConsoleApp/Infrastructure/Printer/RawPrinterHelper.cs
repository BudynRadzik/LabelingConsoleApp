using System;
using System.Runtime.InteropServices;

namespace LabelingConsole.Infrastructure.Printing
{
    public static class RawPrinterHelper
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private class DOCINFOW
        {
            [MarshalAs(UnmanagedType.LPWStr)] public string pDocName = string.Empty;
            [MarshalAs(UnmanagedType.LPWStr)] public string? pOutputFile;
            [MarshalAs(UnmanagedType.LPWStr)] public string pDataType = "RAW";
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterW", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool OpenPrinter(string pPrinterName, out IntPtr phPrinter, IntPtr pDefault);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterW", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In] DOCINFOW di);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", SetLastError = true)]
        private static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        public static bool SendStringToPrinter(string printerName, string data, out string? error)
        {
            error = null;
            IntPtr hPrinter = IntPtr.Zero;
            IntPtr pBytes = IntPtr.Zero;

            try
            {
                if (!OpenPrinter(printerName, out hPrinter, IntPtr.Zero))
                {
                    error = $"OpenPrinter failed: {Marshal.GetLastWin32Error()}";
                    return false;
                }

                var docInfo = new DOCINFOW { pDocName = "Label", pDataType = "RAW" };

                if (!StartDocPrinter(hPrinter, 1, docInfo))
                {
                    error = $"StartDocPrinter failed: {Marshal.GetLastWin32Error()}";
                    return false;
                }

                if (!StartPagePrinter(hPrinter))
                {
                    error = $"StartPagePrinter failed: {Marshal.GetLastWin32Error()}";
                    EndDocPrinter(hPrinter);
                    return false;
                }

                byte[] bytes = System.Text.Encoding.ASCII.GetBytes(data);
                pBytes = Marshal.AllocCoTaskMem(bytes.Length);
                Marshal.Copy(bytes, 0, pBytes, bytes.Length);

                if (!WritePrinter(hPrinter, pBytes, bytes.Length, out int written) || written != bytes.Length)
                {
                    error = $"WritePrinter failed: {Marshal.GetLastWin32Error()}";
                    EndPagePrinter(hPrinter);
                    EndDocPrinter(hPrinter);
                    return false;
                }

                EndPagePrinter(hPrinter);
                EndDocPrinter(hPrinter);
                return true;
            }
            finally
            {
                if (pBytes != IntPtr.Zero) Marshal.FreeCoTaskMem(pBytes);
                if (hPrinter != IntPtr.Zero) ClosePrinter(hPrinter);
            }
        }
    }
}
