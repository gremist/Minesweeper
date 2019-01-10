using System;
using System.Collections.Generic;

namespace Minesweeper {
    class GameLogic {
        public void StartGame(int m, int n, int mines) {
            M = m;
            N = n;

            Mines = mines;
            Remainder = mines;
            safeUnitNum = 0;

            MineMap = new int[m, n];
            MarkMap = new MarkStates[m, n];
            for (int i = 0; i < M; i++) {
                for (int j = 0; j < N; j++) {
                    MineMap[i, j] = 0;
                    MarkMap[i, j] = MarkStates.Idle;
                }
            }
            State = GameStates.Ready;
        }

        public void ResetGame() {
            StartGame(M, N, Mines);
        }

        public void SweepUnit(int x, int y) {
            if (IsInrangeUnit(x, y)) {
                if (State == GameStates.Ready) {
                    LayMines(x, y);
                    State = GameStates.Running;
                }

                if (State == GameStates.Running &&
                    MarkMap[x, y] == MarkStates.Idle ||
                    MarkMap[x, y] == MarkStates.Doubt) {
                    MarkMap[x, y] =  MarkStates.Swept;
                
                    if (MineMap[x, y] == -1) {
                        GameOver();
                        return;
                    }

                    if (++safeUnitNum >= M * N - Mines) {
                        GameWin();
                        return;
                    }

                    if (MineMap[x, y] == 0) {
                        for (int c = 0; c < Cursor.GetLength(0); c++) {
                            int cx = x + Cursor[c, 0];
                            int cy = y + Cursor[c, 1];
                            if (IsInrangeUnit(cx, cy)) {
                                SweepUnit(cx, cy);
                            }
                        }
                    }
                }
            }
        }

        public void SweepArea(int x, int y) {
            if (IsInrangeUnit(x, y) &&
                State == GameStates.Running &&
                MarkMap[x, y] == MarkStates.Swept &&
                MineMap[x, y] > 0) {
                
                int count = 0;
                for (int c = 0; c < Cursor.GetLength(0); c++) {
                    int cx = x + Cursor[c, 0];
                    int cy = y + Cursor[c, 1];
                    if (IsInrangeUnit(cx, cy) &&
                        MarkMap[cx, cy] == MarkStates.Flag) {
                        count++;
                    }
                }

                if (MineMap[x, y] == count) {
                    bool detonate = false;

                    for (int c = 0; c < Cursor.GetLength(0); c++) {
                        int cx = x + Cursor[c, 0];
                        int cy = y + Cursor[c, 1];
                        if (IsInrangeUnit(cx, cy) &&
                           (MarkMap[cx, cy] == MarkStates.Idle ||
                            MarkMap[cx, cy] == MarkStates.Doubt)) {
                            if (MineMap[cx, cy] >= 0) {
                                SweepUnit(cx, cy);
                            } else {
                                detonate = true;
                                MarkMap[cx, cy] = MarkStates.Swept;
                            }
                        }
                    }
                    
                    if (detonate) {
                        GameOver();
                    }
                }
            }
        }

        public void MarkUnit(int x, int y, bool marksDoubt) {
            if (IsInrangeUnit(x, y) &&
               (State == GameStates.Ready ||
                State == GameStates.Running)) {
                switch (MarkMap[x, y]) {
                case MarkStates.Idle:
                    MarkMap[x, y] = MarkStates.Flag;
                    Remainder--;
                    break;
                case MarkStates.Flag:
                    MarkMap[x, y] = marksDoubt ? MarkStates.Doubt : MarkStates.Idle;
                    Remainder++;
                    break;
                case MarkStates.Doubt:
                    MarkMap[x, y] = MarkStates.Idle;
                    break;
                }
            }
        }

        public bool IsInrangeUnit(int x, int y) {
            return 0 <= x && x < M && 0 <= y && y < N;
        }

        private void LayMines(int entx, int enty) {
            List<int> sampleList = new List<int>(M * N);
            for (int i = 0; i < M * N; i++) {
                sampleList.Add(i);
            }
            sampleList.RemoveAt(entx * N + enty);

            Random random = new Random();
            for (int n = 0; n < Mines; n++) {
                int r = random.Next(sampleList.Count);
                int i = sampleList[r] / N;
                int j = sampleList[r] % N;
                MineMap[i, j] = -1;
                sampleList.RemoveAt(r);
            }

            for (int i = 0; i < M; i++) {
                for (int j = 0; j < N; j++) {
                    if (MineMap[i, j] == -1) continue;
                    int count = 0;
                    for (int p = 0; p < Cursor.GetLength(0); p++) {
                        int cx = i + Cursor[p, 0];
                        int cy = j + Cursor[p, 1];
                        if (IsInrangeUnit(cx, cy) && MineMap[cx, cy] < 0) {
                            count++;
                        }
                    }
                    MineMap[i, j] = count;
                }
            }
        }

        private void GameOver() {
            for (int i = 0; i < M; i++) {
                for (int j = 0; j < N; j++) {
                    if (MineMap[i, j] == -1 && MarkMap[i, j] == MarkStates.Idle  ||
                        MineMap[i, j] == -1 && MarkMap[i, j] == MarkStates.Doubt ||
                        MineMap[i, j] >=  0 && MarkMap[i, j] == MarkStates.Flag) {
                        MarkMap[i, j] = MarkStates.Wrong;
                    }
                }
            }
            State = GameStates.Over;
        }

        private void GameWin() {
            for (int i = 0; i < M; i++) {
                for (int j = 0; j < N; j++) {
                    if (MineMap[i, j] == -1 && MarkMap[i, j] == MarkStates.Idle  ||
                        MineMap[i, j] == -1 && MarkMap[i, j] == MarkStates.Doubt) {
                        MarkMap[i, j] = MarkStates.Flag;
                        Remainder--;
                    }
                }
            }
            State = GameStates.Win;
        }

        public enum MarkStates {
            Idle, Flag, Doubt, Swept, Wrong
        }

        public enum GameStates {
            Ready, Running, Win, Over
        }

        public int[,] Cursor { get; } = {
            {-1, -1}, {-1,  0}, {-1,  1}, { 0, -1},
            { 0,  1}, { 1, -1}, { 1,  0}, { 1,  1}
        };

        public int M { get; private set; }
        public int N { get; private set; }
        public int Mines { get; private set; }
        public int Remainder { get; private set; }
        public int[,] MineMap { get; private set; }
        public MarkStates[,] MarkMap { get; private set; }
        public GameStates State { get; private set; }
        
        private int safeUnitNum;
    }
}