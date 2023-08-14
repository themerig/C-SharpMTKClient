using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MtkClientC
{
    internal class Feature
    {

        public async Task<bool> HandShake()
        {
            var sequence = MediatekService.HandShake;
            var i = 0;
            var length = sequence.Count;
            try
            {
                while (i < length)
                {
                    BromSerial.Write(sequence[i], 0, 1);
                    var reply = await Read(1);
                    if (reply.ByteArray == null) return false;
                    if (reply.ByteArray.Length == 1 && reply.ByteArray[0] == (~sequence[i][0] & 0xFF))
                    {
                        i++;
                    }
                    else
                    {
                        i = 0;
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }

        }


        public async Task<ReadOutput> Read(int len = 0, int timeout = 8000)
        {
            try
            {
                if (Cls.Stop)
                {
                    throw new Exception("Stopped Operation By User");
                }
                ReadOutput ReadOutput;
                if (len != 0)
                {
                    ReadOutput = await ReadAsync(len, timeout);
                    if (ReadOutput.ByteArray == null)
                    {
                        ReadOutput = await ReadAsync(len, timeout);

                    }
                }
                else
                {
                    ReadOutput = await ReadAsync(0, timeout);
                }
                if (Cls.Stop)
                {
                    return default(ReadOutput);
                }

                return ReadOutput;
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                if (Cls.Stop)
                {
                    Cls.Stop = false;
                }
            }
        }

        private class SendReceiveAsyncState
        {
            public ManualResetEvent finished = new ManualResetEvent(false);
            public SerialPort src;
            public byte[] sendBuf;
            public byte[] recvBuf;
            public int rcv;
        }

        public Port()
        {
            Handle();
            SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged; ;

        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (e.Mode != PowerModes.Resume)
                BromSerial.Close();
        }

        public struct ReadOutput
        {
            public int ReadedLen;
            public string BitString;
            public byte[] ByteArray;
        }
        public SerialPort BromSerial;

        public SerialPort GetConfig
        {
            get
            {
                var Genp = new SerialPort();
                Genp.ReadTimeout = 30000;
                Genp.WriteTimeout = 30000;
                Genp.DataBits = 8;
                Genp.Parity = Parity.None;
                Genp.StopBits = StopBits.One;
                Genp.DiscardNull = true;
                Genp.RtsEnable = true;
                Genp.DtrEnable = true;
                Genp.WriteBufferSize = 3772724;
                if (!string.IsNullOrEmpty(this.GetConfig.PortName))
                {
                    Genp.PortName = this.GetConfig.PortName;
                }
                return Genp;
            }
        }

        public void Handle()
        {
            if (BromSerial != null)
            {
                Close();
                BromSerial.Dispose();
            }
            BromSerial = GetConfig;
        }



        public async Task WritePort(byte[] b, int lenght)
        {
            await WriteAsync(b, 0, lenght);
        }

        public string WError;
        public async Task WriteAsync(byte[] data, int offset, int len)
        {
            await Task.Run(async () =>
            {
                await Open();
                BromSerial.InitializeLifetimeService();
                BromSerial.DiscardInBuffer();
                BromSerial.DiscardOutBuffer();
                WError = "";
                SendReceiveAsyncState state = new SendReceiveAsyncState
                {
                    sendBuf = data,
                    src = BromSerial
                };
                //using(var writer = new StreamWriter(@"C:\Users\Linda\Documents\Desktop\New folder\debug.txt" , true))
                //{
                //    writer.WriteLine("Writed >> "+ Cls.BytesToHexString(data));
                //}
                if (BromSerial.IsOpen)
                {
                    BromSerial.BaseStream.BeginWrite(state.sendBuf, offset, len, SendReceiveAsyncWriteComplete, state);
                    if (!state.finished.WaitOne(10000))
                    {
                        throw new Exception("Timeout Writing Data On device");
                    }
                }
                else
                {
                    throw new Exception("Device isnot open");
                }


            });
            if (!string.IsNullOrEmpty(WError))
            {
                throw new Exception(WError);
            }
        }

        public async Task<ReadOutput> ReadAsync(int len = 0, int timeout = 8000)
        {
            ReadOutput ReadOutput = default(ReadOutput);
            ReadErr = "";
            if (len != 0)
            {
                await Task.Run(() =>
                {
                    SendReceiveAsyncState state = new SendReceiveAsyncState();
                    state.src = BromSerial;
                    state.recvBuf = new byte[len];
                    if (BromSerial.IsOpen)
                    {
                        BromSerial.BaseStream.BeginRead(state.recvBuf, 0, state.recvBuf.Length, SendReceiveAsyncReadComplete, state);
                        if (state.finished.WaitOne(timeout) && state.rcv > 0)
                        {
                            ReadOutput.ReadedLen = state.rcv;
                            ReadOutput.ByteArray = state.recvBuf;
                        }
                    }
                    else
                    {
                        throw new Exception("port isnot open");
                    }

                });
                if (!string.IsNullOrEmpty(ReadErr))
                {
                    throw new Exception(ReadErr);
                }
            }
            else
            {
                var read = BromSerial.BytesToRead;
                if (read > 0)
                {
                    ReadOutput.ReadedLen = await BromSerial.BaseStream.ReadAsync(ReadOutput.ByteArray, 0, read);
                }

            }

            return ReadOutput;
        }


    }
}
