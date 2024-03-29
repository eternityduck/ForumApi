﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public sealed class Comment
    {
        public int Id { get; init; }
        public string Text { get; set; }
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }
        public Post Post { get; set; }
        public User Author { get; set; }
    }
}