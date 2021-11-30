using PimIVBackend.Models.Base;
using Validator;

namespace PimIVBackend.Models
{
    public class FolioEntity : ModelBase
    {
        public FolioEntity()
        {

        }

        public FolioEntity(Folio folio, Entity entity)
        {
            Guard.Validate(validator =>
                validator
                    .NotNull(entity, nameof(entity), $"{nameof(entity)} faz referência para um objeto nulo")
                    .NotNull(folio, nameof(folio), $"{nameof(folio)} faz referência para um objeto nulo")
            );

            FolioId = folio.Id;
            Entity = entity;
        }
        public int Id { get; set; }
        public int FolioId { get; set; }
        //public virtual Folio Folio { get; set; }
        public int EntityId { get; set; }
        public virtual Entity Entity { get; set; }

    }
}
