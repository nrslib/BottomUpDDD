using Domain.Domain.Users;

namespace Domain.Application.Models
{
    public class UserModel
    {
        public UserModel(User source) {
            Id = source.Id.Value;
            Name = new FullNameModel(source.Name);
        }

        public string Id { get; }
        public FullNameModel Name { get; }
    }
}
