using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserServiceAPI.DTO
{
    public class AssetUserDTO
    {
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssetUserId { get; set; }

        [Required(ErrorMessage = "Asset is required")]
        [ForeignKey("Asset")]
        public Guid AssetId { get; set; }

        [Required(ErrorMessage = "User is required")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public byte Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
