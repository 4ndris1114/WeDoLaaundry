using System.Net;

namespace Service.ModelConversion
{
    public abstract class MovelDtoConversion<M, D>
    {
        public abstract List<D> ToDtoCollection(List<M> dbList);

        public abstract D ToDto(M model);

        public abstract M ToModel(D dto);
    }
}
