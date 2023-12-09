namespace Vezeeta.Domain.Repositories
{
    public interface ISpecializationRepository
    {
        Task<bool> DoesExist(int id);
    }
}
