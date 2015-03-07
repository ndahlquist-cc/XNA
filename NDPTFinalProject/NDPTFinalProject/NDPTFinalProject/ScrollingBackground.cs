/*  Scrolling Background.cs
 *  
 *  This class provides moving background
 *  for the actionScene
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-09 Created:
 *      + declare variables, create draw and update methods
 *      + http://www.markoshea.info/images/photos_habitats/_swampl.jpg forest
 *      + http://fc04.deviantart.net/fs70/i/2013/124/4/3/grass_10_by_wolverine041269-d643w8y.png grass
 *      + http://fc05.deviantart.net/fs71/i/2013/107/b/0/creepy_tree_12_by_wolverine041269-d620mfn.png tree
 *      + http://www.imarvintpa.com/Mapping/Terrain/Plants/Trees/Fallen/decaying_log5.png log
 *      + http://fc01.deviantart.net/fs70/i/2012/068/5/e/dead_tree_02_png_by_gd08-d4s7iiz.png dead tree
 *      + http://fc02.deviantart.net/fs71/i/2013/135/8/c/grass_clumps_04_by_wolverine041269-d65c73b.png grasses
 *  2014-11-10
 *      + move backgrounds left or right with key press, with their own speeds
 *  2014-11-23
 *      + http://www.fernhillpk.com/images/camp-area-2.jpg level two background
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
    /// This class creates moving backgrounds which provide a parallax scrolling effect for the action scenes
    /// </summary>
    public class ScrollingBackground : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //ScrollingBackground class variables
        private SpriteBatch spriteBatch;
        private Texture2D tex;
        private Rectangle srcRect;
        private Vector2 position;
        private Vector2 speed;
        private int layer;
        private Vector2 position2;
        private int delay;
        private int delayCounter;
        private int direction;
        private KeyboardState oldState;

        /// <summary>
        /// ScrollingBackground class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="tex">Background texture</param>
        /// <param name="srcRect">Source rectangle</param>
        /// <param name="position">Position of the background</param>
        /// <param name="speed">Scrolling speed for the background</param>
        /// <param name="delay">Delay</param>
        /// <param name="layer">Background layer</param>
        public ScrollingBackground(Game game, SpriteBatch spriteBatch, Texture2D tex, 
            Rectangle srcRect, Vector2 position, Vector2 speed, int delay, int layer)
            : base(game)
        {
            // Construct child components 
            this.spriteBatch = spriteBatch;
            this.tex = tex;
            this.srcRect = srcRect;
            this.position = position;
            this.speed = speed;
            this.position2 = new Vector2(position.X + tex.Width, position.Y);
            this.delay = delay;
            this.direction = 0;
            this.layer = layer;           
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
            // Do not allow scrolling when kitty is dead
            if (!Shared.isKittyDead)
            {
                KeyboardState ks = Keyboard.GetState();

                //Scroll speed for layer 0
                if (layer == 0)
                {
                    speed = Shared.worldSpeed/2;
                }

                //Scroll speed for layer 1
                if (layer == 1)
                {
                    speed = Shared.worldSpeed;
                }

                //Scroll when moving right
                if ((ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)) && !(oldState.IsKeyDown(Keys.Left) || oldState.IsKeyDown(Keys.A)))
                {

                    position.X -= speed.X;
                    position2.X -= speed.X;

                    delayCounter++;
                    direction = 1;
                    if (delayCounter > delay)
                    {
                        delayCounter = 0;
                    }


                }

                //Scroll when moving left
                if ((ks.IsKeyDown(Keys.Left) || ks.IsKeyDown(Keys.A)) && !(ks.IsKeyDown(Keys.Right) || ks.IsKeyDown(Keys.D)))
                {
                    position.X += speed.X;
                    position2.X += speed.X;

                    delayCounter++;
                    direction = -1;
                    if (delayCounter > delay)
                    {
                        delayCounter = 0;
                    }
                }

                //Reset the background positions
                if (position2.X < 0)
                {
                    position.X = position2.X + tex.Width;
                }
                if (position.X < 0)
                {
                    position2.X = position.X + tex.Width;
                }
                if (position.X > 0)
                {
                    position2.X = position.X - tex.Width;
                }
                if (position2.X > 0)
                {
                    position.X = position2.X - tex.Width;
                }
                oldState = ks;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Draw method for the ScrollingBackground class
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values</param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(tex, position, srcRect, Color.White);
            spriteBatch.Draw(tex, position2, srcRect, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
