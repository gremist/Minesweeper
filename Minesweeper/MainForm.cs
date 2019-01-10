using System;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Minesweeper {
    partial class MainForm : Form {
        public MainForm() {
            // 加载本地数据
            baseInfoManager = new BaseInfoManager();
            baseInfoManager.LoadLocalData();
            baseInfo = baseInfoManager.Local;

            // 加载位图素材
            materiaManager = new MateriaManager();
            materiaManager.ConstructMateria(Properties.Resources.ClassicStyle);
            materia = materiaManager.GetMateria(baseInfo.Color);

            // 加载音效
            tickSound = new SoundPlayer(Properties.Resources.Tick);
            gameWinSound = new SoundPlayer(Properties.Resources.GameWin);
            gameOverSound = new SoundPlayer(Properties.Resources.GameOver);

            // 加载游戏逻辑
            game = new GameLogic();

            // 初始化容器
            InitializeComponent();

            // 开始游戏
            RefreshTopMenuGame();
            RefreshMainFormBackColor();
            ResetGame();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {            
            // 保存本地数据
            baseInfo.Location = this.Location;
            baseInfoManager.SaveLocalData();
        }

        private void Timer_Tick(object sender, EventArgs e) {
            if (timeSpent < 999) {
                timeSpent++;
                if (baseInfo.Sound) {
                    tickSound.Play();
                }
            }
            RedrawTimer();
            gameBox.Refresh();
        }

        private void TopMenuGameNew_Click(object sender, EventArgs e) {
            ResetGame();
        }

        private void TopMenuGameBeginner_Click(object sender, EventArgs e) {
            baseInfo.Level = BaseInfo.Levels.Beginner;
            RefreshTopMenuGameLevel();

            baseInfo.Game.M = 9;
            baseInfo.Game.N = 9;
            baseInfo.Game.Mines = 10;
            
            ResetGame();
        }

        private void TopMenuGameIntermediate_Click(object sender, EventArgs e) {
            baseInfo.Level = BaseInfo.Levels.Intermediate;
            RefreshTopMenuGameLevel();

            baseInfo.Game.M = 16;
            baseInfo.Game.N = 16;
            baseInfo.Game.Mines = 40;

            ResetGame();
        }

        private void TopMenuGameExpert_Click(object sender, EventArgs e) {
            baseInfo.Level = BaseInfo.Levels.Expert;
            RefreshTopMenuGameLevel();
            
            baseInfo.Game.M = 30;
            baseInfo.Game.N = 16;
            baseInfo.Game.Mines = 99;

            ResetGame();
        }

        private void TopMenuGameCustom_Click(object sender, EventArgs e) {
            baseInfo.Level = BaseInfo.Levels.Custom;
            RefreshTopMenuGameLevel();

            CustomForm customForm = new CustomForm(baseInfo.Game, PointToScreen(new Point(0, 0)));
            customForm.ShowDialog();
            customForm.Dispose();

            ResetGame();
        }

        private void TopMenuGameMarksDoubt_Click(object sender, EventArgs e) {
            baseInfo.MarksDoubt = !baseInfo.MarksDoubt;
            RefreshTopMenuGameMarksDoubt();
        }

        private void TopMenuGameColor_Click(object sender, EventArgs e) {
            baseInfo.Color = !baseInfo.Color;
            RefreshTopMenuGameColor();
            
            materia = materiaManager.GetMateria(baseInfo.Color);
            RedrawGameBack();
            RedrawGameBox();
            gameBox.Refresh();
        }

        private void TopMenuGameSound_Click(object sender, EventArgs e) {
            baseInfo.Sound = !baseInfo.Sound;
            RefreshTopMenuGameSound();
        }

        private void TopMenuGameBestTimes_Click(object sender, EventArgs e) {
            BestTimesForm bestTimesForm = new BestTimesForm(baseInfo.BestTimes, PointToScreen(new Point(0, 0)));
            bestTimesForm.ShowDialog();
            bestTimesForm.Dispose();
        }

        private void TopMenuGameExit_Click(object sender, EventArgs e) {
            Close();
        }

        private void TopMenuHelpAboutMinesweeper_Click(object sender, EventArgs e) {
            AboutBox aboutBox = new AboutBox(PointToScreen(new Point(0, 0)));
            aboutBox.ShowDialog();
            aboutBox.Dispose();
        }

        private void GameBox_MouseMove(object sender, MouseEventArgs e) {
            Point p = gameBox.PointToClient(MousePosition);

            if (clickState == ClickStates.PressNewBox) {
                bool inButton = 8 * game.M - 3 <= p.X &&
                    p.X < 8 * game.M + 21 && 13 <= p.Y && p.Y <= 37;
                
                if (inButton) {
                    DrawBitmap(materia.GetButtonPressedBitmap(), 8 * game.M - 3, 13);
                } else {
                    RedrawButton();
                }

                perInButton = inButton;
                gameBox.Refresh();
                return;
            }

            if (clickState == ClickStates.ProbeUnit ||
                clickState == ClickStates.ProbeArea) {

                if (game.State == GameLogic.GameStates.Win ||
                    game.State == GameLogic.GameStates.Over) {
                    return;
                }

                int x = (int)Math.Floor((float)(p.X -  9) / 16);
                int y = (int)Math.Floor((float)(p.Y - 52) / 16);

                if (x == perx && y == pery) {
                    return;
                }

                if (game.IsInrangeUnit(perx, pery)) {
                    if (clickState == ClickStates.ProbeUnit) {
                        RedrawUnit(perx, pery);
                    } else if (clickState == ClickStates.ProbeArea) {
                        RedrawArea(perx, pery);
                    }
                }

                if (game.IsInrangeUnit(x, y)) {
                    if (clickState == ClickStates.ProbeUnit) {
                        ProbeUnit(x, y);
                    } else if (clickState == ClickStates.ProbeArea) {
                        ProbeArea(x, y);
                    }
                }

                perx = x;
                pery = y;
            }

            gameBox.Refresh();
        }

        private void GameBox_MouseDown(object sender, MouseEventArgs e) {
            Point p = gameBox.PointToClient(MousePosition);

            if (clickState == ClickStates.Idle ||
                clickState == ClickStates.ProbeUnit) {

                bool inButton = 8 * game.M - 3 <= p.X &&
                    p.X < 8 * game.M + 21 && 13 <= p.Y && p.Y <= 37;

                if (MouseButtons == MouseButtons.Left && inButton) {
                    clickState = ClickStates.PressNewBox;
                    DrawBitmap(materia.GetButtonPressedBitmap(), 8 * game.M - 3, 13);
                    perInButton = true;
                    gameBox.Refresh();
                    return;
                }

                if (game.State == GameLogic.GameStates.Win ||
                    game.State == GameLogic.GameStates.Over) {
                    return;
                }

                int x = (int)Math.Floor((float)(p.X -  9) / 16);
                int y = (int)Math.Floor((float)(p.Y - 52) / 16);
            
                if (MouseButtons == MouseButtons.Middle ||
                    MouseButtons == MouseButtons.Left && ModifierKeys == Keys.Shift ||
                    MouseButtons == (MouseButtons.Left | MouseButtons.Right)) {
                    DrawBitmap(materia.GetButtonProbedBitmap(), 8 * game.M - 3, 13);
                    clickState = ClickStates.ProbeArea;
                    if (game.IsInrangeUnit(x, y)) {
                        ProbeArea(x, y);
                    }
                    perx = x;
                    pery = y;
                    gameBox.Refresh();
                    return;
                }
            
                if (MouseButtons == MouseButtons.Left) {
                    DrawBitmap(materia.GetButtonProbedBitmap(), 8 * game.M - 3, 13);
                    clickState = ClickStates.ProbeUnit;
                    if (game.IsInrangeUnit(x, y)) {
                        ProbeUnit(x, y);
                    }
                    perx = x;
                    pery = y;
                    gameBox.Refresh();
                    return;
                }

                if (MouseButtons == MouseButtons.Right &&
                    game.IsInrangeUnit(x, y)) {
                    game.MarkUnit(x, y, baseInfo.MarksDoubt);
                    RedrawRemainder();
                    RedrawUnit(x, y);
                }
            }
            gameBox.Refresh();
        }

        private void GameBox_MouseUp(object sender, MouseEventArgs e) {
            if (clickState == ClickStates.PressNewBox) {
                if (perInButton) {
                    ResetGame();
                    clickState = ClickStates.Idle;
                    return;
                }
            }

            bool win = false;
            if (clickState == ClickStates.ProbeUnit ||
                clickState == ClickStates.ProbeArea) {

                if (game.State == GameLogic.GameStates.Win ||
                    game.State == GameLogic.GameStates.Over) {
                    return;
                }

                if (game.IsInrangeUnit(perx, pery)) {
                    if (game.State == GameLogic.GameStates.Ready) {
                        timeSpent++;
                        if (baseInfo.Sound) {
                            tickSound.Play();
                        }
                        RedrawTimer();
                        timer.Start();
                    }
                
                    if (clickState == ClickStates.ProbeUnit) {
                        game.SweepUnit(perx, pery);
                    } else if (clickState == ClickStates.ProbeArea) {
                        game.SweepArea(perx, pery);
                    }

                    if (game.State == GameLogic.GameStates.Win) {
                        timer.Stop();
                        if (baseInfo.Sound) {
                            gameWinSound.Play();
                        }
                        win = true;
                    }
                    
                    if (game.State == GameLogic.GameStates.Over) {
                        timer.Stop();
                        if (baseInfo.Sound) {
                            gameOverSound.Play();
                        }
                    }

                    RedrawRemainder();
                    RedrawGameBox();
                }
                RedrawButton();
                gameBox.Refresh();
            }

            clickState = ClickStates.Idle;

            if (win) {
                CheckRecord();
            }
        }

        private void CheckRecord() {
            string level;
            BestTime best;

            switch (baseInfo.Level) {
            case BaseInfo.Levels.Beginner:
                level = "beginner";
                best = baseInfo.BestTimes.Beginner;
                break;
            case BaseInfo.Levels.Intermediate:
                level = "intermediate";
                best = baseInfo.BestTimes.Intermediate;
                break;
            case BaseInfo.Levels.Expert:
                level = "expert";
                best = baseInfo.BestTimes.Expert;
                break;
            default:
                return;
            }

            if (timeSpent < best.Time) {
                RecordForm recordForm = new RecordForm(level, best.Player);
                recordForm.ShowDialog();
                best.Player = recordForm.player;
                best.Time = timeSpent;
                recordForm.Dispose();

                BestTimesForm bestTimesForm = new BestTimesForm(baseInfo.BestTimes, PointToScreen(new Point(0, 0)));
                bestTimesForm.ShowDialog();
                bestTimesForm.Dispose();
            }
        }

        private void ResetGame() {
            timer.Stop();
            timeSpent = 0;

            int perm = game.M;
            int pern = game.N;

            game.StartGame(baseInfo.Game.M, baseInfo.Game.N, baseInfo.Game.Mines);

            if (game.M != perm || game.N != pern) {
                Bitmap bitmap = new Bitmap(18 + 16 * game.M, 61 + 16 * game.N);

                this.ClientSize = bitmap.Size;

                gameBox.Location = new Point(0, 0);
                gameBox.Size = bitmap.Size;
                gameBox.Image = bitmap;
                
                RedrawGameBack();
            }

            RedrawGameBox();
            gameBox.Refresh();
        }

        private void RefreshTopMenuGame() {
            RefreshTopMenuGameLevel();
            RefreshTopMenuGameMarksDoubt();
            RefreshTopMenuGameColor();
            RefreshTopMenuGameSound();
        }

        private void RefreshTopMenuGameLevel() {
            topMenuGameBeginner.Checked = baseInfo.Level == BaseInfo.Levels.Beginner;
            topMenuGameIntermediate.Checked = baseInfo.Level == BaseInfo.Levels.Intermediate;
            topMenuGameExpert.Checked = baseInfo.Level == BaseInfo.Levels.Expert;
            topMenuGameCustom.Checked = baseInfo.Level == BaseInfo.Levels.Custom;
        }

        private void RefreshTopMenuGameMarksDoubt() {
            topMenuGameMarksDoubt.Checked = baseInfo.MarksDoubt;
        }

        private void RefreshTopMenuGameColor() {
            topMenuGameColor.Checked = baseInfo.Color;
        }

        private void RefreshTopMenuGameSound() {
            topMenuGameSound.Checked = baseInfo.Sound;
        }

        private void RefreshMainFormBackColor() {
            this.BackColor = materia.GetMainFormBackColor();
        }

        private void RedrawGameBack() {
            DrawBitmap(materia.GetGameBackBitmap(0), 0, 0);
            DrawBitmap(materia.GetGameBackBitmap(2), 16 * game.M + 9, 0);
            DrawBitmap(materia.GetGameBackBitmap(6), 0, 16 * game.N + 52);
            DrawBitmap(materia.GetGameBackBitmap(8), 16 * game.M + 9, 16 * game.N + 52);

            for (int i = 0; i < game.M; i++) {
                DrawBitmap(materia.GetGameBackBitmap(1), 16 * i + 9, 0);
            }
            for (int j = 0; j < game.N; j++) {
                DrawBitmap(materia.GetGameBackBitmap(3), 0, 16 * j + 52);
            }
            for (int j = 0; j < game.N; j++) {
                DrawBitmap(materia.GetGameBackBitmap(5), 16 * game.M + 9, 16 * j + 52);
            }
            for (int i = 0; i < game.M; i++) {
                DrawBitmap(materia.GetGameBackBitmap(7), 16 * i + 9, 16 * game.N + 52);
            }

            for (int i = 0; i < game.M; i++) {
                for (int j = 0; j < game.N; j++) {
                    DrawBitmap(materia.GetGameBackBitmap(4), 16 * i + 9, 16 * j + 52);
                }
            }

            DrawBitmap(materia.GetNewFrameBitmap(), 8 * game.M - 4, 12);
            DrawBitmap(materia.GetRemainderFrameBitmap(), 14, 12);
            DrawBitmap(materia.GetTimerFrameBitmap(), 16 * game.M - 37, 12);
        }

        private void RedrawGameBox() {
            RedrawButton();
            RedrawRemainder();
            RedrawTimer();
            RedrawMineField();
        }
        
        private void RedrawButton() {
            switch (game.State) {
            case GameLogic.GameStates.Ready:
            case GameLogic.GameStates.Running:
                DrawBitmap(materia.GetButtonInitBitmap(), 8 * game.M - 3, 13);
                break;
            case GameLogic.GameStates.Win:
                DrawBitmap(materia.GetButtonWinBitmap(), 8 * game.M - 3, 13);
                break;
            case GameLogic.GameStates.Over:
                DrawBitmap(materia.GetButtonOverBitmap(), 8 * game.M - 3, 13);
                break;
            }
        }

        private void RedrawRemainder() {
            RedrawCounter(game.Remainder, 15, 13);
        }

        private void RedrawTimer() {
            RedrawCounter(timeSpent, 16 * game.M - 36, 13);
        }

        private void RedrawMineField() {
            for (int i = 0; i < game.M; i++) {
                for (int j = 0; j < game.N; j++) {
                    RedrawUnit(i, j);
                }
            }
        }

        private void RedrawArea(int x, int y) {
            RedrawUnit(x, y);
            for (int c = 0; c < game.Cursor.GetLength(0); c++) {
                int cx = x + game.Cursor[c, 0];
                int cy = y + game.Cursor[c, 1];
                if (game.IsInrangeUnit(cx, cy)) {
                    RedrawUnit(cx, cy);
                }
            }
        }

        private void RedrawUnit(int x, int y) {
            switch (game.MarkMap[x, y]) {                  
            case GameLogic.MarkStates.Idle:
                DrawBitmap(materia.GetUnitIdleBitmap(), 9 + 16 * x, 52 + 16 * y);
                break;
            case GameLogic.MarkStates.Flag:
                DrawBitmap(materia.GetUnitFlagBitmap(), 9 + 16 * x, 52 + 16 * y);
                break;
            case GameLogic.MarkStates.Doubt:
                DrawBitmap(materia.GetUnitDoubtBitmap(), 9 + 16 * x, 52 + 16 * y);
                break;
            case GameLogic.MarkStates.Swept:
                if (game.MineMap[x, y] < 0) {
                    DrawBitmap(materia.GetUnitSweptMineBitmap(), 9 + 16 * x, 52 + 16 * y);
                } else {
                    DrawBitmap(materia.GetUnitSweptNumBitmap(game.MineMap[x, y]), 9 + 16 * x, 52 + 16 * y);
                }
                break;
            case GameLogic.MarkStates.Wrong:
                if (game.MineMap[x, y] < 0) {
                    DrawBitmap(materia.GetUnitWrongMineBitmap(), 9 + 16 * x, 52 + 16 * y);
                } else {
                    DrawBitmap(materia.GetUnitWrongFlagBitmap(), 9 + 16 * x, 52 + 16 * y);
                }
                break;
            }
        }

        private void ProbeArea(int x, int y) {
            ProbeUnit(x, y);
            for (int c = 0; c < game.Cursor.GetLength(0); c++) {
                int cx = x + game.Cursor[c, 0];
                int cy = y + game.Cursor[c, 1];
                if (game.IsInrangeUnit(cx, cy)) {
                    ProbeUnit(cx, cy);
                }
            }
        }

        private void ProbeUnit(int x, int y) {
            switch(game.MarkMap[x, y]) {
            case GameLogic.MarkStates.Idle:
                DrawBitmap(materia.GetUnitSweptNumBitmap(0), 9 + 16 * x, 52 + 16 * y);
                break;
            case GameLogic.MarkStates.Doubt:
                DrawBitmap(materia.GetUnitProbedDoubtBitmap(), 9 + 16 * x, 52 + 16 * y);
                break;
            }
        }

        private void RedrawCounter(int count, int px, int py) {
            bool minus = count < 0;
            count = Math.Abs(count);

            for (int i = 2; i >= 0; i--) {
                DrawBitmap(materia.GetCounterNumBitmap(count % 10), px + 13 * i, 13);
                count /= 10;
            }
            
            if (minus) {
                DrawBitmap(materia.GetCounterMinusBitmap(), px, py);
            }
        }

        private void DrawBitmap(Bitmap bitmap, int px, int py) {
            Graphics graphics = Graphics.FromImage(gameBox.Image);
            graphics.DrawImage(bitmap, px, py, bitmap.Width, bitmap.Height);
            graphics.Dispose();
        }

        enum ClickStates {
            Idle, PressNewBox, ProbeUnit, ProbeArea
        }
        
        // 游戏逻辑
        private GameLogic game;

        // 本地数据
        private BaseInfoManager baseInfoManager;
        private BaseInfo baseInfo;

        // 位图素材
        private MateriaManager materiaManager;
        private Materia materia;

        // 音效
        private SoundPlayer tickSound;
        private SoundPlayer gameWinSound;
        private SoundPlayer gameOverSound;
        
        // 定时器
        private int timeSpent;

        // 界面交互
        private ClickStates clickState = ClickStates.Idle;
        private bool perInButton;
        private int perx, pery;
    }
}
