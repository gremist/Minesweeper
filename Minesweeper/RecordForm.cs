using System;
using System.Windows.Forms;

namespace Minesweeper {
    partial class RecordForm : Form {
        public RecordForm(string level, string player) {
            this.level = level;
            this.player = player;
            InitializeComponent();
        }

        private void OkButton_Click(object sender, EventArgs e) {
            player = playerBox.Text;
            DialogResult = DialogResult.OK;
        }

        public string player;
        private string level;
    }
}
