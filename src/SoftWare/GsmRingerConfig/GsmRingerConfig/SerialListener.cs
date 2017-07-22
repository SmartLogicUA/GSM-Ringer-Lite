using System;
using System.Collections.Generic;
using System.Text;

namespace GsmRingerConfig
{
    public class SerialPortListener
    {
        System.IO.Ports.SerialPort port;
        System.IO.Ports.SerialDataReceivedEventHandler handler;
        //bool WasOpen = true;

        public event DeviceDataReceivedEventHandler DeviceIdReceived;
        public event DeviceDataReceivedEventHandler DeviceVersionReceived;
        public event DeviceDataReceivedEventHandler SetDefaultsOKReceived;
        public event DeviceDataReceivedEventHandler ErrorReceived;
        public event DeviceDataReceivedEventHandler SetUserNumberOKReceived;
        public event DeviceDataReceivedEventHandler GetUserNumberReceived;
        public event DeviceDataReceivedEventHandler SetTimeActivationOKReceived;
        public event DeviceDataReceivedEventHandler GetTimeActivationReceived;
        public event DeviceDataReceivedEventHandler SetTimeDeactivationOKReceived;
        public event DeviceDataReceivedEventHandler GetTimeDeactivationReceived;
        public event DeviceDataReceivedEventHandler SetCallParamsOKReceived;
        public event DeviceDataReceivedEventHandler GetCallParamsReceived;
        public event DeviceDataReceivedEventHandler SetADCParamsOKReceived;
        public event DeviceDataReceivedEventHandler GetADCParamsReceived;
        public event DeviceDataReceivedEventHandler GetCurrentADCReceived;
        public event DeviceDataReceivedEventHandler USSDCommandReceived;
        public event DeviceDataReceivedEventHandler SetActiveZonesOKReceived;
        public event DeviceDataReceivedEventHandler GetActiveZonesReceived;

        public SerialPortListener(System.IO.Ports.SerialPort serialport)
        {
            port = serialport;
            handler = new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
            port.DataReceived += handler;
        }

