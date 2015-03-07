/*  Flag.cs
 *  
 *  This game component draws the flag at the end of the levels
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-23 Created:
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
    /// This call is a drawable game component that allows the level
    /// end flag to be drawn
    /// </summary>
    public class Flag : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // declare class scope variables
        private SpriteBatch spriteBatch;
        private Texture2D flagTex;
        private Vector2 flagPos;

        public Vector2 FlagPos
        {
            get { return flagPos; }
            set { flagPos = value; }
        }
        private KeyboardState oldState;

        /// <summary>
        /// Flag class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="flagPos">position of flag</param>
        public Flag(Game game, SpriteBatch spriteBatch, Vector2 flagPos)
            : base(game)
        {
            // child components
            this.spriteBatch = spriteBatch;            
            this.flagTex = game.Content.Load<Texture2D>("Images/flag");
            this.flagPos = flagPos - new Vector2(0, flagTex.Height);
            
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
        /// Allows the flag to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //Move level flag as part of world movement
            if (!Shared.isKittyDead)
            {
                KeyboardState ks = Keyboard.GetState();

                if ((ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)) && !(oldState.IsKeyDown(Keys.Left) || oldState.IsKeyDown(Keys.A)))
                {
                    flagPos.X -= Shared.worldSpeed.X;
                }

                if ((ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A)) && !(ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)))
                {
                    flagPos.X += Shared.worldSpeed.X;
                }

                oldState = ks;
            }
            base.Update(gameTime);
        }
        /// <summary>
        /// This is called when the flag should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(flagTex, flagPos, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
