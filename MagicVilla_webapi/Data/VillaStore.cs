using MagicVilla_webapi.Models.Dto;

namespace MagicVilla_webapi.data
{
    public class VillaStore{
        public static List<VillaDTO> VillaList = new List<VillaDTO>{
            new VillaDTO{id=1,name="pool view"},
                 new VillaDTO {id=2,name="beach view"}   
        };
    }
}