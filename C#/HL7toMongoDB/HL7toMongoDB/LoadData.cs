/*
Author: Jayant Singh
Website: www.j4jayant.com
Description: This is a UI to use this program
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;

namespace j4jayant.HL7toMongoDB
{
    /// <summary>
    /// UI Class to provide inputs & display result
    /// </summary>
    public partial class LoadData : Form
    {
        Thread loadThread = null;

        public LoadData()
        {
            InitializeComponent();
        }

        private void LoadData_Load(object sender, EventArgs e)
        {
            txtMongoServer.Text = "localhost";
            txtMongoPort.Text = "27017";
        }

        private void txtMongoServer_Enter(object sender, EventArgs e)
        {
            if (txtMongoServer.Text == "localhost")
                txtMongoServer.Text = "";
        }

        private void txtMongoServer_Leave(object sender, EventArgs e)
        {
            if (txtMongoServer.Text.Trim() == "")
                txtMongoServer.Text = "localhost";
        }

        private void txtMongoPort_Enter(object sender, EventArgs e)
        {
            if (txtMongoPort.Text == "27017")
                txtMongoPort.Text = "";
        }

        private void txtMongoPort_Leave(object sender, EventArgs e)
        {
            if (txtMongoPort.Text.Trim() == "")
                txtMongoPort.Text = "27017";
        }

        void FileBrowse()
        {
            try
            {
                OpenFileDialog browseFile = new OpenFileDialog();

                DialogResult result = browseFile.ShowDialog(); 
                if (result == DialogResult.OK) 
                {
                    this.txtMsgPath.Text = browseFile.FileName;
                }
            }
            catch (Exception ex)
            {
                UpdateActivity_Error("Error: " + ex.Message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FileBrowse();
        }

        public void UpdateLabelCurrent(int count)
        {
            Invoke((MethodInvoker)delegate
            {
                lblCurrent.Text = count.ToString();
                pbStatus.Value = count;
            });
        }
        public void UpdateLabelTotal(int count)
        {
            Invoke((MethodInvoker)delegate
            {
                lblTotal.Text = count.ToString();
                pbStatus.Maximum = count;
            });
        }
        public void UpdateActivity_Message(string text)
        {
            Invoke((MethodInvoker)delegate
            {
                txtActivity.AppendText(Environment.NewLine + "<<<Message>>>" + Environment.NewLine + text + Environment.NewLine);
                txtActivity.ScrollToCaret();
            });
        }

        public void UpdateActivity_Error(string text)
        {
            Invoke((MethodInvoker)delegate
            {
                txtActivity.SelectionStart = txtActivity.TextLength;
                txtActivity.SelectionLength = 0;
                txtActivity.SelectionColor = Color.Red;
                txtActivity.AppendText(Environment.NewLine + text + Environment.NewLine);
                txtActivity.SelectionColor = txtActivity.ForeColor;
                txtActivity.ScrollToCaret();
            });
        }

        public void UpdateActivity_Info(string text)
        {
            Invoke((MethodInvoker)delegate
            {
                txtActivity.AppendText(Environment.NewLine + text + Environment.NewLine);
                txtActivity.ScrollToCaret();
            });
        }

        private bool ValidateForm()
        {
            if (txtFeed.Text.Trim() == "")
            {
                MessageBox.Show("Please enter valid HL7 Feed Name, example: ADT, LAB, MED");
                txtFeed.Focus();
                return false;
            }

            if (txtMongoServer.Text.Trim() == "")
            {
                MessageBox.Show("Please enter valid MongoDB server");
                txtMongoServer.Focus();
                return false;
            }

            int port = 0;
            bool isValidPort = Int32.TryParse(txtMongoPort.Text.Trim(), out port);
            if (!isValidPort || txtMongoPort.Text.Trim() == "")
            {
                MessageBox.Show("Please enter valid MongoDB port");
                txtMongoPort.Focus();
                return false;
            }

            return true;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                ProcessMessage pm = new ProcessMessage();
                pm.MessagePath = txtMsgPath.Text.Trim();
                pm.DBServer = txtMongoServer.Text.Trim();
                pm.DBPort = txtMongoPort.Text.Trim();
                pm.DBUser = txtMongoUser.Text.Trim();
                pm.DBPassword = txtMongoPass.Text.Trim();
                pm.Feed = txtFeed.Text.Trim();

                loadThread = new Thread((ThreadStart)delegate { pm.ProcessFile(this); });
                loadThread.IsBackground = true;
                loadThread.Start();
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopLoadThread();
        }

        private void StopLoadThread()
        {
            if (loadThread != null && loadThread.IsAlive)
            {
                loadThread.Abort();
                loadThread = null;
            }
        }

        private void LoadData_FormClosing(object sender, FormClosingEventArgs e)
        {
            StopLoadThread();
        }

    }
}
