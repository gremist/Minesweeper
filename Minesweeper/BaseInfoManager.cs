using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Minesweeper {
    class BaseInfoManager {
        static readonly string dir = Environment.GetFolderPath(
            Environment.SpecialFolder.UserProfile,
            Environment.SpecialFolderOption.DoNotVerify) + 
            @"\Saved Games\Minesweeper";
        static readonly string file = "Minesweeper.save";
        static readonly string path = dir + @"\" + file;

        public void LoadLocalData() {
            if (!File.Exists(path)) {
                Local = new BaseInfo();
                return;
            }

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(path, FileMode.Open);
            try {
                Local = (BaseInfo)formatter.Deserialize(file);
            } catch {
                Local = new BaseInfo();
            }
            file.Close();
        }

        public void SaveLocalData() {
            if (!Directory.Exists(dir)) {
                Directory.CreateDirectory(dir);
            }

            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Create(path);
            formatter.Serialize(file, Local);
            file.Close();
        }

        public BaseInfo Local { get; private set; }
    }

    [Serializable]
    class BaseInfo {
        public BaseInfo() {
            Level = Levels.Beginner;
            MarksDoubt = true;
            Color = true;
            Sound = false;
            Location = new Point();

            Game = new GameInfo();
            BestTimes = new BestTimesInfo();
        }

        public enum Levels {
            Beginner, Intermediate, Expert, Custom
        }
        
        public Levels Level { get; set; }
        public bool MarksDoubt { get; set; }
        public bool Color { get; set; }
        public bool Sound { get; set; }
        public Point Location { get; set; }
        public GameInfo Game { get; set; }
        public BestTimesInfo BestTimes { get; set; }
    }

    [Serializable]
    class GameInfo {
        public GameInfo() {
            M = 9;
            N = 9;
            Mines = 10;
        }

        public int M { get; set; }
        public int N { get; set; }
        public int Mines { get; set; }
    }

    [Serializable]
    class BestTimesInfo {
        public BestTimesInfo() {
            Beginner = new BestTime();
            Intermediate = new BestTime();
            Expert = new BestTime();
        }

        public BestTime Beginner;
        public BestTime Intermediate;
        public BestTime Expert;
    }

    [Serializable]
    class BestTime {
        public BestTime() {
            Time = 999;
            Player = "Anonymous";
        }

        public int Time { get; set; }
        public string Player { get; set; }
    }
}
