using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RoadSenseRemote
{
    public partial class Form1 : Form
    {
        SocketServer _scServer = new SocketServer();
        bool _goodStarted = false;
        bool _okStarted = false;
        bool _badStarted = false;
        bool _worstStarted = false;
        bool _excellentStarted = false;
        bool _bumpStarted = false;


        public Form1()
        {
            InitializeComponent();
            _scServer.RecvEvt = recvText;
            _scServer.ConnectionDone = Connected;
            EnableDisableButtons(false);
            //btnStart.BackColor  = Color.Green;
            btnStart.Enabled = true;
        }

        public void Connected()
        {
            
            lblDataRecv.Invoke(new MethodInvoker(() => UpdateUIForConnection()));
        }

        public void UpdateUIForConnection()
        {
            EnableDisableButtons(true);
            btnStart.BackColor = Color.Green;
            btnStart.Enabled = false;
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            _scServer.StartServer();
             btnStart.Enabled = false;
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            _scServer.SendData(txtDataSend.Text);
        }

        private void recvText(string msg)
        {
            lblDataRecv.Invoke(new MethodInvoker(() => UpdateUI(msg)));

            //lblDataRecv.Invoke(_uiUpdater);
        }

        void UpdateUI(string msg)
        {
            lblDataRecv.Text += msg + Environment.NewLine;
        }

        void setTheButtons(Button b, RoadType actionType, bool action)
        {
            if (action)
            {
                EnableDisableButtons(false);
                b.Enabled = true;
                b.BackColor = Color.Red;
                b.Text = "Stop " + actionType.ToString();
            }
            else
            {
                EnableDisableButtons(true);
                b.BackColor = Color.Green;
                b.Text = "Start " + actionType.ToString();
            }
            int actionCmd = (int)actionType;
            if (action)
            {
                actionCmd = actionCmd * 10;
            }
            else
            {
                actionCmd = actionCmd * 10 + 1;
            }
            _scServer.SendData(actionCmd.ToString());
        }

        private void EnableDisableButtons(bool state)
        {
            btnGood.Enabled = state;
            btnOk.Enabled = state;
            btnBad.Enabled = state;
            btnWorst.Enabled = state;
            //btnStart.Enabled = state;
            btnSend.Enabled = state;
            btnBumper.Enabled = state;
            if (state)
            {
                btnGood.BackColor = Color.Green;
                btnOk.BackColor = Color.Green;
                btnBad.BackColor = Color.Green;
                btnWorst.BackColor = Color.Green;
                //btnStart.BackColor = Color.Green;
                btnSend.BackColor = Color.Green;
                btnBumper.BackColor = Color.Green;
              
            }
            else
            {
               
                btnGood.BackColor = Color.Gray;
                btnOk.BackColor = Color.Gray;
                btnBad.BackColor = Color.Gray;
                btnWorst.BackColor = Color.Gray;
                //btnStart.BackColor = Color.Gray;
                btnSend.BackColor = Color.Gray;
                btnBumper.BackColor = Color.Gray;
            }
        }

        private void btnGood_Click(object sender, EventArgs e)
        {
            _goodStarted = !_goodStarted;
            setTheButtons(btnGood,RoadType.Good, _goodStarted);
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            _okStarted = !_okStarted;
            setTheButtons(btnOk, RoadType.Ok, _okStarted);
        }

        private void btnDiscard_Click(object sender, EventArgs e)
        {
            EnableDisableButtons(true);
            _excellentStarted = false;
            _goodStarted = false;
            _okStarted = false;
            _badStarted = false;
            _worstStarted = false;
            _bumpStarted = false;
            _scServer.SendData("-1");
            
        }

        private void btnBad_Click(object sender, EventArgs e)
        {
            _badStarted = !_badStarted;
            setTheButtons(btnBad, RoadType.Bad, _badStarted);
        }

        private void btnWorst_Click(object sender, EventArgs e)
        {
            _worstStarted = !_worstStarted;
            setTheButtons(btnWorst, RoadType.Worst, _worstStarted);
        }

        private void btnBumper_Click(object sender, EventArgs e)
        {
            _bumpStarted = !_bumpStarted;
            setTheButtons(btnBumper, RoadType.Bumper, _bumpStarted);
        }
    }
}
