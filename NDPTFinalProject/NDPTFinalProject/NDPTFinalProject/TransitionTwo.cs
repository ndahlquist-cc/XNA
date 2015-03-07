/*  TransitionTwo.cs
 *  
 *  This class provides the game's transition scene 
 *  between second and third level (currently end of game)
 *  
 *  Inherits from Transition.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-30 Created:
 *      + declare variables, create draw and update methods
 *      + set background
 *      + create messages to loop through(loops indefinitely)
 *      + Theme park image http://thesuiteworld.com/wp-content/uploads/2011/05/Hello-Kitty-theme-park-China.jpg
 *      + formatting
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
    /// This class provides the trnasition scene between the second level and the end of the game
    /// </summary>
    public class TransitionTwo : Transition
    {        

        /// <summary>
        /// Constructor for the TransitionTwo Class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public TransitionTwo(Game game, SpriteBatch spriteBatch)
            : base(game,spriteBatch)
        {
            //Set background texture and messages specific to this transition scene
            this.backgroundTex = game.Content.Load<Texture2D>("Images/transitionTwoToThree");
            Vector2 position = new Vector2(Shared.stage.X / 2 - 250, Shared.stage.Y / 2 - 100);
            message =  new string[6];
            message[0] = "Hello Kitty leaves the village disheartened.";
            message[1] = "Her friends were no where to be found.";
            message[2] = "She decides to check the Hello Kitty Theme Park";
            message[3] = "Perhaps they took refuge there when the village was overrun.";
            message[4] = "CONGRATULATIONS, YOU HAVE COMPLETED THIS DEMO!!";

            transitionMessage = new SimpleString(game, spriteBatch, highlightFont, "",
                Color.White, position);
            
            this.Components.Add(transitionMessage);
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
            KeyboardState ks = Keyboard.GetState();

            //Scroll through messages
            delayCounter++;            
            if (delayCounter > delay || (ks.IsKeyDown(Keys.Space) && ks != oldState) || messageIndex == -1)
            {
                delayCounter = 0;
                messageIndex++;

                //Load high scores after last message has been displayed
                if(messageIndex >= 5)
                {
                    game.loadHighScoreEntry();
                }

                //Display next message 
                else
                {                        
                    Vector2 dim = highlightFont.MeasureString(message[messageIndex]);
                    Vector2 position = new Vector2(Shared.stage.X / 2 - dim.X / 2, Shared.stage.Y / 4 - dim.Y / 2);
                    transitionMessage.Message = message[messageIndex];
                    transitionMessage.Position = position;
                }                   
                        
            }

            //Make kitty walk across the screen
            kitty.pathRight();
            oldState = ks;
            base.Update(gameTime);
        }

    }
}
