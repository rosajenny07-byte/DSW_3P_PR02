using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Services
{
    public interface IEvento
    {
        List<Evento> GetAll();
        Evento? GetById(int id);
        Evento Add(Evento evento);
        Evento? Update(int id, Evento evento);
        bool Delete(int id);
    }
}