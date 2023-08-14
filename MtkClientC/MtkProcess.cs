using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Net;
using System.Windows.Forms;
using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;

namespace MtkClientC
{
    public class MtkProcess
    {

        public SerialPort hCom = null;
        private const int BAUD = 115200;
        private const int TIMEOUT = 5000;
        ushort vid = 0x0E8D;
        ushort pid = 0x0003;
        private const string PAYLOAD_DIR = "payloads/";
        private string magichex = "efeeeefe";

        public MtkProcess(string port = null)
        {
            hCom = null;
            if (port != null)
            {
                hCom = new SerialPort(port, BAUD);
                hCom.ReadTimeout = TIMEOUT;
                hCom.WriteTimeout = TIMEOUT;
            }
        }

        public void Driver()
        {
            if(LibUsbFilterDriver(vid, pid) == false)
            {
                Msg("Mediatek sürücüsü bulunamadı",Color.Black,1);
            } else
            {
                Msg("Mediatek sürücüsü bulundu. USB aygıtıyla bir şeyler yapın.",Color.Black,1);
            }
        }


        public string FindMtkDevice()
        {
            if (hCom != null)
            {
                Msg("Device already found", Color.Purple, 1);
            }

            Form1.Instance.richTextBox1.Clear();

            Msg("Scann BootROm ..." + Environment.NewLine, Color.Purple, 1);

            Msg("Press Volume Up + Power and Insert USB" + Environment.NewLine, Color.Purple, 2);

            var olddev = SerialPort.GetPortNames();
            string port = null;
            while (true)
            {
                var newdev = SerialPort.GetPortNames();

                if (newdev.Length > olddev.Length)
                {
                    foreach (var newPort in newdev)
                    {
                        if (!olddev.Contains(newPort))
                        {
                            port = newPort;
                            break;
                        }
                    }
                    break;
                }

                else if (newdev.Length < olddev.Length)
                {
                    olddev = newdev;
                }

                System.Threading.Thread.Sleep(250);
            }

            hCom = new SerialPort(port, BAUD);
            hCom.ReadTimeout = TIMEOUT;
            hCom.WriteTimeout = TIMEOUT;
            hCom.Open();

            var deviceInfo = "Mtk Port: " + port;
            Form1.Instance.portList.Items.Add(deviceInfo);
            Form1.Instance.portList.SelectedItem = deviceInfo;

            return port;

        }


        public void connect_brom(string port)
        {
            MessageBox.Show(port);

        }




















































        public static bool LibUsbFilterDriver(ushort vid, ushort pid)
        {
            var usbDeviceFinder = new UsbDeviceFinder(vid, pid);
            using (var usbDevice = UsbDevice.OpenUsbDevice(usbDeviceFinder))
            {
                if (usbDevice == null)
                {
                    return false;
                }
                return true;
            }
        }
        public void Msg(string text, Color color, int style)
        {
            var rch = Form1.Instance.richTextBox1;
            try
            {
                rch.SelectionColor = color;
                rch.SelectionStart = rch.TextLength;
                rch.SelectionFont = GetFont(style);
                rch.SelectedText = text;
                rch.ScrollToCaret();
            }
            catch
            {
            }
        }

        private Font GetFont(int style)
        {
            FontStyle fontStyle = FontStyle.Regular;
            if (style == 1)
                fontStyle = FontStyle.Bold;
            else if (style == 2)
                fontStyle = FontStyle.Italic;

            return new Font("Microsoft Sans Serif", 8, fontStyle);
        }
    }
}
