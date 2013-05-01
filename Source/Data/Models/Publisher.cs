using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Publisher
    {
        private readonly ICollection<Book> books = new HashSet<Book>();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public virtual ICollection<Book> Books
        {
            get
            {
                return books;
            }
        }
    }
} 