namespace GLab.Domains.Models.Users
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserState State { get; set; }

        private User(string userId, string userName, string password, UserState state)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            State = state;
        }

        private User(string userId, string userName, UserState state)
        {
            UserId = userId;
            UserName = userName;
            Password = String.Empty;
            State = state;
        }

        public static User Create(string userId, string userName, UserState state)
        {
            return new User(userId, userName, state);
        }

        private void changeState(UserState state)
        {
            State = state;
        }

        public void Allow()
        {
            changeState(UserState.Allowed);
        }

        public void block()
        {
            changeState(UserState.Bloqued);
        }

        public void Delete()
        {
            changeState(UserState.Deleted);
        }
    }

    public enum UserState
    {
        Bloqued = 0,
        Allowed = 1,
        Deleted = -1
    }
}