namespace Mango.Services.ProductAPI.Models.Dto
{
    public interface IResponseDto
    {
        bool IsSuccess { get; set; }
        object Result { get; set; }
        string DisplayMessage { get; set; }
        List<string> ErrorMessages { get; set; }
    }
}
