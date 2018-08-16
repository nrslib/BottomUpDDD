using System.Collections.Generic;
using System.Data.SqlClient;
using Domain.Domain.Users;

namespace ProductInfrastructure {
    public class UserRepository : IUserRepository {
        public User Find(UserId id) {
            using (var con = new SqlConnection(Config.ConnectionString)) {
                con.Open();
                using (var com = con.CreateCommand()) {
                    com.CommandText = "SELECT * FTOM t_user WHERE id = @id";
                    com.Parameters.Add(new SqlParameter("@id", id.Value));
                    var reader = com.ExecuteReader();
                    if (reader.Read()) {
                        var firstname = reader["firstname"] as string;
                        var familyname = reader["familyname"] as string;
                        var name = new FullName(firstname, familyname);
                        return new User(id, name);
                    } else {
                        return null;
                    }
                }
            }
        }

        public User Find(FullName name) {
            using (var con = new SqlConnection(Config.ConnectionString)) {
                con.Open();
                using (var com = con.CreateCommand()) {
                    com.CommandText = "SELECT * FTOM t_user WHERE firstname = @firstname AND familyname = @familyname";
                    com.Parameters.Add(new SqlParameter("@firstname", name.FirstName));
                    com.Parameters.Add(new SqlParameter("@familyname", name.FamilyName));
                    var reader = com.ExecuteReader();
                    if (reader.Read()) {
                        var id = reader["id"] as string;
                        var userid = new UserId(id);
                        return new User(userid, name);
                    } else {
                        return null;
                    }
                }
            }
        }

        public IEnumerable<User> FindAll() {
            using (var con = new SqlConnection(Config.ConnectionString)) {
                con.Open();
                using (var com = con.CreateCommand()) {
                    com.CommandText = "SELECT * FTOM t_user";
                    var reader = com.ExecuteReader();
                    var results = new List<User>();
                    while (reader.Read()) {
                        var id = reader["id"] as string;
                        var firstname = reader["firstname"] as string;
                        var familyname = reader["familyname"] as string;

                        var userid = new UserId(id);
                        var name = new FullName(firstname, familyname);
                        var user = new User(userid, name);
                        results.Add(user);
                    }
                    return results;
                }
            }
        }

        public void Save(User user) {
            using (var con = new SqlConnection(Config.ConnectionString)) {
                con.Open();

                bool isExist;
                using (var com = con.CreateCommand()) {
                    com.CommandText = "SELECT * FTOM t_user WHERE id = @id";
                    com.Parameters.Add(new SqlParameter("@id", user.Id.Value));
                    var reader = com.ExecuteReader();
                    isExist = reader.Read();
                }

                using (var transaction = con.BeginTransaction())
                using (var command = con.CreateCommand()) {
                    command.CommandText = isExist
                        ? "UPDATE t_user SET firstname = @firstname, familyname = @familyname WHERE id = @id"
                        : "INSERT INTO t_user VALUES(@id, @firstname, @familyname)";
                    command.Parameters.Add(new SqlParameter("@id", user.Id.Value));
                    command.Parameters.Add(new SqlParameter("@firstname", user.Name.FirstName));
                    command.Parameters.Add(new SqlParameter("@familyname", user.Name.FamilyName));
                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
            }
        }

        public void Remove(User user) {
            using (var con = new SqlConnection(Config.ConnectionString)) {
                con.Open();
                using (var transaction = con.BeginTransaction())
                using (var command = con.CreateCommand()) {
                    command.CommandText = "DELETE FROM t_user WHERE id = @id";
                    command.Parameters.Add(new SqlParameter("@id", user.Id.Value));
                    command.ExecuteNonQuery();
                    transaction.Commit();
                }
            }
        }
    }
}