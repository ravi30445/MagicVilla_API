using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_webapi.Models{
    public class Villa{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id{get;set;}
        public string name{get;set;}
        public double rate{get;set;}
        public int sqft{get;set;}
        public string details{get;set;}
        public int occupancy{get;set;}
        public DateTime UpdatedDate { get; internal set; }
    }
}