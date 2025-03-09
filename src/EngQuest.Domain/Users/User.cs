using EngQuest.Domain.Abstractions;
using EngQuest.Domain.Users.Events;

namespace EngQuest.Domain.Users;

public sealed class User : Entity
{
    private readonly List<Role> _roles = [];

    public string IdentityId { get; private set; } = string.Empty;

    public FirstName FirstName { get; private set; }
    public LastName LastName { get; private set; }
    public Email Email { get; private set; }

    public IReadOnlyList<Role> Roles => _roles.AsReadOnly();

    private User(FirstName firstName, LastName lastName, Email email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    // ReSharper disable once UnusedMember.Local
    private User()
    {
    }

    public static User Create(FirstName firstName, LastName lastName, Email email)
    {
        var user = new User(firstName, lastName, email);

        user._roles.Add(Role.Registered);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        return user;
    }

    public void Update(FirstName firstName, LastName lastName, Email email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
