using DesktopClient;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = DesktopClient.Message;

namespace WF_Client_4
{
    public partial class Form1 : Form
    {
        private HubConnection hubConnection;
        private bool subscribed;
        public Form1()
        {
            InitializeComponent();
            hubConnection = GetHubConnection();
        }

        private HubConnection GetHubConnection(string name = null)
        {
            var url = "http://localhost:57785/messages";

            if (name != null)
                url += $"?name={name}";

            var result = new HubConnectionBuilder()
                .WithAutomaticReconnect()
                .WithUrl(url)
                .Build();

            result.On<NewMessage>("Send", message =>
            {
                AppendTextToTextBox(message.Sender, message.Text, Color.Black);
            });

            result.Reconnected += connId =>
            {
                MessageBox.Show("Reconnected");
                return Task.CompletedTask;
            };

            result.Reconnecting += error =>
            {
                MessageBox.Show("Reconnecting");
                return Task.CompletedTask;
            };

            return result;
        }

        private void AppendTextToTextBox(string sender, string text, Color color)
        {
            BeginInvoke((Action)(() =>
            {
                chatTextBox.SelectionStart = chatTextBox.TextLength;
                chatTextBox.SelectionLength = 0;
                chatTextBox.SelectionColor = color;
                chatTextBox.AppendText(string.Format("Author: {0}{2}Text: {1}{2}{2}", sender, text, Environment.NewLine));
                chatTextBox.SelectionColor = chatTextBox.ForeColor;
            }));
        }

        private async Task ConnectToServer()
        {
            if (hubConnection.State == HubConnectionState.Disconnected)
            {
                try
                {
                    await hubConnection.StartAsync();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                if (hubConnection.State == HubConnectionState.Connected)
                {
                    connectButton.Text = "Disconnect";
                    stateLabelValue.ForeColor = Color.Green;
                    stateLabelValue.Text = "Connected";
                }
            }
            else if (hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.StopAsync();
                connectButton.Text = "Connect";
                stateLabelValue.ForeColor = Color.Red;
                stateLabelValue.Text = "Disconnected";
            }
        }

        private void ShowError(Exception ex)
        {
            MessageBox.Show(ex?.Message ?? "Error");
        }

        private async void connectButton_Click(object sender, EventArgs e)
        {
            await ConnectToServer();
        }

        private async void setNameButton_Click(object sender, EventArgs e)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                await hubConnection.StopAsync();
                await hubConnection.DisposeAsync();
                hubConnection = GetHubConnection(nameTextBox.Text);
                await ConnectToServer();
            }
        }

        private async void sendButton_Click(object sender, EventArgs e)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                var message = new Message
                {
                    Text = messageTextBox.Text
                };

                try
                {
                    await hubConnection.SendAsync("SendToOthers", message);
                    AppendTextToTextBox("Me", message.Text, Color.Green);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    messageTextBox.Clear();
                }
            }
        }

        private void freezeButton_Click(object sender, EventArgs e)
        {
            Thread.Sleep(31_000);
        }

        private async void subscribeButton_Click(object sender, EventArgs e)
        {
            if (subscribed)
            {
                try
                {
                    await hubConnection.InvokeAsync("Unsubscribe");
                    subscribed = false;
                    subscribeButton.Text = "Subscribe";
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
            else
            {
                try
                {
                    await hubConnection.InvokeAsync("Subscribe");
                    subscribed = true;
                    subscribeButton.Text = "Unsubscribe";
                }
                catch (Exception ex)
                {
                    ShowError(ex);
                }
            }
        }
    }
}
