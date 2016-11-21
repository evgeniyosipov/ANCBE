using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ANCBE.Models
{
    public class Post
    {
        public string Title { get; set; }
        public DateTime PostedDate { get; set; }
        public string Auhor { get; set; }
        public string Body { get; set; }
    }
}
