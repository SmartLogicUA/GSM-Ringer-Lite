using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GsmRingerConfig
{
    public partial class CalibrateForm : Form
    {
        SerialPortListener listener;
        System.Collections.ObjectModel.ReadOnlyCollection<char> digits = new System.Collections.ObjectModel.ReadOnlyCollection<char>(new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' });

        DeviceDataReceivedEventHandler ErrorReceivedHandler;
        DeviceDataReceivedEventHandler ADCParamsReceivedHandler;
        DeviceDataReceivedEventHandler CurrentADCReceivedHandler;
        DeviceDataReceivedEventHandler SetADCParamsOKReceivedHandler;

        SetControlValue setValue;
        
        public CalibrateForm()
        {
            InitializeComponent();
        }

        private void CalibrateForm_Load(object sender, EventArgs e)
        {
            listener = (this.Owner as GsmConfig).Listener;
            try
            {
                ErrorReceivedHandler = new DeviceDataReceivedEventHandler(listener_ErrorReceived);
                listener.ErrorReceived += ErrorReceivedHandler;

                ADCParamsReceivedHandler = new DeviceDataReceivedEventHandler(listener_GetADCParamsReceived);
                listener.GetADCParamsReceived += ADCParamsReceivedHandler;

                CurrentADCReceivedHandler = new DeviceDataReceivedEventHandler(listener_GetCurrentADCReceived);
                listener.GetCurrentADCReceived += CurrentADCReceivedHandler;
                SetADCParamsOKReceivedHandler = new DeviceDataReceivedEventHandler(listener_SetADCParamsOKReceived);
                listener.SetADCParamsOKReceived += SetADCParamsOKReceivedHandler;

                setValue = SetValue;

                MessageBox.Show("Внимание! Перед калибровкой убедитесь, что все контролируемые точки находятся в закрытом состоянии", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
                MessageBox.Show("Сначала выберите порт", "Порт не выбран", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
#pragma warning restore 168
            toolTip1.SetToolTip(readCurrADCBtn, "Считать текущие значения зон");
            toolTip1.SetToolTip(readZone1Params, "Считать");
            toolTip1.SetToolTip(readZone2Params, "Считать");
            toolTip1.SetToolTip(readZone3Params, "Считать");
            toolTip1.SetToolTip(readZone4Params, "Считать");
            toolTip1.SetToolTip(writeZone1Params, "Записать");
            toolTip1.SetToolTip(writeZone2Params, "Записать");
            toolTip1.SetToolTip(writeZone3Params, "Записать");
            toolTip1.SetToolTip(writeZone4Params, "Записать");
        }

        void listener_SetADCParamsOKReceived(object sender, StringDataReceivedEventArgs e)
        {
            MessageBox.Show("Параметры калибровки успешно записаны", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void listener_GetCurrentADCReceived(object sender, StringDataReceivedEventArgs e)
        {
            this.Invoke(setValue, zone1CurrBox, Convert.ToInt32(e.Message.Substring(0, 2), 16).ToString());
            this.Invoke(setValue, zone2CurrBox, Convert.ToInt32(e.Message.Substring(2, 2), 16).ToString());
            this.Invoke(setValue, zone3CurrBox, Convert.ToInt32(e.Message.Substring(4, 2), 16).ToString());
            this.Invoke(setValue, zone4CurrBox, Convert.ToInt32(e.Message.Substring(6, 2), 16).ToString());
        }

        void listener_GetADCParamsReceived(object sender, StringDataReceivedEventArgs e)
        {
            switch (e.Message[0])
            {
                case '0':
                    this.Invoke(setValue, zone1MinBox, Convert.ToInt32(e.Message.Substring(1, 2), 16).ToString());
                    this.Invoke(setValue, zone1MaxBox, Convert.ToInt32(e.Message.Substring(3, 2), 16).ToString());
                    break;
                case '1':
                    this.Invoke(setValue, zone2MinBox, Convert.ToInt32(e.Message.Substring(1, 2), 16).ToString());
                    this.Invoke(setValue, zone2MaxBox, Convert.ToInt32(e.Message.Substring(3, 2), 16).ToString());
                    break;
                case '2':
                    this.Invoke(setValue, zone3MinBox, Convert.ToInt32(e.Message.Substring(1, 2), 16).ToString());
                    this.Invoke(setValue, zone3MaxBox, Convert.ToInt32(e.Message.Substring(3, 2), 16).ToString());
                    break;
                case '3':
                    this.Invoke(setValue, zone4MinBox, Convert.ToInt32(e.Message.Substring(1, 2), 16).ToString());
                    this.Invoke(setValue, zone4MaxBox, Convert.ToInt32(e.Message.Substring(3, 2), 16).ToString());
                    break;
            }
        }

        void listener_ErrorReceived(object sender, StringDataReceivedEventArgs e)
        {
            MessageBox.Show(e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void CalibrateForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                listener.ErrorReceived -= ErrorReceivedHandler;
                listener.GetADCParamsReceived -= ADCParamsReceivedHandler;
                listener.GetCurrentADCReceived -= CurrentADCReceivedHandler;
                listener.SetADCParamsOKReceived -= SetADCParamsOKReceivedHandler;
            }
#pragma warning disable 168
            catch (NullReferenceException except)
            {
            }
#pragma warning restore 168
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void zone1MinBox_TextChanged(object sender, EventArgs e)
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

        void SetValue(Control control, string value)
        {
            //if (control is CheckBox)
            //    (control as CheckBox).Checked = bool.Parse(value);
            //else if (control is Label)
            //    (control as Label).Text = value;
            if (control is TextBox)
                (control as TextBox).Text = value;
            //else if (control is MaskedTextBox)
            //    (control as MaskedTextBox).Text = value;
            else
                throw new ArgumentException("Не получается распознать тип управляющего элемента");
            listener.EndListening();
        }

        private void readCurrADCBtn_Click(object sender, EventArgs e)
        {
            GsmConfig main = this.Owner as GsmConfig;
            main.GetCurrentADC();
        }

        private void readZone1Params_Click(object sender, EventArgs e)
        {
            GsmConfig main = this.Owner as GsmConfig;
            main.GetADCParams(0);
        }

        private void readZone2Params_Click(object sender, EventArgs e)
        {
            GsmConfig main = this.Owner as GsmConfig;
            main.GetADCParams(1);
        }

        private void readZone3Params_Click(object sender, EventArgs e)
        {
            GsmConfig main = this.Owner as GsmConfig;
            main.GetADCParams(2);
        }

        private void readZone4Params_Click(object sender, EventArgs e)
        {
            GsmConfig main = this.Owner as GsmConfig;
            main.GetADCParams(3);
        }

        private void writeZone1Params_Click(object sender, EventArgs e)
        {
            int min, max;
            if (int.TryParse(zone1MinBox.Text, out min) && int.TryParse(zone1MaxBox.Text, out max))
            {
                if (min >= 0 && min < 256 && max >= 0 && max < 256)
                {
                    GsmConfig main = this.Owner as GsmConfig;
                    main.SetADCParams(0, min, max);
                }
                else
                    MessageBox.Show("Вводимые значения должны быть от 0 до 255","Ошибка ввода",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void writeZone2Params_Click(object sender, EventArgs e)
        {
            int min, max;
            if (int.TryParse(zone2MinBox.Text, out min) && int.TryParse(zone2MaxBox.Text, out max))
            {
                if (min >= 0 && min < 256 && max >= 0 && max < 256)
                {
                    GsmConfig main = this.Owner as GsmConfig;
                    main.SetADCParams(1, min, max);
                }
                else
                    MessageBox.Show("Вводимые значения должны быть от 0 до 255", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void writeZone3Params_Click(object sender, EventArgs e)
        {
            int min, max;
            if (int.TryParse(zone3MinBox.Text, out min) && int.TryParse(zone3MaxBox.Text, out max))
            {
                if (min >= 0 && min < 256 && max >= 0 && max < 256)
                {
                    GsmConfig main = this.Owner as GsmConfig;
                    main.SetADCParams(2, min, max);
                }
                else
                    MessageBox.Show("Вводимые значения должны быть от 0 до 255", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void writeZone4Params_Click(object sender, EventArgs e)
        {
            int min, max;
            if (int.TryParse(zone4MinBox.Text, out min) && int.TryParse(zone4MaxBox.Text, out max))
            {
                if (min >= 0 && min < 256 && max >= 0 && max < 256)
                {
                    GsmConfig main = this.Owner as GsmConfig;
                    main.SetADCParams(3, min, max);
                }
                else
                    MessageBox.Show("Вводимые значения должны быть от 0 до 255", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void autoCalculateBtn_Click(object sender, EventArgs e)
        {
            int currNum;
            int min;
            int max;
            if (int.TryParse(zone1CurrBox.Text, out currNum))
            {
                min = currNum - (currNum / 10);
                max = currNum + (currNum / 10);
                if (min < 0)
                    min = 0;
                if (max > 255)
                    max = 255;
                zone1MinBox.Text = min.ToString();
                zone1MaxBox.Text = max.ToString();
            }

            if (int.TryParse(zone2CurrBox.Text, out currNum))
            {
                min = currNum - (currNum / 10);
                max = currNum + (currNum / 10);
                if (min < 0)
                    min = 0;
                if (max > 255)
                    max = 255;
                zone2MinBox.Text = min.ToString();
                zone2MaxBox.Text = max.ToString();
            }

            if (int.TryParse(zone3CurrBox.Text, out currNum))
            {
                min = currNum - (currNum / 10);
                max = currNum + (currNum / 10);
                if (min < 0)
                    min = 0;
                if (max > 255)
                    max = 255;
                zone3MinBox.Text = min.ToString();
                zone3MaxBox.Text = max.ToString();
            }

            if (int.TryParse(zone4CurrBox.Text, out currNum))
            {
                min = currNum - (currNum / 10);
                max = currNum + (currNum / 10);
                if (min < 0)
                    min = 0;
                if (max > 255)
                    max = 255;
                zone4MinBox.Text = min.ToString();
                zone4MaxBox.Text = max.ToString();
            }
        }
    }
}
