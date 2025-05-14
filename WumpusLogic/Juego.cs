using System;

namespace WumpusWinForms.WumpusLogic
{
    public class Juego
    {
        public Laberinto LaberintoActual { get; private set; }
        public Jugador JugadorActual { get; private set; }
        public EstadoJuego EstadoActual { get; private set; }
        public string MensajeResultado { get; private set; } = "";

        private Random _random = new Random();

        public Juego()
        {
            int tamano = _random.Next(5, 9);
            LaberintoActual = new Laberinto(tamano);
            (int jX, int jY) = LaberintoActual.ColocarElementos();
            JugadorActual = new Jugador(jX, jY);
            EstadoActual = EstadoJuego.Jugando;
        }

        public bool ProcesarMovimiento(char direccion)
        {
            if (EstadoActual != EstadoJuego.Jugando) return false;

            int xAntes = JugadorActual.X;
            int yAntes = JugadorActual.Y;

            JugadorActual.Mover(direccion);

            if (!LaberintoActual.EsPosicionValida(JugadorActual.X, JugadorActual.Y))
            {
                JugadorActual.X = xAntes;
                JugadorActual.Y = yAntes;
                return false;
            }
            else if (xAntes != JugadorActual.X || yAntes != JugadorActual.Y)
            {
                LaberintoActual.MarcarVisitado(xAntes, yAntes);
            }

            ActualizarEstadoJuego();
            return true;
        }

        private void ActualizarEstadoJuego()
        {
            ContenidoCasilla contenido = LaberintoActual.ObtenerContenidoReal(JugadorActual.X, JugadorActual.Y);

            switch (contenido)
            {
                case ContenidoCasilla.Wumpus:
                    EstadoActual = EstadoJuego.PerdidoPorWumpus;
                    MensajeResultado = $"¡Te ha encontrado el Wumpus en ({LaberintoActual.WumpusX},{LaberintoActual.WumpusY})!";
                    break;
                case ContenidoCasilla.Oro:
                    EstadoActual = EstadoJuego.Ganado;
                    MensajeResultado = $"¡Wena! ¡Encontraste el oro en ({LaberintoActual.OroX},{LaberintoActual.OroY})!";
                    break;
                case ContenidoCasilla.Pozo:
                    EstadoActual = EstadoJuego.PerdidoPorPozo;
                    MensajeResultado = $"¡Caíste en un pozo en ({JugadorActual.X},{JugadorActual.Y})!";
                    break;
            }
        }

        public (bool hayHedor, bool hayBrisa) ObtenerPistasActuales()
        {
            return LaberintoActual.HayPistasEn(JugadorActual.X, JugadorActual.Y);
        }
    }
}

