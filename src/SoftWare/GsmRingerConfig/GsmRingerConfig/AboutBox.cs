using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GsmRingerConfig
{
    public partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();
            linkLabel1.LinkArea = new LinkArea(0, linkLabel1.Text.Length);
            linkLabel1.Links[0] = new LinkLabel.Link(0, linkLabel1.Text.Length, "www.smartlogic.com.ua");
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Link.LinkData as string);
        }
    }
}
