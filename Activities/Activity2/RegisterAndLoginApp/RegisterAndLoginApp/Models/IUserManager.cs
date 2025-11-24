namespace RegisterAndLoginApp.Models
{
	public interface IUserManager
	{
		public List<UserModel> GetAllUsers(); // return all users stored in list/DB
		public UserModel GetUserById(int id); // Find a user with a matching id to the param id
		public int AddUser(UserModel user); // add a new user to the list/DB Used during registration
		public void DeleteUser(UserModel user); // find a user with a matching id and delete it
		public void UpdateUser(UserModel user); // find a user with a matching id and replace it with the param model
		public int CheckCredentials(string username, string password); // verify login
	}
}
