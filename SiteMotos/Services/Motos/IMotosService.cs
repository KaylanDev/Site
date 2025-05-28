using SiteMotos.Models;

namespace SiteMotos.Services.Motos
{
    public interface IMotosService
    {
        public  Task<IEnumerable<MotosModelView>> GetAll();
        public Task<MotosModelView> GetByIdAsync(int id);
        public Task<MotosModelView> PostAsync(MotosModelView moto);
        public Task<bool> PutAsync(int id, MotosModelView moto);
        public Task<bool> DeleteAsync(int id);
    }
}
