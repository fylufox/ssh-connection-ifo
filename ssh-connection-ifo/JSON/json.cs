using System;
using System.Collections.Generic;
using System.Text;

namespace ssh_connection_ifo.JSON
{
    public class Param
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
    }

    public class Payload
    {
        public string text { get; set; }
        public Attachments[] attachments { get; set; }
    }

    public class Attachments
    {
        public string color { get; set; }
        public Block[] blocks { get; set; }
    }

    public class Block
    {
        public string type { get; set; }
        public Text text { get; set; }
    }

    public class Text
    {
        public string type { get; set; }
        public string text { get; set; }
    }
}
