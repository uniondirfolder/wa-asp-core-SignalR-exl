using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopClient
{
    public partial class Form1 : Form
    {
        private  HubConnection hubConnection;
        const string url = "http://localhost:57701/message";
        public Form1()
        {
            InitializeComponent();

            hubConnection = new HubConnectionBuilder()
                .WithUrl(url)
                .WithAutomaticReconnect()
                .AddMessagePackProtocol()
                .Build();

            hubConnection.On<NewMessage>("Send", message =>
            {
                AppendTextToTextBox(message.Sender, message.Text, Color.Black);
            });

            hubConnection.Closed += error => 
            {
                MessageBox.Show($"Connection closed. {error.Message}");
                return Task.CompletedTask;
            };

            hubConnection.Reconnected += id =>
            {
                MessageBox.Show($"Connection reconnected with id: {id}");
                return Task.CompletedTask;
            };

            hubConnection.Reconnecting += error =>
            {
                MessageBox.Show($"Connection reconnecting. {error.Message}");
                return Task.CompletedTask;
            };
        }

        private void AppendTextToTextBox(string sender, string text, Color color)
        {
            chatTextBox.SelectionStart = chatTextBox.TextLength;
            chatTextBox.SelectionLength = 0;
            chatTextBox.SelectionColor = color;
            chatTextBox.AppendText(string.Format("Author: {0}{2}Text: {1}{2}{2}", sender, text, Environment.NewLine));
            chatTextBox.SelectionColor = chatTextBox.ForeColor;
        }

        private async void connectButton_Click(object sender, EventArgs e)
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

        private async void getNameButton_Click(object sender, EventArgs e)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                try
                {
                    var name = await hubConnection.InvokeAsync<string>("GetMyName");

                    if (string.IsNullOrWhiteSpace(name))
                        nameTextBox.Text = "Anonymous";
                    else
                        nameTextBox.Text = name;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private async void setNameButton_Click(object sender, EventArgs e)
        {
            if (hubConnection.State == HubConnectionState.Connected)
            {
                try
                {
                    await hubConnection.SendAsync("SetMyName", nameTextBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
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

        private string token = string.Empty;
        
        private void InitConnection()
        {

            hubConnection = new HubConnectionBuilder()
                .WithUrl($"http://localhost:57701/messages?token={token}")
                .WithAutomaticReconnect()
                .Build();

            hubConnection.On<NewMessage>("Send", message =>
            {
                AppendTextToTextBox(message.Sender, message.Text, Color.Black);
            });

            hubConnection.Closed += error =>
            {
                MessageBox.Show($"Connection closed. {error?.Message}");
                return Task.CompletedTask;
            };

            hubConnection.Reconnected += id =>
            {
                MessageBox.Show($"Connection reconnected with id: {id}");
                return Task.CompletedTask;
            };

            hubConnection.Reconnecting += error =>
            {
                MessageBox.Show($"Connection reconnecting. {error?.Message}");
                return Task.CompletedTask;
            };
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            var token = await GetToken();

            if (!string.IsNullOrEmpty(token))
            {
                this.token = token;
                InitConnection();

                var tokenParts = token.Split('.');
                var decodedToken = new StringBuilder();
                for (int i = 0; i < 2; i++)
                {
                    var tokenBytes = WebEncoders.Base64UrlDecode(tokenParts[i]);
                    var decodedPart = Encoding.UTF8.GetString(tokenBytes);
                    decodedToken.AppendLine(decodedPart.PrettifyJsonString());
                }
                decodedToken.AppendLine(tokenParts[2]);

                MessageBox.Show(decodedToken.ToString());
            }
        }

        private async Task<string> GetToken()
        {
            using var httpClient = new HttpClient();

            var authModel = new { Login = nameTextBox.Text, Password = passwordTextBox.Text };

            var json = JsonSerializer.Serialize(authModel);

            var content = new StringContent(json, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await httpClient.PostAsync("http://localhost:57701/api/auth/token", content);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                MessageBox.Show(response.StatusCode.ToString());
                return string.Empty;
            }
        }

        private async void btn_StreamFromServer_Click(object sender, EventArgs e)
        {
            var stream = hubConnection.StreamAsync<int>("DownloadStream");
            await foreach (var element in stream)
            {
                Debug.WriteLine(element);
            }

            Debug.WriteLine("Stream from server completed");
        }

        private async void btn_StreamToServer_Click(object sender, EventArgs e)
        {
            var test = TestAsyncEnumerable();
            await hubConnection.SendAsync("UploadStream", test);
        }

        async IAsyncEnumerable<int> TestAsyncEnumerable() 
        {
            for (int i = 9; i >= 0; i--)
            {
                yield return i;
                await Task.Delay(1000);
            }
        }
    }
}
