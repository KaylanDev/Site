using SiteMotos.Models;

namespace SiteMotos.Services
{
    public interface IMotos
    {
        public  Task<IEnumerable<Models.MotosModelView>> GetAll();
        public Task<MotosModelView> GetByIdAsync(int id);
        public Task<MotosModelView> PostAsync(MotosModelView moto);
        public Task<bool> PutAsync(int id, MotosModelView moto);
        public Task<bool> DeleteAsync(int id);
    }
}
