/*  CreditScene.cs
 *  
 *  This game component provides the game creator names
 *  
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
    /// This class is a game component that provides 
    /// the game creator's names/images
    /// </summary>
    public class CreditScene : GameScene
    {
        // declare class scope variables
        private SpriteBatch spriteBatch;
        private Texture2D tex;

        /// <summary>
        /// Credit scene class contructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public CreditScene(Game game, SpriteBatch spriteBatch)
            : base(game)
        {
            // child components
            this.spriteBatch = spriteBatch;
            tex = game.Content.Load<Texture2D>("Images/credit");
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
        /// Allows the credit scene to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
        /// <summary>
        /// Allows the credit scene to draw itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, Vector2.Zero, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
