using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper {
    partial class BestTimesForm : Form {
        public BestTimesForm(BestTimesInfo bestTimesInfo, Point location) {
            this.bestTimesInfo = bestTimesInfo;
            this.location = location;
            InitializeComponent();
            RefreshRecordText();
        }

        private void PlayerLabel_TextChanged(object sender, EventArgs e) {
            Label label = sender as Label;
            Graphics graphics = CreateGraphics();
            label.Width = (int)graphics.MeasureString(label.Text, SystemFonts.DialogFont).Width + 5;
            graphics.Dispose();
        }

        private void OkButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void ResetScoresButton_Click(object sender, EventArgs e) {
            bestTimesInfo.Beginner.Time = 999;
            bestTimesInfo.Beginner.Player = "Anonymous";
            
            bestTimesInfo.Intermediate.Time = 999;
            bestTimesInfo.Intermediate.Player = "Anonymous";
            
            bestTimesInfo.Expert.Time = 999;
            bestTimesInfo.Expert.Player = "Anonymous";

            RefreshRecordText();
        }

        private void RefreshRecordText() {
            beginnerTime.Text = bestTimesInfo.Beginner.Time.ToString() + " seconds";
            beginnerPlayer.Text = bestTimesInfo.Beginner.Player;

            intermediateTime.Text = bestTimesInfo.Intermediate.Time.ToString() + " seconds";
            intermediatePlayer.Text = bestTimesInfo.Intermediate.Player;
            
            expertTime.Text = bestTimesInfo.Expert.Time.ToString() + " seconds";
            expertPlayer.Text = bestTimesInfo.Expert.Player;
        }

        private Point location;
        private BestTimesInfo bestTimesInfo;
    }
}
