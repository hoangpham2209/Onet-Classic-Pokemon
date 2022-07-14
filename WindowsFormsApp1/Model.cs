using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Model
    {
        private int[,] table;
        private int width;
        private int height;

        public Model(int width, int height, int pokemons)
        {
            this.width = width;
            this.height = height;
            table = new int[height, width];

            HashSet<int> bangSet = new HashSet<int>();
            Random random = new Random();

            for (int i = 0; i < width * height / 2; i++)
            {
                int pokemon = random.Next(1, pokemons + 1);

                int cell1;
                do
                {
                    cell1 = random.Next(0, width * height);
                } while (bangSet.Contains(cell1));

                table[cell1 / width, cell1 % width] = pokemon;
                bangSet.Add(cell1);

                int cell2;
                do
                {
                    cell2 = random.Next(0, width * height);
                } while (bangSet.Contains(cell2));

                table[cell2 / width, cell2 % width] = pokemon;
                bangSet.Add(cell2);
            } 
        }

        public int cellIndex(int i, int j)
        {
            return table[i, j];
        }

        public int[,] Table { get => table; set => table = value; }
        public int Width { get => width; set => width = value; }
        public int Height { get => height; set => height = value; }
    }
}
