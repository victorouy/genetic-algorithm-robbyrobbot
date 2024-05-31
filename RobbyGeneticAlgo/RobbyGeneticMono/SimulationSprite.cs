using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using RobbyGeneticAlgo;

namespace RobbyGeneticMono
{

    /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
     * 
     *Sprites for the Game 
     */
    public class SimulationSprite : DrawableGameComponent
    {
        private SpriteFont spriteFont;

        private SpriteBatch spriteBatch;
        private Texture2D empImg;
        private Texture2D canImg;
        private Texture2D robImg;
        private Game1 game;

        private int count;
        private int threshold;
        private int genNum;

        private int fileIndex;

        private int moves;
        private int numMoves;
        private int score;
        private Contents[,] testGrid;
        private int xRob;
        private int yRob;

        private string txt;
        private string[] filePaths;

        private Chromosome chromo;


        /*
        * @Author: Victor Ouy
        * @Date: 13/04/2024
         * 
         * @Param: game
         * 
         *Constructor
         */
        public SimulationSprite(Game1 game) : base(game)
        {
            this.game = game;
            this.count = 0;
            this.threshold = 5;
            this.moves = 0;

            this.score = 0;

            this.xRob = 4;
            this.yRob = 4;
            this.fileIndex = 0;
        }


        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
      * 
      * Overrides the Initialize method
      * used to initialize the paths
      */
        public override void Initialize()
        {
            filePaths = new string[6];
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            filePaths[0] = Path.Combine(executableLocation, "GenerationInfo1.txt");
            filePaths[1] = Path.Combine(executableLocation, "GenerationInfo20.txt");
            filePaths[2] = Path.Combine(executableLocation, "GenerationInfo100.txt");
            filePaths[3] = Path.Combine(executableLocation, "GenerationInfo200.txt");
            filePaths[4] = Path.Combine(executableLocation, "GenerationInfo500.txt");
            filePaths[5] = Path.Combine(executableLocation, "GenerationInfo1000.txt");

            readFiles();

            base.Initialize();
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
        * 
        * Overrides the LoadContent method
        * used to initialize the Texture2D objects (sprites)
        */
        protected override void LoadContent()
        {
            this.spriteFont = this.game.Content.Load<SpriteFont>("scoreFont");

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.empImg = game.Content.Load<Texture2D>("empty");
            this.canImg = game.Content.Load<Texture2D>("can");
            this.robImg = game.Content.Load<Texture2D>("robby");

            base.LoadContent();
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
        * 
        * Helper Method used to read the generation txt file and create a chromosome using it
        */
        public void readFiles()
        {
            testGrid = Helpers.GenerateRandomTestGrid(10);

            this.txt = File.ReadAllText(filePaths[fileIndex]);
            string[] txtArr = txt.Split(',');

            this.genNum = Int32.Parse(txtArr[0]);
            this.numMoves = Int32.Parse(txtArr[1]);

            Allele[] allele = new Allele[txtArr.Length - 3];
            int geneCounter = 0;

            for (int i = 3; i < txtArr.Length; i++)
            {
                if (txtArr[i] == "North")
                {
                    allele[geneCounter] = (Allele)(0);
                    geneCounter++;
                }
                else if (txtArr[i] == "South")
                {
                    allele[geneCounter] = (Allele)(1);
                    geneCounter++;
                }
                else if (txtArr[i] == "East")
                {
                    allele[geneCounter] = (Allele)(2);
                    geneCounter++;
                }
                else if (txtArr[i] == "West")
                {
                    allele[geneCounter] = (Allele)(3);
                    geneCounter++;
                }
                else if (txtArr[i] == "Nothing")
                {
                    allele[geneCounter] = (Allele)(4);
                    geneCounter++;
                }
                else if (txtArr[i] == "PickUp")
                {
                    allele[geneCounter] = (Allele)(5);
                    geneCounter++;
                }
                else if (txtArr[i] == "Random")
                {
                    allele[geneCounter] = (Allele)(6);
                    geneCounter++;
                }
            }

             this.chromo = new Chromosome(allele);
        }


        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
        * 
        * @Param: gameTime
        * 
        * Overrides the Update method
        * Used to get Robby's moves using the ScoreForAllele helper method
        */
        public override void Update(GameTime gameTime)
        {
            if (moves < numMoves)
            {
                if (count > threshold)
                {
                    score += Helpers.ScoreForAllele(chromo, testGrid, ref yRob, ref xRob);

                    count = 0;
                    moves++;
                }
                else
                {
                    count++;
                }
            }
            else
            {
                xRob = 4;
                yRob = 4;
                moves = 0;
                score = 0;

                fileIndex++;
                if (fileIndex < 6)
                {
                    readFiles();
                }
                else
                {
                    Game.Exit();
                }
            }

            base.Update(gameTime);
        }

        /*
      * @Author: Victor Ouy
      * @Date: 13/04/2024
        * 
        * @Param: gameTime
        * 
        * Overrides the Draw method
        * used to draw the game board (all the sprites). The changes in the gameboard are caused by thius method
        */
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            
            int x = 0;
            int y = 0;
            for (int i = 0; i < testGrid.GetLength(0); i++)
            {
                for (int j = 0; j < testGrid.GetLength(1); j++)
                {
                    if (x <= 9)
                    {
                        if (testGrid[i, j] == (Contents)(0))
                        {
                            spriteBatch.Draw(empImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                            x++;
                        }
                        else
                        {
                            spriteBatch.Draw(canImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                            x++;
                        }
                    }
                    else
                    {
                        x = 0;
                        y++;
                        if (testGrid[i, j] == (Contents)(0))
                        {
                            spriteBatch.Draw(empImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                            x++;
                        }
                        else
                        {
                            spriteBatch.Draw(canImg, new Rectangle(x * 32, y * 32, 32, 32), Color.White);
                            x++;
                        }
                    }
                }
            }
            spriteBatch.Draw(robImg, new Rectangle(xRob * 32, yRob * 32, 32, 32), Color.White);

            string genDisplay = "Generation: " + genNum;
            this.spriteBatch.DrawString(this.spriteFont, genDisplay, new Vector2(0, 330), Color.Black);

            string movesDisplay = "Moves: " + moves + "/" + numMoves;
            this.spriteBatch.DrawString(this.spriteFont, movesDisplay, new Vector2(0, 350), Color.Black);

            string scoreDisplay = "Score: " + score;
            this.spriteBatch.DrawString(this.spriteFont, scoreDisplay, new Vector2(0, 370), Color.Black);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
