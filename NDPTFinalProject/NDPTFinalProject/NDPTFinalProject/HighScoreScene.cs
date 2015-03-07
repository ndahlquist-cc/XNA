/*  HighScoreScene.cs
 *  
 *  This class displays, saves, and loads player scores
 *  Inherits from GameScene.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-23 Created
 *  
 *  2014-11-26
 *      + Added file i/o and display for scoreboard
 *      + Created static method to set scoreboard to input mode and display mode
 *  2014-11-30
 *      + http://cdn-wac.emirates247.com/polopoly_fs/1.495565.1361200239!/image/2555144569.jpg balloons background
 *      + formatting
 */
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.IO;


namespace NDPTFinalProject
{
    /// <summary>
    /// This class displays, saves, and loads player scores
    /// </summary>
    public class HighScoreScene : GameScene
    {

        //HighScoreScene variables and constants
        private const int NUMBER_OF_SCORES_DISPLAY = 10;
        private const string SPACING = "     ";
        private const string DIRECTORY = "scores.txt";

        private Game1 game;
        private SpriteBatch spriteBatch;

        private SpriteFont regularFont;
        private SpriteFont gameOverFont;
        private SimpleString inputDisplay;
        private SimpleString headerDisplay;
        private SimpleString instructionDisplay;        
        private List<SimpleString> scoreDisplayList = new List<SimpleString>();
       
        private List<string> alphabet = new List<string>() 
        { "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S", "T", "U", "V", "W", "X", "Y", "Z", "_" };
        private int letterIndex = 0;

        private static string inputMessage;
        private string headerMessage;
        private string instructionMessage;
        private Vector2 instructionPos;
        private Vector2 inputPos;
        private Vector2 headerPos;

        private List<string> playerList = new List<string>();
        private static int playerScore = 0;
        private static bool scoreInputMode = false;
        private Texture2D backgroundTex;
        public static bool ScoreInputMode
        {
            get { return HighScoreScene.scoreInputMode; }
            set { HighScoreScene.scoreInputMode = value; }
        }
        private int initialsAdded = 0;
        private string playerInitials = "";
        private bool scoreAdded = false;
        private KeyboardState oldState;
        private float timer;

        /// <summary>
        /// HighScoreScene constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public HighScoreScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            this.game = (Game1)game;
            this.spriteBatch = spriteBatch;

            //Initialize simple strings to be displayed on the HighScoreScene
            this.gameOverFont = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            this.regularFont = game.Content.Load<SpriteFont>("Fonts/RegularFont");

            instructionMessage = "Arrow keys to select, space to insert.";
            inputMessage = "Score: " + playerScore + SPACING + "Enter your initials: ";

            inputPos = new Vector2(Shared.stage.X / 2 - regularFont.MeasureString (inputMessage).X/2, regularFont.MeasureString(inputMessage).Y + 75);
            instructionPos = new Vector2(Shared.stage.X / 2 - regularFont.MeasureString(instructionMessage).X / 2 + 15, inputPos.Y - regularFont.MeasureString(instructionMessage).Y);

            inputDisplay = new SimpleString(game, spriteBatch, regularFont, inputMessage, Color.Fuchsia, inputPos);
            instructionDisplay = new SimpleString(game, spriteBatch, regularFont, instructionMessage, Color.Fuchsia, instructionPos);

            headerMessage = "High Scores";
            headerPos = new Vector2(Shared.stage.X / 2 - regularFont.MeasureString(headerMessage).X / 2, inputPos.Y + regularFont.MeasureString(headerMessage).Y*2);
            headerDisplay = new SimpleString(game, spriteBatch, gameOverFont, "High Scores", Color.DeepPink, headerPos);

            //Add components
            this.Components.Add(instructionDisplay);
            this.Components.Add(inputDisplay);
            this.Components.Add(headerDisplay);
            this.backgroundTex = this.backgroundTex = game.Content.Load<Texture2D>("Images/balloons");
            timer = 0f;
            
            loadScores();
            // TODO: Construct any child components here
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here            

            base.Initialize();
        }

        /// <summary>
        /// Public method to set the scoreAdded boolean to false
        /// </summary>
        public void addScore()
        {
            scoreAdded = false;
        }

        /// <summary>
        /// Public method to set the input mode for the highscore scene on and off
        /// </summary>
        /// <param name="inputScoreMode">True: input score mode, False: display score mode</param>
        public static void setScoreMode(bool inputScoreMode)
        {
            scoreInputMode = inputScoreMode;

            //Display messages specific to the input score mode
            if(inputScoreMode)
            {
                playerScore = Shared.score;
                inputMessage = "Score: " + playerScore + SPACING + "Enter your initials: ";
                
            }            
        }

        /// <summary>
        /// Method to save the score after the player has input their initials
        /// </summary>
        public void saveScore()
        {
            StreamWriter writer;
            playerScore = Shared.score;
            initialsAdded = 0;
            inputDisplay.Message = "Your score has been added!";
            inputDisplay.Position = inputPos;
            instructionDisplay.Message = "";

            //Create score file if it does not exist
            if (!File.Exists(DIRECTORY))
            {
                StreamWriter tempWriter = File.CreateText(DIRECTORY);
                tempWriter.Close();
            }

            //Open score file and write score to file
            writer = File.AppendText(DIRECTORY);
            writer.WriteLine(playerScore + " " + playerInitials);
            writer.Close();
            
        }

