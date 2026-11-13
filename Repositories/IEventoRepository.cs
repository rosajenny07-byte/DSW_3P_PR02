using DSW_3P_PR02.Models;
namespace DSW_3P_PR02.Repositories
{
    public interface IEventoRepository
    {
       public List<Evento> ObtenerTodos();

        public Evento? ObtenerPorId(int id);
        public Evento Crear(Evento evento);
        public Evento? Actualizar(int id, Evento evento);
        public bool Eliminar(int id);
    }
}