using System;
using System.Drawing;
using System.Windows.Forms;
using WumpusWinForms.WumpusLogic;

namespace WumpusWinForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tlpBoard = new System.Windows.Forms.TableLayoutPanel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tlpBoard
            // 
            this.tlpBoard.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tlpBoard.ColumnCount = 2;
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBoard.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBoard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpBoard.Location = new System.Drawing.Point(0, 23);
            this.tlpBoard.Name = "tlpBoard";
            this.tlpBoard.RowCount = 2;
            this.tlpBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBoard.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpBoard.Size = new System.Drawing.Size(700, 350);
            this.tlpBoard.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = false;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Location = new System.Drawing.Point(0, 373);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(700, 23);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "Bienvenido!";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNewGame.Location = new System.Drawing.Point(0, 0);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(700, 23);
            this.btnNewGame.TabIndex = 2;
            this.btnNewGame.Text = "Nuevo Juego";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 400);
            this.Controls.Add(this.tlpBoard);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnNewGame);
            this.Name = "Form1";
            this.Text = "Wumpus World";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpBoard;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnNewGame;
    }
}