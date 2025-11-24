using System.Security.Cryptography;
using System.Text;

namespace RegisterAndLoginApp.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public byte[] Salt { get; set; }
		public string Groups { get; set; }

		private const int keySize = 32;
		private const int iterations = 1000;
		private HashAlgorithmName hashAlgorithm = HashAlgorithmName.SHA512;

		/// <summary>
		/// Constructor model, generates a 32 byte salt value upon instantiation
		/// </summary>
		public UserModel()
		{
			// FIXME: TESTONLY - would need to be generated and stored in UserModel instantiation
			Salt = RandomNumberGenerator.GetBytes(keySize);
		}

		/// <summary>
		/// Hashes the plain text password using a generated salting value
		/// </summary>
		/// <param name="password"></param>
		public void SetPassword(string password)
		{ // Sets the Password Hash 
			PasswordHash = HashPassword(password, Salt);
		}

		/// <summary>
		/// Hashes the given plaintext password and compares it to the stored PasswordHash
		/// </summary>
		/// <param name="password">plaintext password value to compare</param>
		/// <returns>true is hashes match, false if not</returns>
		public bool VerifyPassword(string password)
		{
			if (PasswordHash.Equals(HashPassword(password, Salt)))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Utility method to hash a password
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		private string HashPassword(string password, byte[] salt)
		{
			var hash = Rfc2898DeriveBytes.Pbkdf2(
				Encoding.UTF8.GetBytes(password),
				salt,
				iterations,
				hashAlgorithm,
				keySize);

			return Convert.ToHexString(hash);
		}

		public override string ToString()
		{
			return String.Format("ID:{0}\nU:{1} \nP:{2}\nSALT:{3}\nGROUPS:{4}",Id, Username, PasswordHash, Encoding.UTF8.GetString(Salt), Groups);
		}
	}
}
