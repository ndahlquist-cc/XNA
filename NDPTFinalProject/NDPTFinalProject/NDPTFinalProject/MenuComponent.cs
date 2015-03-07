/*  MenuComponent.cs
 *  
 *  This class provides a menu which allows the user to select from a variety of options
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-06 Created:
 *      + declare variables, create draw and update methods
 * Revision History:
 *  2014-11-30
 *      + added minMenuIndex
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
    /// This class provides a menu which allows the user to select from a variety of options
    /// </summary>
    public class MenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //MenuComponent class variables
        private SpriteBatch spriteBatch;
        private SpriteFont regularFont;
        private SpriteFont highlightFont;
        private List<string> menuItems;
        private int selectedIndex = Shared.minMenuIndex;
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }
        private Vector2 position;
        private Color regularColour = Color.White;
        private Color highlightColour = Color.Red;
        private KeyboardState oldState;
        
        /// <summary>
        /// MenuComponent class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="regularFont">Font to display menu items with</param>
        /// <param name="highlightFont">Font to display the currently selected menu item</param>
        /// <param name="menus">Array containing the names of the items in the menu</param>
        public MenuComponent(Game game, SpriteBatch spriteBatch, SpriteFont regularFont, SpriteFont highlightFont,
            string[] menus)
            : base(game)
        {
            // Construct child components
            this.spriteBatch = spriteBatch;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            menuItems = new List<string>();

            //Add menu items to list
            for (int i = 0; i < menus.Length; i++)
            {
                menuItems.Add(menus[i]);
            }
            position = new Vector2(Shared.stage.X / 2 + 70, Shared.stage.Y / 2 + 90);
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
            // update code
            KeyboardState ks = Keyboard.GetState();

            //Menu item selection with down arrow key
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                selectedIndex++;
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = Shared.minMenuIndex;
                }
            }

            //Menu item selection with up arrow key
            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                selectedIndex--;
                if (selectedIndex == (Shared.minMenuIndex - 1))
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            oldState = ks;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            spriteBatch.Begin();
            //Draw the menu items, use the highlighted font for the selected item
            for (int i = Shared.minMenuIndex; i < menuItems.Count; i++)
            {
                if (selectedIndex == i)
                {
                    spriteBatch.DrawString(highlightFont, menuItems[i],
                        tempPos, highlightColour);
                    tempPos.Y += regularFont.LineSpacing;
                }
                else
                {
                    spriteBatch.DrawString(regularFont, menuItems[i],
                        tempPos, regularColour);
                    tempPos.Y += regularFont.LineSpacing;
                }
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
