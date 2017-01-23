﻿using System;

namespace ANCBE.Models
{
    public class Comment
    {
        public long Id { get; set; }

        public long PostId { get; set; }
        public virtual Post Post { get; set; }

        public DateTime Posted { get; set; }
        public string Author { get; set; }
        public string Body { get; set; }
    }
}
