using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        
        [Column(TypeName = "timestamp without time zone")]
        public DateTime CreatedAt { get; set; }
        
        public Post Post { get; set; }
        public User Author { get; set; }
    }
}