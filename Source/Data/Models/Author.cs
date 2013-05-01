﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Author
    {
        private readonly ICollection<Book> books = new HashSet<Book>();
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual ICollection<Book> Books
        {
            get
            {
                return books;
            }
        }
    }
}