using System.Windows.Forms;

namespace Minesweeper {
    partial class MainForm {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent() {
            this.SuspendLayout();

            // Init mainForm
            this.Location = baseInfo.Location;
            this.Icon = Properties.Resources.Mine;
            this.Text = "Minesweeper";
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = baseInfo.Location.IsEmpty ?
                FormStartPosition.CenterScreen :
                FormStartPosition.Manual;
            this.FormClosed += MainForm_FormClosed;
            
            // Init timer
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;   
            this.timer = timer;
            
            // Init topMenu
            MainMenu topMenu = new MainMenu();
            this.Menu = topMenu;
            
            MenuItem topMenuGame = new MenuItem();
            topMenuGame.Text = "&Game";
            topMenu.MenuItems.Add(topMenuGame);

            MenuItem topMenuGameNew = new MenuItem();
            topMenuGameNew.Text = "&New";
            topMenuGameNew.Shortcut = Shortcut.F2;
            topMenuGameNew.Click += TopMenuGameNew_Click;
            topMenuGame.MenuItems.Add(topMenuGameNew);

            MenuItem topMenuGameSeparator1 = new MenuItem();
            topMenuGameSeparator1.Text = "-";
            topMenuGame.MenuItems.Add(topMenuGameSeparator1);

            MenuItem topMenuGameBeginner = new MenuItem();
            topMenuGameBeginner.Text = "&Beginner";
            topMenuGameBeginner.Click += TopMenuGameBeginner_Click;
            topMenuGame.MenuItems.Add(topMenuGameBeginner);
            this.topMenuGameBeginner = topMenuGameBeginner;

            MenuItem topMenuGameIntermediate = new MenuItem();
            topMenuGameIntermediate.Text = "&Intermediate";
            topMenuGameIntermediate.Click += TopMenuGameIntermediate_Click;
            topMenuGame.MenuItems.Add(topMenuGameIntermediate);
            this.topMenuGameIntermediate = topMenuGameIntermediate;

            MenuItem topMenuGameExpert = new MenuItem();
            topMenuGameExpert.Text = "&Expert";
            topMenuGameExpert.Click += TopMenuGameExpert_Click;
            topMenuGame.MenuItems.Add(topMenuGameExpert);
            this.topMenuGameExpert = topMenuGameExpert;
            
            MenuItem topMenuGameCustom = new MenuItem();
            topMenuGameCustom.Text = "&Custom...";
            topMenuGameCustom.Click += TopMenuGameCustom_Click;
            topMenuGame.MenuItems.Add(topMenuGameCustom);
            this.topMenuGameCustom = topMenuGameCustom;
            
            MenuItem topMenuGameSeparator2 = new MenuItem();
            topMenuGameSeparator2.Text = "-";
            topMenuGame.MenuItems.Add(topMenuGameSeparator2);

            MenuItem topMenuGameMarksDoubt = new MenuItem();
            topMenuGameMarksDoubt.Text = "&Marks (?)";
            topMenuGameMarksDoubt.Click += TopMenuGameMarksDoubt_Click;
            topMenuGame.MenuItems.Add(topMenuGameMarksDoubt);
            this.topMenuGameMarksDoubt = topMenuGameMarksDoubt;

            MenuItem topMenuGameColor = new MenuItem();
            topMenuGameColor.Text = "Co&lor";
            topMenuGameColor.Click += TopMenuGameColor_Click;
            topMenuGame.MenuItems.Add(topMenuGameColor);
            this.topMenuGameColor = topMenuGameColor;

            MenuItem topMenuGameSound = new MenuItem();
            topMenuGameSound.Text = "&Sound";
            topMenuGameSound.Click += TopMenuGameSound_Click;
            topMenuGame.MenuItems.Add(topMenuGameSound);
            this.topMenuGameSound = topMenuGameSound;

            MenuItem topMenuGameSeparator3 = new MenuItem();
            topMenuGameSeparator3.Text = "-";
            topMenuGame.MenuItems.Add(topMenuGameSeparator3);

            MenuItem topMenuGameBestTimes = new MenuItem();
            topMenuGameBestTimes.Text = "Best &Times...";
            topMenuGameBestTimes.Click += TopMenuGameBestTimes_Click;
            topMenuGame.MenuItems.Add(topMenuGameBestTimes);

            MenuItem topMenuGameSeparator4 = new MenuItem();
            topMenuGameSeparator4.Text = "-";
            topMenuGame.MenuItems.Add(topMenuGameSeparator4);

            MenuItem topMenuGameExit = new MenuItem();
            topMenuGameExit.Text = "E&xit";
            topMenuGameExit.Click += TopMenuGameExit_Click;
            topMenuGame.MenuItems.Add(topMenuGameExit);

            MenuItem topMenuHelp = new MenuItem();
            topMenuHelp.Text = "&Help";
            topMenu.MenuItems.Add(topMenuHelp);

            MenuItem topMenuHelpContents = new MenuItem();
            topMenuHelpContents.Text = "&Contents";
            topMenuHelpContents.Shortcut = Shortcut.F1;
            topMenuHelpContents.Enabled = false;
            topMenuHelp.MenuItems.Add(topMenuHelpContents);

            MenuItem topMenuHelpSearchForHelp = new MenuItem();
            topMenuHelpSearchForHelp.Text = "&Search for Help on...";
            topMenuHelpSearchForHelp.Enabled = false;
            topMenuHelp.MenuItems.Add(topMenuHelpSearchForHelp);

            MenuItem topMenuHelpUsingHelp = new MenuItem();
            topMenuHelpUsingHelp.Text = "Using &Help";
            topMenuHelpUsingHelp.Enabled = false;
            topMenuHelp.MenuItems.Add(topMenuHelpUsingHelp);

            MenuItem topMenuHelpSeparator = new MenuItem();
            topMenuHelpSeparator.Text = "-";
            topMenuHelp.MenuItems.Add(topMenuHelpSeparator);

            MenuItem topMenuHelpAboutMinesweeper = new MenuItem();
            topMenuHelpAboutMinesweeper.Text = "&About Minesweeper...";
            topMenuHelpAboutMinesweeper.Click += TopMenuHelpAboutMinesweeper_Click;
            topMenuHelp.MenuItems.Add(topMenuHelpAboutMinesweeper);

            // Init gameBox
            PictureBox gameBox = new PictureBox();
            gameBox.MouseMove += GameBox_MouseMove;
            gameBox.MouseDown += GameBox_MouseDown;
            gameBox.MouseUp += GameBox_MouseUp;
            this.Controls.Add(gameBox);
            this.gameBox = gameBox;

            this.ResumeLayout(false);
        }

        private Timer timer;

        private MenuItem topMenuGameBeginner;
        private MenuItem topMenuGameIntermediate;
        private MenuItem topMenuGameExpert;
        private MenuItem topMenuGameCustom;

        private MenuItem topMenuGameMarksDoubt;
        private MenuItem topMenuGameColor;
        private MenuItem topMenuGameSound;
        
        private PictureBox gameBox;
    }
}

