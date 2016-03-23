namespace RoadSenseRemote
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnSend = new System.Windows.Forms.Button();
            this.txtDataSend = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblDataRecv = new System.Windows.Forms.Label();
            this.btnGood = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnBad = new System.Windows.Forms.Button();
            this.btnWorst = new System.Windows.Forms.Button();
            this.btnBumper = new System.Windows.Forms.Button();
            this.btnDiscard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(813, 70);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtDataSend
            // 
            this.txtDataSend.Location = new System.Drawing.Point(768, 42);
            this.txtDataSend.Name = "txtDataSend";
            this.txtDataSend.Size = new System.Drawing.Size(120, 22);
            this.txtDataSend.TabIndex = 1;
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(28, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(148, 51);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblDataRecv
            // 
            this.lblDataRecv.AutoSize = true;
            this.lblDataRecv.Location = new System.Drawing.Point(34, 377);
            this.lblDataRecv.Name = "lblDataRecv";
            this.lblDataRecv.Size = new System.Drawing.Size(46, 17);
            this.lblDataRecv.TabIndex = 3;
            this.lblDataRecv.Text = "label1";
            // 
            // btnGood
            // 
            this.btnGood.Location = new System.Drawing.Point(28, 89);
            this.btnGood.Name = "btnGood";
            this.btnGood.Size = new System.Drawing.Size(148, 56);
            this.btnGood.TabIndex = 4;
            this.btnGood.Text = "Start Good";
            this.btnGood.UseVisualStyleBackColor = true;
            this.btnGood.Click += new System.EventHandler(this.btnGood_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(216, 89);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(148, 56);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Start Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnBad
            // 
            this.btnBad.Location = new System.Drawing.Point(28, 167);
            this.btnBad.Name = "btnBad";
            this.btnBad.Size = new System.Drawing.Size(148, 56);
            this.btnBad.TabIndex = 6;
            this.btnBad.Text = "Start Bad";
            this.btnBad.UseVisualStyleBackColor = true;
            this.btnBad.Click += new System.EventHandler(this.btnBad_Click);
            // 
            // btnWorst
            // 
            this.btnWorst.Location = new System.Drawing.Point(216, 167);
            this.btnWorst.Name = "btnWorst";
            this.btnWorst.Size = new System.Drawing.Size(148, 56);
            this.btnWorst.TabIndex = 7;
            this.btnWorst.Text = "Start Worst";
            this.btnWorst.UseVisualStyleBackColor = true;
            this.btnWorst.Click += new System.EventHandler(this.btnWorst_Click);
            // 
            // btnBumper
            // 
            this.btnBumper.Location = new System.Drawing.Point(28, 262);
            this.btnBumper.Name = "btnBumper";
            this.btnBumper.Size = new System.Drawing.Size(148, 56);
            this.btnBumper.TabIndex = 8;
            this.btnBumper.Text = "Start Bumper";
            this.btnBumper.UseVisualStyleBackColor = true;
            this.btnBumper.Click += new System.EventHandler(this.btnBumper_Click);
            // 
            // btnDiscard
            // 
            this.btnDiscard.Location = new System.Drawing.Point(216, 13);
            this.btnDiscard.Name = "btnDiscard";
            this.btnDiscard.Size = new System.Drawing.Size(148, 56);
            this.btnDiscard.TabIndex = 9;
            this.btnDiscard.Text = "Discard Previous";
            this.btnDiscard.UseVisualStyleBackColor = true;
            this.btnDiscard.Click += new System.EventHandler(this.btnDiscard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 733);
            this.Controls.Add(this.btnDiscard);
            this.Controls.Add(this.btnBumper);
            this.Controls.Add(this.btnWorst);
            this.Controls.Add(this.btnBad);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnGood);
            this.Controls.Add(this.lblDataRecv);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtDataSend);
            this.Controls.Add(this.btnSend);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtDataSend;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblDataRecv;
        private System.Windows.Forms.Button btnGood;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnBad;
        private System.Windows.Forms.Button btnWorst;
        private System.Windows.Forms.Button btnBumper;
        private System.Windows.Forms.Button btnDiscard;
    }
}

