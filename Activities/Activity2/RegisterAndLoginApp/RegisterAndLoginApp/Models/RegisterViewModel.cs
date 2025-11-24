namespace RegisterAndLoginApp.Models
{
    /// <summary>
    /// Internal class GroupViewModel used to name the groups that the user can be assigned
    /// </summary>
    public class GroupViewModel
    { 
        public bool IsSelected { get; set; }
        public string GroupName { get; set; }
    }

    public class RegisterViewModel
    {
        //Properties for the RegisterViewModel class
        public string Username { get; set; }
        public string Password { get; set; }
        public List<GroupViewModel> Groups { get; set; }

        public RegisterViewModel()
        {
            Username = "";
            Password = "";
            Groups = new List<GroupViewModel>
            {
                new GroupViewModel { GroupName = "Admin", IsSelected = false },
                new GroupViewModel { GroupName = "Users", IsSelected = false },
                new GroupViewModel { GroupName = "Students", IsSelected = false }
            };
        }
    }
}
