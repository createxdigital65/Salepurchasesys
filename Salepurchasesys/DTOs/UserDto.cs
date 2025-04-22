namespace SalePurchasesys.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }

    public class UserCreateDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }

    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }
    }
}
