
namespace DesktopClient
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Button btn_StreamFromServer;
            System.Windows.Forms.Button btn_StreamToServer;
            this.getNameButton = new System.Windows.Forms.Button();
            this.setNameButton = new System.Windows.Forms.Button();
            this.nameLabel = new System.Windows.Forms.Label();
            this.nameTextBox = new System.Windows.Forms.TextBox();
            this.chatTextBox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.TextBox();
            this.messageTextLabel = new System.Windows.Forms.Label();
            this.stateLabelValue = new System.Windows.Forms.Label();
            this.stateLabel = new System.Windows.Forms.Label();
            this.connectButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.serverPort = new System.Windows.Forms.TextBox();
            btn_StreamFromServer = new System.Windows.Forms.Button();
            btn_StreamToServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_StreamFromServer
            // 
            btn_StreamFromServer.Location = new System.Drawing.Point(12, 397);
            btn_StreamFromServer.Name = "btn_StreamFromServer";
            btn_StreamFromServer.Size = new System.Drawing.Size(153, 23);
            btn_StreamFromServer.TabIndex = 7;
            btn_StreamFromServer.Text = "Stream from server";
            btn_StreamFromServer.UseVisualStyleBackColor = true;
            btn_StreamFromServer.Click += new System.EventHandler(this.btn_StreamFromServer_Click);
            // 
            // btn_StreamToServer
            // 
            btn_StreamToServer.Location = new System.Drawing.Point(12, 368);
            btn_StreamToServer.Name = "btn_StreamToServer";
            btn_StreamToServer.Size = new System.Drawing.Size(153, 23);
            btn_StreamToServer.TabIndex = 7;
            btn_StreamToServer.Text = "Stream to server";
            btn_StreamToServer.UseVisualStyleBackColor = true;
            btn_StreamToServer.Click += new System.EventHandler(this.btn_StreamToServer_Click);
            // 
            // getNameButton
            // 
            this.getNameButton.Location = new System.Drawing.Point(66, 75);
            this.getNameButton.Name = "getNameButton";
            this.getNameButton.Size = new System.Drawing.Size(75, 23);
            this.getNameButton.TabIndex = 16;
            this.getNameButton.Text = "Get";
            this.getNameButton.UseVisualStyleBackColor = true;
            this.getNameButton.Click += new System.EventHandler(this.getNameButton_Click);
            // 
            // setNameButton
            // 
            this.setNameButton.Location = new System.Drawing.Point(147, 75);
            this.setNameButton.Name = "setNameButton";
            this.setNameButton.Size = new System.Drawing.Size(75, 23);
            this.setNameButton.TabIndex = 6;
            this.setNameButton.Text = "Set";
            this.setNameButton.UseVisualStyleBackColor = true;
            this.setNameButton.Click += new System.EventHandler(this.setNameButton_Click);
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(8, 50);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(39, 15);
            this.nameLabel.TabIndex = 11;
            this.nameLabel.Text = "Name";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point(52, 46);
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.Size = new System.Drawing.Size(170, 23);
            this.nameTextBox.TabIndex = 13;
            this.nameTextBox.Text = "Anonymous";
            // 
            // chatTextBox
            // 
            this.chatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chatTextBox.Location = new System.Drawing.Point(228, 17);
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.ReadOnly = true;
            this.chatTextBox.Size = new System.Drawing.Size(565, 475);
            this.chatTextBox.TabIndex = 15;
            this.chatTextBox.Text = "";
            // 
            // sendButton
            // 
            this.sendButton.Location = new System.Drawing.Point(147, 187);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(75, 23);
            this.sendButton.TabIndex = 7;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Location = new System.Drawing.Point(52, 104);
            this.messageTextBox.Multiline = true;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(170, 77);
            this.messageTextBox.TabIndex = 14;
            // 
            // messageTextLabel
            // 
            this.messageTextLabel.AutoSize = true;
            this.messageTextLabel.Location = new System.Drawing.Point(8, 107);
            this.messageTextLabel.Name = "messageTextLabel";
            this.messageTextLabel.Size = new System.Drawing.Size(28, 15);
            this.messageTextLabel.TabIndex = 12;
            this.messageTextLabel.Text = "Text";
            // 
            // stateLabelValue
            // 
            this.stateLabelValue.AutoSize = true;
            this.stateLabelValue.ForeColor = System.Drawing.Color.Red;
            this.stateLabelValue.Location = new System.Drawing.Point(52, 21);
            this.stateLabelValue.Name = "stateLabelValue";
            this.stateLabelValue.Size = new System.Drawing.Size(79, 15);
            this.stateLabelValue.TabIndex = 9;
            this.stateLabelValue.Text = "Disconnected";
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Location = new System.Drawing.Point(8, 21);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(36, 15);
            this.stateLabel.TabIndex = 10;
            this.stateLabel.Text = "State:";
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(147, 17);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(75, 23);
            this.connectButton.TabIndex = 8;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 15);
            this.label1.TabIndex = 17;
            this.label1.Text = "Password";
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Location = new System.Drawing.Point(71, 265);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(151, 23);
            this.passwordTextBox.TabIndex = 19;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(147, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "Get token";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 240);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 15);
            this.label2.TabIndex = 18;
            this.label2.Text = "Name";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(71, 236);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(151, 23);
            this.textBox1.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 449);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 15);
            this.label3.TabIndex = 22;
            this.label3.Text = "Server port";
            // 
            // serverPort
            // 
            this.serverPort.Location = new System.Drawing.Point(78, 446);
            this.serverPort.Name = "serverPort";
            this.serverPort.Size = new System.Drawing.Size(100, 23);
            this.serverPort.TabIndex = 23;
            this.serverPort.Text = "10000";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 509);
            this.Controls.Add(this.serverPort);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.getNameButton);
            this.Controls.Add(this.setNameButton);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.nameTextBox);
            this.Controls.Add(this.chatTextBox);
            this.Controls.Add(btn_StreamToServer);
            this.Controls.Add(btn_StreamFromServer);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.messageTextLabel);
            this.Controls.Add(this.stateLabelValue);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.connectButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button getNameButton;
        private System.Windows.Forms.Button setNameButton;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.RichTextBox chatTextBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox messageTextBox;
        private System.Windows.Forms.Label messageTextLabel;
        private System.Windows.Forms.Label stateLabelValue;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.Button connectButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serverPort;
    }
}

