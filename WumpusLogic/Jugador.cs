namespace WumpusWinForms.WumpusLogic
{
    public class Jugador
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Jugador(int xInicial, int yInicial)
        {
            X = xInicial;
            Y = yInicial;
        }

        public void Mover(char direccion)
        {
            switch (char.ToUpper(direccion))
            {
                case 'W': Y--; break;
                case 'A': X--; break;
                case 'S': Y++; break;
                case 'D': X++; break;
            }
        }
    }
}
