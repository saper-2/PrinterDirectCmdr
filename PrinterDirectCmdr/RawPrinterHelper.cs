using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using PrinterDirectCmdr.Properties;
using System.ComponentModel;

namespace PrinterHelpers
{

    public class RawPrinterHelper
    {
        // Structure and API declarations:
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        internal class PRINTERDEFAULTSClass
        {
            public IntPtr pDatatype;
            public IntPtr pDevMode;
            public int DesiredAccess;
        }

        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, ref IntPtr hPrinter, IntPtr pd);

        [DllImport("winspool.Drv", EntryPoint = "ClosePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe bool ClosePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", SetLastError = true, CharSet = CharSet.Ansi, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe bool StartDocPrinter(IntPtr hPrinter, Int32 level, [In()][MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);

        [DllImport("winspool.Drv", EntryPoint = "EndDocPrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe bool EndDocPrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "StartPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe bool StartPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "EndPagePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe bool EndPagePrinter(IntPtr hPrinter);

        [DllImport("winspool.Drv", EntryPoint = "WritePrinter", SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern unsafe bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, Int32 dwCount, ref Int32 dwWritten);

        #region Common Printer commands
        /// <summary>
        /// Init printer & clear all bufers
        /// </summary>
        public const string cmdClear = "\x1b@";// Strings.Chr(27) + "@";
        /// <summary>
        /// Justify text to center
        /// </summary>
        public const string cmdTextAlignCenter = "\x1b" + "a1";// Strings.Chr(27) + Strings.Chr(97) + "1";
        /// <summary>
        /// Justify text to left
        /// </summary>
        public const string cmdTextAlignLeft = "\x1b" + "a0";// Strings.Chr(27) + Strings.Chr(97) + "0";
        /// <summary>
        /// Justify text to right
        /// </summary>
        public const string cmdTextAlignRight = "\x1b" + "a2";//Strings.Chr(27) + Strings.Chr(97) + "2";
        /// <summary>
        /// Open drawer 0
        /// </summary>
        public const string cmdOpenDrawer = cmdClear + "\x1bp\x00" + ".}";//eClear + Strings.Chr(27) + "p" + Strings.Chr(0) + ".}";
        /// <summary>
        /// Cut paper
        /// </summary>
        public const string cmdCut = "\x1b" + "i\x0d\x0a";//Strings.Chr(27) + "i" + Constants.vbCrLf;
        /// <summary>
        /// Set font to small font (Font 0) [Emphasized =Off, Double-height=off, Double-width=off, Underline=off]
        /// </summary>
        public const string cmdSmallFont = "\x1b!\x01";//Strings.Chr(27) + "!" + Strings.Chr(1);
        /// <summary>
        /// Set font to normal font (Font 1) [Emphasized =Off, Double-height=off, Double-width=off, Underline=off]
        /// </summary>
        public const string cmdNormalFont = "\x1b!\x00";//Strings.Chr(27) + "!" + Strings.Chr(0);
        /// <summary>
        /// Initial command: set font 0 (small), charset: USA [0]
        /// </summary>
        public const string cmdInit = cmdSmallFont + "\x0d\x1b" + "R0" + "\x0d\x0a";//eNmlText + Strings.Chr(13) + Strings.Chr(27) + "c6" + Strings.Chr(1) + Strings.Chr(27) + "R3" + Constants.vbCrLf;
                                                                                    // don't know what "c6" doing
                                                                                    //public const string cmdInit = cmdSmallFont + "\x0d\x1b" + "c6\x01\x1b" + "R0" + "\x0d\x0a";//eNmlText + Strings.Chr(13) + Strings.Chr(27) + "c6" + Strings.Chr(1) + Strings.Chr(27) + "R3" + Constants.vbCrLf;
        /// <summary>
        /// Big mode: Font=0, Emphasized=ON, Double-height=ON, Double-width=ON, Underline=off
        /// </summary>
        public const string cmdBigCharOn = "\x1b" + "!8";//Strings.Chr(27) + "!" + Strings.Chr(56);
        /// <summary>
        /// Switch back to normal mode: Font=0, Emphasized=Off, Double-height=Off, Double-width=Off, Underline=Off
        /// </summary>
        public const string cmdBigCharOff = "\x1b" + "!\x00";//Strings.Chr(27) + "!" + Strings.Chr(0);
        #endregion

        private IntPtr hPrinter = new IntPtr(0);
        private DOCINFOA di = new DOCINFOA();
        private bool PrinterOpen = false;
        public bool PrinterIsOpen { get { return PrinterOpen; } }

        private string printerName;
        public string PrinterName { get { return printerName; } }

        private int win32LastErrCode = 0;
        public int Win32LastErrorCode {  get { return win32LastErrCode; } }

        /// <summary>
        /// Open printer handle, document & start it.
        /// Return: 0=printer_opened&doc_started&page_started , 1=Can't open printer , 2=cant' start doc, 3=can't start page
        /// </summary>
        /// <param name="szPrinterName"></param>
        /// <param name="docName"></param>
        /// <returns></returns>
        public int OpenPrinter(string szPrinterName, string docName = ".NET-RAW-PRINT")
        {
            int re = 1;
            win32LastErrCode = 0;
            if (PrinterOpen == false)
            {
                di.pDocName = docName;
                di.pDataType = "RAW";

                if (OpenPrinter(szPrinterName.Normalize(), ref hPrinter, IntPtr.Zero))
                {
                    // Start a document.
                    if (StartDocPrinter(hPrinter, 1, di))
                    {
                        if (StartPagePrinter(hPrinter))
                        {
                            PrinterOpen = true;
                            printerName = szPrinterName;
                            re = 0;
                        } else
                        {
                            win32LastErrCode = Marshal.GetLastWin32Error();
                            re = 3;
                        }
                    } else
                    {
                        win32LastErrCode = Marshal.GetLastWin32Error();
                        re = 2;
                    }
                } else
                {
                    win32LastErrCode = Marshal.GetLastWin32Error();
                    re = 1;
                }
            } else
            {
                re = 0;
            }

            return re;
        }

        /// <summary>
        /// Cose printer handle
        /// </summary>
        public int ClosePrinter()
        {
            int re = 0;
            if (PrinterOpen)
            {
                if (!EndPagePrinter(hPrinter)) re |= 0x01;

                if (!EndDocPrinter(hPrinter)) re |= 0x02;
                
                if (!ClosePrinter(hPrinter)) re |= 0x04;
                
                PrinterOpen = false;
            }
            return re;
        }

        /// <summary>
        /// Send command string to printer. Return 0 on success, or Win32LastError on error
        /// </summary>
        /// <param name="szString"></param>
        /// <returns></returns>
        public int SendStringToPrinter(string szString)
        {

            bool re = false;
            int rei = -1;
            win32LastErrCode = 0;
            if (PrinterOpen)
            {
                IntPtr pBytes;
                Int32 dwCount;
                Int32 dwWritten = 0;

                dwCount = szString.Length;

                pBytes = Marshal.StringToCoTaskMemAnsi(szString);

                re = WritePrinter(hPrinter, pBytes, dwCount, ref dwWritten);

                if (!re) { 
                    rei = Marshal.GetLastWin32Error();
                    win32LastErrCode = rei;
                } else
                {
                    rei = 0;
                }

                Marshal.FreeCoTaskMem(pBytes);
            }
            //else return false;

            return rei;
        }
    }


    #region Get available printer names
    public struct DRIVER_INFO_2
    {
        public uint cVersion;
        [MarshalAs(UnmanagedType.LPTStr)] public string pName;
        [MarshalAs(UnmanagedType.LPTStr)] public string pEnvironment;
        [MarshalAs(UnmanagedType.LPTStr)] public string pDriverPath;
        [MarshalAs(UnmanagedType.LPTStr)] public string pDataFile;
        [MarshalAs(UnmanagedType.LPTStr)] public string pConfigFile;
    }

    public static class EnumeratePrinterDriverNames
    {
        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int EnumPrinterDrivers(String pName, String pEnvironment, uint level, IntPtr pDriverInfo,
            uint cdBuf, ref uint pcbNeeded, ref uint pcRetruned);

        public static IEnumerable<string> Enumerate()
        {
            const int ERROR_INSUFFICIENT_BUFFER = 122;

            uint needed = 0;
            uint returned = 0;
            if (EnumPrinterDrivers(null, null, 2, IntPtr.Zero, 0, ref needed, ref returned) != 0)
            {
                //succeeds, but shouldn't, because buffer is zero (too small)!
                throw new ApplicationException("EnumPrinters should fail!");
            }

            int lastWin32Error = Marshal.GetLastWin32Error();
            if (lastWin32Error != ERROR_INSUFFICIENT_BUFFER)
            {
                throw new Win32Exception(lastWin32Error);
            }

            IntPtr address = Marshal.AllocHGlobal((IntPtr)needed);
            try
            {
                if (EnumPrinterDrivers(null, null, 2, address, needed, ref needed, ref returned) == 0)
                {
                    throw new Win32Exception(Marshal.GetLastWin32Error());
                }

                var type = typeof(DRIVER_INFO_2);
                IntPtr offset = address;
                int increment = Marshal.SizeOf(type);

                for (uint i = 0; i < returned; i++)
                {
                    var di = (DRIVER_INFO_2)Marshal.PtrToStructure(offset, type);
                    offset += increment;

                    yield return di.pName;
                }
            }
            finally
            {
                Marshal.FreeHGlobal(address);
            }
        }
    }
    #endregion

}