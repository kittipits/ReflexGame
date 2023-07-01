using DxLibDLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLib
{
    internal class DrawCenter
    {
        public static void DrawStringCenter(int y, string s, uint color)
        {
            int width = DX.GetDrawStringWidth(s, s.Length);
            int screenWidth = 640;
            int x = screenWidth / 2 - width / 2;
            DX.DrawString(x, y, s, color);
        }

        public static void DrawStringCenterToHandle(int y, string s, uint color, int fontHandle)
        {
            int width = DX.GetDrawStringWidthToHandle(s, s.Length, fontHandle);
            int screenWidth = 640;
            int x = screenWidth / 2 - width / 2;
            DX.DrawStringToHandle(x, y, s, color, fontHandle);
        }
    }
}
