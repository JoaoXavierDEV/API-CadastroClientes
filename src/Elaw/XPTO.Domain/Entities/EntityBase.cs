using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

namespace XPTO.Domain.Entities
{
    public interface IDataTransferObject
    {
        abstract Guid Id { get; set; }
    }
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }

    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [NotMapped]
        [System.Text.Json.Serialization.JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; }

        protected EntityBase()
        {
            //Id = Guid.NewGuid();
        }


        public abstract bool EhValido();

    }
}
