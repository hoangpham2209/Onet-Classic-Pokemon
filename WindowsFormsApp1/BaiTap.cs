using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1
{
    public partial class BaiTap : Form 
    {
        protected int pokemon1, pokemon2, score, endGame, level, width, height, pokemons;
        protected byte countClick = 0;
        protected PictureBox[] pb;
        protected PictureBox overlayPb = new PictureBox();
        protected Model model;
        protected Timer time;

        public BaiTap()
        {
            InitializeComponent();
        }

        // =========================== Game Layout ===============================

        public void menu()
        {
            this.BackColor = Color.Orange;
            showImage();
            showMenuButton();
        }

        public void randomBackground()
        {
            Random random = new Random();
            string namePic = random.Next(1, 13).ToString() + ".jpg";

            this.BackgroundImage = Image.FromFile((@"bg\" + namePic));
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void makeGame(int width, int height, int pokemons)
        {
            randomBackground();
            this.BackColor = Color.DarkGray;

            model = new Model(width, height, pokemons);
            pb = new PictureBox[model.Height * model.Width];

            for (int i = 0; i < model.Height; i++)
                for (int j = 0; j < model.Width; j++)
                {
                    int cell = i * model.Width + j;
                    pb[cell] = new PictureBox();
                    pb[cell].Width = 43;
                    pb[cell].Height = 53;

                    switch (width)    // Padding
                    {
                        case 10:
                            pb[cell].Top = 160 + i * 53;
                            pb[cell].Left = 270 + j * 43;
                            break;
                        case 15:
                            pb[cell].Top = 45 + i * 53;
                            pb[cell].Left = 170 + j * 43;
                            break;
                        case 20:
                            pb[cell].Top = 45 + i * 53;
                            pb[cell].Left = 50 + j * 43;
                            break;
                    }

                    // Border color
                    pb[cell].SizeMode = PictureBoxSizeMode.CenterImage;
                    pb[cell].BackColor = Color.LightGray;

                    pb[cell].Click += new EventHandler(pictureBoxClickEventhandle);
                    pb[cell].MouseHover += new EventHandler(pictureBoxMouseHoverEventhandle);
                    pb[cell].MouseLeave += new EventHandler(pictureBoxMouseLeaveEventhandle);

                    string name = "pic" + model.cellIndex(i, j).ToString() + ".png";
                    pb[cell].Image = Image.FromFile((@"img\" + name));
                    this.Controls.Add(pb[cell]);
                }
        }

        public void pictureBoxMouseHoverEventhandle(object sender, EventArgs e)
        {
            (sender as PictureBox).BackColor = Color.Red;
        }

        public void pictureBoxMouseLeaveEventhandle(object sender, EventArgs e)
        {
            (sender as PictureBox).BackColor = Color.LightGray;
        }

        public void pictureBoxClickEventhandle(object sender, EventArgs e)
        {
            if (progressBar1.Value != 0)
            {
                countClick++;
                addOverlay(sender);
                samePokemon(sender);
                save();
            }
            else saveBoard();
        }

        public void saveBoard()
        {
            time.Stop();
            panelSave.Show();
            lbScore.Text = scoring.Text;
            lbBonus.Text = progressBar1.Value.ToString();

            if (progressBar1.Value == 0)
            {
                label1.Text = "LOSER";
                label1.ForeColor = Color.Red;
                lbNextLevel.Text = "Play Again";
            }

            score += progressBar1.Value;
            lbTotal.Text = (score).ToString();
        }

        public void deleteAllPokemons()
        {
            for (int i = 0; i < model.Height; i++)
                for (int j = 0; j < model.Width; j++)
                {
                    int cell = i * model.Width + j;
                    pb[cell].Dispose();
                }
        }

        public void countDown(int sec)
        {
            progressBar1.Value = 100;
            time = new Timer();
            time.Interval = sec; // time * 100 
            time.Tick += new EventHandler(barTime);

            time.Start();
        }

        public void barTime(object sender, EventArgs e)
        {
            progressBar1.Increment(-1);
            if (progressBar1.Value == progressBar1.Maximum)
                time.Stop();
        }

        public void overlay()
        {
            overlayPb.Dock = DockStyle.Fill;
            overlayPb.BackColor = Color.FromArgb(150, Color.Black);
        }

        public void addOverlay(object sender)
        {
            overlay();
            PictureBox pic = (sender as PictureBox);
            pic.Controls.Add(overlayPb);
        }

        public void removeOverlay(object sender)
        {
            overlay();
            PictureBox pic = (sender as PictureBox);
            pic.Controls.Remove(overlayPb);
        }

        public void hideMenuButton()
        {
            btCredit.Hide();
            btExit.Hide();
            btLeader.Hide();
            btPlay.Hide();
        }

        public void hideModeButton()
        {
            btEasy.Hide();
            btNormal.Hide();
            btHard.Hide();
            btBack.Hide();
        }

        public void hideImage()
        {
            logo.Hide();
            img1.Hide();
            img2.Hide();
        }

        public void showImage()
        {
            logo.Show();
            img1.Show();
            img2.Show();
        }

        public void showMenuButton()
        {
            lbBack.Hide();
            lbIntro.Hide();
            hideModeButton();

            btCredit.Show();
            btExit.Show();
            btLeader.Show();
            btPlay.Show();
        }

        public void showModeButton()
        {
            hideMenuButton();
            lbBack.Hide();
            lbIntro.Hide();

            btEasy.Show();
            btNormal.Show();
            btHard.Show();
            btBack.Show();
        }

        public void showTopBar()
        {
            txtScore.Show();
            lbLv.Show();
            lvl.Show();
            scoring.Show();
            progressBar1.Show();
        }

        public void lbMenu_Click(object sender, EventArgs e)
        {
            panelSave.Hide();
            this.BackgroundImage = null;
            this.BackColor = Color.Orange;
            panelTopBar.Hide();

            removeOverlay(pb[pokemon1]);
            deleteAllPokemons();
            menu();
        }

        public void btExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void btPlay_Click(object sender, EventArgs e)
        {
            hideMenuButton();
            showModeButton();
        }

        public void btLeader_Click(object sender, EventArgs e)
        {

        }

        public void btCredit_Click(object sender, EventArgs e)
        {
            hideMenuButton();
            lbBack.Show();
            lbIntro.Show();
        }

        public void lbBack_Click(object sender, EventArgs e)
        {
            showMenuButton();
        }

        public void btBack_Click(object sender, EventArgs e)
        {
            hideModeButton();
            showMenuButton();
        }

        // =========================== Logic Game ===============================

        public void samePokemon(object sender)
        {
            if (countClick == 2)
            {
                pokemon2 = int.Parse(Array.IndexOf(pb, (PictureBox)sender).ToString());
                removeOverlay(pb[pokemon1]);
                removeOverlay(pb[pokemon2]);

                if (model.cellIndex(pokemon1 / model.Width, pokemon1 % model.Width) == model.cellIndex(pokemon2 / model.Width, pokemon2 % model.Width))
                {
                    pb[pokemon1].Dispose();
                    pb[pokemon2].Dispose();

                    score += 10;
                    scoring.Text = score.ToString();
                    endGame++;
                }

                countClick = 0;
            }
            else pokemon1 = int.Parse(Array.IndexOf(pb, (PictureBox)sender).ToString());
        }

        public void save()
        {
            if ((endGame == model.Width * model.Height / 2) || progressBar1.Value == 0)
                saveBoard();
        }

        public void newGame()
        {
            level = 1;
            score = 0;
            endGame = 0;
            countClick = 0;
            scoring.Text = "0";
            lvl.Text = "1";
        }

        public void startNewGame()
        {
            hideModeButton();
            hideImage();
            panelTopBar.Show();
            newGame();
        }

        public void lbNextLevel_Click(object sender, EventArgs e)
        {
            if (label1.Text.Equals("LOSER"))
            {
                panelSave.Hide();
                this.BackgroundImage = null;
                this.BackColor = Color.Orange;

                newGame();
                removeOverlay(pb[pokemon1]);
                deleteAllPokemons();
            }

            else
            {
                level++;
                lvl.Text = level.ToString();
                scoring.Text = score.ToString();
            }

            endGame = 0;
            panelSave.Hide();
            makeGame(width, height, pokemons < 36 ? pokemons + level - 1 : 36);

            switch (width)
            {
                case 10:
                    countDown(10);
                    break;
                case 15:
                    countDown(20);
                    break;
                case 20:
                    countDown(30);
                    break;
            }
        }

        public void btEasy_Click(object sender, EventArgs e)
        {
            startNewGame();
            switch ((sender as Button).Text)
            {
                case "EASY":
                    width = 10;
                    height = 2;
                    pokemons = 1;
                    makeGame(width, height, pokemons);
                    countDown(100);
                    break;
                case "NORMAL":
                    width = 15;
                    height = 10;
                    pokemons = 20;
                    makeGame(width, height, pokemons);
                    countDown(200);
                    break;
                case "HARD":
                    width = 20;
                    height = 10;
                    pokemons = 30;
                    makeGame(width, height, pokemons);
                    countDown(10);
                    break;
            }
        }

        private void delay(int timeDelay)
        {
            Thread.Sleep(timeDelay);

            Task.Delay(timeDelay).Wait();
        }
    }
}