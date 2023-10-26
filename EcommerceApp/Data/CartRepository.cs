using System;

IUserFactory userFactory = new UserFactory();

User user1 = userFactory.CreateUser("userone", "password1", true, true);
User user2 = userFactory.CreateUser("usertwo", "password2", false, false);

Console.WriteLine("User 1 - Username: " + user1.Username);
Console.WriteLine("User 2 - Username: " + user2.Username);

if (user1 is AuthorizedUser authorizedUser1)
{
    Console.WriteLine("User 1 is an Authorized User, Is Admin: " + authorizedUser1.IsAdmin);
}

if (user2 is AuthorizedUser authorizedUser2)
{
    Console.WriteLine("User 2 is an Authorized User, Is Admin: " + authorizedUser2.IsAdmin);
}
else
{
    Console.WriteLine("User 2 is a standard User.");
}

interface IUserFactory
{
    User CreateUser(string username, string password, bool twoFactor, bool isAdmin);
}

class User
{
    public string Username { get; private set; }
    public string Password { get; private set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public virtual void PasswordHash()
    {
    }
}

class AuthorizedUser : User
{
    public bool IsAdmin { get; private set; }

    public AuthorizedUser(string username, string password, bool isAdmin)
        : base(username, password)
    {
        IsAdmin = isAdmin;
    }
}

class UserFactory : IUserFactory
{
    public User CreateUser(string username, string password, bool twoFactor, bool isAdmin)
    {
        if (twoFactor)
        {
            return new AuthorizedUser(username, password, isAdmin);
        }
        else
        {
            if (isAdmin)
            {
                throw new Exception("TwoFactorAuthentication is required for creating a user.");
            }
            return new User(username, password);
        }
    }
}

