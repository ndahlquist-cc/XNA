/*  MenuBackground.cs
 *  
 *  This class draws the menu background
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
 *      + declare variables, create draw and update methods
 *      + http://fc01.deviantart.net/fs70/f/2014/078/6/1/creepy_forest_mansion_by_lmorse-d7avgn3.jpg menu background
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
    /// Class used to draw the menu background
    /// </summary>
    public class MenuBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //MenuBackground class variables
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Vector2 position;

        /// <summary>
        /// Constructor for the MenuBackground class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Background texture</param>
        /// <param name="srcRect">Background source rectangle</param>
        /// <param name="position">Background position</param>
        public MenuBackground(Game game, SpriteBatch spriteBatch, Texture2D tex,
            Rectangle srcRect, Vector2 position)
            : base(game)
        {
            // Construct child components
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.position = position;
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
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
