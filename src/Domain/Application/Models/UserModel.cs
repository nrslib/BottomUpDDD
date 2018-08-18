using Domain.Domain.Users;

namespace Domain.Application.Models
{
    public class UserModel
    {
        public UserModel(User source) {
            Id = source.Id.Value;
            UserName = source.UserName.Value;
            Name = new FullNameModel(source.Name);
        }

        public string Id { get; }
        public string UserName { get; }
        public FullNameModel Name { get; }
    }
}
