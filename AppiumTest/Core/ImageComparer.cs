/* ***********************************************
 * Author		:  kingthy
 * Email		:  kingthy@gmail.com
 * DateTime		:  2008-06-15
 * Description	:  图像比较.用于找出两副图片之间的差异位置
 * License      :  MIT license
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace AppiumTest.Core
{
    /// <summary>
    /// 图像比较.用于找出两副图片之间的差异位置
    /// </summary>
    public class ImageComparer
    {
        /// <summary>
        /// 图像颜色
        /// </summary>
        [StructLayout(LayoutKind.Explicit)]
        private struct ICColor
        {
            [FieldOffset(0)]
            public byte B;
            [FieldOffset(1)]
            public byte G;
            [FieldOffset(2)]
            public byte R;
        }

        /// <summary>
        /// 比较两个图像
        /// </summary>
        /// <param name="bmp1">图片1</param>
        /// <param name="bmp2">图片2</param>
        /// <returns>相同的部分</returns>
        public static List<Rectangle> Compare(Bitmap bmp1, Bitmap bmp2)
        {
            List<Rectangle> rects = new List<Rectangle>();
            PixelFormat pf = PixelFormat.Format24bppRgb;

            BitmapData bd1 = bmp1.LockBits(new Rectangle(0, 0, bmp1.Width, bmp1.Height), ImageLockMode.ReadOnly, pf);
            BitmapData bd2 = bmp2.LockBits(new Rectangle(0, 0, bmp2.Width, bmp2.Height), ImageLockMode.ReadOnly, pf);
            Size block = new Size(bd1.Width, bd1.Height);
            try
            {
                unsafe
                {
                    int startX = 0, startY = 0;
                    while (startX <= block.Width)
                    {
                        while (startY <= block.Height)
                        {
                            int w = startX, h = startY;
                            while (h < bd2.Height && h + block.Height <= bd2.Height)
                            {
                                byte* p1 = (byte*)bd1.Scan0;
                                byte* p2 = (byte*)bd2.Scan0 + h * bd2.Stride;

                                w = startX;
                                while (w < bd2.Width && w + block.Width <= bd2.Width)
                                {
                                    bool result = true;
                                    //按块大小进行扫描
                                    for (int i = 0; i < block.Width; i++)
                                    {
                                        int wi = w + i;
                                        if (wi >= bd2.Width) break;

                                        for (int j = 0; j < block.Height; j++)
                                        {
                                            int hj = h + j;
                                            if (hj >= bd2.Height) break;
                                            ICColor* pc1 = (ICColor*)(p1 + i * 3 + bd1.Stride * j);
                                            ICColor* pc2 = (ICColor*)(p2 + wi * 3 + bd2.Stride * j);

                                            if (pc1->R != pc2->R || pc1->G != pc2->G || pc1->B != pc2->B)
                                            { result = false; break; }
                                        }
                                        if (!result)
                                            break;
                                    }
                                    if (result)
                                    {
                                        rects.Add(new Rectangle(w, h, block.Width, block.Height));
                                    }
                                    E:
                                    w += block.Width;
                                }
                                h += block.Height;
                            }
                            startY++;
                        }
                        startX++;
                        startY = 1;
                    }
                }
            }
            finally
            {
                bmp1.UnlockBits(bd1);
                bmp2.UnlockBits(bd2);
            }

            return rects;
        }
    }
}
