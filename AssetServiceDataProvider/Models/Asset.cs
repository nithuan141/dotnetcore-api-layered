using System;
using System.ComponentModel.DataAnnotations;

namespace AssetServiceDataProvider.Models
{
    public class Asset
    {

        [Key]
        public Guid AssetId { get; set; }
        public string AssetName { get; set; }
        public string Description { get; set; }
        public byte Status { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
