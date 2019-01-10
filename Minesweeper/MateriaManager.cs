using System;
using System.Drawing;

namespace Minesweeper {
    class MateriaManager {
        public void ConstructMateria(Bitmap source) {
            if (source.Width >= 276 && source.Height >= 133) {
                Rectangle rect = new Rectangle(138, 0, 138, 133);
                Bitmap bitmap = source.Clone(rect, source.PixelFormat);
                GrayScaleMateria = new Materia(bitmap);
            } else {
                GrayScaleMateria = null;
            }

            if (source.Width >= 138 && source.Height >= 133) {
                Rectangle rect = new Rectangle(0, 0, 138, 133);
                Bitmap bitmap = source.Clone(rect, source.PixelFormat);
                ColorMateria = new Materia(bitmap);
            } else {
                ColorMateria = null;
            }
        }

        public Materia GetMateria(bool color) {
            return color ? ColorMateria : GrayScaleMateria;
        }

        public Materia ColorMateria { get; private set; }
        public Materia GrayScaleMateria { get; private set; }
    }

    class Materia {
        public Materia(Bitmap source) {
            LoadMateria(source);
        }

        private void LoadMateria(Bitmap source) {
            buttonBitmaps = SplitBitmap(source, 0, 32,
                new int[] {24, 24, 24, 24, 24},
                new int[] {24});

            counterBitmaps = SplitBitmap(source, 34, 81,
                new int[] {13, 13, 13, 13, 13, 13},
                new int[] {23, 23});

            unitBitmaps = SplitBitmap(source, 0, 0,
                new int[] {16, 16, 16, 16, 16, 16, 16, 16},
                new int[] {16, 16});
            
            Rectangle rect = new Rectangle(112, 81, 26, 26);
            buttonFrameBitmap = source.Clone(rect, source.PixelFormat);

            counterFrameBitmaps = SplitBitmap(source, 34, 56,
                new int[] {41, 41},
                new int[] {25});

            gameBackBitmaps = SplitBitmap(source, 0, 56,
                new int[] {9, 16, 9},
                new int[] {52, 16, 9});

            mainFormBackColor = source.GetPixel(137, 0);
        }

        private Bitmap[] SplitBitmap(Bitmap source, int x, int y, int[] widths, int[] heights) {
            int wl = widths.Length;
            int hl = heights.Length;
            Bitmap[] ret = new Bitmap[wl * hl];
            for (int i = 0, cx = x; i < wl; cx += widths[i++]) {
                for (int j = 0, cy = y; j < hl; cy += heights[j++]) {
                    Rectangle rect = new Rectangle(cx, cy, widths[i], heights[j]);
                    ret[j * wl + i] = source.Clone(rect, source.PixelFormat);
                }
            }
            return ret;
        }

        public Bitmap GetButtonInitBitmap() {
            return buttonBitmaps[0];
        }

        public Bitmap GetButtonProbedBitmap() {
            return buttonBitmaps[1];
        }

        public Bitmap GetButtonOverBitmap() {
            return buttonBitmaps[2];
        }

        public Bitmap GetButtonWinBitmap() {
            return buttonBitmaps[3];
        }

        public Bitmap GetButtonPressedBitmap() {
            return buttonBitmaps[4];
        }

        public Bitmap GetCounterNumBitmap(int i) {
            if (i < 0 || i > 9) {
                throw new IndexOutOfRangeException();
            }
            return counterBitmaps[i];
        }

        public Bitmap GetCounterMinusBitmap() {
            return counterBitmaps[11];
        }

        public Bitmap GetUnitSweptNumBitmap(int i) {
            if (i < 0 || i > 8) {
                throw new IndexOutOfRangeException();
            }
            return unitBitmaps[i];
        }

        public Bitmap GetUnitProbedDoubtBitmap() {
            return unitBitmaps[9];
        }

        public Bitmap GetUnitWrongMineBitmap() {
            return unitBitmaps[10];
        }

        public Bitmap GetUnitWrongFlagBitmap() {
            return unitBitmaps[11];
        }

        public Bitmap GetUnitSweptMineBitmap() {
            return unitBitmaps[12];
        }

        public Bitmap GetUnitDoubtBitmap() {
            return unitBitmaps[13];
        }

        public Bitmap GetUnitFlagBitmap() {
            return unitBitmaps[14];
        }

        public Bitmap GetUnitIdleBitmap() {
            return unitBitmaps[15];
        }

        public Bitmap GetNewFrameBitmap() {
            return buttonFrameBitmap;
        }

        public Bitmap GetRemainderFrameBitmap() {
            return counterFrameBitmaps[0];
        }

        public Bitmap GetTimerFrameBitmap() {
            return counterFrameBitmaps[1];
        }

        public Bitmap GetGameBackBitmap(int i) {
            if (i < 0 || i > 8) {
                throw new IndexOutOfRangeException();
            }
            return gameBackBitmaps[i];
        }

        public Color GetMainFormBackColor() {
            return mainFormBackColor;
        }

        public Bitmap[] buttonBitmaps;
        public Bitmap[] counterBitmaps;
        public Bitmap[] unitBitmaps ;

        public Bitmap buttonFrameBitmap;
        public Bitmap[] counterFrameBitmaps;
        public Bitmap[] gameBackBitmaps;

        public Color mainFormBackColor;
    }
}
