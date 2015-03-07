/*  LevelOne.cs
 *  
 *  This class provides the components for the first level of the game
 *  Inherits from ActionScene.cs
 *  
 *  Nicole Dahlquist & Peter Thomson
 *  
 *  2014-11-23 Created:
 *      + declare variables, create draw and update methods
 *      + object layouts and game placements moved from actionScene
 *      *      
 *  Revision History:
 *  2014-11-24
 *      + added stick pits, small and large
 *      + https://www.youtube.com/watch?v=qMfoRqbS-Ko Level One Music
 *  2014-11-30
 *      + added stone wall to beginning so player can not travel backwards out of level
 *  2014-12-01
 *      + completed level two obstacle addition
 *      + added zombies
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
    /// This class provides the components for the first level of the game
    /// </summary>
    public class LevelOne : ActionScene
    {
        //LevelOne class variables
        private Game1 game;

        /// <summary>
        /// Constructor for LevelOne class
        /// </summary>
        /// <param name="game">Main game</param>
        /// <param name="spriteBatch">SpriteBatch</param>
        /// <param name="backGroundTex">Level one background texture</param>
        /// <param name="foreGroundTex">Level one foreground texture</param>
        public LevelOne(Game game, SpriteBatch spriteBatch, Texture2D backGroundTex, Texture2D foreGroundTex)
            : base(game, spriteBatch)
        {

            // Construct child components
            this.spriteBatch = spriteBatch;
            this.game = (Game1)game;
            
            this.backgroundTex = backGroundTex;
            this.foregroundTex = foreGroundTex;            
            srcB = new Rectangle(0, 0, 1024, 500);
            srcF = new Rectangle(0, 0, 1024, 350);
            pos2 = new Vector2(0, Shared.stage.Y - srcB.Height);
            pos1 = new Vector2(0, Shared.stage.Y - srcF.Height);
            int layer0 = 0;
            int layer1 = 1;
            sb2 = new ScrollingBackground(game, spriteBatch, this.backgroundTex, srcB, pos2, new Vector2(0, 0), delay, layer0);
            sb1 = new ScrollingBackground(game, spriteBatch, this.foregroundTex, srcF, pos1, new Vector2(0, 0), delay, layer1);

            

            //Blocks
            createExtraTallLeftSolid(game, new Vector2(-270, Shared.stage.Y));
            createExtraTallRightSolid(game, new Vector2(-180, Shared.stage.Y));
            createExtraTallLeftSolid(game, new Vector2(-270, Shared.stage.Y - 180));
            createExtraTallRightSolid(game, new Vector2(-180, Shared.stage.Y - 180));
            createExtraTallLeftSolid(game, new Vector2(-270, Shared.stage.Y - 360));
            createExtraTallRightSolid(game, new Vector2(-180, Shared.stage.Y - 360));            
            
            createExtraTallLeftSolid(game, new Vector2(1460, Shared.stage.Y));
            
            createStone(game, new Vector2(50, Shared.stage.Y));
            createShort(game, new Vector2(425, Shared.stage.Y));
            createExtraTall(game, new Vector2(1050, Shared.stage.Y));
            createExtraTallRightSolid(game, new Vector2(1250, Shared.stage.Y));
            createShort(game, new Vector2(1100, Shared.stage.Y));
            createMedium(game, new Vector2(950, Shared.stage.Y));
            createStone(game, new Vector2(1000, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(500, Shared.stage.Y - 360), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(490, Shared.stage.Y - 200), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(700, Shared.stage.Y - 160), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(100, Shared.stage.Y - 160), Block.sharedTypes.STAR_BLOCK_1));
            createLog(game, new Vector2(1150, Shared.stage.Y - 210));
            createLog(game, new Vector2(1360, Shared.stage.Y - 210));
            createSmallStickPit(game, new Vector2(1370, Shared.stage.Y), 3);
            createShort(game, new Vector2(1950, Shared.stage.Y));
            createLargeStickPit(game, new Vector2(2070, Shared.stage.Y), 12);
            blocks.Add(new Block(game, spriteBatch, new Vector2(2100, Shared.stage.Y - 150), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2110, Shared.stage.Y - 310), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2175, Shared.stage.Y - 225), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2175, Shared.stage.Y - 400), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2475, Shared.stage.Y - 320), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2485, Shared.stage.Y - 480), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(2675, Shared.stage.Y - 250), Block.sharedTypes.PLATFORM));
            createExtraTallLeftSolid(game, new Vector2(2790, Shared.stage.Y));
            createMedium(game, new Vector2(2890, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3325, Shared.stage.Y - 100), Block.sharedTypes.PLATFORM));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3325, Shared.stage.Y - 190), Block.sharedTypes.PLATFORM));
            createExtraTallLeftSolid(game, new Vector2(3400, Shared.stage.Y));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3685, Shared.stage.Y - 160), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3750, Shared.stage.Y - 320), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(3925, Shared.stage.Y - 160), Block.sharedTypes.STAR_BLOCK_1));
            blocks.Add(new Block(game, spriteBatch, new Vector2(4000, Shared.stage.Y - 320), Block.sharedTypes.STAR_BLOCK_1));

            //Zombies
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(1600, Shared.stage.Y - 64), delay, 1));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(750, Shared.stage.Y - 64), delay, 2));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(3050, Shared.stage.Y - 64), delay, 2));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(3550, Shared.stage.Y - 64), delay, 1));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(3570, Shared.stage.Y - 64), delay*5/3, 2));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(3590, Shared.stage.Y - 64), delay*2, 1));
            zombies.Add(new Zombie(game, spriteBatch, new Vector2(3610, Shared.stage.Y - 64), delay*3/2, 1));
            createStone(game, new Vector2(4200, Shared.stage.Y));
            endLevelPos = new Vector2(4250, Shared.stage.Y);
            endLevelFlag = new Flag(game, spriteBatch, endLevelPos);

            // Add components            
            this.Components.Add(sb2);
            this.Components.Add(sb1);
            
            
            foreach (Zombie z in zombies)
            {
                this.Components.Add(z);
                z.start();
            }
            foreach (Block b in blocks)
            {
                this.Components.Add(b);

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
        // make small stick pit
        private void createSmallStickPit(Game game, Vector2 position, int length=1)
        {
            position.Y = position.Y - 44;
            for (int i = 0; i < length; i++)
            {
                blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + (30*i), position.Y), Block.blockTypes.STICK_PIT_SMALL));
            }
            
        }
        // make large stick pit
        private void createLargeStickPit(Game game, Vector2 position, int length = 1)
        {
            position.Y = position.Y - 44;
            for (int i = 0; i < length; i++)
            {
                blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + (60 * i), position.Y), Block.blockTypes.STICK_PIT_LARGE));
            }
        }
        // make the log
        private void createLog(Game game, Vector2 position)
        {
            position.Y = position.Y - 23;
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.LOG));
            //Star Block
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 150), Block.sharedTypes.STAR_BLOCK_1));
        }
        // make a short pillar
        private void createShort(Game game, Vector2 position)
        {
            position.Y = position.Y - 30;
            //Bottom row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y), Block.blockTypes.DEFAULT_RIGHT));
            //2nd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 30), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 30), Block.blockTypes.DEFAULT_RIGHT));
            //Top row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.blockTypes.GRASS_TOP_EDGE_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 60), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 60), Block.blockTypes.GRASS_TOP_EDGE_RIGHT));
        }
        // make a medium height pillar
        private void createMedium(Game game, Vector2 position)
        {
            position.Y = position.Y - 30;
            //Bottom row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y), Block.blockTypes.DEFAULT_RIGHT));
            //2nd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 30), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 30), Block.blockTypes.DEFAULT_RIGHT));
            //3rd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 60), Block.blockTypes.DEFAULT_RIGHT));
            //Top row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 90), Block.blockTypes.GRASS_TOP_EDGE_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 90), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 90), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 90), Block.blockTypes.GRASS_TOP_EDGE_RIGHT));
        }
        // make a tall pillar
        private void createTall(Game game, Vector2 position)
        {
            position.Y = position.Y - 30;
            //Bottom row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y), Block.blockTypes.DEFAULT_RIGHT));
            //2nd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 30), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 30), Block.blockTypes.DEFAULT_RIGHT));
            //3rd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 60), Block.blockTypes.DEFAULT_RIGHT));
            //4th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 90), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 90), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 90), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 90), Block.blockTypes.DEFAULT_RIGHT));
            //5th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 120), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 120), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 120), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 120), Block.blockTypes.DEFAULT_RIGHT));
            //Top row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 150), Block.blockTypes.GRASS_TOP_EDGE_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 150), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 150), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 150), Block.blockTypes.GRASS_TOP_EDGE_RIGHT));


        }
        private void createExtraTall(Game game, Vector2 position)
        {
            position.Y = position.Y - 30;
            //Bottom row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y), Block.blockTypes.DEFAULT_RIGHT));
            //2nd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 30), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 30), Block.blockTypes.DEFAULT_RIGHT));
            //3rd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 60), Block.blockTypes.DEFAULT_RIGHT));
            //4th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 90), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 90), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 90), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 90), Block.blockTypes.DEFAULT_RIGHT));
            //5th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 120), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 120), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 120), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 120), Block.blockTypes.DEFAULT_RIGHT));
            //6th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 150), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 150), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 150), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 150), Block.blockTypes.DEFAULT_RIGHT));
            //Top row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 180), Block.blockTypes.GRASS_TOP_EDGE_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 180), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 180), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 180), Block.blockTypes.GRASS_TOP_EDGE_RIGHT));


        }
        private void createExtraTallLeftSolid(Game game, Vector2 position)
        {
            position.Y = position.Y - 30;
            //Bottom row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.GRASS_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y), Block.blockTypes.DEFAULT_RIGHT));
            //2nd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 30), Block.blockTypes.GRASS_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 30), Block.blockTypes.DEFAULT_RIGHT));
            //3rd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.blockTypes.GRASS_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 60), Block.blockTypes.DEFAULT_RIGHT));
            //4th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 90), Block.blockTypes.GRASS_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 90), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 90), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 90), Block.blockTypes.DEFAULT_RIGHT));
            //5th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 120), Block.blockTypes.GRASS_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 120), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 120), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 120), Block.blockTypes.DEFAULT_RIGHT));
            //6th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 150), Block.blockTypes.GRASS_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 150), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 150), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 150), Block.blockTypes.DEFAULT_RIGHT));
            //Top row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 180), Block.blockTypes.GRASS_LEFT_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 180), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 180), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 180), Block.blockTypes.GRASS_TOP_EDGE_RIGHT));


        }
        private void createExtraTallRightSolid(Game game, Vector2 position)
        {
            position.Y = position.Y - 30;
            //Bottom row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y), Block.blockTypes.GRASS_RIGHT));
            //2nd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 30), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 30), Block.blockTypes.GRASS_RIGHT));
            //3rd row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 60), Block.blockTypes.GRASS_RIGHT));
            //4th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 90), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 90), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 90), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 90), Block.blockTypes.GRASS_RIGHT));
            //5th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 120), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 120), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 120), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 120), Block.blockTypes.GRASS_RIGHT));
            //6th row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 150), Block.blockTypes.DEFAULT_LEFT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 150), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 150), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 150), Block.blockTypes.GRASS_RIGHT));
            //Top row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 180), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 180), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 180), Block.blockTypes.GRASS_TOP));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 90, position.Y - 180), Block.blockTypes.GRASS_RIGHT_TOP));


        }
        //make a square
        private void createCube(Game game, Vector2 position)
        {
            //Bottom Row
            position.Y = position.Y - 30;
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y), Block.blockTypes.DEFAULT));

            //Middle Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 30), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 30), Block.blockTypes.DEFAULT));

            //Top Row
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 30, position.Y - 60), Block.blockTypes.DEFAULT));
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X + 60, position.Y - 60), Block.blockTypes.DEFAULT));
        }
        // make a stone
        private void createStone(Game game, Vector2 position)
        {
            position.Y = position.Y - 30;
            blocks.Add(new Block(game, spriteBatch, new Vector2(position.X, position.Y), Block.blockTypes.GRASS_PEAK));
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
                MediaPlayer.Stop();
                Shared.score += 1000;
                Shared.level = 2;
                game.loadTransitionOne();
            }
            base.Update(gameTime);
        }
    }
}
