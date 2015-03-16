using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace W_GJS.Models
{
    public class MessageModel
    {
        public string UserName { get; set; }

        public string Message { get; set; }

        public string UserGroup { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string MsgDate { get; set; }
    }
}