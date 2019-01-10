using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper {
    partial class RecordForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.SuspendLayout();

            this.ClientSize = new Size(160, 140);
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Paint += RecordForm_Paint;

            Label row1 = new Label();
            row1.Location = new Point(12, 7);
            row1.Size = new Size(135, 20);
            row1.Text = "You have the fastest time";
            row1.TextAlign = ContentAlignment.MiddleCenter;
            row1.Font = SystemFonts.DialogFont;
            this.Controls.Add(row1);

            Label row2 = new Label();
            row2.Location = new Point(12, 24);
            row2.Size = new Size(135, 20);
            row2.Text = "for " + level + " level.";
            row2.TextAlign = ContentAlignment.MiddleCenter;
            row2.Font = SystemFonts.DialogFont;
            this.Controls.Add(row2);

            Label row3 = new Label();
            row3.Location = new Point(12, 41);
            row3.Size = new Size(135, 20);
            row3.Text = "Please enter your name.";
            row3.TextAlign = ContentAlignment.MiddleCenter;
            row3.Font = SystemFonts.DialogFont;
            this.Controls.Add(row3);

            TextBox playerBox = new TextBox();
            playerBox.Location = new Point(17, 68);
            playerBox.Size = new Size(125, 20);
            playerBox.Text = player;
            playerBox.Font = SystemFonts.DialogFont;
            playerBox.MaxLength = 32;
            playerBox.TabIndex = 2;
            this.Controls.Add(playerBox);
            this.playerBox = playerBox;

            Button okButton = new Button();
            okButton.Location = new Point(50, 100);
            okButton.Size = new Size(60, 25);
            okButton.Text = "OK";
            okButton.Font = SystemFonts.DialogFont;
            okButton.TabIndex = 1;
            okButton.Click += OkButton_Click;
            this.AcceptButton = okButton;
            this.CancelButton = okButton;
            this.Controls.Add(okButton);

            this.ResumeLayout(false);
        }

        private void RecordForm_Paint(object sender, PaintEventArgs e) {
            Graphics graphics = e.Graphics;
            
            // draw outer border
            graphics.DrawLine(Pens.LightGray, 0, 0, 0, this.ClientSize.Height - 1);
            graphics.DrawLine(Pens.LightGray, 0, 0, this.ClientSize.Width - 1, 0);
            graphics.DrawLine(Pens.Gray, this.ClientSize.Width - 1, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
            graphics.DrawLine(Pens.Gray, 0, this.ClientSize.Height - 1, this.ClientSize.Width - 1, this.ClientSize.Height - 1);

            // draw inner border
            graphics.DrawLine(Pens.White, 1, 1, 1, this.ClientSize.Height - 2);
            graphics.DrawLine(Pens.White, 1, 1, this.ClientSize.Width - 2, 1);
            graphics.DrawLine(Pens.DarkGray, this.ClientSize.Width - 2, 1, this.ClientSize.Width - 2, this.ClientSize.Height - 2);
            graphics.DrawLine(Pens.DarkGray, 1, this.ClientSize.Height - 2, this.ClientSize.Width - 2, this.ClientSize.Height - 2);

            graphics.Dispose();
        }

        private TextBox playerBox;
    }
}