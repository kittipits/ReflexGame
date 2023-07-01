using DxLibDLL;
using MyLib;
using System;

namespace Reflexes
{
    public class Game
    {
        enum State
        { 
            Title,
            Ready,
            Push,
            Result,
        }

        Random rnd = new Random();
        State state;
        int readyTimer;
        int TimeToPush;
        bool isFlying;
        float highScore = float.MaxValue;

        float sumScore = 0;
        float playTime = 0;
        int oneFrame;
        float frameCounter;

        int bigFont;


        int rndR;
        int rndG;
        int rndB;
        int rndX;
        int rndY;
            
    public void Init()
        {
            Input.Init();
            MyRandom.Init();

            state = State.Title;

            bigFont = DX.CreateFontToHandle(null, 60, -1);
            rndR = rnd.Next(0,256);
            rndG = rnd.Next(0, 256);
            rndB = rnd.Next(0, 256);

            rndX = rnd.Next(100, 500);
            rndY = rnd.Next(20, 420);
        }

        public void Update()
        {
            Input.Update();

            if (state == State.Title)
            {
                if (rndR > 255) rndR = 0;
                if (rndG > 255) rndG = 0;
                if (rndB > 255) rndB = 0;
                rndR++;
                rndG++;
                rndB++;

                if (Input.GetButtonDown(DX.PAD_INPUT_1))
                {
                    readyTimer = MyRandom.Range(90, 300);
                    isFlying = false;
                    state = State.Ready;
                }
            }
            else if (state == State.Ready)
            {
                readyTimer--;

                if (Input.GetButtonDown(DX.PAD_INPUT_1) && isFlying == true)
                {
                    state = State.Title;
                }
                else if (Input.GetButtonDown(DX.PAD_INPUT_1))
                {
                    isFlying = true;
                }
                else if (readyTimer <= 0 && isFlying == false)
                {
                    state = State.Push;
                    TimeToPush = 0;
                }
            }
            else if (state == State.Push)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_1))
                {
                    state = State.Result;
                }
                else
                {
                    TimeToPush++;
                }
            }
            else if (state == State.Result)
            {
                if (Input.GetButtonDown(DX.PAD_INPUT_1))
                {
                    state = State.Title;
                }
            }
        }

        public void Draw()
        {
            uint white = DX.GetColor(255, 255, 255);
            uint grey = DX.GetColor(150, 150, 150);
            uint green = DX.GetColor(0, 255, 0);
            uint red = DX.GetColor(255, 0, 0);
            uint blue = DX.GetColor(0, 0, 255);            
            uint special = DX.GetColor(rndR, rndG, rndB);

            frameCounter++;

            if (state == State.Title)
            {
                oneFrame = 0;
                DrawCenter.DrawStringCenterToHandle(150, "反射神経測定ゲーム", special, bigFont);
                if (frameCounter % 90 > 45)
                {
                    DrawCenter.DrawStringCenter(450, "Zキーでスタート", grey);
                }
            }
            else if (state == State.Ready)
            {
                if (isFlying == true)
                {
                    DrawCenter.DrawStringCenterToHandle(150, "フライング!!!!", red, bigFont);
                    
                    if (frameCounter % 90 > 45)
                    {
                        DrawCenter.DrawStringCenter(450, "Zキーでタイトルの画面へ", grey);
                    }
                }
                else
                {
                    if (frameCounter % 15 == 0)
                    {
                        rndX = rnd.Next(100, 500);
                        rndY = rnd.Next(20, 420);
                    }
                    DX.DrawString(rndX, rndY, "Ready...", grey);
                }
            }
            else if (state == State.Push)
            {
                if (frameCounter % 10 > 5)
                {
                    DrawCenter.DrawStringCenterToHandle(150, "PUSH!!!!", white, bigFont);
                }
            }
            else if (state == State.Result)
            {
                float sec = TimeToPush / 60f;

                if (oneFrame == 0)
                {
                    playTime += 1;
                    sumScore += sec;
                    oneFrame++;
                }

                float averageScore = sumScore / playTime;

                if (sec < highScore) 
                {
                    highScore = sec;
                }
                DrawCenter.DrawStringCenterToHandle(50, "結果：" + sec.ToString("00.00秒"), white, bigFont);
                DrawCenter.DrawStringCenter(150, "ハイスコア：" + highScore.ToString("00.00秒"), green);
                DrawCenter.DrawStringCenter(200, "平均値：" + averageScore.ToString("00.00秒"), blue);
                
                if (frameCounter % 90 > 45)
                {
                    DrawCenter.DrawStringCenter(450, "Zキーでタイトルの画面へ", grey);
                }
            }
        }
    }
}
