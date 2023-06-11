using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WPF_Chat_Application_Homework.models;
using static WPF_Chat_Application_Homework.models.JsonHandling;

namespace WPF_Chat_Application_Homework {
    public partial class App : Application {

        protected override void OnExit(ExitEventArgs e) {
            foreach (var user in WPF_Chat_Application_Homework.MainWindow.Users!) {
                if (WPF_Chat_Application_Homework.MainWindow._currentUser == user) user.Messages = WPF_Chat_Application_Homework.MainWindow.ChatMessages.ToList<Message>();
            }
            WriteData<List<User>>(WPF_Chat_Application_Homework.MainWindow.Users, "Contacts");
            base.OnExit(e);
        }

    }
}
