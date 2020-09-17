using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestAndroid
{
    public class MapTile
    {
        private Texture2D sprite;
        private Vector2 position;
        private Rectangle collider;
        private int type;

        /*
         * sprite: спрай
         * size: размеры коллайдера
         * type: 0 - up, 1 - left, 2 - right, 3 - down, 4 - void
         */
        public MapTile(Texture2D sprite, Vector2 position, Vector2 size, int type)
        {
            this.sprite = sprite;
            this.position = position;
            this.type = type;
            collider = ResetCollider(size);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 delta)
        {
            spriteBatch.Draw(sprite, position - delta, Color.White);
        }

        public Rectangle GetCollider()
        {
            return collider;
        }

        public int GetTileType()
        {
            return type;
        }

        private Rectangle ResetCollider(Vector2 size)
        {
           return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);            
        }

        static public MapTile GroundUp (Texture2D sprite, Vector2 position)
        {
            return new MapTile(sprite, position, new Vector2(120, 10), 0);
        }

        static public MapTile IceUp(Texture2D sprite, Vector2 position)
        {
            return new MapTile(sprite, position, new Vector2(120, 10), 5);
        }

        static public MapTile GroundVoid(Texture2D sprite, Vector2 position)
        {
            return new MapTile(sprite, position, new Vector2(120, 120), 4);
        }

        static public MapTile GroundLeft(Texture2D sprite, Vector2 position)
        {
            return new MapTile(sprite, position, new Vector2(120, 130), 1);
        }

        static public MapTile GroundRight(Texture2D sprite, Vector2 position)
        {
            return new MapTile(sprite, position, new Vector2(120, 130), 2);
        }

        static public MapTile CornerLeft(Texture2D sprite, Vector2 position)
        {
            return new MapTile(sprite, position, new Vector2(120, 130), 6);
        }

        static public MapTile CornerRight(Texture2D sprite, Vector2 position)
        {
            return new MapTile(sprite, position, new Vector2(120, 130), 7);
        }
    }
    public class DecorTile
    {
        private Texture2D[] sprites;
        private Vector2 position;
        private Rectangle collider;
        private int curFrame;
        private int type;

        // Анимация
        private int curTime = 0;
        private int period;

        public DecorTile(Texture2D sprite, Vector2 position)
        {
            sprites = new Texture2D[1];
            sprites[0] = sprite;
            this.position = position + new Vector2(120 - sprites[0].Width, 120 - sprites[0].Height);
            collider = ResetCollider(new Vector2(sprites[0].Width, sprites[0].Height));
            type = 0;
            curFrame = 0;
        }

        public DecorTile(Texture2D [] sprites, Vector2 position, int type)
        {
            this.sprites = sprites;
            this.position = position;
            this.type = type;
            collider = ResetCollider(new Vector2(120, 120));
            curFrame = 0;

            switch (this.type)
            {
                case 1:
                    period = 640;
                    break;
                case 2:
                    period = 120;
                    break;
            }
        }

        public Rectangle GetCollider()
        {
            return collider;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 delta)
        {
            spriteBatch.Draw(sprites[curFrame], position - delta, Color.White);
        }


        public void Update(GameTime gameTime)
        {
            if (sprites.Length > 1)
            {
                curTime += gameTime.ElapsedGameTime.Milliseconds;
                if (curTime > period)
                {
                    curTime = 0;
                    curFrame++;
                    if (curFrame >= sprites.Length)
                        curFrame = 0;
                }   
            }
        }

        private Rectangle ResetCollider(Vector2 size)
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }
    }
    public class CreepTile
    {
        private Texture2D[] sprites;
        private Vector2 position;
        private Vector2 start;
        private Vector2 finish;
        private bool gocha;
        private Rectangle collider;
        private int curFrame;
        private int type;
        private bool isExist = true;

        // Анимация
        private int curTime = 0;
        private int period;

        public CreepTile(Texture2D[] sprites, Vector2 position, int type)
        {
            this.sprites = sprites;            
            this.position = position;            
            this.type = type;            
            curFrame = 0;

            switch (this.type)
            {
                case 1: // yoszch right
                    start = position;
                    period = 128;
                    finish = position + new Vector2(120, 0) * 4;
                    collider = ResetCollider(new Vector2(120, 86));
                    break;
                case 2: // tornado right
                    start = position;
                    finish = position + new Vector2(120, 0) * 4;
                    period = 96;
                    collider = ResetCollider(new Vector2(120, 130));
                    break;
                case 3: // yoszch left
                    start = position;
                    period = 128;
                    finish = position - new Vector2(120, 0) * 4;
                    collider = ResetCollider(new Vector2(120, 86));
                    break;
                case 4: // tornado left
                    start = position;
                    finish = position - new Vector2(120, 0) * 4;
                    period = 96;
                    collider = ResetCollider(new Vector2(120, 130));
                    break;
                case 5: // cherry
                    start = position;
                    collider = ResetCollider(new Vector2(120, 130));
                    break;
                case 6: // icecream
                    start = position;
                    collider = ResetCollider(new Vector2(120, 130));
                    break;
                case 7: // baloon
                    start = position;
                    collider = ResetCollider(new Vector2(120, 140));
                    break;
                case 8: // pony
                    start = position;
                    collider = ResetCollider(new Vector2(sprites[0].Width + 200, sprites[0].Height + 10));
                    break;
                case 9: // map
                    start = position;
                    collider = ResetCollider(new Vector2(120, 140));
                    break;
            }
        }

        public Rectangle GetCollider()
        {
            return collider;
        }

        public int GetTileType()
        {
            return type;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 delta)
        {
            spriteBatch.Draw(sprites[curFrame], position - delta, Color.White);
        }

        public bool IsExist()
        {
            return isExist;
        }

        public void Kill()
        {
            isExist = false;
        }

        public void WakeUp()
        {
            isExist = true;
        }

        public void Update(GameTime gameTime)
        {
            switch (type)
            {
                case 1:
                    curTime += gameTime.ElapsedGameTime.Milliseconds;
                    if (curTime > period)
                    {
                        curTime = 0;
                        curFrame++;

                        if (!gocha)
                        {                            
                            if (curFrame > 3)
                                curFrame = 2;
                        }

                        if (gocha)
                        {
                            if (curFrame > 1)
                                curFrame = 0;
                        }
                    }

                    if (position.X >= finish.X)
                    {
                        gocha = true;
                        curFrame = 0;
                    }
                    if (position.X <= start.X)
                    {
                        gocha = false;
                        curFrame = 2;
                    }

                    if (!gocha)
                        position.X += 4;
                    else
                        position.X -= 4;

                    collider.X = (int)position.X;
                    break;
                case 2:
                    curTime += gameTime.ElapsedGameTime.Milliseconds;
                    if (curTime > period)
                    {
                        curTime = 0;
                        curFrame++;
                        if (curFrame >= sprites.Length)
                            curFrame = 0;
                    }

                    if (position.X >= finish.X)
                        gocha = true;
                    if (position.X <= start.X)
                        gocha = false;

                    if (!gocha)
                        position.X += 5;
                    else
                        position.X -= 5;

                    collider.X = (int)position.X;
                    break;
                case 3:
                    curTime += gameTime.ElapsedGameTime.Milliseconds;
                    if (curTime > period)
                    {
                        curTime = 0;
                        curFrame++;

                        if (gocha)
                        {
                            if (curFrame > 3)
                                curFrame = 2;
                        }

                        if (!gocha)
                        {
                            if (curFrame > 1)
                                curFrame = 0;
                        }
                    }

                    if (position.X <= finish.X)
                    {
                        gocha = true;
                        curFrame = 2;
                    }
                    if (position.X >= start.X)
                    {
                        gocha = false;
                        curFrame = 0;
                    }

                    if (!gocha)
                        position.X -= 4;
                    else
                        position.X += 4;

                    collider.X = (int)position.X;
                    break;
                case 4:
                    curTime += gameTime.ElapsedGameTime.Milliseconds;
                    if (curTime > period)
                    {
                        curTime = 0;
                        curFrame++;
                        if (curFrame >= sprites.Length)
                            curFrame = 0;
                    }

                    if (position.X <= finish.X)
                        gocha = true;
                    if (position.X >= start.X)
                        gocha = false;

                    if (!gocha)
                        position.X -= 5;
                    else
                        position.X += 5;

                    collider.X = (int)position.X;
                    break;
                default:
                    break;
        }
        }

        private Rectangle ResetCollider(Vector2 size)
        {
            return new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
        }
    }
}