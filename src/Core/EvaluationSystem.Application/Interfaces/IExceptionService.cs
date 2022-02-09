namespace EvaluationSystem.Application.Interfaces
{
    public interface IExceptionService
    {
        public void ThrowExceptionWhenEntityDoNotExist<T>(int id, IGenericRepository<T> repository);
    }
}