        protected virtual void OnGetActiveZonesReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetActiveZonesReceived != null)
                GetActiveZonesReceived(sender, e);
        }
        
        protected virtual void OnSetActiveZonesOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetActiveZonesOKReceived != null)
                SetActiveZonesOKReceived(sender, e);
        }
        
        protected virtual void OnUSSDCommandReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (USSDCommandReceived != null)
                USSDCommandReceived(sender, e);
        }
        
        protected virtual void OnGetCurrentADCReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetCurrentADCReceived != null)
                GetCurrentADCReceived(sender, e);
        }
        
        protected virtual void OnGetADCParamsReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetADCParamsReceived != null)
                GetADCParamsReceived(sender, e);
        }
        
        protected virtual void OnSetADCParamsOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetADCParamsOKReceived != null)
                SetADCParamsOKReceived(sender, e);
        }
        
        protected virtual void OnGetCallParamsReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetCallParamsReceived != null)
                GetCallParamsReceived(sender, e);
        }
        
        protected virtual void OnSetCallParamsOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetCallParamsOKReceived != null)
                SetCallParamsOKReceived(sender, e);
        }
        
        protected virtual void OnGetTimeDeactivationReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetTimeDeactivationReceived != null)
                GetTimeDeactivationReceived(sender, e);
        }
        
        protected virtual void OnSetTimeDeactivationOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetTimeDeactivationOKReceived != null)
                SetTimeDeactivationOKReceived(sender, e);
        }
        
        protected virtual void OnDeviceIdReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (DeviceIdReceived != null)
                DeviceIdReceived(sender, e);
        }

        protected virtual void OnDeviceVersionReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (DeviceVersionReceived != null)
                DeviceVersionReceived(sender, e);
        }

        protected virtual void OnSetDefaultsOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetDefaultsOKReceived != null)
                SetDefaultsOKReceived(sender, e);
        }

        protected virtual void OnErrorReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (ErrorReceived != null)
                ErrorReceived(sender, e);
        }

        protected virtual void OnSetUserNumberOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetUserNumberOKReceived != null)
                SetUserNumberOKReceived(sender, e);
        }

        protected virtual void OnGetUserNumberReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetUserNumberReceived != null)
                GetUserNumberReceived(sender, e);
        }

        protected virtual void OnSetTimeActivationOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (SetTimeActivationOKReceived != null)
                SetTimeActivationOKReceived(sender, e);
        }

        protected virtual void OnGetTimeActivationReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (GetTimeActivationReceived != null)
                GetTimeActivationReceived(sender, e);
        }

        public void BeginListening()
        {
            //handler = new System.IO.Ports.SerialDataReceivedEventHandler(port_DataReceived);
            //port.DataReceived += handler;
            //if (!port.IsOpen)
            //{
            //    port.Open();
            //    WasOpen = false;
            //}
            //else
            //    WasOpen = true;
        }

        public void EndListening()
        {
            //port.DataReceived -= handler;
            //if (!WasOpen)
            //{
            //    port.Close();
            //    WasOpen = true;
            //}
        }


        void port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            port.ReadTo("$");
            string message = port.ReadLine();
            if (IsCorrectCRC(message))
            {
                message = message.Substring(0, message.Length - 3);
                switch (message.Substring(0, 5))
                {
                    case "SYHND":
                        OnDeviceIdReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYVER":
                        OnDeviceVersionReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYSFS":
                        if (message.Substring(6, 2) == "OK")
                            OnSetDefaultsOKReceived(this, null);
                        else
                            OnErrorReceived(this, new StringDataReceivedEventArgs("Во время установки параметров по умолчанию произошла ошибка"));
                        break;
                    case "SYSUN":
                        if (message.Substring(6, 2) == "OK")
                            OnSetUserNumberOKReceived(this, null);
                        else
                            OnErrorReceived(this, new StringDataReceivedEventArgs("Во время установки номера пользователя произошла ошибка"));
                        break;
                    case "SYGUN":
                        OnGetUserNumberReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYSTA":
                        if (message.Substring(6, 2) == "OK")
                            OnSetTimeActivationOKReceived(this, null);
                        else
                            OnErrorReceived(this, new StringDataReceivedEventArgs("Во время установки времени активации сигнализации произошла ошибка"));
                        break;
                    case "SYGTA":
                        OnGetTimeActivationReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYSTD":
                        if (message.Substring(6, 2) == "OK")
                            OnSetTimeDeactivationOKReceived(this, null);
                        else
                            OnErrorReceived(this, new StringDataReceivedEventArgs("Во время установки времени деактивации сигнализации произошла ошибка"));
                        break;
                    case "SYGTD":
                        OnGetTimeDeactivationReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYSCS":
                        if (message.Substring(6, 2) == "OK")
                            OnSetCallParamsOKReceived(this, null);
                        else
                            OnErrorReceived(this, new StringDataReceivedEventArgs("Во время установки параметров оповещения произошла ошибка"));
                        break;
                    case "SYGCS":
                        OnGetCallParamsReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYSVA":
                        if (message.Substring(6, 2) == "OK")
                            OnSetADCParamsOKReceived(this, null);
                        else
                            OnErrorReceived(this, new StringDataReceivedEventArgs("Во время установки параметров АЦП произошла ошибка"));
                        break;
                    case "SYGVA":
                        OnGetADCParamsReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYGCV":
                        OnGetCurrentADCReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYUSD":
                        if (message.Substring(6, 5) == "ERROR")
                            OnErrorReceived(this, new StringDataReceivedEventArgs("Во время выполнения USSD-команды произошла ошибка"));
                        else
                            OnUSSDCommandReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                    case "SYSAZ":
                        if (message.Substring(6, 2) == "OK")
                            OnSetActiveZonesOKReceived(this, null);
                        else
                            OnErrorReceived(this, new StringDataReceivedEventArgs("Во время установки активных зон произошла ошибка"));
                        break;
                    case "SYGAZ":
                        OnGetActiveZonesReceived(this, new StringDataReceivedEventArgs(message.Substring(6)));
                        break;
                }
            }
        }

        public static string CalculateCRC(string data)
        {
            byte crc = 0;
            foreach (byte b in Encoding.ASCII.GetBytes(data))
            {
                if (b == 0x3f)
                    crc ^= 0xff;
                else
                    crc ^= b;
            }
            return crc.ToString("X2");
        }

        static bool IsCorrectCRC(string message)
        {
            if (CalculateCRC(message.Substring(0, message.Length - 3)) == message.Substring(message.Length - 2, 2))
                return true;
            else
                return false;
        }

        public bool AreHandlersEmpty
        {
            get
            {
                if (DeviceIdReceived == null)
                    return true;
                else
                    return false;
            }
        }
    }

    public class StringDataReceivedEventArgs : EventArgs
    {
        string message = "";

        public StringDataReceivedEventArgs(string message)
        {
            this.message = message;
        }

        public string Message
        {
            get
            {
                return message;
            }
        }
    }

    public delegate void DeviceDataReceivedEventHandler(object sender, StringDataReceivedEventArgs e);
}
