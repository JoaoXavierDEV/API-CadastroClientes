namespace XPTO.Domain.Entities
{
    public interface IDataTransferObject
    {
        Guid Id { get; set; }
    }
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }

    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

    }
}
