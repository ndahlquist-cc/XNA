/*  SimpleString.cs
 *  
 *  This class draws simple messages on the screen to be displayed
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-17 Created:
 *  + declare variables, create draw and update methods
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
    /// Class which draws simple message on the screen to be displayed using a specified SpriteFont
    /// </summary>
    public class SimpleString : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //Declare SimpleString class variables
        private SpriteBatch spriteBatch;
        private Color color;
        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }
        private Vector2 position;
        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
        private SpriteFont font;
        
        /// <summary>
        /// Constructor for SimpleString class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="font">Font to be used when displaying the message</param>
        /// <param name="message">Message to be displayed</param>
        /// <param name="color">Color of the font</param>
        /// <param name="position">Position on the screen to display the message</param>
        public SimpleString(Game game, SpriteBatch spriteBatch, SpriteFont font, string message, Color color,
            Vector2 position)
            : base(game)
        {
            // TODO: Construct any child components here
            this.spriteBatch = spriteBatch;
            this.font = font;
            this.message = message;
            this.position = position;
            this.color = color;
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
        /// allows the simple string to draw itself
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            //Draw the string message
            spriteBatch.Begin();
            spriteBatch.DrawString(font, message, position, color);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
