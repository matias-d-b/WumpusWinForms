using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WumpusWinForms.WumpusLogic;

namespace WumpusWinForms
{
    public partial class Form1 : Form
    {
        private Juego _juegoActual;
        private Label[,] _cellLabels;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            _juegoActual = new Juego();
            InitializeBoardUI();
            RenderBoard();
            UpdateStatusLabel();
        }

        private void InitializeBoardUI()
        {
            if (_juegoActual == null) return;

            tlpBoard.Controls.Clear();
            tlpBoard.RowCount = _juegoActual.LaberintoActual.Tamano;
            tlpBoard.ColumnCount = _juegoActual.LaberintoActual.Tamano;

            _cellLabels = new Label[_juegoActual.LaberintoActual.Tamano, _juegoActual.LaberintoActual.Tamano];

            tlpBoard.RowStyles.Clear();
            tlpBoard.ColumnStyles.Clear();

            float percentSize = 100F / _juegoActual.LaberintoActual.Tamano;
            for (int i = 0; i < _juegoActual.LaberintoActual.Tamano; i++)
            {
                tlpBoard.RowStyles.Add(new RowStyle(SizeType.Percent, percentSize));
                tlpBoard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, percentSize));
            }

            for (int y = 0; y < _juegoActual.LaberintoActual.Tamano; y++)
            {
                for (int x = 0; x < _juegoActual.LaberintoActual.Tamano; x++)
                {
                    Label lbl = new Label
                    {
                        Text = "",
                        Dock = DockStyle.Fill,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Font = new Font("Segoe UI Emoji", 20F, FontStyle.Regular),
                        Margin = new Padding(1),
                        BorderStyle = BorderStyle.FixedSingle
                    };
                    _cellLabels[x, y] = lbl;
                    tlpBoard.Controls.Add(lbl, x, y);
                }
            }
        }

        private void RenderBoard(bool revealAll = false)
        {
            if (_juegoActual == null || _cellLabels == null) return;

            int tamano = _juegoActual.LaberintoActual.Tamano;

            for (int y = 0; y < tamano; y++)
            {
                for (int x = 0; x < tamano; x++)
                {
                    Label currentLabel = _cellLabels[x, y];
                    string cellText = " ";
                    Color backColor = SystemColors.ControlLight;
                    Color foreColor = Color.Black;

                    if (_juegoActual.JugadorActual.X == x && _juegoActual.JugadorActual.Y == y)
                    {
                        cellText = "👤";
                        backColor = Color.LightSkyBlue;
                    }
                    else if (revealAll || _juegoActual.LaberintoActual.Casillas[x, y] == ContenidoCasilla.Visitado)
                    {
                        ContenidoCasilla realContent = _juegoActual.LaberintoActual.ObtenerContenidoReal(x, y);
                        if (realContent == ContenidoCasilla.Wumpus && revealAll)
                        {
                            cellText = "🦖";
                            backColor = Color.DarkRed;
                            foreColor = Color.White;
                        }
                        else if (realContent == ContenidoCasilla.Oro && (revealAll || _juegoActual.LaberintoActual.Casillas[x, y] == ContenidoCasilla.Visitado))
                        {
                            cellText = "💰";
                            backColor = Color.Gold;
                        }
                        else if (realContent == ContenidoCasilla.Pozo && revealAll)
                        {
                            cellText = "🕳️";
                            backColor = Color.Black;
                            foreColor = Color.White;
                        }
                        else if (_juegoActual.LaberintoActual.Casillas[x, y] == ContenidoCasilla.Visitado)
                        {
                            cellText = "👣";
                            backColor = Color.LightGray;
                        }
                    }

                    currentLabel.Text = cellText;
                    currentLabel.BackColor = backColor;
                    currentLabel.ForeColor = foreColor;
                }
            }
        }

        private void UpdateStatusLabel()
        {
            if (_juegoActual == null) return;

            string status = "";
            if (_juegoActual.EstadoActual == EstadoJuego.Jugando)
            {
                var (hayHedor, hayBrisa) = _juegoActual.ObtenerPistasActuales();
                if (hayHedor) status += "Sientes un Hedor! ";
                if (hayBrisa) status += "Sientes una Brisa! ";
                if (string.IsNullOrWhiteSpace(status)) status = "Todo tranquilo...";
            }
            else
            {
                status = _juegoActual.MensajeResultado;
            }
            lblStatus.Text = status;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (_juegoActual == null || _juegoActual.EstadoActual != EstadoJuego.Jugando)
            {
                e.Handled = true;
                return;
            }

            char move = ' ';
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.Up:
                    move = 'W';
                    break;
                case Keys.A:
                case Keys.Left:
                    move = 'A';
                    break;
                case Keys.S:
                case Keys.Down:
                    move = 'S';
                    break;
                case Keys.D:
                case Keys.Right:
                    move = 'D';
                    break;
                default:
                    return;
            }

            e.Handled = true;
            e.SuppressKeyPress = true;

            bool moveProcessed = _juegoActual.ProcesarMovimiento(move);

            if (moveProcessed)
            {
                RenderBoard();
                UpdateStatusLabel();

                if (_juegoActual.EstadoActual != EstadoJuego.Jugando)
                {
                    RenderBoard(true);
                    MessageBox.Show(_juegoActual.MensajeResultado, "Juego Terminado", MessageBoxButtons.OK,
                        _juegoActual.EstadoActual == EstadoJuego.Ganado ? MessageBoxIcon.Information : MessageBoxIcon.Warning);
                }
            }
            else
            {
            }
        }
    }
}