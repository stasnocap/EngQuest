using EngQuest.Domain.Abstractions;

namespace EngQuest.Domain.UnitTests.Infrastructure;

public abstract class BaseTest
{
    public static T AssertDomainEventWasPublished<T>(Entity entity)
        where T : IDomainEvent
    {
        T? domainEvent = entity.GetDomainEvents().OfType<T>().SingleOrDefault() ?? throw new Exception($"{typeof(T).Name} was not published");

        return domainEvent;
    }
}
