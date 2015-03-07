/*  HowToPlayScene.cs
 *  
 *  This class displays the gameplay instructions for the game
 *  Inherits from GameScene.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-18 Created:
 *      + declare variables, create draw and update methods
 * 
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
    /// This class displays the game play instructions for the game
    /// </summary>
    public class HowToPlayScene : GameScene
    {
        //HowToPlayScene class variables
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        /// <summary>
        /// Constructor for HowToPlayScene
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">How To Play image</param>
        public HowToPlayScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/HowToPlay"); ;
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
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here

            base.Update(gameTime);
        }

        /// <summary>
        /// Allows the how to play scene to draw itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End(); 
            base.Draw(gameTime);
        }
    }
}
