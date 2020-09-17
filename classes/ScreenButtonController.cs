using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace TestAndroid
{
    public class ScreenButtonController
    {        
        ScreenButton[] buttons;

        public ScreenButtonController(ScreenButton [] buttons) // 0 - left, 1 - right,  2 - a 
        {
            this.buttons = buttons;
        }

        public int [] Listener()
        {            
            int[] answer = { 0, 0 }; // 1)0/1/2 - no/left/right  2) 0/1 - no/a

            if (GamePad.GetState(PlayerIndex.One).IsConnected) // для XBox геймпада
            {
                answer[0] = GamePad.GetState(PlayerIndex.One).DPad.Left != ButtonState.Pressed ? (GamePad.GetState(PlayerIndex.One).DPad.Right != ButtonState.Pressed ? 0: 2) : 1;
                answer[1] = GamePad.GetState(PlayerIndex.One).Buttons.A != ButtonState.Pressed ? 0 : 1;
            }
            else
            {               

                TouchCollection touches = TouchPanel.GetState(); // Массив косаний к экрану /  *далее используем первые два
                if (touches.Count > 0) // Проверка что массив заполнен значениями
                {
                    for (int i = 0; i < touches.Count; i++) // Проверка пересечения касаний с кнопками
                    {
                        if (buttons[0].Crossing(touches[i].Position))
                            answer[0] = 1;
                        if (buttons[1].Crossing(touches[i].Position))
                            answer[0] = 2;
                        if (buttons[2].Crossing(touches[i].Position))
                            answer[1] = 1;
                    }
                }
                else
                {
                    answer[0] = Keyboard.GetState().IsKeyDown(Keys.Left) != true ? (Keyboard.GetState().IsKeyDown(Keys.Right) != true ? 0 : 2) : 1;
                    answer[1] = Keyboard.GetState().IsKeyDown(Keys.Space) != true ? 0 : 1;
                }
            }

            /* установка значений состояния кнопок в зависимости от значений answer */
            switch (answer[0])
            {
                case 0:
                    if(buttons[0].GetState())
                    {
                        buttons[0].SetUp();
                    }
                    if (buttons[1].GetState())
                    {
                        buttons[1].SetUp();
                    }
                    break;
                case 1:
                    if (!buttons[0].GetState())
                    {
                        buttons[0].SetDown();
                    }
                    if (buttons[1].GetState())
                    {
                        buttons[1].SetUp();
                    }
                    break;
                case 2:
                    if (buttons[0].GetState())
                    {
                        buttons[0].SetUp
();
                    }
                    if (!buttons[1].GetState())
                    {
                        buttons[1].SetDown();
                    }
                    break;
            }
            switch (answer[1])
            {
                case 0:
                    if (buttons[2].GetState())
                    {
                        buttons[2].SetUp();
                    }
                    break;
                case 1:
                    if (!buttons[2].GetState())
                    {
                        buttons[2].SetDown();
                    }
                    break;
            }

            return answer; 
        }
    }
}