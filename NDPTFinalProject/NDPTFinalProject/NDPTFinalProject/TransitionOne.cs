/*  TransitionOne.cs
 *  
 *  This game component provides the game's transition scene 
 *  between first and second levels
 *  
 *  Inherits from Transition.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-29 Created:
 *      + declare variables, create draw and update methods
 *      + set background
 *      + create messages to loop through(loops indefinitely)
 *  2014-11-20
 *      + fixed infinite loop
 *      + moved shareable components into parent class
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
    /// This class provides the transition scene between the first and second levels of the game
    /// </summary>
    public class TransitionOne : Transition
    {
        /// <summary>
        /// Constructor for the TransitionOne class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        public TransitionOne(Game game, SpriteBatch spriteBatch)
            : base(game, spriteBatch)
        {
            //Set background texture and messages specific to this scene           
            this.backgroundTex = game.Content.Load<Texture2D>("Images/TransitionOneToTwo");

            Vector2 position = new Vector2(Shared.stage.X / 2 - 250, Shared.stage.Y / 2 - 100);
            message = new string[5];
            message[0] = "Hello Kitty has just survived a forest filled with zombies. ";
            message[1] = "She is more concerned than ever about her friends.";
            message[2] = "As she exits the forest she can see the village across a clearing.";
            message[3] = "What will Hello Kitty find when she arrives?";
            message[4] = "Are her friends safe? Will Hello Kitty find safety?";

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

        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();

            //Change messsages displayed after a set delay
            delayCounter++;
            if (delayCounter > delay || (ks.IsKeyDown(Keys.Space) && ks != oldState) || messageIndex == -1)
            {
                delayCounter = 0;
                messageIndex++;
                //Load level two after the last message
                if (messageIndex >= message.Length)
                {
                    game.loadLevelTwo();
                }

                //Display the next message
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
