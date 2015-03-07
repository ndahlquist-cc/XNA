/*  Transition.cs
 *  
 *  This game component is the parent class
 *  for the transition scenes between levels
 *  
 *  Inherits from GameScene.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-30 Created:
 *      + declare variables, create draw and update methods
 *      + adopted shareable components from transition classes 
 *      + added kitty to walk across transition scenes
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
    /// This is an abstract parent class which provides the template for the transition scenes between levels
    /// </summary>
    public abstract class Transition : GameScene
    {
        //Declare class varaibles
        protected Texture2D backgroundTex;
        protected SpriteBatch spriteBatch;
        protected HelloKitty kitty;
        protected Game1 game;
        protected SimpleString transitionMessage;
        protected string[] message;
        protected int delayCounter = 0;
        protected int delay = 400;
        protected SpriteFont highlightFont;
        protected int messageIndex = -1;
        protected KeyboardState oldState;

        /// <summary>
        /// Constructor for the Transitoin Class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public Transition(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            highlightFont = game.Content.Load<SpriteFont>("Fonts/HighlightFont");
            kitty = new HelloKitty(game, spriteBatch, 20);   
            kitty.start();
            this.Components.Add(kitty);
                    
            this.game = (Game1)game;

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
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>       

        public override void show()
        {
            this.Enabled = true;
            this.Visible = true;
            kitty.Position = new Vector2(0, Shared.stage.Y - 60);
            messageIndex = -1;

        }
        /// <summary>
        /// Draw method for the Transition class
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(backgroundTex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
