using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace BaWuClub.Web.Common
{
    public class ImageTools
    {
        public static void CreateThumb(string name,string path,string destPath, int destHeight, int destWidth){
            System.Drawing.Image imgSource = Image.FromFile(path+name) ;
            System.Drawing.Imaging.ImageFormat thisFormat = imgSource.RawFormat;
            int sW = 0, sH = 0;
            int sWidth = imgSource.Width;
            int sHeight = imgSource.Height;
            if (sHeight > destHeight || sWidth > destWidth){
                if ((sWidth * destHeight) > (sHeight * destWidth)){
                    sW = destWidth;
                    sH = (destWidth * sHeight) / sWidth;
                }
                else{
                    sH = destHeight;
                    sW = (sWidth * destHeight) / sHeight;
                }
            }
            else{
                sW = sWidth;
                sH = sHeight;
            }
            Bitmap outBmp = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(outBmp);
            g.Clear(Color.Transparent);
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(imgSource, new Rectangle((destWidth - sW) / 2, (destHeight - sH) / 2, sW, sH), 0, 0, imgSource.Width, imgSource.Height, GraphicsUnit.Pixel);
            EncoderParameters encoderParams = new EncoderParameters();
            long[] quality = new long[1];
            quality[0] = 100;
            EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            encoderParams.Param[0] = encoderParam;
            DirectoryInfo dir = new DirectoryInfo(destPath);
            if (!dir.Exists)
                dir.Create();
            outBmp.Save(dir + name);
            g.Dispose();
            imgSource.Dispose();
        }
    }
}
