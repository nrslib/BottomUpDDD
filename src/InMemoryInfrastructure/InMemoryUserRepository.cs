using System.Collections.Generic;
using System.Linq;
using Domain.Domain.Users;

namespace InMemoryInfrastructure {
    public class InMemoryUserRepository : IUserRepository {
        private readonly Dictionary<UserId, User> data = new Dictionary<UserId, User>();

        public User Find(UserId id) {
            if (data.TryGetValue(id, out var target)) {
                return target;
            } else {
                return null;
            }
        }

        public User Find(UserName name) {
            return data.Values.FirstOrDefault(x => x.UserName.Equals(name));
        }

        public IEnumerable<User> FindAll() {
            return data.Values;
        }

        public void Save(User user) {
            data[user.Id] = user;
        }

        public void Remove(User user) {
            data.Remove(user.Id);
        }
    }
}