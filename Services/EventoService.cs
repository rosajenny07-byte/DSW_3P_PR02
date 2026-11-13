using DSW_3P_PR02.Models;
using DSW_3P_PR02.Repositories;

namespace DSW_3P_PR02.Services;

public class EventoService : IEvento
{
    private readonly IEventoRepository _eventoRepository;

    public EventoService(IEventoRepository eventoRepository)
    {
        _eventoRepository = eventoRepository;
    }

    public List<Evento> GetAll()
    {
        return _eventoRepository.ObtenerTodos();
    }

    public Evento? GetById(int id)
    {
        return _eventoRepository.ObtenerPorId(id);
    }

    public Evento Add(Evento evento)
    {
        ArgumentNullException.ThrowIfNull(evento);

        Validar(evento);

        evento.Activo = true;

        _eventoRepository.Crear(evento);
        return evento;
    }

    public Evento? Update(int id, Evento evento)
    {
        ArgumentNullException.ThrowIfNull(evento);

        if (_eventoRepository.ObtenerPorId(id) == null)
        {
            throw new KeyNotFoundException($"No existe un evento con id {id}.");
        }


        _eventoRepository.Actualizar(id, evento);
        return evento;
    }

    public bool Delete(int id)
    {
        var evento = _eventoRepository.ObtenerPorId(id);
        if (evento == null)
        {
            throw new KeyNotFoundException($"No existe un evento con id {id}.");
        }

        if (evento.Activo)
        {
            throw new InvalidOperationException("No se puede eliminar un evento activo. Desactívelo primero.");
        }

        _eventoRepository.Eliminar(id);
        return true;
    }

    private static void Validar(Evento evento)
    {
        if (string.IsNullOrWhiteSpace(evento.Nombre))
        {
            throw new ArgumentException("El nombre del evento es obligatorio.", nameof(evento));
        }

        if (string.IsNullOrWhiteSpace(evento.Categoria))
        {
            throw new ArgumentException("La categoría del evento es obligatoria.", nameof(evento));
        }

        if (string.IsNullOrWhiteSpace(evento.Lugar))
        {
            throw new ArgumentException("El lugar del evento es obligatorio.", nameof(evento));
        }

        if (evento.NumeroMaximo <= 0)
        {
            throw new ArgumentException("El número máximo de asistentes debe ser mayor que cero.", nameof(evento));
        }
    }


}