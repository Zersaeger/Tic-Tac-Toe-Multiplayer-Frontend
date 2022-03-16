using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SimpleTCP;

namespace tic_Tac_Toe_2
{
    public partial class MainWindow : Window
    {
        private string NumberToSign(char num)
        {
            if (num == '1')
            {
                return "X";
            }
            else if (num == '2')
            {
                return "O";
          }
            return "";
        }

        SimpleTcpClient client = new SimpleTcpClient();
        public bool turn;
        
        public MainWindow()
        {
            client = client.Connect("Julianh.de", 7531);
            client.Delimiter = (byte)'\n';
            client.DelimiterDataReceived += (sender, msg) =>
            {
                Recieved(msg.MessageString);
            };
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            Console.WriteLine(button.Content.ToString());
            if (button.Content.ToString() == "")
            {
                if (turn)
                {
                    string name = button.Name;
                    client.WriteLine("click:" + name[6]);
                    turn = false;
                    
                }
            }
            
        }
        public void Recieved(string text)
        {
            Console.WriteLine(text);
            if (text == "ping")
            {
                client.WriteLine("pong");
            }
            else if (text.StartsWith("match-found"))
            {
                Console.WriteLine("Hello there!");
                Label1.Dispatcher.Invoke(new Action(() => Label1.Content = "We found a match for you"));

            }
            else if (text.StartsWith("your-turn"))
            {
                Label1.Dispatcher.Invoke(new Action(() => Label1.Content = "it`s your turn"));
                turn = true;
            }
            else if (text.StartsWith("their-turn"))
            {
                Label1.Dispatcher.Invoke(new Action(() => Label1.Content = "it`s their turn"));
            }
            else if (text.StartsWith("board"))
            {
                string board = text.Substring(6);
                Button0.Dispatcher.Invoke(new Action(() => Button0.Content = NumberToSign(board[0])));
                Button1.Dispatcher.Invoke(new Action(() => Button1.Content = NumberToSign(board[1])));
                Button2.Dispatcher.Invoke(new Action(() => Button2.Content = NumberToSign(board[2])));
                Button3.Dispatcher.Invoke(new Action(() => Button3.Content = NumberToSign(board[3])));
                Button4.Dispatcher.Invoke(new Action(() => Button4.Content = NumberToSign(board[4])));
                Button5.Dispatcher.Invoke(new Action(() => Button5.Content = NumberToSign(board[5])));
                Button6.Dispatcher.Invoke(new Action(() => Button6.Content = NumberToSign(board[6])));
                Button7.Dispatcher.Invoke(new Action(() => Button7.Content = NumberToSign(board[7])));
                Button8.Dispatcher.Invoke(new Action(() => Button8.Content = NumberToSign(board[8])));

            }
            else if (text.StartsWith("winner"))
            {
                Label1.Dispatcher.Invoke(new Action(() => Label1.Content = "You win!!!"));
                MessageBoxResult result = MessageBox.Show("Would you like to play again?", "Play again", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        client.WriteLine("again");
                        Button0.Content = "";
                        Button1.Content = "";
                        Button2.Content = "";
                        Button3.Content = "";
                        Button4.Content = "";
                        Button5.Content = "";
                        Button6.Content = "";
                        Button7.Content = "";
                        Button8.Content = "";
                        break;
                    case MessageBoxResult.No:
                        
                        break;
                    case MessageBoxResult.Cancel:
                        
                        break;
                }
            }
            else if (text.StartsWith("loser"))
            {
                Label1.Dispatcher.Invoke(new Action(() => Label1.Content = "You lose :("));
                MessageBoxResult result = MessageBox.Show("Would you like to play again?", "Play again", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        client.WriteLine("again");
                        Button0.Content = "";
                        Button1.Content = "";
                        Button2.Content = "";
                        Button3.Content = "";
                        Button4.Content = "";
                        Button5.Content = "";
                        Button6.Content = "";
                        Button7.Content = "";
                        Button8.Content = "";
                        break;
                    case MessageBoxResult.No:

                        return;
                    case MessageBoxResult.Cancel:

                        return;
                }
            }
            else if (text.StartsWith("tie"))
            {
                Label1.Dispatcher.Invoke(new Action(() => Label1.Content = "Nobody wins :|"));
                MessageBoxResult result = MessageBox.Show("Would you like to play again?", "Play again", MessageBoxButton.YesNoCancel);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        client.WriteLine("again");
                        Button0.Content = "";
                        Button1.Content = "";
                        Button2.Content = "";
                        Button3.Content = "";
                        Button4.Content = "";
                        Button5.Content = "";
                        Button6.Content = "";
                        Button7.Content = "";
                        Button8.Content = "";
                        break;
                    case MessageBoxResult.No:
                        return;
                    case MessageBoxResult.Cancel:
                        return;                
                }
            }
        }
    }
}
