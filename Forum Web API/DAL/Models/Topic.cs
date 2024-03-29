﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public sealed class Topic
    {
        public int Id { get; init; }
        public string Title { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}