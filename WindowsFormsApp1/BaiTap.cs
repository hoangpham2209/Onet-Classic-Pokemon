using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WindowsFormsApp1
{
    public partial class BaiTap : Form 
    {
        public static int pokemon1, pokemon2, score, endGame, level, width, height, pokemons;
        public static Dictionary<string, int> dataUsers = new Dictionary<string, int>();
        public int[,] pokemonTable;
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
            string namePic = random.Next(1, 12).ToString() + ".jpg";

            this.BackgroundImage = Image.FromFile((@"bg\" + namePic));
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        public void makeGame(int width, int height, int pokemons)
        {
            randomBackground();
            this.BackColor = Color.DarkCyan;

            model = new Model(width, height, pokemons);
            pb = new PictureBox[height * width];

            for (int i = 0; i < height; i++)
                for (int j = 0; j < width; j++)
                {
                    int cell = i * width + j;
                    pb[cell] = new PictureBox();
                    pb[cell].Width = 43;
                    pb[cell].Height = 53;

                    switch (width)    // Padding
                    {
                        case 10:
                            pb[cell].Top = 200 + i * 53;
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

            pokemonTable = model.Table;
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
            panelRank.Hide();
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
            label1.Text = "CONGRATULATIONS";
            label1.ForeColor = Color.Yellow;
            lbNextLevel.Text = "Next level";

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
            panelRank.Show();

            if (dataUsers.Count > 0)
            {
                int count = 0;
                List<string> listUsersName = new List<string>();
                List<string> listUsersScore = new List<string>();
                foreach (var i in dataUsers)
                {
                    if (count == 5) break;
                    listUsersName.Add(String.Format("    {0,-8}", i.Key));
                    listUsersScore.Add(String.Format("{0}", i.Value));
                    count++;
                }

                lbRank.Text = string.Join("\n", listUsersName);
                lbScoreRank.Text = string.Join("\n", listUsersScore);
            }
            else
            {
                lbRank.Text = "No Player";
                lbScoreRank.Text = "No Score";
            }
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
        class pair
        {
            public int first, second;

            public pair(int first, int second)
            {
                this.first = first;
                this.second = second;
            }
        }

        private bool isValid(int x, int y, int[,] temp)
        {
            if (x < 0 || y < 0 || x >= height + 2|| y >= width + 2)
                return false;
            if (temp[x, y] != 0)
                return false;

            return true;
        }

        private Queue<pair> findPath(pair start, pair end)
        {
            int[,] temp = new int[height + 2, width + 2];
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    if (pokemonTable[i, j] != 0)
                        temp[i + 1, j + 1] = pokemonTable[i, j];
                }
            }

            //BFS
            int[,] dir = { { 0, 1 }, { 0, -1 }, { 1, 0 }, { -1, 0 } };

            end.first++; end.second++;
            start.first++; start.second++;

            Queue<pair> q = new Queue<pair>();
            q.Enqueue(end);

            pair[,] trace = new pair[height + 2, width + 2];
            for (int i = 0; i < height + 2; i++)
                for (int j = 0; j < width + 2; j++)
                    trace[i, j] = new pair(-1, -1); 

            trace[end.first, end.second] = new pair(-2, -2);
            temp[start.first, start.second] = 0;
            temp[end.first, end.second] = 0;

            while (q.Count != 0)
            {
                var u = q.Peek();
                q.Dequeue();
                if (u.Equals(start)) break;

                for (int i = 0; i < 4; i++)
                {
                    int zx = u.first + dir[i, 0];
                    int zy = u.second + dir[i, 1];

                    while (isValid(zx, zy, temp))
                    {
                        if (trace[zx, zy].first == -1)
                        {
                            trace[zx, zy] = u;
                            q.Enqueue(new pair(zx, zy));
                        }
                        zx += dir[i, 0];
                        zy += dir[i, 1];
                    }
                }
            }

            //trace back
            Queue<pair> path = new Queue<pair>();

            if (trace[start.first, start.second].first != -1)
            {
                while (start.first != -2)
                {
                    path.Enqueue(new pair(start.first - 1, start.second - 1));
                    start = trace[start.first, start.second];
                }
            }
            
            return path;
        }

        private void drawLineMatching(int x1, int y1, int x2, int y2, Queue<pair> path)
        {
            PaintEventArgs e = new PaintEventArgs(this.CreateGraphics(), ClientRectangle);

            Point[] points = new Point[path.Count()];

            int coorX = 0, coorY = 0;
            switch (width)
            {
                case 10:
                    coorX = 270;
                    coorY = 200;
                    break;
                case 15:
                    coorX = 170;
                    coorY = 45;
                    break;
                case 20:
                    coorX = 50;
                    coorY = 45;
                    break;
            }

            int i = 0;
            Pen red = new Pen(Color.Red, 3);

            foreach (var p in path)
            {
                points[i] = new Point(p.second * 43 + coorX + 15, p.first * 53 + coorY + 15);
                if (i > 0)
                    e.Graphics.DrawLine(red, points[i - 1], points[i]);
                i++;
            }

            delay(75);
            Refresh();
        }

        public void samePokemon(object sender)
        {
            if (countClick == 2)
            {
                pokemon2 = int.Parse(Array.IndexOf(pb, (PictureBox)sender).ToString());
                removeOverlay(pb[pokemon1]);
                removeOverlay(pb[pokemon2]);

                int x1 = pokemon1 / width, y1 = pokemon1 % width;
                int x2 = pokemon2 / width, y2 = pokemon2 % width;
                var path = findPath(new pair(x1, y1), new pair(x2, y2));

                if (pokemonTable[x1, y1] == pokemonTable[x2, y2] && path.Count() >= 2 && path.Count() <= 4)
                {
                    drawLineMatching(x1, y1, x2, y2, path);

                    pb[pokemon1].Dispose();
                    pb[pokemon2].Dispose();
                    pokemonTable[x1, y1] = 0;
                    pokemonTable[x2, y2] = 0;

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
                label1.Text = "CONGRATULATIONS";
                label1.ForeColor = Color.Yellow;
                lbNextLevel.Text = "Next level";
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
                    countDown(600);
                    break;
                case 15:
                    countDown(1800);
                    break;
                case 20:
                    countDown(3000);
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
                    height = 5;
                    pokemons = 5;
                    makeGame(width, height, pokemons);
                    countDown(600);
                    break;
                case "NORMAL":
                    width = 15;
                    height = 10;
                    pokemons = 10;
                    makeGame(width, height, pokemons);
                    countDown(1800);
                    break;
                case "HARD":
                    width = 20;
                    height = 10;
                    pokemons = 15;
                    makeGame(width, height, pokemons);
                    countDown(3000);
                    break;
            }
        }

        static FormSaving formSaving = new FormSaving();

        public void lbSave_Click(object sender, EventArgs e)
        {
            formSaving.ShowDialog();
        }

        private void BaiTap_Load(object sender, EventArgs e)
        {
            StreamReader input = new StreamReader(@"data.txt");

            do
            {
                string s = input.ReadLine();
                if (s != null)
                {
                    int index = s.IndexOf(":");
                    dataUsers.Add(s.Substring(0, index), int.Parse(s.Substring(index + 1)));
                }
                if (dataUsers.Count == 5) break; 
            }
            while (input.Peek() != -1);

            input.Close();
        }

        private void delay(int timeDelay)
        {
            Thread.Sleep(timeDelay);

            Task.Delay(timeDelay).Wait();
        }
    }
}