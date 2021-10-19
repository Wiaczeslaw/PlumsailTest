namespace Test.Domain.Entities.Abstractions
{
    public interface IDeletable
    {
        bool IsDeleted { get; set; }
    }
}