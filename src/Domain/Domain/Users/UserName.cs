using System;

namespace Domain.Domain.Users
{
    public class UserName : IEquatable<UserName> {
        private readonly string name;

        public UserName(string name) {
            if (string.IsNullOrEmpty(name)) {
                throw new ArgumentNullException(nameof(name));
            }
            if (name.Length > 50) {
                throw new ArgumentOutOfRangeException("It must be 50 characters or less", nameof(name));
            }
            this.name = name;
        }

        public string Value { get { return name; } }

        public bool Equals(UserName other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(name, other.name);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((UserName) obj);
        }

        public override int GetHashCode() {
            return (name != null ? name.GetHashCode() : 0);
        }
    }
}
