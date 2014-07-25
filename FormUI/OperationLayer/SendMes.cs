using System.Collections.Generic;

namespace FormUI.OperationLayer
{
    public class SendMes
    {
        public string Phone { get; set; }
        public string Terminal { get; set; }
        public string Content { get; set; }

        public SendMes(string phone, string terminal, string content)
        {
            Phone = phone;
            Terminal = terminal;
            Content = content;
        }
    }
}