/*  GameScene.cs
 *  
 *  This game component is the parent class for all scenes
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
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
    /// This class maintains a list of all components, allows for the selected 
    /// scene to be shown and enabled, hidden and disabled, updated and drawn.
    /// </summary>
    public abstract class GameScene : Microsoft.Xna.Framework.DrawableGameComponent
    {
        // class scope variables
        private List<GameComponent> components;

        public List<GameComponent> Components
        {
            get { return components; }
            set { components = value; }
        }

        /// <summary>
        /// this method will make the chosen scene enabled and visible
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        /// <summary>
        /// this method will make the chosen scene disabled and invisible
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }
        /// <summary>
        /// Game scene class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        public GameScene(Game game)
            : base(game)
        {
            // child components 
            components = new List<GameComponent>();
            hide();
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
        /// Allows the chosen game scene to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // update code
            foreach (GameComponent item in components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the chosen scene should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            DrawableGameComponent comp = null;
            foreach (GameComponent item in components)
            {
                if (item is DrawableGameComponent)
                {
                    comp = (DrawableGameComponent)item;
                }
                
                if (comp.Visible)
                {
                    comp.Draw(gameTime);
                }
            }
            base.Draw(gameTime);
        }
    }
}
