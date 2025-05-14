using System;
using System.Collections.Generic;
using System.Linq;

namespace WumpusWinForms.WumpusLogic
{
    public class Laberinto
    {
        public int Tamano { get; }
        public ContenidoCasilla[,] Casillas { get; private set; }
        public int WumpusX { get; private set; }
        public int WumpusY { get; private set; }
        public int OroX { get; private set; }
        public int OroY { get; private set; }
        public List<(int x, int y)> Pozos { get; private set; } = new List<(int, int)>();

        private const int NumeroPozos = 3;
        private Random _random = new Random();

        public Laberinto(int tamano)
        {
            if (tamano < 5 || tamano > 8)
            {
                throw new ArgumentOutOfRangeException("tamano", "El tamaño debe estar entre 5 y 8.");
            }
            Tamano = tamano;
            Casillas = new ContenidoCasilla[Tamano, Tamano];
            InicializarVacio();
        }

        private void InicializarVacio()
        {
            for (int y = 0; y < Tamano; y++)
            {
                for (int x = 0; x < Tamano; x++)
                {
                    Casillas[x, y] = ContenidoCasilla.Vacio;
                }
            }
        }

        public (int jugadorX, int jugadorY) ColocarElementos()
        {
            Pozos.Clear();
            List<(int x, int y)> posicionesOcupadas = new List<(int, int)>();

            int jX = _random.Next(Tamano);
            int jY = _random.Next(Tamano);
            posicionesOcupadas.Add((jX, jY));

            (WumpusX, WumpusY) = ObtenerPosicionAleatoriaUnica(posicionesOcupadas);
            posicionesOcupadas.Add((WumpusX, WumpusY));

            (OroX, OroY) = ObtenerPosicionAleatoriaUnica(posicionesOcupadas);
            posicionesOcupadas.Add((OroX, OroY));

            for (int k = 0; k < NumeroPozos; k++)
            {
                (int px, int py) = ObtenerPosicionAleatoriaUnica(posicionesOcupadas);
                Pozos.Add((px, py));
                posicionesOcupadas.Add((px, py));
            }

            return (jX, jY);
        }

        private (int x, int y) ObtenerPosicionAleatoriaUnica(List<(int x, int y)> ocupadas)
        {
            int x, y;
            bool posicionUnica;
            do
            {
                x = _random.Next(Tamano);
                y = _random.Next(Tamano);
                posicionUnica = !ocupadas.Any(p => p.x == x && p.y == y);
            } while (!posicionUnica);
            return (x, y);
        }

        public bool EsPosicionValida(int x, int y)
        {
            return x >= 0 && x < Tamano && y >= 0 && y < Tamano;
        }

        public void MarcarVisitado(int x, int y)
        {
            if (EsPosicionValida(x, y) && Casillas[x, y] == ContenidoCasilla.Vacio)
            {
                Casillas[x, y] = ContenidoCasilla.Visitado;
            }
        }

        public ContenidoCasilla ObtenerContenidoReal(int x, int y)
        {
            if (x == WumpusX && y == WumpusY) return ContenidoCasilla.Wumpus;
            if (x == OroX && y == OroY) return ContenidoCasilla.Oro;
            if (Pozos.Any(p => p.x == x && p.y == y)) return ContenidoCasilla.Pozo;
            return Casillas[x, y];
        }

        public (bool hayHedor, bool hayBrisa) HayPistasEn(int x, int y)
        {
            bool hedor = false;
            bool brisa = false;
            int[] dx = { 0, 0, 1, -1 };
            int[] dy = { -1, 1, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                int adjX = x + dx[i];
                int adjY = y + dy[i];

                if (EsPosicionValida(adjX, adjY))
                {
                    if (adjX == WumpusX && adjY == WumpusY) hedor = true;
                    if (Pozos.Any(p => p.x == adjX && p.y == adjY)) brisa = true;
                }
            }
            return (hedor, brisa);
        }
    }
}
