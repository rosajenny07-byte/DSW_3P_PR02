using DSW_3P_PR02.Data;
using DSW_3P_PR02.Models;

namespace DSW_3P_PR02.Repositories;

public class EventoSQLRepository : IEventoRepository
{
    private readonly AppDbContext _context;
    public EventoSQLRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Evento> ObtenerTodos()
    {
        return _context.Eventos.ToList();
    }
    public Evento? ObtenerPorId(int id)
    {
        return _context.Eventos.Find(id);
    }

    public Evento Crear(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
        return evento;
    }
    public Evento? Actualizar(int id, Evento evento)
    {
        var eventoExistente = _context.Eventos.Find(id);
        if (eventoExistente == null)
        {
            return null;
        }

        eventoExistente.Nombre = evento.Nombre;
        eventoExistente.Categoria = evento.Categoria;
        eventoExistente.Lugar = evento.Lugar;
        eventoExistente.NumeroMaximo = evento.NumeroMaximo;
        eventoExistente.Activo = evento.Activo;

        _context.SaveChanges();
        return eventoExistente;
    }
    public bool Eliminar(int id)
    {
        var evento = _context.Eventos.Find(id);
        if (evento == null)
        {
            return false;
        }

        _context.Eventos.Remove(evento);
        _context.SaveChanges();
        return true;
    }
}