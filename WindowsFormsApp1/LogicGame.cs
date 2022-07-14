using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class LogicGame: GameLayout
    {
        public void save()
        {
            if ((endGame == model.Width * model.Height / 2) || progressBar1.Value == 0)
                saveBoard();
        }

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
                if (pokemons < 36)
                    pokemons++;

                level++;
                lvl.Text = level.ToString();
                scoring.Text = score.ToString();
            }


            endGame = 0;
            panelSave.Hide();
            makeGame(width, height, pokemons);

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
            showTopBar();
            newGame();
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
    }
}