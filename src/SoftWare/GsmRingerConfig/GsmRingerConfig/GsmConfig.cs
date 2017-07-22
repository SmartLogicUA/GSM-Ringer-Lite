using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GsmRingerConfig
{
    public partial class GsmConfig : Form
    {
        static System.IO.Ports.SerialPort port;
        static SerialPortListener listener;
        static int commandCount = 0;
        static bool wasError = false;
        static bool isMultiCommand = false;

        DeviceDataReceivedEventHandler currentADCReceivedHandler;

        SetControlValue setValue;

        System.Collections.ObjectModel.ReadOnlyCollection<char> digits = new System.Collections.ObjectModel.ReadOnlyCollection<char>(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

        CalibrateForm manualCalibrateForm;

        DeviceDataReceivedEventHandler SetActiveZonesOKReceivedHandler;
        DeviceDataReceivedEventHandler SetCallParamsOKReceivedHandler;
        DeviceDataReceivedEventHandler SetTimeActivationOKReceivedHandler;
        DeviceDataReceivedEventHandler SetTimeDeactivationOKReceivedHandler;
        DeviceDataReceivedEventHandler SetUserNumberOKReceivedHandler;
        
        public GsmConfig()
        {
            InitializeComponent();
        }

        private void GsmConfig_Load(object sender, EventArgs e)
        {
            try
            {
                setValue = SetValue;
                portNameBox.Items.AddRange(System.IO.Ports.SerialPort.GetPortNames());
                portNameBox.SelectedIndex = 0;
                currentADCReceivedHandler = new DeviceDataReceivedEventHandler(listener_GetCurrentADCReceived);
                //deviceIdLbl.Text = "00000";
                toolTip1.SetToolTip(readAllBtn, "Считать все");
                toolTip1.SetToolTip(readActivationTimeBtn, "Считать");
                toolTip1.SetToolTip(readCallParamsBtn, "Считать");
                toolTip1.SetToolTip(readDeactivationTimeBtn, "Считать");
                toolTip1.SetToolTip(readDeviceIdBtn, "Считать");
                toolTip1.SetToolTip(readUser1NumBtn, "Считать");
                toolTip1.SetToolTip(readUser2NumBtn, "Считать");
                toolTip1.SetToolTip(readUser3NumBtn, "Считать");
                toolTip1.SetToolTip(readUser4NumBtn, "Считать");
                toolTip1.SetToolTip(readUser5NumBtn, "Считать");
                toolTip1.SetToolTip(writeActivationTimeBtn, "Записать");
                toolTip1.SetToolTip(writeAllBtn, "Записать все");
                toolTip1.SetToolTip(writeCallParamsBtn, "Записать");
                toolTip1.SetToolTip(writeDeactivationTimeBtn, "Записать");
                toolTip1.SetToolTip(writeUser1NumBtn, "Записать");
                toolTip1.SetToolTip(writeUser2NumBtn, "Записать");
                toolTip1.SetToolTip(writeUser3NumBtn, "Записать");
                toolTip1.SetToolTip(writeUser4NumBtn, "Записать");
                toolTip1.SetToolTip(writeUser5NumBtn, "Записать");
                toolTip1.SetToolTip(readActiveZonesBtn, "Считать");
                toolTip1.SetToolTip(writeActiveZonesBtn, "Записать");

                SetActiveZonesOKReceivedHandler = new DeviceDataReceivedEventHandler(listener_SetActiveZonesOKReceived);
                SetCallParamsOKReceivedHandler = new DeviceDataReceivedEventHandler(listener_SetCallParamsOKReceived);
                SetTimeActivationOKReceivedHandler = new DeviceDataReceivedEventHandler(listener_SetTimeActivationOKReceived);
                SetTimeDeactivationOKReceivedHandler = new DeviceDataReceivedEventHandler(listener_SetTimeDeactivationOKReceived);
                SetUserNumberOKReceivedHandler = new DeviceDataReceivedEventHandler(listener_SetUserNumberOKReceived);
            }
#pragma warning disable 168
            catch (ArgumentOutOfRangeException except)
            {
                MessageBox.Show("Не доступен не один последовательный порт.\r\nПриложение будет закрыто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
#pragma warning restore 168
        }
        
        void listener_GetUserNumberReceived(object sender, StringDataReceivedEventArgs e)
        {
            switch (e.Message[0])
            {
                case '0':
                    this.Invoke(setValue, user1NumBox, e.Message.Substring(1));
                    break;
                case '1':
                    this.Invoke(setValue, user2NumBox, e.Message.Substring(1));
                    break;
                case '2':
                    this.Invoke(setValue, user3NumBox, e.Message.Substring(1));
                    break;
                case '3':
                    this.Invoke(setValue, user4NumBox, e.Message.Substring(1));
                    break;
                case '4':
                    this.Invoke(setValue, user5NumBox, e.Message.Substring(1));
                    break;
            }
        }

        void listener_GetTimeDeactivationReceived(object sender, StringDataReceivedEventArgs e)
        {
            int time = int.Parse(e.Message, System.Globalization.NumberStyles.HexNumber);
            this.Invoke(setValue, deactivationTimeBox, time.ToString());
            listener.EndListening();
        }

        void listener_GetTimeActivationReceived(object sender, StringDataReceivedEventArgs e)
        {
            int time = int.Parse(e.Message, System.Globalization.NumberStyles.HexNumber);
            this.Invoke(setValue, activationTimeBox, time.ToString());
            listener.EndListening();
        }

        void SetValue(Control control, string value)
        {
            if (control is CheckBox)
                (control as CheckBox).Checked = bool.Parse(value);
            else if (control is Label)
                (control as Label).Text = value;
            else if (control is TextBox)
                (control as TextBox).Text = value;
            else if (control is MaskedTextBox)
                (control as MaskedTextBox).Text = value;
            else
                throw new ArgumentException("Не получается распознать тип управляющего элемента");
            listener.EndListening();
        }
        
        void listener_GetCallParamsReceived(object sender, StringDataReceivedEventArgs e)
        {
            byte smsByte = byte.Parse(e.Message.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte callByte = byte.Parse(e.Message.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);

            if ((smsByte & 1) == 0)
                this.Invoke(setValue, smsUser1CheckBox, "false");
            else
                this.Invoke(setValue, smsUser1CheckBox, "true");

            if ((smsByte & 2) == 0)
                this.Invoke(setValue, smsUser2CheckBox, "false");
            else
                this.Invoke(setValue, smsUser2CheckBox, "true");

            if ((smsByte & 4) == 0)
                this.Invoke(setValue, smsUser3CheckBox, "false");
            else
                this.Invoke(setValue, smsUser3CheckBox, "true");

            if ((smsByte & 8) == 0)
                this.Invoke(setValue, smsUser4CheckBox, "false");
            else
                this.Invoke(setValue, smsUser4CheckBox, "true");

            if ((smsByte & 16) == 0)
                this.Invoke(setValue, smsUser5CheckBox, "false");
            else
                this.Invoke(setValue, smsUser5CheckBox, "true");

            if ((callByte & 1) == 0)
                this.Invoke(setValue, callUser1CheckBox, "false");
            else
                this.Invoke(setValue, callUser1CheckBox, "true");

            if ((callByte & 2) == 0)
                this.Invoke(setValue, callUser2CheckBox, "false");
            else
                this.Invoke(setValue, callUser2CheckBox, "true");

            if ((callByte & 4) == 0)
                this.Invoke(setValue, callUser3CheckBox, "false");
            else
                this.Invoke(setValue, callUser3CheckBox, "true");

            if ((callByte & 8) == 0)
                this.Invoke(setValue, callUser4CheckBox, "false");
            else
                this.Invoke(setValue, callUser4CheckBox, "true");

            if ((callByte & 16) == 0)
                this.Invoke(setValue, callUser5CheckBox, "false");
            else
                this.Invoke(setValue, callUser5CheckBox, "true");
            listener.EndListening();
        }

        void listener_ErrorReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (isMultiCommand)
            {
                commandCount++;
                wasError = true;
                if (commandCount > 8)
                {
                    EndOfMultiCommand();
                }
            }
            else
                MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listener.EndListening();
        }

        void listener_DeviceVersionReceived(object sender, StringDataReceivedEventArgs e)
        {
            MessageBox.Show(e.Message, "Версия устройства", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listener.EndListening();
        }

        void listener_DeviceIdReceived(object sender, StringDataReceivedEventArgs e)
        {
            //deviceIdLbl.Text = e.Message;
            this.Invoke(setValue, deviceIdLbl, e.Message);
            listener.EndListening();
        }

        private void SendCommand(string cmd)
        {
            try
            {
                if (!port.IsOpen)
                    port.Open();
                listener.BeginListening();
                port.WriteLine("$" + cmd + "*" + SerialPortListener.CalculateCRC(cmd));
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                //MessageBox.Show("Сначала выберите порт", "Порт не выбран", MessageBoxButtons.OK, MessageBoxIcon.Error);
                listener_ErrorReceived(this, new StringDataReceivedEventArgs("Сначала выберите порт"));
            }
#pragma warning restore 168
            catch (Exception except)
            {
                //MessageBox.Show(except.Message, except.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                listener_ErrorReceived(this, new StringDataReceivedEventArgs(except.Message));
            }
        }

        private void GetDeviceId()
        {
            //listener.BeginListening();
            SendCommand("PCHND");
        }

        private void GetDeviceVersion()
        {
            SendCommand("PCVER");
        }

        private void SetDefaults()
        {
            SendCommand("PCSFS");
        }

        public void SetADCParams(int zoneNum, int minVal, int maxVal)
        {
            SendCommand("PCSVA " + zoneNum.ToString() + minVal.ToString("X2") + maxVal.ToString("X2"));
        }

        public void GetADCParams(int zoneNum)
        {
            SendCommand("PCGVA " + zoneNum.ToString());
        }

        public void GetCurrentADC()
        {
            SendCommand("PCGCV");
        }

        private void SendUSSDCommand(string cmd)
        {
            SendCommand("PCUSD " + cmd.Replace('*', '@') + Encoding.ASCII.GetString(new byte[] { 0x00 }));
        }
        
        private void SetUserNumber(int usrNum, string num)
        {
            SendCommand("PCSUN " + usrNum.ToString() + num);
        }

        private void GetUserNumber(int usrNum)
        {
            SendCommand("PCGUN " + usrNum.ToString());
        }

        private void SetActivationTime(string time)
        {
            SendCommand("PCSTA " + time);
        }

        private void GetActivationTime()
        {
            SendCommand("PCGTA");
        }

        private void SetDeactivationTime(string time)
        {
            SendCommand("PCSTD " + time);
        }

        private void GetDeactivationTime()
        {
            SendCommand("PCGTD");
        }

        private void SetCallParams(string param)
        {
            SendCommand("PCSCS " + param);
        }

        private void GetCallParams()
        {
            SendCommand("PCGCS");
        }

        private void readDeviceIdBtn_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(user1NumBox.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", ""));
            GetDeviceId();
        }

        private void readUser1NumBtn_Click(object sender, EventArgs e)
        {
            GetUserNumber(0);
        }

        private void readUser2NumBtn_Click(object sender, EventArgs e)
        {
            GetUserNumber(1);
        }

        private void readUser3NumBtn_Click(object sender, EventArgs e)
        {
            GetUserNumber(2);
        }

        private void readUser4NumBtn_Click(object sender, EventArgs e)
        {
            GetUserNumber(3);
        }

        private void readUser5NumBtn_Click(object sender, EventArgs e)
        {
            GetUserNumber(4);
        }

        private void writeUser1NumBtn_Click(object sender, EventArgs e)
        {
            string num = user1NumBox.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (num.Length == 10)
                SetUserNumber(0, num);
            else
                MessageBox.Show(String.Format("Номер {0} введен неправильно", 1), "Ошибка ввода номера", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void writeUser2NumBtn_Click(object sender, EventArgs e)
        {
            string num = user2NumBox.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (num.Length == 10)
                SetUserNumber(1, num);
            else
                MessageBox.Show(String.Format("Номер {0} введен неправильно", 2), "Ошибка ввода номера", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void writeUser3NumBtn_Click(object sender, EventArgs e)
        {
            string num = user3NumBox.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (num.Length == 10)
                SetUserNumber(2, num);
            else
                MessageBox.Show(String.Format("Номер {0} введен неправильно", 3), "Ошибка ввода номера", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void writeUser4NumBtn_Click(object sender, EventArgs e)
        {
            string num = user4NumBox.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (num.Length == 10)
                SetUserNumber(3, num);
            else
                MessageBox.Show(String.Format("Номер {0} введен неправильно", 4), "Ошибка ввода номера", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void writeUser5NumBtn_Click(object sender, EventArgs e)
        {
            string num = user5NumBox.Text.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            if (num.Length == 10)
                SetUserNumber(4, num);
            else
                MessageBox.Show(String.Format("Номер {0} введен неправильно", 5), "Ошибка ввода номера", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void readActivationTimeBtn_Click(object sender, EventArgs e)
        {
            GetActivationTime();
        }

        private void writeActivationTimeBtn_Click(object sender, EventArgs e)
        {
            int time;
            if (int.TryParse(activationTimeBox.Text, out time))
            {
                if (time >= 1 && time <= 255)
                    SetActivationTime(time.ToString("X2"));
                else
                    MessageBox.Show("Введите значение от 1 до 255", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Время отсрочки активации не введено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void readDeactivationTimeBtn_Click(object sender, EventArgs e)
        {
            GetDeactivationTime();
        }

        private void writeDeactivationTimeBtn_Click(object sender, EventArgs e)
        {
            int time;
            if (int.TryParse(deactivationTimeBox.Text, out time))
            {
                if (time >= 1 && time <= 255)
                    SetDeactivationTime(time.ToString("X2"));
                else
                    MessageBox.Show("Введите значение от 1 до 255", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Время отсрочки оповещения не введено", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void writeCallParamsBtn_Click(object sender, EventArgs e)
        {
            byte smsByte = 0;
            if (smsUser1CheckBox.Checked)
                smsByte |= 1;
            if (smsUser2CheckBox.Checked)
                smsByte |= 2;
            if (smsUser3CheckBox.Checked)
                smsByte |= 4;
            if (smsUser4CheckBox.Checked)
                smsByte |= 8;
            if (smsUser5CheckBox.Checked)
                smsByte |= 16;

            byte callByte = 0;
            if (callUser1CheckBox.Checked)
                callByte |= 1;
            if (callUser2CheckBox.Checked)
                callByte |= 2;
            if (callUser3CheckBox.Checked)
                callByte |= 4;
            if (callUser4CheckBox.Checked)
                callByte |= 8;
            if (callUser5CheckBox.Checked)
                callByte |= 16;

            SetCallParams(smsByte.ToString("X2") + callByte.ToString("X2"));
        }

        private void readCallParamsBtn_Click(object sender, EventArgs e)
        {
            GetCallParams();
        }

        private void readAllBtn_Click(object sender, EventArgs e)
        {
            readDeviceIdBtn_Click(null, null);
            readCallParamsBtn_Click(null, null);
            readUser1NumBtn_Click(null, null);
            readUser2NumBtn_Click(null, null);
            readUser3NumBtn_Click(null, null);
            readUser4NumBtn_Click(null, null);
            readUser5NumBtn_Click(null, null);
            readActivationTimeBtn_Click(null, null);
            readDeactivationTimeBtn_Click(null, null);
            readActiveZonesBtn_Click(null, null);
        }

        private void writeAllBtn_Click(object sender, EventArgs e)
        {
            isMultiCommand = true;
            wasError = false;
            commandCount = 0;
            writeCallParamsBtn_Click(null, null);
            writeUser1NumBtn_Click(null, null);
            writeUser2NumBtn_Click(null, null);
            writeUser3NumBtn_Click(null, null);
            writeUser4NumBtn_Click(null, null);
            writeUser5NumBtn_Click(null, null);
            writeActivationTimeBtn_Click(null, null);
            writeDeactivationTimeBtn_Click(null, null);
            writeActiveZonesBtn_Click(null, null);
        }

        private void portNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                port = new System.IO.Ports.SerialPort(portNameBox.SelectedItem as string, 9600, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One);
                port.NewLine = "\r\n";
                listener = new SerialPortListener(port);
                if (listener.AreHandlersEmpty)
                    SetListenerProperties();
                //port.Open();
            }
            catch (Exception except)
            {
                //MessageBox.Show(except.Message, except.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                listener_ErrorReceived(this, new StringDataReceivedEventArgs(except.Message));
            }
        }

        void SetListenerProperties()
        {
            listener.DeviceIdReceived += new DeviceDataReceivedEventHandler(listener_DeviceIdReceived);
            listener.DeviceVersionReceived += new DeviceDataReceivedEventHandler(listener_DeviceVersionReceived);
            listener.ErrorReceived += new DeviceDataReceivedEventHandler(listener_ErrorReceived);
            listener.GetCallParamsReceived += new DeviceDataReceivedEventHandler(listener_GetCallParamsReceived);
            listener.GetTimeActivationReceived += new DeviceDataReceivedEventHandler(listener_GetTimeActivationReceived);
            listener.GetTimeDeactivationReceived += new DeviceDataReceivedEventHandler(listener_GetTimeDeactivationReceived);
            listener.GetUserNumberReceived += new DeviceDataReceivedEventHandler(listener_GetUserNumberReceived);
            listener.USSDCommandReceived += new DeviceDataReceivedEventHandler(listener_USSDCommandReceived);
            listener.SetDefaultsOKReceived += new DeviceDataReceivedEventHandler(listener_SetDefaultsOKReceived);
            listener.GetActiveZonesReceived += new DeviceDataReceivedEventHandler(listener_GetActiveZonesReceived);
            //listener.SetActiveZonesOKReceived += new DeviceDataReceivedEventHandler(listener_SetActiveZonesOKReceived);
            //listener.SetCallParamsOKReceived += new DeviceDataReceivedEventHandler(listener_SetCallParamsOKReceived);
            //listener.SetTimeActivationOKReceived += new DeviceDataReceivedEventHandler(listener_SetTimeActivationOKReceived);
            //listener.SetTimeDeactivationOKReceived += new DeviceDataReceivedEventHandler(listener_SetTimeDeactivationOKReceived);
            //listener.SetUserNumberOKReceived += new DeviceDataReceivedEventHandler(listener_SetUserNumberOKReceived);
            listener.SetActiveZonesOKReceived += SetActiveZonesOKReceivedHandler;
            listener.SetCallParamsOKReceived += SetCallParamsOKReceivedHandler;
            listener.SetTimeActivationOKReceived += SetTimeActivationOKReceivedHandler;
            listener.SetTimeDeactivationOKReceived += SetTimeDeactivationOKReceivedHandler;
            listener.SetUserNumberOKReceived += SetUserNumberOKReceivedHandler;
        }

        void listener_SetUserNumberOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            //WriteLog("Номер пользователя успешно установлен");
            if (isMultiCommand)
            {
                commandCount++;
                if (commandCount > 8)
                {
                    EndOfMultiCommand();
                }
            }
            else
                MessageBox.Show("Номер пользователя успешно установлен", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void listener_SetTimeDeactivationOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (isMultiCommand)
            {
                commandCount++;
                if (commandCount > 8)
                {
                    EndOfMultiCommand();
                }
            }
            else
                MessageBox.Show("Время отсрочки оповещения успешно установлено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void listener_SetTimeActivationOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (isMultiCommand)
            {
                commandCount++;
                if (commandCount > 8)
                {
                    EndOfMultiCommand();
                }
            }
            else
                MessageBox.Show("Время отсрочки активации успешно установлено", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void listener_SetCallParamsOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (isMultiCommand)
            {
                commandCount++;
                if (commandCount > 8)
                {
                    EndOfMultiCommand();
                }
            }
            else
                MessageBox.Show("Способы дозвона успешно установлены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void listener_SetActiveZonesOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            if (isMultiCommand)
            {
                commandCount++;
                if (commandCount > 8)
                {
                    EndOfMultiCommand();
                }
            }
            else
                MessageBox.Show("Активные зоны успешно установлены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void EndOfMultiCommand()
        {
            if (wasError)
            {
                isMultiCommand = false;
                MessageBox.Show("Во время выполнения команды произошла ошибка", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                isMultiCommand = false;
                MessageBox.Show("Все команды выполнены успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        void listener_GetActiveZonesReceived(object sender, StringDataReceivedEventArgs e)
        {
            byte paramByte = byte.Parse(e.Message, System.Globalization.NumberStyles.HexNumber);
            //if (byte.TryParse(e.Message,System.Globalization.NumberStyles.HexNumber,null,out paramByte))
            //{
                if ((paramByte & 1) == 0)
                    this.Invoke(setValue, zone1EnableCheckBox, "false");
                else
                    this.Invoke(setValue, zone1EnableCheckBox, "true");
                if ((paramByte & 2) == 0)
                    this.Invoke(setValue, zone2EnableCheckBox, "false");
                else
                    this.Invoke(setValue, zone2EnableCheckBox, "true");
                if ((paramByte & 4) == 0)
                    this.Invoke(setValue, zone3EnableCheckBox, "false");
                else
                    this.Invoke(setValue, zone3EnableCheckBox, "true");
                if ((paramByte & 8) == 0)
                    this.Invoke(setValue, zone4EnableCheckBox, "false");
                else
                    this.Invoke(setValue, zone4EnableCheckBox, "true");
            //}
            //else
            //    MessageBox.Show("Устройство вернуло неверные данные. Проверьте правильность подключения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void listener_SetDefaultsOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            MessageBox.Show("Параметры по умолчанию успешно установлены", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void listener_USSDCommandReceived(object sender, StringDataReceivedEventArgs e)
        {
            MessageBox.Show(e.Message.Replace('@', '*'), "USSD", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void readDeviceVersionBtn_Click(object sender, EventArgs e)
        {
            GetDeviceVersion();
        }

        private void activationTimeBox_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb.TextLength > 0)
            {
                if (!digits.Contains(tb.Text[tb.TextLength - 1]))
                {
                    string tmp = tb.Text.Substring(0, tb.Text.Length - 1);
                    tb.Text = "";
                    tb.AppendText(tmp);
                }
            }

            if (tb.TextLength > 3)
            {
                string tmp = tb.Text.Substring(0, 3);
                tb.Text = "";
                tb.AppendText(tmp);
            }
        }

        private void sendUSSDBtn_Click(object sender, EventArgs e)
        {
            SendUSSDCommand(ussdBox.Text);
        }

        public SerialPortListener Listener
        {
            get
            {
                return listener;
            }
        }

        private void manualCalibrateBtn_Click(object sender, EventArgs e)
        {
            if (listener == null)
                MessageBox.Show("Сначала выберите порт", "Порт не выбран", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                manualCalibrateForm = new CalibrateForm();
                manualCalibrateForm.ShowDialog(this);
            }
        }

        private void setCalibrateDefaultsBtn_Click(object sender, EventArgs e)
        {
            SetADCParams(0, 0x95, 0xdf);
            SetADCParams(1, 0x95, 0xdf);
            SetADCParams(2, 0x95, 0xdf);
            SetADCParams(3, 0x95, 0xdf);
        }

        private void setDefaultsBtn_Click(object sender, EventArgs e)
        {
            SetDefaults();
        }

        private void autoCalibrateBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Убедитесь, что все контролируемые точки находятся в закрытом состоянии", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    listener.GetCurrentADCReceived += currentADCReceivedHandler;
                    GetCurrentADC();
                }
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                MessageBox.Show("Сначала выберите порт", "Порт не выбран", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
#pragma warning restore 168
        }

        void listener_GetCurrentADCReceived(object sender, StringDataReceivedEventArgs e)
        {
            int[] cur = new int[4];
            int[] min = new int[4];
            int[] max = new int[4];

            if (int.TryParse(e.Message.Substring(0, 2), System.Globalization.NumberStyles.HexNumber, null, out cur[0]) && int.TryParse(e.Message.Substring(2, 2), System.Globalization.NumberStyles.HexNumber, null, out cur[1]) && int.TryParse(e.Message.Substring(4, 2), System.Globalization.NumberStyles.HexNumber, null, out cur[2]) && int.TryParse(e.Message.Substring(6, 2), System.Globalization.NumberStyles.HexNumber, null, out cur[3]))
            {
                for (int i = 0; i < 4; i++)
                {
                    min[i] = cur[i] - (cur[i] / 10);
                    max[i] = cur[i] + (cur[i] / 10);
                    if (min[i] < 0)
                        min[i] = 0;
                    if (max[i] > 255)
                        max[i] = 255;
                    SetADCParams(i, min[i], max[i]);
                }
                MessageBox.Show("Калибровка завершена", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Устройство вернуло неверные данные. Проверьте правильность подключения", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listener.EndListening();
            listener.GetCurrentADCReceived -= currentADCReceivedHandler;
        }

        private void writeActiveZonesBtn_Click(object sender, EventArgs e)
        {
            byte paramByte = 0;
            if (zone1EnableCheckBox.Checked)
                paramByte |= 1;
            if (zone2EnableCheckBox.Checked)
                paramByte |= 2;
            if (zone3EnableCheckBox.Checked)
                paramByte |= 4;
            if (zone4EnableCheckBox.Checked)
                paramByte |= 8;
            SetActiveZones(paramByte.ToString("X1"));
        }

        private void SetActiveZones(string param)
        {
            SendCommand("PCSAZ " + param);
        }

        private void readActiveZonesBtn_Click(object sender, EventArgs e)
        {
            GetActiveZones();
        }

        private void GetActiveZones()
        {
            SendCommand("PCGAZ");
        }

        private void GsmConfig_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            (new AboutBox()).Show();
            e.Cancel = true;
        }
    }

    delegate void SetControlValue(Control control, string value);
    delegate void VoidString(string value);
}
