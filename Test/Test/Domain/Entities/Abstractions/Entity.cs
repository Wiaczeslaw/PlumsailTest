using System;

namespace Test.Domain.Entities.Abstractions
{
    public class Entity : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}