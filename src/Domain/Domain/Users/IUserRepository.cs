using System.Collections.Generic;

namespace Domain.Domain.Users {
    public interface IUserRepository {
        User Find(UserId id);
        User Find(UserName name);
        IEnumerable<User> FindAll();
        void Save(User user);
        void Remove(User user);
    }
}