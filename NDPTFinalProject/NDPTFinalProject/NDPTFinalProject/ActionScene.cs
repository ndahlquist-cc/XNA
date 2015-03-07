/*  ActionScene.cs
 *  
 *  This game component is a parent class to the level scenes
 *  
 *  Inherits from GameScene.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
 *      + declare variables, create draw and update methods
 * 2014-11-09
 *      + moved actionScene load content here
 *  2014-11-10
 *      + added layer parameter
 *  2014-11-17
 *      + add new block objects
 *  2014-11-22
 *      + added pink block objects, star objects, and log object
 *  2014-11-23
 *      + made action scene abstract and created level classes that inherit from it
 *      + added flag http://icons.iconarchive.com/icons/icons-land/vista-map-markers/256/Map-Marker-Flag-3-Right-Pink-icon.png
 *  2014-11-29
 *      + added game over message
 *  2014-11-30
 *      + added message on how to escape from game over message
 *      
 *  Note: All town(except the fire), forest, and shared object textures were created by Nicole and not found online
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


namespace NDPTFinalProject
{
    /// <summary>
    /// This is an abstract class from which the level scenes
    /// inherit. 
    /// </summary>
    public abstract class ActionScene : GameScene
    {
        // declare class scope variables
        protected SpriteBatch spriteBatch;
        protected HelloKitty kitty;
        protected ScrollingBackground sb2;
        protected ScrollingBackground sb1;
        protected Rectangle srcB;
        protected Rectangle srcF;
        protected Vector2 pos2;
        protected Vector2 pos1;
        protected Vector2 endLevelPos;
        protected KeyboardState oldState;
        protected Texture2D backgroundTex;
        protected Texture2D foregroundTex;
        protected Flag endLevelFlag;
        protected List<Block> blocks = new List<Block>();
        protected CollisionManager cm;
        protected int delay = 10;  
        protected List<Zombie> zombies = new List<Zombie>();        
        protected SimpleString score;
        protected SimpleString gameOver;
        protected SimpleString cont;
        private Game1 game;

        /// <summary>
        /// Action scene class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">Spritebatch</param>
        public ActionScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            
            // Score
            SpriteFont font = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            string message = "Player Score: " + Shared.score;
            Vector2 position = new Vector2(0, 0);
            score = new SimpleString(game, spriteBatch, font, message, Color.White, position);

            // game over
            SpriteFont gameOverFont = game.Content.Load<SpriteFont>("Fonts/GameOverFont");
            SpriteFont continueFont = game.Content.Load<SpriteFont>("Fonts/ContinueFont");
            string gameOverMessage = "GAME OVER";
            string continueMessage = "press enter to \n     continue...";
            Vector2 gameOverPosition = new Vector2(Shared.stage.X/2 - gameOverFont.MeasureString(gameOverMessage).X/2, Shared.stage.Y/2 - 100);
            Vector2 continuePosition = new Vector2(Shared.stage.X / 2 - continueFont.MeasureString(continueMessage).X / 2, 
                gameOverPosition.Y + continueFont.MeasureString(continueMessage).Y);
            gameOver = new SimpleString(game, spriteBatch, gameOverFont, gameOverMessage, Color.DeepPink, gameOverPosition);
            cont = new SimpleString(game,spriteBatch,continueFont,continueMessage,Color.DeepPink, continuePosition);
            gameOver.Visible = false;
            cont.Visible = false;

            //Kitty game character            
            kitty = new HelloKitty(game, spriteBatch, delay);

            this.game = (Game1)game;
           
        }        

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();
        }      

        /// <summary>
        /// Allows the action scene to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            score.Message = "Player Score: " + Shared.score;

            if(Shared.isKittyDead)
            {                
                gameOver.Visible = true;
                cont.Visible = true;
                if (ks.IsKeyDown(Keys.Enter))
                {
                    game.loadHighScoreEntry();
                }

            }
            base.Update(gameTime);
        }
    }
}
