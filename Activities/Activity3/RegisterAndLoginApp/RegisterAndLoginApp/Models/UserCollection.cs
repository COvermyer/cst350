
namespace RegisterAndLoginApp.Models
{
	public class UserCollection : IUserManager
	{
		// in-memory list of users. In a proper application, this would be a database connection.
		// By convention, the underscore indicates a private field.
		private List<UserModel> _users;

		public UserCollection() 
		{
			_users = new List<UserModel>();
			// Create some user accounts
			GenerateUserData(); // FIXME: testonly
		}

		// TESTONLY method
		private void GenerateUserData()
		{
			// Set up a test Admin account
			UserModel user1 = new UserModel();
			user1.Username = "Harry";
			user1.SetPassword("prince");
			user1.Groups = "Admin";
			AddUser(user1);
			
			// Set up a test user account
			UserModel user2 = new UserModel();
			user2.Username = "Megan";
			user2.SetPassword("princess");
			user2.Groups = "Admin, User";
			AddUser(user2);
		}

		public int AddUser(UserModel user)
		{
			user.Id = _users.Count + 1;
			_users.Add(user);
			return user.Id;
		}

		public int CheckCredentials(string username, string password)
		{
			// given a username and password, find a matching user, returning their ID
			foreach (UserModel user in _users) 
			{ 
				if (user.Username == username && user.VerifyPassword(password))
				{
					return user.Id;
				}
			}

			// No matches found
			return -1;
		}

		public void DeleteUser(UserModel user)
		{
			_users.Remove(user); // attempt to remove a user matching the param
		}

		public List<UserModel> GetAllUsers()
		{
			return _users; // return the list of users
		}

		public UserModel GetUserById(int id)
		{
			return _users.Find(u => u.Id == id);
		}

		public void UpdateUser(UserModel user)
		{
			_users[_users.FindIndex(u => u.Id == user.Id)] = user;
		}
	}
}
