﻿using System.Collections.Generic;
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
                        var username = reader["username"] as string;
                        var firstname = reader["firstname"] as string;
                        var familyname = reader["familyname"] as string;
                        return new User(
                            id,
                            new UserName(username),
                            new FullName(firstname, familyname)
                        );
                    } else {
                        return null;
                    }
                }
            }
        }

        public User Find(UserName userName) {
            using (var con = new SqlConnection(Config.ConnectionString)) {
                con.Open();
                using (var com = con.CreateCommand()) {
                    com.CommandText = "SELECT * FTOM t_user WHERE username = @username";
                    com.Parameters.Add(new SqlParameter("@username", userName.Value));
                    var reader = com.ExecuteReader();
                    if (reader.Read()) {
                        var id = reader["id"] as string;
                        var firstname = reader["firstname"] as string;
                        var familyname = reader["familyname"] as string;

                        return new User(
                            new UserId(id),
                            userName,
                            new FullName(firstname, familyname)
                        );
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
                        var username = reader["username"] as string;
                        var user = new User(
                            new UserId(id),
                            new UserName(username),
                            new FullName(firstname, familyname)
                        );
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
                        ? "UPDATE t_user SET username = @username, firstname = @firstname, familyname = @familyname WHERE id = @id"
                        : "INSERT INTO t_user VALUES(@id, @username, @firstname, @familyname)";
                    command.Parameters.Add(new SqlParameter("@id", user.Id.Value));
                    command.Parameters.Add(new SqlParameter("@firstname", user.UserName.Value));
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