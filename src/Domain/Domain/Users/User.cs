using System;

namespace Domain.Domain.Users {
    public class User : IEquatable<User> {
        private readonly UserId id;
        private UserName userName;
        private FullName name;

        public User(UserId id, UserName userName, FullName name) {
            this.id = id;
            this.userName = userName;
            this.name = name;
        }

        public User(UserName userName, FullName name) {
            id = new UserId(Guid.NewGuid().ToString());
            this.userName = userName;
            this.name = name;
        }

        public UserId Id {
            get { return id; }
        }

        public UserName UserName {
            get { return userName; }
        }

        public FullName Name {
            get { return name; }
        }
        
        public void ChangeUserName(UserName newName) {
            if(newName == null) throw new ArgumentNullException(nameof(newName));
            userName = newName;
        }

        public void ChangeName(FullName newName) {
            if (newName == null) throw new ArgumentNullException(nameof(newName));
            this.name = newName;
        }

        public bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(id, other.id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return (id != null ? id.GetHashCode() : 0);
        }
    }
}