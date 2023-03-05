using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models.Dto
{
    public class VillaDTO{
        public int id{get;set;}
        public string name{get;set;}
        [Required]
        [MaxLength(50)]
        public DateTime CreatedDate {get;set;}
        public double rate{get;set;}
        public int sqft{get;set;}
        public string details{get;set;}
        public int occupancy{get;set;}
    
    }
}