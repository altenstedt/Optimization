using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Book
    {
        private readonly ICollection<Author> authors = new HashSet<Author>();

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string Isbn { get; set; }

        public DateTime ReleaseDate { get; set; }

        public virtual Publisher Publisher { get; set; }

        public virtual ICollection<Author> Authors
        {
            get
            {
                return authors;
            }
        }
    }
}