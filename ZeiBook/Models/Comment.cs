using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ZeiBook.Models
{
    public class Comment
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("Book")]
        public int BookId { get; set; }
        [Required]
        public String Content { get; set; }
        public DateTime PostTime { get; set; }

        public ApplicationUser User { get; set; }
        public Book Book { get; set; }
    }
}
