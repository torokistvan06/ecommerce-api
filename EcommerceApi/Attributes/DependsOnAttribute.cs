namespace EcommerceApi.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class DependsOnAttribute: Attribute
{
    public Type DependedSeeder { get; }

    public DependsOnAttribute(Type dependedSeeder)
    {
        DependedSeeder = dependedSeeder;
    }
}
