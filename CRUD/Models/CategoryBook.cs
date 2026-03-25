using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRUD.Models
{
    public class CategoryBook
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCategoryBook { get; set; }

        public int IdBook { get; set; }

        [ForeignKey("IdBook")]
        public virtual Book Book { get; set; }

        public int IdCategory { get; set; }

        [ForeignKey("IdCategory")]

        public virtual Category Category { get; set; }


    }
}
