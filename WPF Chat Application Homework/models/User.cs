using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Chat_Application_Homework.models {
    public class User {

        public string? Username { get; set; }
        public string? Image { get; set; }
        public string? LatestMessage { get; set; }
        public string? LatestOnline { get; set; }

        public List<Message>? Messages { get; set; }
    }
}
