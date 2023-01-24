namespace RestaurantSystem.Shared.Model
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data = default, bool success = true)
        {
            Data = data;
            Success = success;
        }

        public string ErrorMessage { get; set; }
        public bool Success { get; set; } = true;
        public T Data { get; set; }
    }
}
