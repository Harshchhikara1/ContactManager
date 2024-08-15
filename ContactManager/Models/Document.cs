using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Models
{
    public class Document
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; } = string.Empty; // Providing default value

        [Required]
        public string Content { get; set; } = string.Empty; // Providing default value

        public string? OwnerID { get; set; } // Marked as nullable

        [ForeignKey("OwnerID")]
        public IdentityUser? Owner { get; set; } // Marked as nullable

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Providing default value
    }
}
