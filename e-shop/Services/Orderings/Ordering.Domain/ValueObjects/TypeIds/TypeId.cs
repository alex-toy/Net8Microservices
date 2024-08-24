namespace Ordering.Domain.ValueObjects.TypeIds;

public record TypeId
{
    public Guid Value { get; }
    protected TypeId(Guid value) => Value = value;


    //public static implicit operator Guid(TypeId name) => name.Value;

    //public static implicit operator TypeId(Guid name) => new(name);
}
