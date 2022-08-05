using RedisPagination.Core.BaseModel;

namespace RedisPagination.Entities
{
    public class EmployeeReadDto : EmployeeDto, IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime CreatedAt { get; set; }
        
    }
}
