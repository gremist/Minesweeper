using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper {
    partial class CustomForm {
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
            this.ClientSize = new Size(190, 90);
            this.Text = "Custom Field";
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.Manual;

            Label heightLabel = new Label();
            heightLabel.Location = new Point(12, 10);
            heightLabel.Size = new Size(45, 20);
            heightLabel.Text = "&Height:";
            heightLabel.TextAlign = ContentAlignment.MiddleLeft;
            heightLabel.Font = SystemFonts.DialogFont;
            heightLabel.TabIndex = 3;
            this.Controls.Add(heightLabel);

            Label widthLabel = new Label();
            widthLabel.Location = new Point(12, 35);
            widthLabel.Size = new Size(45, 20);
            widthLabel.Text = "&Width:";
            widthLabel.TextAlign = ContentAlignment.MiddleLeft;
            widthLabel.Font = SystemFonts.DialogFont;
            widthLabel.TabIndex = 4;
            this.Controls.Add(widthLabel);

            Label minesLabel = new Label();
            minesLabel.Location = new Point(12, 60);
            minesLabel.Size = new Size(45, 20);
            minesLabel.Text = "&Mines:";
            minesLabel.TextAlign = ContentAlignment.MiddleLeft;
            minesLabel.Font = SystemFonts.DialogFont;
            minesLabel.TabIndex = 5;
            this.Controls.Add(minesLabel);

            TextBox heightBox = new TextBox();
            heightBox.Location = new Point(60, 10);
            heightBox.Size = new Size(40, 20);
            heightBox.Text = gameInfo.N.ToString();
            heightBox.Font = SystemFonts.DialogFont;
            heightBox.TabIndex = 3;
            heightBox.KeyPress += TextBox_KeyPress;
            this.Controls.Add(heightBox);
            this.heightBox = heightBox;

            TextBox widthBox = new TextBox();
            widthBox.Location = new Point(60, 35);
            widthBox.Size = new Size(40, 20);
            widthBox.Text = gameInfo.M.ToString();
            widthBox.Font = SystemFonts.DialogFont;
            widthBox.TabIndex = 4;
            widthBox.KeyPress += TextBox_KeyPress;
            this.Controls.Add(widthBox);
            this.widthBox = widthBox;

            TextBox minesBox = new TextBox();
            minesBox.Location = new Point(60, 60);
            minesBox.Size = new Size(40, 20);
            minesBox.Text = gameInfo.Mines.ToString();
            minesBox.Font = SystemFonts.DialogFont;
            minesBox.TabIndex = 5;
            minesBox.KeyPress += TextBox_KeyPress;
            this.Controls.Add(minesBox);
            this.minesBox = minesBox;

            Button okButton = new Button();
            okButton.Location = new Point(115, 16);
            okButton.Size = new Size(60, 25);
            okButton.Text = "OK";
            okButton.Font = SystemFonts.DialogFont;
            okButton.TabIndex = 1;
            okButton.Click += OkButton_Click;
            this.AcceptButton = okButton;
            this.Controls.Add(okButton);
            
            Button cancelButton = new Button();
            cancelButton.Location = new Point(115, 49);
            cancelButton.Size = new Size(60, 25);
            cancelButton.Text = "Cancel";
            cancelButton.Font = SystemFonts.DialogFont;
            cancelButton.TabIndex = 2;
            this.CancelButton = cancelButton;
            this.Controls.Add(cancelButton);

            this.ResumeLayout(false);
        }

        private TextBox heightBox, widthBox, minesBox;
    }
}