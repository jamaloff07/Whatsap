using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Whatsapp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<MessageItem> Messages { get; set; }
        public string ImagePath { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Messages = new ObservableCollection<MessageItem>();
            listBox.ItemsSource = Messages;

            ImagePath = "images/send.jpg";

            DataContext = this;
        }

        private void AddMessageToListBox(string time, string message)
        {
            Messages.Add(new MessageItem { Time = time, Message = message });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string userMessage = textBox.Text;

            AddMessageToListBox(DateTime.Now.ToString("HH:mm"), userMessage);

            ShowTypingIndicator();

            await Task.Delay(3000);

            HideTypingIndicator();

            string botResponse = GetBotResponse(userMessage);

            AddMessageToListBox(DateTime.Now.ToString("HH:mm"), botResponse);

            textBox.Clear();
        }

        private string GetBotResponse(string userMessage)
        {
            if (userMessage.ToLower().Equals("salam"))
            {
                return "Salam!";
            }
            else if (userMessage.ToLower().Equals("necesen"))
            {
                return "Yaxşıyam, sən?";
            }
            else if (userMessage.ToLower().Equals("yaxsiyam mende"))
            {
                return "yaxsi ol";
            }
            else if (userMessage.ToLower().Equals("cox saol"))
            {
                return "deymez";
            }
            else if (userMessage.ToLower().Equals("yaxsi sonra danisariq"))
            {
                return "yaxsi saol";
            }
            else
            {
                return "hay?";
            }
        }

        private async void ShowTypingIndicator()
        {
            try
            {
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 0;
                animation.To = -20;
                animation.AutoReverse = true;
                animation.RepeatBehavior = RepeatBehavior.Forever;
                animation.Duration = TimeSpan.FromSeconds(0.5);

                TextBlock typingIndicator = new TextBlock();
                typingIndicator.Text = "...";
                typingIndicator.Margin = new Thickness(10);
                typingIndicator.VerticalAlignment = VerticalAlignment.Center;
                typingIndicator.BeginAnimation(TextBlock.MarginProperty, animation);

                listBox.Items.Add(typingIndicator);

                await Task.Delay(3000);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xeta: {ex.Message}");
            }
        }

        private void HideTypingIndicator()
        {
            if (listBox.Items.Count > 0 && listBox.Items[listBox.Items.Count - 1] is TextBlock)
            {
                listBox.Items.RemoveAt(listBox.Items.Count - 1);
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //hecne
        }
    }

    public class MessageItem
    {
        public string? Time { get; set; }
        public string? Message { get; set; }
    }
}
