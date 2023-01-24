using System.ComponentModel.DataAnnotations;

namespace RestaurantSystem.Core.Entities.Base
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool SoftDeleted { get; set; }
    }
}
