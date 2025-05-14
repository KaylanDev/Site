using SiteMotos.Models;

namespace SiteMotos.Services
{
    public interface IMotos
    {
        public  Task<IEnumerable<Models.Motos>> GetAll();
    }
}
