using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TestAndroid
{
    static public class ReadMap
    {
        static public MapTile[] MapTiles(Texture2D texture2DMap, Texture2D[] spritesPack)
        {
            /* +4 */
            Color groundVoid = new Color(255, 255, 0);
            /* +0 */
            Color groundUp = new Color(0, 255, 0);
            /* +1 */
            Color groundLeft = new Color(0, 200, 0);
            /* +2 */
            Color groundRight = new Color(0, 100, 0);
            /* +3 */
            Color groundLeft2 = new Color(100, 200, 0);
            /* +4 */
            Color groundRight2 = new Color(100, 100, 0);

            /* +6 */
            Color groundLeftCorner = new Color(200, 200, 200);
            /* +7 */
            Color borderLeft = new Color(160, 160, 160);
            /* +8 */
            Color borderLeft2 = new Color(120, 120, 120);
            /* +9 */
            Color groundRightCorner = new Color(80, 80, 80);
            /* +10 */
            Color borderRight = new Color(40, 40, 40);
            /* +11 */
            Color borderRight2 = new Color(0, 0, 0);
            /* 12 */
            Color bridgeUp = new Color(0, 255, 255);
            /* 13 */
            Color bridgeLeft = new Color(0, 200, 255);
            /* 14 */
            Color bridgeRight = new Color(0, 100, 255);
            /* 15 */
            Color waterVoid = new Color(0, 0, 255);
            /* 16 */
            Color iceUp = new Color(0, 0, 200);
            /* 17 */
            Color iceLeft = new Color(0, 0, 150);
            /* 18 */
            Color iceRight = new Color(0, 0, 100);
            /* 19 */
            Color cornerLeft = new Color(100, 100, 100);
            /* 20 */
            Color cornerRight = new Color(10, 10, 10);

            int tmp = texture2DMap.Width * texture2DMap.Height;
            Color[] colors = new Color[tmp];

            texture2DMap.GetData(colors);

            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == Color.White)
                    tmp--;
            }

            MapTile[] answer = new MapTile[tmp];

            int colorsCount = 0, answerCount = 0;
            for (int j = 0; j < texture2DMap.Height; j++)
            {
                for (int i = 0; i < texture2DMap.Width; i++)
                {
                    if (colors[colorsCount] == groundVoid)
                    {
                        answer[answerCount] = MapTile.GroundVoid(spritesPack[5], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == groundLeftCorner)
                    {
                        answer[answerCount] = MapTile.GroundVoid(spritesPack[6], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == groundRightCorner)
                    {
                        answer[answerCount] = MapTile.GroundVoid(spritesPack[9], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == groundUp)
                    {
                        answer[answerCount] = MapTile.GroundUp(spritesPack[0], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == groundLeft)
                    {
                        answer[answerCount] = MapTile.GroundUp(spritesPack[1], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == groundRight)
                    {
                        answer[answerCount] = MapTile.GroundUp(spritesPack[2], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == groundLeft2)
                    {
                        answer[answerCount] = MapTile.GroundUp(spritesPack[3], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == groundRight2)
                    {
                        answer[answerCount] = MapTile.GroundUp(spritesPack[4], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == borderLeft)
                    {
                        answer[answerCount] = MapTile.GroundLeft(spritesPack[7], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == borderLeft2)
                    {
                        answer[answerCount] = MapTile.GroundLeft(spritesPack[8], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == borderRight)
                    {
                        answer[answerCount] = MapTile.GroundRight(spritesPack[10], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == borderRight2)
                    {
                        answer[answerCount] = MapTile.GroundRight(spritesPack[11], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == bridgeUp)
                    {
                        answer[answerCount] = MapTile.GroundUp(spritesPack[12], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == bridgeLeft)
                    {
                        answer[answerCount] = MapTile.GroundUp(spritesPack[13], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == bridgeRight)
                    {
                        answer[answerCount] = MapTile.GroundUp(spritesPack[14], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == waterVoid)
                    {
                        answer[answerCount] = MapTile.GroundVoid(spritesPack[15], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == iceUp)
                    {
                        answer[answerCount] = MapTile.IceUp(spritesPack[16], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == iceLeft)
                    {
                        answer[answerCount] = MapTile.IceUp(spritesPack[17], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == iceRight)
                    {
                        answer[answerCount] = MapTile.IceUp(spritesPack[18], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == cornerLeft)
                    {
                        answer[answerCount] = MapTile.CornerLeft(spritesPack[19], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }
                    if (colors[colorsCount] == cornerRight)
                    {
                        answer[answerCount] = MapTile.CornerRight(spritesPack[20], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    colorsCount++;
                }
            }
            return answer;
        }

        static public DecorTile[] DecorTiles(Texture2D texture2DMap, Texture2D[] spritesPack)
        {
            /* 0 */
            Color bushLeft = new Color(0, 255, 100);
            Color house = new Color(80, 50, 50);
            Color bborder = new Color(100, 60, 50);
            Color well = new Color(120, 80, 50);
            Color btree = new Color(140, 90, 60);

            Color fborder = new Color(70, 50, 30);
            Color cake = new Color(80, 60, 30);
            Color ftree = new Color(120, 90, 60);

            /* 1 */
            Color bushRight = new Color(0, 200, 100);
            /* 2 */
            Color bushLeftAnimate = new Color(100, 255, 100);
            /* 3 */
            Color bushRightLedyBug = new Color(200, 255, 100);
            /* 4 */
            Color splash = new Color(100, 100, 200);

            int tmp = texture2DMap.Width * texture2DMap.Height;
            Color[] colors = new Color[tmp];

            texture2DMap.GetData(colors);

            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == Color.White)
                    tmp--;
            }

            DecorTile[] answer = new DecorTile[tmp];

            int colorsCount = 0, answerCount = 0;
            for (int j = 0; j < texture2DMap.Height; j++)
            {
                for (int i = 0; i < texture2DMap.Width; i++)
                {
                    if (colors[colorsCount] == bushLeft)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[0], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == bushRight)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[1], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == bushLeftAnimate)
                    {
                        Texture2D[] sprites = { spritesPack[2], spritesPack[3] };
                        answer[answerCount] = new DecorTile(sprites, new Vector2(i * 120, j * 120), 1);
                        answerCount++;
                    }

                    if (colors[colorsCount] == bushRightLedyBug)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[4], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == splash)
                    {
                        Texture2D[] sprites = { spritesPack[5], spritesPack[6], spritesPack[7], spritesPack[8]};
                        answer[answerCount] = new DecorTile(sprites, new Vector2(i * 120, j * 120), 2);
                        answerCount++;
                    }

                    if (colors[colorsCount] == house)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[9], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == bborder)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[10], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == well)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[11], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == btree)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[12], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == fborder)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[13], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == cake)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[14], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    if (colors[colorsCount] == ftree)
                    {
                        answer[answerCount] = new DecorTile(spritesPack[15], new Vector2(i * 120, j * 120));
                        answerCount++;
                    }

                    colorsCount++;
                }
            }

            return answer;
        }

        static public CreepTile[] CreepTiles(Texture2D texture2DMap, Texture2D[] spritesPack)
        {
            // tornado
            Color tornadoLeft = new Color(0, 0, 50);            
            Color tornadoRight = new Color(0, 0, 100);
            
            // yoshcz
            Color eLeft = new Color(80, 80, 80);            
            Color eRight = new Color(70, 70, 70);

            // cherry
            Color cherry = new Color(255, 0, 0);

            // icecream
            Color icecream = new Color(0, 255, 0);

            // baloon
            Color baloon = new Color(0, 0, 255);

            // pony
            Color pony = new Color(120, 190, 220);

            // map
            Color map = new Color(255, 130, 100);

            int tmp = texture2DMap.Width * texture2DMap.Height;
            Color[] colors = new Color[tmp];

            texture2DMap.GetData(colors);

            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == Color.White)
                    tmp--;
            }

            CreepTile[] answer = new CreepTile[tmp];

            int colorsCount = 0, answerCount = 0;
            for (int j = 0; j < texture2DMap.Height; j++)
            {
                for (int i = 0; i < texture2DMap.Width; i++)
                {
                    if (colors[colorsCount] == tornadoLeft)
                    {
                        Texture2D[] sprites = { spritesPack[4], spritesPack[5], spritesPack[6], spritesPack[7] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2( i * 120, j * 120), 4);
                        answerCount++;
                    }

                    if (colors[colorsCount] == tornadoRight)
                    {
                        Texture2D[] sprites = { spritesPack[4], spritesPack[5], spritesPack[6], spritesPack[7] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2(i * 120, j * 120), 2);
                        answerCount++;
                    }

                    if (colors[colorsCount] == eLeft)
                    {
                        Texture2D[] sprites = { spritesPack[0], spritesPack[1], spritesPack[2], spritesPack[3] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2(i * 120, j * 120 + 44), 3);
                        answerCount++;
                    }

                    if (colors[colorsCount] == eRight)
                    {
                        Texture2D[] sprites = { spritesPack[0], spritesPack[1], spritesPack[2], spritesPack[3] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2(i * 120, j * 120 + 44), 1);
                        answerCount++;
                    }

                    if (colors[colorsCount] == cherry)
                    {
                        Texture2D[] sprites = { spritesPack[8] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2(i * 120, j * 120), 5);
                        answerCount++;
                    }

                    if (colors[colorsCount] == icecream)
                    {
                        Texture2D[] sprites = { spritesPack[9] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2(i * 120, j * 120), 6);
                        answerCount++;
                    }

                    if (colors[colorsCount] == baloon)
                    {
                        Texture2D[] sprites = { spritesPack[10] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2(i * 120, j * 120), 7);
                        answerCount++;
                    }

                    if (colors[colorsCount] == pony)
                    {
                        Texture2D[] sprites = { spritesPack[11], spritesPack[12] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2(i * 120, j * 120 - 120), 8);
                        answerCount++;
                    }

                    if (colors[colorsCount] == map)
                    {
                        Texture2D[] sprites = { spritesPack[13] };
                        answer[answerCount] = new CreepTile(sprites, new Vector2(i * 120, j * 120), 9);
                        answerCount++;
                    }

                    colorsCount++;
                }
            }
            return answer;
        }
    }
}