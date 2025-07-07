using System.ComponentModel.DataAnnotations.Schema;
using FluentValidation.Results;

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
        public Guid Id { get; set; } = Guid.NewGuid();

        [NotMapped]
        [System.Text.Json.Serialization.JsonIgnore]
        public ValidationResult ValidationResult { get; protected set; } = new ValidationResult();

        protected EntityBase()
        {
            //Id = Guid.NewGuid();
        }

        public abstract bool EhValido();

        public virtual T Copiar<T>() where T : EntityBase, new()
        {
            var novo = new T();
            var propriedades = typeof(T).GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(p => p.CanRead && p.CanWrite);

            foreach (var prop in propriedades)
            {
                var valor = prop.GetValue(this);

                // Se for uma entidade do domínio, faz cópia profunda
                if (valor is EntityBase entidade)
                {
                    // Usa reflexão para chamar Copiar dinamicamente
                    var copiarMethod = entidade.GetType().GetMethod("Copiar", Type.EmptyTypes);
                    if (copiarMethod != null)
                    {
                        if (copiarMethod.IsGenericMethod)
                        {
                            copiarMethod = copiarMethod.MakeGenericMethod(entidade.GetType());
                        }
                        // Chama o método Copiar sem parâmetros
                        var copia = copiarMethod.Invoke(entidade, null);
                        prop.SetValue(novo, copia);
                        continue;
                    }
                }

                // Se for uma coleção de entidades
                if (valor is System.Collections.IEnumerable enumerable && prop.PropertyType != typeof(string))
                {
                    var elementType = prop.PropertyType.IsGenericType
                        ? prop.PropertyType.GetGenericArguments()[0]
                        : null;

                    if (elementType != null && typeof(EntityBase).IsAssignableFrom(elementType))
                    {
                        var listType = typeof(List<>).MakeGenericType(elementType);
                        var novaLista = (System.Collections.IList)Activator.CreateInstance(listType);

                        foreach (var item in enumerable)
                        {
                            if (item is EntityBase entidadeItem)
                            {
                                var copiarMethod = entidadeItem.GetType().GetMethod("Copiar", Type.EmptyTypes);
                                if (copiarMethod != null)
                                {
                                    var copia = copiarMethod.Invoke(entidadeItem, null);
                                    novaLista.Add(copia);
                                }
                            }
                            else
                            {
                                novaLista.Add(item);
                            }
                        }
                        prop.SetValue(novo, novaLista);
                        continue;
                    }
                }

                // Cópia superficial para tipos simples
                prop.SetValue(novo, valor);
            }
            return novo;
        }
    }
}