        /// <summary>
        /// Method to load the scores from the score text file
        /// </summary>
        public void loadScores()
        {
            StreamReader reader;
            string[] line;
            List<int> scoreList = new List<int>();
            List<int> hiScoreIndices = new List<int>();
            List<string> fileLines = new List<string>();

            //Disable previous score display
            foreach (SimpleString s in scoreDisplayList)
            {
                s.Enabled = false;
                s.Visible = false;
            }
            scoreDisplayList.Clear();
            

            //Create score file if it does not exist
            if (!File.Exists(DIRECTORY))
            {
                StreamWriter tempWriter = File.CreateText(DIRECTORY);
                tempWriter.Close();
            }
            //Open score file
            reader = File.OpenText(DIRECTORY);

            //Read scores and add to list of scores
            while (reader.EndOfStream == false)
            {
                line = reader.ReadLine().Split();
                string player = line[1];
                int score = int.Parse(line[0]);
                scoreList.Add(score);
                fileLines.Add(score + " " + player);
            }

            //Close reader
            reader.Close();

            List<int> tempScoreList = scoreList;
            //Create a list of the indices for the 5 highest scores on the file
            for (int i = 0; i < NUMBER_OF_SCORES_DISPLAY; i++)
            {
                if(scoreList.Count() > i )
                {
                    int index = scoreList.IndexOf(tempScoreList.Max());
                    if(!hiScoreIndices.Contains(index))
                    {
                        hiScoreIndices.Add(index);
                        tempScoreList[index] = -1;
                    }                    
                }                
            }
           
            float posY = headerPos.Y;
            float playerPosX = 0;
            float scorePosX = 0;

            //Draw strings for the highest scores
            foreach (int i in hiScoreIndices)
            {
                SimpleString scoreDisplay;
                SimpleString playerDisplay;
                string score = fileLines[i].Split()[0];
                string player = fileLines[i].Split()[1].Trim();
                Vector2 messageSize = regularFont.MeasureString(player);
                Vector2 scoreSize = regularFont.MeasureString(score);
                posY += messageSize.Y*1.2f;
                playerPosX = Shared.stage.X/2 + 30;
                scorePosX = Shared.stage.X / 2 - 60;
                Vector2 playerPosition = new Vector2(playerPosX,posY);
                Vector2 scorePosition = new Vector2(scorePosX, posY);
                scoreDisplay = new SimpleString(game, spriteBatch, regularFont, score, Color.Fuchsia, scorePosition);
                playerDisplay = new SimpleString(game, spriteBatch, regularFont, player, Color.Fuchsia, playerPosition);
                scoreDisplayList.Add(scoreDisplay);
                scoreDisplayList.Add(playerDisplay);
            }

            //Add the scores to the display
            foreach (SimpleString s in scoreDisplayList)
            {
                this.Components.Add(s);
            }
            
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            timer += gameTime.ElapsedGameTime.Milliseconds;
            // TODO: Add your update code here
            KeyboardState ks = Keyboard.GetState();
            if(scoreInputMode)
            {
                inputDisplay.Enabled = true;
                inputDisplay.Visible = true;
                instructionDisplay.Enabled = true;
                instructionDisplay.Visible = true;

                
                if (!scoreAdded && initialsAdded < 3 && oldState != ks)
                {
                    //Scroll through letters with arrow keys
                    if (ks.IsKeyDown(Keys.Up))
                    {
                        letterIndex -= 1;
                    }

                    if (ks.IsKeyDown(Keys.Down))
                    {
                        letterIndex += 1;
                    }

                    if(letterIndex > 26)
                    {
                        letterIndex = 0;
                    }

                    if(letterIndex < 0)
                    {
                        letterIndex = 26;
                    }

                    inputDisplay.Message = inputMessage + alphabet[letterIndex];

                    //Input letter with spacebar
                    if (ks.IsKeyDown(Keys.Space) && timer > 1000 && ks!= oldState)
                    {
                        inputMessage += alphabet[letterIndex];
                        playerInitials += alphabet[letterIndex];
                        initialsAdded++;
                    }
                   
                }

                //Save score after entering 3 initials
                else if (!scoreAdded && initialsAdded == 3)
                {
                    saveScore();
                    scoreAdded = true;
                    loadScores();
                }
            }

            //Disable score input mode strings
            else
            {
                instructionDisplay.Visible = false;
                instructionDisplay.Enabled = false;
                inputDisplay.Visible = false;
                inputDisplay.Enabled = false;
            }

            //Press enter or space after entering a score to return to start scene
            if (scoreAdded && (ks.IsKeyDown(Keys.Enter) || ks.IsKeyDown(Keys.Space)) && ks != oldState)
            {
                game.loadStartScene();
            }
            
            base.Update(gameTime);
            oldState = ks;
        }

        /// <summary>
        /// Draw method for the HighScoreScene class
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
