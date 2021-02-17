using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AssetServiceDataProvider.Models
{
    public class AssetUser
    {
        [Key]
        public int AssetUserId { get; set; }
        [ForeignKey("Asset")]
        public Guid AssetId { get; set; }
        [ForeignKey("User")]
        public Guid UserId { get; set; }
        public byte Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
