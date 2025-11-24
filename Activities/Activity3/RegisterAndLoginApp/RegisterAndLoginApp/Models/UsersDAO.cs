
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;

namespace RegisterAndLoginApp.Models
{
	public class UsersDAO : IUserManager
	{
		string CONNECTION_STRING = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Connect Timeout=30;";

		// Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Test;Integrated Security=True;Connect Timeout=30;Encrypt=True;Application Intent=ReadWrite;Multi Subnet Failover=False

		public int AddUser(UserModel user)
		{
			using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				string query = "INSERT INTO users (Username, PasswordHash, Salt, Groups) " +
					"VALUES (@Username, @PasswordHash, @Salt, @Gourps); " +
					"SELECT SCOPE_IDENTITY();";

				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue("@Username", user.Username);
					cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
					cmd.Parameters.AddWithValue("@Salt", Convert.ToHexString(user.Salt));
					cmd.Parameters.AddWithValue("@Gourps", user.Groups);

					object result = cmd.ExecuteScalar();
					return Convert.ToInt32(result);
				}
			}

		}

		public int CheckCredentials(string username, string password)
		{
			using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				string query = "SELECT * FROM users WHERE Username = @Username";
				SqlCommand cmd = new SqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@Username", username);
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						UserModel user = new UserModel()
						{ 
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							Username = reader.GetString(reader.GetOrdinal("Username")),
							PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
							Salt = Convert.FromHexString(reader.GetString(reader.GetOrdinal("Salt"))),
							Groups = reader.GetString(reader.GetOrdinal("Groups"))
						};

						bool IsValid = user.VerifyPassword(password);
						if (IsValid) return user.Id; // user found, valid password
						else return 0; // User found, invalid password
					}
					return 0; // User not found
				}
			}
		}

		public void DeleteUser(UserModel user)
		{
			using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				string query = "DELETE FROM users WHERE Id = @Id";

				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue("@Id", user.Id);
					cmd.ExecuteNonQuery();
				}
			}
		}

		public List<UserModel> GetAllUsers()
		{
			using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				string query = "SELECT * FROM users";
				SqlCommand cmd = new SqlCommand(query, conn);
				using (SqlDataReader reader = cmd.ExecuteReader())
				{
					List<UserModel> users = new List<UserModel>();
					while (reader.Read())
					{
						UserModel user = new UserModel()
						{
							Id = reader.GetInt32(reader.GetOrdinal("Id")),
							Username = reader.GetString(reader.GetOrdinal("Username")),
							PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
							Salt = Convert.FromHexString(reader.GetString(reader.GetOrdinal("Salt"))),
							Groups = reader.GetString(reader.GetOrdinal("Groups"))
						};
						users.Add(user);
					}
					return users;
				}
			}
		}

		public UserModel GetUserById(int id)
		{
			using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				string query = "SELECT * FROM users WHERE Id = @Id";
				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue("@Id", id);
					using (SqlDataReader reader = cmd.ExecuteReader())
					{
						if (reader.Read())
						{
							UserModel user = new UserModel()
							{
								Id = reader.GetInt32(reader.GetOrdinal("Id")),
								Username = reader.GetString(reader.GetOrdinal("Username")),
								PasswordHash = reader.GetString(reader.GetOrdinal("PasswordHash")),
								Salt = Convert.FromHexString(reader.GetString(reader.GetOrdinal("Salt"))),
								Groups = reader.GetString(reader.GetOrdinal("Groups"))
							};
							return user;
						}
						return null; // User not found
					}
				}
			}
		}

		public void UpdateUser(UserModel user)
		{
			UserModel found = this.GetUserById(user.Id);
			if (found == null) return; // User not found, nothing to update

			using (SqlConnection conn = new SqlConnection(CONNECTION_STRING))
			{
				conn.Open();
				string query = "UPDATE users SET Username = @Username, PasswordHash = @PasswordHash, " +
					"Salt = @Salt, Groups = @Groups WHERE Id = @Id";
				using (SqlCommand cmd = new SqlCommand(query, conn))
				{
					cmd.Parameters.AddWithValue("@Id", user.Id);
					cmd.Parameters.AddWithValue("@Username", user.Username);
					cmd.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
					cmd.Parameters.AddWithValue("@Salt", Convert.ToHexString(user.Salt));
					cmd.Parameters.AddWithValue("@Groups", user.Groups);

					cmd.ExecuteNonQuery();
				}
			}
		}
	}
}
