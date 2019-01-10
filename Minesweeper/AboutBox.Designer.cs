using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper {
    partial class AboutBox {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.SuspendLayout();

            this.Location = location;
            this.ClientSize = new Size(223, 68);
            this.Text = "About " + AssemblyTitle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.Manual;
            this.KeyDown += AboutBox_KeyDown;
            this.Click += AboutBox_Click;

            PictureBox iconBox = new PictureBox();
            iconBox.Location = new Point(15, 18);
            iconBox.Size = new Size(32, 32);
            iconBox.Image = Properties.Resources.Mine.ToBitmap();
            iconBox.Click += AboutBox_Click;
            this.Controls.Add(iconBox);

            Console.WriteLine(Properties.Resources.Mine.ToBitmap().Size);
            Label product = new Label();
            product.Location = new Point(60, 5);
            product.Size = new Size(160, 20);
            product.Text = AssemblyProduct + " v" + SubStr(AssemblyVersion, '.', 2) + " " + (Environment.Is64BitProcess ? "x64" : "x32");
            product.TextAlign = ContentAlignment.MiddleLeft;
            product.Font = SystemFonts.DialogFont;
            product.Click += AboutBox_Click;
            this.Controls.Add(product);

            Label framework = new Label();
            framework.Location = new Point(60, 22);
            framework.Size = new Size(160, 20);
            framework.Text = "Based on .NET Framework 4.0";
            framework.TextAlign = ContentAlignment.MiddleLeft;
            framework.Font = SystemFonts.DialogFont;
            framework.Click += AboutBox_Click;
            this.Controls.Add(framework);

            Label copyright = new Label();
            copyright.Location = new Point(60, 39);
            copyright.Size = new Size(160, 20);
            copyright.Text = AssemblyCopyright + " " + AssemblyCompany;
            copyright.TextAlign = ContentAlignment.MiddleLeft;
            copyright.Font = SystemFonts.DialogFont;
            copyright.Click += AboutBox_Click;
            this.Controls.Add(copyright);

            this.ResumeLayout(false);
        }

        private void AboutBox_Click(object sender, EventArgs e) {
            Close();
        }

        private void AboutBox_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter ||
                e.KeyCode == Keys.Escape) {
                Close();
            }
        }
    }
}
