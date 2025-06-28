using k8sFormResultViewApp.Models;

namespace k8sFormResultViewApp.Services
{
    public interface IPersonService
    {
        Task<List<PersonViewModel>> GetAllPersonsAsync();
        Task<int> GetTotalCountAsync();
    }
}
