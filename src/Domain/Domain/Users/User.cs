using System;

namespace Domain.Domain.Users {
    public class User {
        private readonly UserId id;
        private FullName name;

        public User(UserId id, FullName name) {
            this.id = id;
            this.name = name;
        }

        public User(FullName name) {
            id = new UserId(Guid.NewGuid().ToString());
            this.name = name;
        }

        public UserId Id {
            get { return id; }
        }

        public FullName Name {
            get { return name; }
        }

        public bool EqualsEntity(User arg) {
            if (ReferenceEquals(null, arg)) return false;
            if (ReferenceEquals(this, arg)) return true;
            return id.Equals(arg.id);
        }

        public void ChangeName(FullName newName) {
            if (newName == null) throw new ArgumentNullException(nameof(newName));
            this.name = newName;
        }
    }
}