using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NoteApp.Models
{
    public class Note
    {

       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int NoteID { get; set; }
       public string Title { get; set; }
       [Required]
       public string Body { get; set; }
       public int Status { get; set; }
       public User Creator { get; set; }

    }
}