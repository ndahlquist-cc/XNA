/*  LevelTwo.cs
 *  
 *  This class provides the components for the second level of the game
 *  Inherits from ActionScene.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-23 Created:
 *      + declare variables, create draw and update methods
 *      + http://www.fernhillpk.com/images/camp-area-2.jpg background image
 *      + added obstacle objects, scrolling background
 *      + http://opengameart.org/sites/default/files/CampFire_0.png fire image
 *  2014-11-25
 *      + added trees, fences, fire and windows with awnings to town spritesheet
 *      + added topCollision for the new items that needed it
 *  2014-11-27
 *      + added new object layouts and game placement
 *      + added flag to end of level (needs to do something still)
 *  2014-11-29
 *      + https://www.youtube.com/watch?v=ywDcqijf0fw level two music
 *      + added more objects to complete level
 *      + test level with new gravity and initial jump speeds
 *  2014-11-30
 *      + added lots of fire to start so player can not go backwards out of level
 *      + added points for completing the level
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
    /// This class provides the components for the second level of the game
    /// </summary>
    public class LevelTwo : ActionScene
    {
        //LevelTwo class variables
        private Game1 game;
        private Song levelTwoSong;

        /// <summary>
        /// LevelTwo class constructor
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="backGroundTex">Level two background texture</param>
        /// <param name="foreGroundTex">Level two foreground texture</param>
        /// <param name="levelTwoSong">level two music</param>
        public LevelTwo(Game game, SpriteBatch spriteBatch, Texture2D backGroundTex, Texture2D foreGroundTex, Song levelTwoSong)
            : base(game, spriteBatch)
        {
            //Scrolling foreground and background
            this.spriteBatch = spriteBatch;

            // Construct child components 
            this.backgroundTex = backGroundTex;
            this.foregroundTex = foreGroundTex;
            this.levelTwoSong = levelTwoSong;
            this.game = (Game1)game;
            srcB = new Rectangle(0, 0, 1024, 700);
            srcF = new Rectangle(0, 0, 1024, 200);
            pos2 = new Vector2(0, Shared.stage.Y - srcB.Height);
            pos1 = new Vector2(0, Shared.stage.Y - srcF.Height);
            int layer0 = 0;
            int layer1 = 1;
            sb2 = new ScrollingBackground(game, spriteBatch, this.backgroundTex, srcB, pos2, new Vector2(0, 0), delay, layer0);
            sb1 = new ScrollingBackground(game, spriteBatch, this.foregroundTex, srcF, pos1, new Vector2(0, 0), delay, layer1);

            //Blocks  
            createFire(game, new Vector2(-420, Shared.stage.Y));
            createFire(game, new Vector2(-390, Shared.stage.Y));
            createFire(game, new Vector2(-360, Shared.stage.Y));
            createFire(game, new Vector2(-330, Shared.stage.Y));
            createFire(game, new Vector2(-300, Shared.stage.Y));
            createFire(game, new Vector2(-270, Shared.stage.Y));
            createFire(game, new Vector2(-240, Shared.stage.Y));
            createFire(game, new Vector2(-210, Shared.stage.Y));
            createFire(game, new Vector2(-180, Shared.stage.Y));
            createFire(game, new Vector2(-150, Shared.stage.Y));
            createFire(game, new Vector2(-120, Shared.stage.Y));
            createFire(game, new Vector2(-90, Shared.stage.Y));
            createFire(game, new Vector2(-60, Shared.stage.Y));
            createFire(game, new Vector2(-30, Shared.stage.Y));
            createFire(game, new Vector2(0, Shared.stage.Y));
            createFire(game, new Vector2(30, Shared.stage.Y));
            createSmallHouse(game, new Vector2(590, Shared.stage.Y));
            createMediumHouse(game, new Vector2(350, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(425, Shared.stage.Y - 400), Block.sharedTypes.STAR_BLOCK_1));
            
            createFence(game, new Vector2(530, Shared.stage.Y));
            createTree(game, new Vector2(1100, Shared.stage.Y));
            createTree(game, new Vector2(300, Shared.stage.Y));
            createFire(game, new Vector2(300, Shared.stage.Y - 90));
            createTree(game, new Vector2(700, Shared.stage.Y));
            createLargeHouse(game, new Vector2(1250, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(1400, Shared.stage.Y - 450), Block.sharedTypes.STAR_BLOCK_1));
            createFence(game, new Vector2(1070, Shared.stage.Y), 3);
            createFire(game, new Vector2(1130, Shared.stage.Y - 55));
            createFence(game, new Vector2(1550, Shared.stage.Y), 3);
            createTree(game, new Vector2(1750, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(150, Shared.stage.Y - 160), Block.sharedTypes.STAR_BLOCK_1));
            createFire(game, new Vector2(1370, Shared.stage.Y - 175));
            blocks.Add(new Block(game, spriteBatch, new Vector2(860, Shared.stage.Y - 330), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(1060, Shared.stage.Y - 360), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(850, Shared.stage.Y - 175), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(1050, Shared.stage.Y - 200), Block.sharedTypes.PLATFORM));
            
            createLargeHouse(game, new Vector2(1825, Shared.stage.Y));

            createFire(game, new Vector2(1945, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(1635, Shared.stage.Y - 300), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(1875, Shared.stage.Y - 450), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(1950, Shared.stage.Y - 450), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2025, Shared.stage.Y - 450), Block.sharedTypes.STAR_BLOCK_1));
            createFire(game, new Vector2(2075, Shared.stage.Y - 300));
            createTree(game, new Vector2(2140, Shared.stage.Y));
            createLongHouse(game, new Vector2(2200, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2630, Shared.stage.Y - 125), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2650, Shared.stage.Y - 285), Block.sharedTypes.STAR_BLOCK_1));
            createFence(game, new Vector2(2560, Shared.stage.Y), 4);
            createFire(game, new Vector2(2550, Shared.stage.Y));
            createFire(game, new Vector2(2600, Shared.stage.Y));
            createFire(game, new Vector2(2650, Shared.stage.Y));
            createFire(game, new Vector2(2700, Shared.stage.Y));
            createFire(game, new Vector2(2750, Shared.stage.Y)); 
            createTree(game, new Vector2(2810, Shared.stage.Y));
            createSmallHouse(game, new Vector2(2870, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3160, Shared.stage.Y - 450), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3150, Shared.stage.Y - 100), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3150, Shared.stage.Y - 200), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3150, Shared.stage.Y - 300), Block.sharedTypes.PLATFORM));
            createLargeHouse(game, new Vector2(3365, Shared.stage.Y));
            createFire(game, new Vector2(3490, Shared.stage.Y - 175));         

            createFire(game, new Vector2(3500, Shared.stage.Y));            

            endLevelPos = new Vector2(4000, Shared.stage.Y);
            endLevelFlag = new Flag(game, spriteBatch, endLevelPos);


            //Zombies
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(2050, Shared.stage.Y - 64), delay, 1));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(750, Shared.stage.Y - 64), delay, 2));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(750, Shared.stage.Y - 64), delay*3/2, 1));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(2820, Shared.stage.Y - 64), delay, 1));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(2900, Shared.stage.Y - 64), delay*5/3, 2));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(3150, Shared.stage.Y - 64), delay*3/2, 1));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(3575, Shared.stage.Y - 64), delay, 1));

            // Add components            
            this.Components.Add(sb2);
            this.Components.Add(sb1);
            foreach (Block b in blocks)
            {
                this.Components.Add(b);

            }
            foreach (Zombie z in zombies)
            {
                this.Components.Add(z);
                z.start();
            }
            

            this.Components.Add(kitty);
            this.Components.Add(endLevelFlag); 
            this.Components.Add(score);
            this.Components.Add(gameOver);
            this.Components.Add(cont);

            //Collision manager
            cm = new CollisionManager(game, kitty, zombies, blocks);
            this.Components.Add(cm);
            kitty.start();

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
        #region Object Layouts
        // make a fire
        private void createFire(Game game, Vector2 position)
        {
            position.Y -= 60;
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.townTypes.FIRE_1));
        }
        // make a fence
        private void createFence(Game game, Vector2 position, int length = 1)
        {
            position.Y -= 60;
            for (int i = 0; i < length; i++)
            {
                blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + (60 * i), position.Y), Block.townTypes.FENCE));
            }
        }
        // make a tree
        private void createTree(Game game, Vector2 position)
        {
            position.Y = position.Y - 120;
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.townTypes.TREE));
        }
        // make a small house
        private void createSmallHouse(Game game, Vector2 position)
        {
            position.Y = position.Y - 60;
            // Bottom Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.townTypes.BUSHES));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.townTypes.DOOR));
            // Top Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.townTypes.ROOF_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.townTypes.ROOF_RIGHT));
        }
        // make a large house
        private void createLargeHouse(Game game, Vector2 position)
        {
            position.Y = position.Y - 60;
            // Bottom Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.townTypes.WHITE_BLOCK));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.townTypes.WINDOW_WITH_AWNING));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y), Block.townTypes.DOOR));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 180, position.Y), Block.townTypes.WINDOW_WITH_AWNING));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 240, position.Y), Block.townTypes.WHITE_BLOCK));
            // 2nd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.townTypes.SMALL_WINDOW));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.townTypes.WHITE_BLOCK));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y - 60), Block.townTypes.ROUND_WINDOW));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 180, position.Y - 60), Block.townTypes.WHITE_BLOCK));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 240, position.Y - 60), Block.townTypes.SMALL_WINDOW));
            // 3rd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 120), Block.townTypes.WHITE_BLOCK));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 120), Block.townTypes.WINDOW));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y - 120), Block.townTypes.BALCONY_RAIL));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 180, position.Y - 120), Block.townTypes.WINDOW));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 240, position.Y - 120), Block.townTypes.WHITE_BLOCK));
            // 4th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 180), Block.townTypes.SMALL_WINDOW));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 180), Block.townTypes.WHITE_BLOCK));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y - 180), Block.townTypes.BALCONY_DOOR));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 180, position.Y - 180), Block.townTypes.WHITE_BLOCK));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 240, position.Y - 180), Block.townTypes.SMALL_WINDOW));
            // top row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 240), Block.townTypes.ROOF_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 240), Block.townTypes.ROOF_MID));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y - 240), Block.townTypes.ROOF_MID));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 180, position.Y - 240), Block.townTypes.ROOF_MID));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 240, position.Y - 240), Block.townTypes.ROOF_RIGHT));
        }
        // make a medium house
        private void createMediumHouse(Game game, Vector2 position)
        {
            position.Y = position.Y - 60;
            // Bottom Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.townTypes.BUSHES));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.townTypes.DOOR));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y), Block.townTypes.SMALL_WINDOW));
            // 2nd Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.townTypes.WHITE_BLOCK));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.townTypes.BALCONY_RAIL));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y - 60), Block.townTypes.WHITE_BLOCK));
            // 3rd Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 120), Block.townTypes.WINDOW));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 120), Block.townTypes.BALCONY_DOOR));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y - 120), Block.townTypes.WHITE_BLOCK));
            // Top Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 180), Block.townTypes.ROOF_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 180), Block.townTypes.ROOF_MID));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y - 180), Block.townTypes.ROOF_RIGHT));
        }
        // make a long house
        private void createLongHouse(Game game, Vector2 position)
        {
            position.Y = position.Y - 60;
            // Bottom Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.townTypes.BUSHES));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.townTypes.ROUND_WINDOW));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y), Block.townTypes.DOOR));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 180, position.Y), Block.townTypes.SMALL_WINDOW));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 240, position.Y), Block.townTypes.BUSHES));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 300, position.Y), Block.townTypes.BALCONY_DOOR));
            // Top Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.townTypes.ROOF_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.townTypes.ROOF_MID));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 120, position.Y - 60), Block.townTypes.ROOF_MID));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 180, position.Y - 60), Block.townTypes.ROOF_MID));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 240, position.Y - 60), Block.townTypes.ROOF_MID));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 300, position.Y - 60), Block.townTypes.ROOF_RIGHT));
        }
        #endregion
        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            //End of level
            if (kitty.Position.X >= endLevelFlag.FlagPos.X)
            {
                Shared.score += 2000;
                game.loadTransitionTwo();
            }
            base.Update(gameTime);
        }
    }
}
