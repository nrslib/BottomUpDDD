using System;

namespace Domain.Domain.Users
{
    public class FullName : IEquatable<FullName> {
        private readonly string firstname;
        private readonly string familyname;

        public FullName(string firstname, string familyname) {
            this.firstname = firstname;
            this.familyname = familyname;
        }

        public string FirstName { get { return firstname; } }
        public string FamilyName { get { return familyname; } }

        public bool Equals(FullName other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(firstname, other.firstname) && string.Equals(familyname, other.familyname);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FullName)obj);
        }

        public override int GetHashCode() {
            unchecked {
                return ((firstname != null ? firstname.GetHashCode() : 0) * 397) ^ (familyname != null ? familyname.GetHashCode() : 0);
            }
        }
    }
}
