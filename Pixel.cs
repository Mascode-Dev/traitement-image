using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSI_Jusseaume_Jego
{
    public class Pixel
    {
        public byte R;
        public byte G;
        public byte B;

        public Pixel(byte r, byte g, byte b) // Constructeur BYTE
        {
            if (r > 255)
            {
                r = 255;
            }

            if (g > 255)
            {
                g = 255;
            }

            if (b > 255)
            {
                b = 255;
            }
            R = r; G = g; B = b;
        }

        public Pixel(int r, int g, int b) // Si les valeurs sont INT -> BYTE
        {
            if (r > 255) r = 255;
            if (g > 255) g = 255;
            if (b > 255) b = 255;
            R = Convert.ToByte(r);
            B = Convert.ToByte(b);
            G = Convert.ToByte(g);
        }

        public Pixel() //Pas de paramètre -> Mettre à 0
        {
            R = (byte)0;
            G = (byte)0;
            B = (byte)0;
        }

        public void NoirEtBlanc()
        {
            double moyenne = (R + G + B) / 3.0;
            R = (byte)Math.Round(moyenne);
            G = (byte)Math.Round(moyenne);
            B = (byte)Math.Round(moyenne);
        }

        public static void BlackAndWhite(Pixel p)
        {
            byte avg = (byte)((p.R + p.G + p.B) / 3.0);
            p.R = avg;
            p.G = avg;
            p.B = avg;
        }
        public static void Luminosite(Pixel p, byte value,bool assombrir)
        {
            if (!assombrir)
            {
                if (p.R + value > 255)
                {
                    byte avg = 255;
                    p.R = avg;
                }
                else
                {
                    p.R = Convert.ToByte(p.R + value);
                }

                if (p.B + value > 255)
                {
                    byte avg = 255;
                    p.B = avg;
                }
                else
                {
                    p.B = Convert.ToByte(p.B + value);
                }

                if (p.G + value > 255)
                {
                    byte avg = 255;
                    p.G = avg;
                }
                else
                {
                    p.G = Convert.ToByte(p.G + value);
                }
            }

            if (assombrir==true)
            {
                if (p.R - value<0)
                {
                    byte avg = 0;
                    p.R = avg;
                }
                else
                {
                    p.R = Convert.ToByte(p.R - value);
                }

                if (p.B - value < 0)
                {
                    byte avg = 0;
                    p.B = avg;
                }
                else
                {
                    p.B = Convert.ToByte(p.B - value);
                }

                if (p.G - value < 0)
                {
                    byte avg = 0;
                    p.G = avg;
                }
                else
                {
                    p.G = Convert.ToByte(p.G - value);
                }
            }
        }

        public static void BAW(Pixel p)
        {
            byte avg = (byte)((p.R + p.G + p.B) / 3.0);
            
            if (avg < 128)
            {
                avg = 0;
            }
            else
            {
                avg = 255;
            }

            p.R = avg;
            p.G = avg;
            p.B = avg;
        }
        
        public override string ToString() // Affichage des couleurs | Conditions si jamais <10, <100
        {
            string ch = "" + R;
            if (R < 10) ch += "0";
            if (R < 100) ch += "0";
            ch += "|" + G;
            if (G < 10) ch += "0";
            if (G < 100) ch += "0";
            ch += "|" + B;
            if (B < 10) ch += "0";
            if (B < 100) ch += "0";
            return ch;
        }



    }
}
