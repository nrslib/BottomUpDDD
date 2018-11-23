using System.Collections.Generic;
using System.Linq;
using Domain.Domain.Users;

namespace InMemoryInfrastructure {
    public class InMemoryUserRepository : IUserRepository {
        private readonly Dictionary<UserId, User> data = new Dictionary<UserId, User>();

        public User Find(UserId id) {
            if (data.TryGetValue(id, out var target)) {
                return cloneUser(target);
            } else {
                return null;
            }
        }

        public User Find(UserName name) {
            var target = data.Values.FirstOrDefault(x => x.UserName.Equals(name));
            if (target != null) {
                return cloneUser(target);
            } else {
                return null;
            }
        }

        public IEnumerable<User> FindAll() {
            return data.Values.Select(cloneUser);
        }

        public void Save(User user) {
            data[user.Id] = user;
        }

        public void Remove(User user) {
            data.Remove(user.Id);
        }

        private User cloneUser(User user)
        {
            return new User(user.Id, user.UserName, user.Name);
        }
    }
}