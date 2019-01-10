using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper {
    partial class BestTimesForm {
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
            this.ClientSize = new Size(235, 100);
            this.Text = "Fastest Mine Sweepers";
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.Manual;

            Label beginner = new Label();
            beginner.Location = new Point(12, 5);
            beginner.Size = new Size(75, 20);
            beginner.Text = "Beginner:";
            beginner.TextAlign = ContentAlignment.MiddleLeft;
            beginner.Font = SystemFonts.DialogFont;
            this.Controls.Add(beginner);

            Label intermediate = new Label();
            intermediate.Location = new Point(12, 22);
            intermediate.Size = new Size(75, 20);
            intermediate.Text = "Intermediate:";
            intermediate.TextAlign = ContentAlignment.MiddleLeft;
            intermediate.Font = SystemFonts.DialogFont;
            this.Controls.Add(intermediate);

            Label expert = new Label();
            expert.Location = new Point(12, 39);
            expert.Size = new Size(75, 20);
            expert.Text = "Expert:";
            expert.TextAlign = ContentAlignment.MiddleLeft;
            expert.Font = SystemFonts.DialogFont;
            this.Controls.Add(expert);

            Label beginnerTime = new Label();
            beginnerTime.Location = new Point(79, 5);
            beginnerTime.Size = new Size(75, 20);
            beginnerTime.TextAlign = ContentAlignment.MiddleRight;
            beginnerTime.Font = SystemFonts.DialogFont;
            this.Controls.Add(beginnerTime);
            this.beginnerTime = beginnerTime;

            Label intermediateTime = new Label();
            intermediateTime.Location = new Point(79, 22);
            intermediateTime.Size = new Size(75, 20);
            intermediateTime.TextAlign = ContentAlignment.MiddleRight;
            intermediateTime.Font = SystemFonts.DialogFont;
            this.Controls.Add(intermediateTime);
            this.intermediateTime = intermediateTime;

            Label expertTime = new Label();
            expertTime.Location = new Point(79, 39);
            expertTime.Size = new Size(75, 20);
            expertTime.TextAlign = ContentAlignment.MiddleRight;
            expertTime.Font = SystemFonts.DialogFont;
            this.Controls.Add(expertTime);
            this.expertTime = expertTime;

            Label beginnerPlayer = new Label();
            beginnerPlayer.Location = new Point(157, 5);
            beginnerPlayer.Height = 20;
            beginnerPlayer.TextAlign = ContentAlignment.MiddleLeft;
            beginnerPlayer.Font = SystemFonts.DialogFont;
            beginnerPlayer.TextChanged += PlayerLabel_TextChanged;
            this.Controls.Add(beginnerPlayer);
            this.beginnerPlayer = beginnerPlayer;

            Label intermediatePlayer = new Label();
            intermediatePlayer.Location = new Point(157, 22);
            intermediatePlayer.Height = 20;
            intermediatePlayer.TextAlign = ContentAlignment.MiddleLeft;
            intermediatePlayer.Font = SystemFonts.DialogFont;
            intermediatePlayer.TextChanged += PlayerLabel_TextChanged;
            this.Controls.Add(intermediatePlayer);
            this.intermediatePlayer = intermediatePlayer;

            Label expertPlayer = new Label();
            expertPlayer.Location = new Point(157, 39);
            expertPlayer.Height = 20;
            expertPlayer.TextAlign = ContentAlignment.MiddleLeft;
            expertPlayer.Font = SystemFonts.DialogFont;
            expertPlayer.TextChanged += PlayerLabel_TextChanged;
            this.Controls.Add(expertPlayer);
            this.expertPlayer = expertPlayer;

            Button resetScoresButton = new Button();
            resetScoresButton.Location = new Point(51, 65);
            resetScoresButton.Size = new Size(90, 25);
            resetScoresButton.Text = "&Reset Scores";
            resetScoresButton.Font = SystemFonts.DialogFont;
            resetScoresButton.TabIndex = 2;
            resetScoresButton.Click += ResetScoresButton_Click;
            this.Controls.Add(resetScoresButton);

            Button okButton = new Button();
            okButton.Location = new Point(158, 65);
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

        private Label beginnerTime, beginnerPlayer;
        private Label intermediateTime, intermediatePlayer;
        private Label expertTime, expertPlayer;
    }
}