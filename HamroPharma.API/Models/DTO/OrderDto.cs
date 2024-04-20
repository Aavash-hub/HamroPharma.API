using HamroPharma.API.Models.DTOs;

namespace HamroPharma.API.Models.DTO
{
    public class OrderDto
    {
        public List<OrderDetailDTO> OrderDetails { get; set; }
    }
}
