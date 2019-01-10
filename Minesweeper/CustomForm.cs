using System;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper {
    partial class CustomForm : Form {
        public CustomForm(GameInfo gameInfo, Point location) {
            this.gameInfo = gameInfo;
            this.location = location;
            InitializeComponent();
        }

        private void TextBox_KeyPress(object sender, KeyPressEventArgs e) {
            TextBox box = sender as TextBox;
            Graphics graphics = CreateGraphics();
            if (!Char.IsControl(e.KeyChar) &&
                graphics.MeasureString(e.KeyChar.ToString(), SystemFonts.DialogFont).Width +
                graphics.MeasureString(box.Text, SystemFonts.DialogFont).Width >= box.Width) {
                e.Handled = true;
            }
            graphics.Dispose();
        }

        private void OkButton_Click(object sender, EventArgs e) {
            gameInfo.M = ParseInt(widthBox.Text, 30, 9);
            gameInfo.N = ParseInt(heightBox.Text, 24, 9);
            gameInfo.Mines = ParseInt(minesBox.Text, 667, 10);
            this.DialogResult = DialogResult.OK;
        }

        private static int ParseInt(string str, int max, int min) {
            int ret = 0, sign = 0;
            foreach (char ch in str) {
                if (sign == 0 && ch == ' ') {
                    continue;
                } else if (sign == 0 && ch == '-') {
                    sign = -1;
                } else if (sign == 0 && ch == '+') {
                    sign = 1;
                } else if (sign == 0 && '0' <= ch && ch <= '9') {
                    sign = 1;
                    ret = ch - '0';
                } else if ('0' <= ch && ch <= '9') {
                    if (sign > 0 && (int.MaxValue - ch + '0') / 10 < ret) {
                        return int.MaxValue;
                    }
                    if (sign < 0 && (int.MinValue + ch - '0') / 10 > ret) {
                        return int.MinValue;
                    }
                    ret = ret * 10 + sign * (ch - '0');
                } else {
                    break;
                }
            }
            ret = Math.Max(ret, min);
            ret = Math.Min(ret, max);
            return ret;
        }

        private GameInfo gameInfo;
        private Point location;
    }
}
