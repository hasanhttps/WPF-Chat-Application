using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
using WPF_Chat_Application_Homework.models;
using static WPF_Chat_Application_Homework.models.JsonHandling;

namespace WPF_Chat_Application_Homework {
    public partial class MainWindow : Window, INotifyPropertyChanged {

        // Private Fields

        public static ObservableCollection<Message> ChatMessages { get; set; } = new();
        private string? _currentUsername;
        private string? _currentProfile;
        private string? _message;

        // Properties

        public static User _currentUser = new();
        public static List<User>? Users { get; set; }
        public string? currentUsername { get { return _currentUsername; } 
            set { 
                _currentUsername = value;
                OnPropertyChange();
            } 
        }
        public string? currentProfile { get { return _currentProfile; }
            set
            {
                _currentProfile = value;
                OnPropertyChange();
            }
        }

        public MainWindow() {
            InitializeComponent();

            Users = ReadData<List<User>>("Contacts");
            DataContext = this;
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChange([CallerMemberName] string? name = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private void ListView_SelectionChanged(object sender, RoutedEventArgs e) {

            User currentUser = (Direct.SelectedItem as User)!;
            foreach (var user in Users) {
                if (_currentUser == user) user.Messages = ChatMessages.ToList<Message>();
            }
            _currentUser.Messages = ChatMessages.ToList<Message>();
            _currentUser = currentUser;
            ChatMessages.Clear();
            foreach (var message in currentUser.Messages!) {
                ChatMessages.Add(message);
            }
            

            currentProfile = currentUser.Image;
            currentUsername = currentUser.Username;
            OnMessage.Visibility = Visibility.Visible;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
            TextBox? textBox = sender as TextBox;
            textBox!.Text = "";
            textBox!.Foreground = new SolidColorBrush(Colors.Black);
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e) {
            TextBox? textBox = sender as TextBox;
            _message = textBox!.Text;
            textBox!.Text = "Type a message.";
            textBox!.Foreground = new SolidColorBrush(Colors.Gray);
        }

        private void SendButton_Click(object sender, RoutedEventArgs e) {
            if (_message == "") return;

            string horizontalAl = "Right", content = _message!, Background = "#20C997";
            string? date = DateTime.Now.ToShortTimeString();

            Message message = new() {
                HorizontalAlignment = horizontalAl, 
                Content = content,
                Background = Background,
                dateTime = date
            };
            ChatMessages.Add(message);
        }

    }
}
