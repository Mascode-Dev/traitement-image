using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace PSI_Jusseaume_Jego
{
    public class MyImage
    {
        public string type;
        public int offsetIm;
        public int hauteur;
        public int largeur;
        public int tailleFichier;
        public int r1;
        public int tailleImage;
        public int r2;
        public int taille_BMPI;
        public int nbP;
        public int formatComp;
        public int bitsPixel;
        public int ResHauteur;
        public int ResLargeur;
        public int nbCouleur;
        public int indexCouleur;
        public Pixel[,] image;


        public MyImage(string myfile)
        {
            if (File.Exists(myfile))
            {
                Console.WriteLine("Fichier trouvé !");
                byte[] tableau1;
                byte[] tableau2;
                byte[] fichier1 = File.ReadAllBytes(myfile);

                //Nous allons parcourir tous les bytes du fichier 

                //Deux premiers caractères
                type = "" + Convert.ToChar(fichier1[0]) + Convert.ToChar(fichier1[1]);

                //Caractere 2 à 5 -> taille du fichier
                tableau1 = new byte[4];
                for (int i = 2; i <= 5; i++)
                {
                    tableau1[i - 2] = fichier1[i];
                }
                tailleFichier = Endian2Int(tableau1);

                //Caractere 6 à 7 -> Premier domaine reservé
                tableau2 = new byte[2];
                for (int i = 6; i <= 7; i++)
                {
                    tableau2[i - 6] = fichier1[i];
                }
                r1 = Endian2Int(tableau2);

                //Caractere 8 à 9 -> Premier domaine reservé
                for (int i = 8; i <= 9; i++)
                {
                    tableau2[i - 8] = fichier1[i];
                }
                r2 = Endian2Int(tableau2);

                // Caractere 10 à 13 -> OffsetImage
                for (int i = 10; i <= 13; i++)
                {
                    tableau1[i - 10] = fichier1[i];
                }
                offsetIm = Endian2Int(tableau1);

                //Caractère 14 à 17 -> Taille de la Zone Bitmap
                for (int i = 14; i <= 17; i++)
                {
                    tableau1[i - 14] = fichier1[i];
                }
                taille_BMPI = Endian2Int(tableau1);

                //Caractère 18 à 21 -> Largeur
                for (int i = 18; i <= 21; i++)
                {
                    tableau1[i - 18] = fichier1[i];
                }
                largeur = Endian2Int(tableau1);

                //Caractere 22 à 25 -> Hauteur
                for (int i = 22; i <= 25; i++)
                {
                    tableau1[i - 22] = fichier1[i];
                }
                hauteur = Endian2Int(tableau1);

                //Caractère 26 à 27 -> Nombre Plan
                tableau2 = new byte[2];
                for (int i = 26; i <= 27; i++)
                {
                    tableau2[i - 26] = fichier1[i];
                }
                nbP = Endian2Int(tableau2);

                //Caractère 28 à 29 -> Bits par pixel
                for (int i = 28; i <= 29; i++)
                {
                    tableau2[i - 28] = fichier1[i];
                }
                bitsPixel = Endian2Int(tableau2);

                // Caractère 30 à 33 -> Format de compression
                for (int i = 30; i <= 33; i++)
                {
                    tableau1[i - 30] = fichier1[i];
                }
                formatComp = Endian2Int(tableau1);

                // Caractère 34 à 37 -> Taille de l'Image
                for (int i = 34; i <= 37; i++)
                {
                    tableau1[i - 34] = fichier1[i];
                }
                tailleImage = Endian2Int(tableau1);

                // Caractère 38 à 41 -> Resolution Largeur
                for (int i = 38; i <= 41; i++)
                {
                    tableau1[i - 38] = fichier1[i];
                }
                ResLargeur = Endian2Int(tableau1);

                // Caractère 38 à 41 -> Resolution Largeur
                for (int i = 42; i <= 45; i++)
                {
                    tableau1[i - 42] = fichier1[i];
                }
                ResHauteur = Endian2Int(tableau1);

                // Caractère 46 à 49 -> Nombre de couleur
                for (int i = 46; i <= 49; i++)
                {
                    tableau1[i - 46] = fichier1[i];
                }
                nbCouleur = Endian2Int(tableau1);

                // Caractère 50 à 53 -> Index des couleurs
                for (int i = 50; i <= 53; i++)
                {
                    tableau1[i - 50] = fichier1[i];
                }
                indexCouleur = Endian2Int(tableau1);

                // Enregistrement des pixels un à un
                image = new Pixel[hauteur, largeur];
                Console.WriteLine("Création de l'image");
                for (int i = 0; i < hauteur; i++)
                {
                    for (int j = 0; j < largeur; j++)
                    {
                        int offset = 54 + (i * largeur + j) * (bitsPixel / 8);

                        byte red = fichier1[offset];
                        byte green = fichier1[offset + 1];
                        byte blue = fichier1[offset + 2];

                        image[i, j] = new Pixel(red, green, blue);
                    }
                }

            }
            else
            {
                type = "BM";
                tailleFichier = 0;
                r1 = 0;
                r2 = 0;
                offsetIm = 54;
                taille_BMPI = 40;
                largeur = 0;
                hauteur = 0;
                nbP = 1;
                bitsPixel = 24;
                formatComp = 0;
                tailleImage = 0;
                ResLargeur = 0;
                ResHauteur = 0;
                nbCouleur = 0;
                indexCouleur = 0;
                image = new Pixel[0, 0];
                Console.WriteLine("Image introuvable");
            }

        }
        public void UpdateHeader()
        {
            // Entête fichier
            tailleFichier = (image.Length * bitsPixel) / 8 + 54;
            r1 = 0;
            r2 = 0;


            // Entête image
            hauteur = image.GetLength(0);
            largeur = image.GetLength(1);
            tailleImage = image.Length * 3;
        }
        public MyImage() // Constructeur vide
        {
            type = "BM";
            tailleFichier = 0;
            r1 = 0;
            r2 = 0;
            offsetIm = 54;
            taille_BMPI = 40;
            largeur = 0;
            hauteur = 0;
            nbP = 1;
            bitsPixel = 24;
            formatComp = 0;
            tailleImage = 0;
            ResLargeur = 0;
            ResHauteur = 0;
            nbCouleur = 0;
            indexCouleur = 0;
            image = new Pixel[0, 0];

        }

        public MyImage(int largeurIm, int hauteurIm)
        {
            type = "BM";
            tailleFichier = 0;
            r1 = 0;
            r2 = 0;
            offsetIm = 54;
            taille_BMPI = 40;
            largeur = largeurIm;
            hauteur = hauteurIm;
            nbP = 1;
            bitsPixel = 24;
            formatComp = 0;
            tailleImage = largeurIm * hauteurIm * 3;
            ResLargeur = 0;
            ResHauteur = 0;
            nbCouleur = 0;
            indexCouleur = 0;
            image = new Pixel[largeurIm, hauteurIm];
            UpdateHeader();
        }

        //Méthode Image -> File
        public void Image2File(string file)
        {
            byte[] tableau = new byte[tailleFichier];
            tableau[0] = (byte)type[0];
            tableau[1] = (byte)type[1];

            // Remplis les 5 premiers élements du tableau.
            for (int i = 2; i <= 5; i++)
            {
                if (i - 2 < Int2Endian(tailleFichier).Count)
                {
                    tableau[i] = Int2Endian(tailleFichier)[i - 2];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Domaine Reservé à 0
            tableau[6] = 0;
            tableau[7] = 0;
            tableau[8] = 0;
            tableau[9] = 0;

            //Offset Image
            for (int i = 10; i <= 13; i++)
            {
                if (i - 10 < Int2Endian(offsetIm).Count)
                {
                    tableau[i] = Int2Endian(offsetIm)[i - 10];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Zone Bitmap
            for (int i = 14; i <= 17; i++)
            {
                if (i - 14 < Int2Endian(taille_BMPI).Count)
                {
                    tableau[i] = Int2Endian(taille_BMPI)[i - 14];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Largeur
            for (int i = 18; i <= 21; i++)
            {
                if (i - 18 < Int2Endian(largeur).Count)
                {
                    tableau[i] = Int2Endian(largeur)[i - 18];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Hauteur
            for (int i = 22; i <= 25; i++)
            {
                if (i - 22 < Int2Endian(hauteur).Count)
                {
                    tableau[i] = Int2Endian(hauteur)[i - 22];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Nombre de plan
            for (int i = 26; i <= 27; i++)
            {
                if (i - 26 < Int2Endian(nbP).Count)
                {
                    tableau[i] = Int2Endian(nbP)[i - 26];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Bit par pixel
            for (int i = 28; i <= 29; i++)
            {
                if (i - 28 < Int2Endian(bitsPixel).Count)
                {
                    tableau[i] = Int2Endian(bitsPixel)[i - 28];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Format de compression
            for (int i = 30; i <= 33; i++)
            {
                if (i - 30 < Int2Endian(formatComp).Count)
                {
                    tableau[i] = Int2Endian(formatComp)[i - 30];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Taille de l'image
            for (int i = 34; i <= 37; i++)
            {
                if (i - 34 < Int2Endian(tailleImage).Count)
                {
                    tableau[i] = Int2Endian(tailleImage)[i - 34];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Resolution largeur
            for (int i = 38; i <= 41; i++)
            {
                if (i - 38 < Int2Endian(ResLargeur).Count)
                {
                    tableau[i] = Int2Endian(ResLargeur)[i - 38];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Resolution hauteur
            for (int i = 42; i <= 45; i++)
            {
                if (i - 42 < Int2Endian(ResHauteur).Count)
                {
                    tableau[i] = Int2Endian(ResHauteur)[i - 42];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            for (int i = 46; i <= 49; i++)
            {
                if (i - 46 < Int2Endian(nbCouleur).Count)
                {
                    tableau[i] = Int2Endian(nbCouleur)[i - 46];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Resolution hauteur
            for (int i = 50; i <= 53; i++)
            {
                if (i - 50 < Int2Endian(indexCouleur).Count)
                {
                    tableau[i] = Int2Endian(indexCouleur)[i - 50];
                }
                else
                {
                    tableau[i] = 0;
                }
            }

            //Donnée de l'image - Parcourir chaque pixel, de ligne en ligne.
            int x = 1;
            int y = 1;
            int index = 0;

            for (int i = 54; x < image.GetLength(0); i++)
            {
                tableau[i] = image[x, y].R;
                i++;
                tableau[i] = image[x, y].G;
                i++;
                tableau[i] = image[x, y].B;
                y++;

                if (y == largeur)
                {
                    y = 0;
                    x++;
                }
                index = i;
            }

            if (File.Exists(file)) //Eviter les erreurs de conflits
            {
                File.Delete(file);
            }
            BinaryWriter write = new BinaryWriter(File.OpenWrite(file));
            write.Write(tableau);
            write.Close();
        }

        public override string ToString() //Mot clé override car ToString() est déjà une méthode native définie
        {
            string ch = "Information du fichier\n";
            ch += $"Type du fichier : {type}\nTaille du fichier : {tailleFichier}\nHauteur : {hauteur}\nLargeur : {largeur}\nOffset : {offsetIm}\nNombre de plan : {nbP}\nTaille Zone Bitmap : {taille_BMPI}\n";
            ch += $"Bit par pixel : {bitsPixel}\nFormat de compression : {formatComp}\nTaille de l'image : {tailleImage}\nRésolution largeur : {ResLargeur}\nRésolution hauteur : {ResHauteur}\n";
            ch += $"Nombre de couleur : {nbCouleur}\nIndex couleur : {indexCouleur}";

            return ch;
        }

        public int Endian2Int(byte[] tableau)
        {
            int res = 0;
            for (int i = 0; i < tableau.Length; i++)
            {
                res = (int)(res + tableau[i] * Math.Pow(256, i)); // Formule pour faire la conversion correctement
            }
            return res;
        }

        public List<byte> Int2Endian(int val)
        {
            int quotient = val;
            List<byte> res = new List<byte>();

            while (quotient != 0)
            {
                //Faire l'opération inverse, donc diviser par 256
                res.Add((byte)(quotient % 256));
                quotient = quotient / 256;
            }
            return res;
        }



        /////////////////////////////////////////////////////////////////////////////////////////
        public void Agrandissement(double coef)
        {
            // Calculer les nouvelles dimensions de l'image
            int newHeight = (int)Math.Round(hauteur * coef);
            int newWidth = (int)Math.Round(largeur * coef);

            // Créer une nouvelle matrice de pixels pour stocker l'image agrandie
            Pixel[,] newImage = new Pixel[newHeight, newWidth];

            // Initialiser chaque pixel de la nouvelle image avec une nouvelle instance de Pixel
            for (int i = 0; i < newImage.GetLength(0); i++)
            {
                for (int j = 0; j < newImage.GetLength(1); j++)
                {
                    newImage[i, j] = new Pixel();
                }
            }

            // Parcourir chaque pixel de la nouvelle image agrandie
            for (int i = 0; i < newHeight; i++)
            {
                for (int j = 0; j < newWidth; j++)
                {
                    // Trouver les 4 coins de l'image originale qui se trouvent autour du pixel agrandi
                    int X1 = (int)Math.Floor(i / coef);
                    int X2 = (int)Math.Ceiling(i / coef);
                    int Y1 = (int)Math.Floor(j / coef);
                    int Y2 = (int)Math.Ceiling(j / coef);

                    // Vérifier si les indices de ligne et de colonne dépassent les dimensions de l'image originale
                    if (X2 >= hauteur) X2 = hauteur - 1;
                    if (Y2 >= largeur) Y2 = largeur - 1;

                    // Récupérer les couleurs des 4 coins de l'image originale
                    Pixel couleur1 = image[X1, Y1];
                    Pixel couleur2 = image[X2, Y1];
                    Pixel couleur3 = image[X1, Y2];
                    Pixel couleur4 = image[X2, Y2];

                    // Calculer la nouvelle couleur pour le pixel agrandi
                    byte red = (byte)Math.Round((couleur1.R + couleur2.R + couleur3.R + couleur4.R) / 4.0);
                    byte green = (byte)Math.Round((couleur1.G + couleur2.G + couleur3.G + couleur4.G) / 4.0);
                    byte blue = (byte)Math.Round((couleur1.B + couleur2.B + couleur3.B + couleur4.B) / 4.0);

                    // Assigner la nouvelle couleur au pixel agrandi
                    newImage[i, j] = new Pixel(red, green, blue);
                }
            }

            // Assigner la nouvelle image à l'image originale
            image = newImage;

            // Mettre à jour les dimensions de l'image et redessiner l'image
            UpdateHeader();
        }

        public void NoirEtBlanc()
        {
            foreach (Pixel p in image)
            {
                p.NoirEtBlanc();
            }
        }

        public void Luminosite(byte value,bool assombrir)
        {
            foreach (Pixel p in image)
            {
                Pixel.Luminosite(p,value,assombrir);
            }
        }
        public void BlackAndWhite() //Appliquer B&W à un ensemble de pixel
        {
            foreach (Pixel p in image)
            {
                Pixel.BlackAndWhite(p);
            }
        }
        public void BAW()
        {
            foreach(Pixel p in image)
            {
                Pixel.BAW(p);
            }
        }
        public void NuanceGris()
        {
            for (int i = 0; i < image.GetLength(0); i++)
            {
                for (int j = 0; j < image.GetLength(1); j++)
                {
                    Pixel.BlackAndWhite(image[i, j]); //Application de B&W à chaque pixel de l'image
                }
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////
        
        public void Rotation(int angle)
        {
            while(angle >= 90)
            {
                Pixel[,] rotated = new Pixel[largeur, hauteur];
                for(int i = 0; i < largeur; i++)
                {
                    for(int j = 0; j < hauteur; j++)
                    {
                        rotated[i, j] = image[j, largeur - 1 - i];
                    }
                }

                image = rotated;
                UpdateHeader();
                angle -= 90;
            }

            while(angle < 0)
            {
                Pixel[,] rotated = new Pixel[largeur, hauteur];
                for(int i = 0; i < largeur; i++)
                {
                    for(int j = 0; j < hauteur; j++)
                    {
                        rotated[i, j] = image[hauteur - j - 1,i];
                    }
                }
                image = rotated;
                UpdateHeader();
                angle += 90;
            }

            while(angle > 0 && angle < 90)
            {
                double radians = angle * Math.PI / 180;
                double cos = Math.Abs(Math.Cos(radians));
                double sin = Math.Abs(Math.Sin(radians));
                int largeur2 = (int)Math.Abs(Math.Round(largeur * cos + hauteur * sin));
                int hauteur2 = (int)Math.Abs(Math.Round(hauteur * cos + largeur * sin));

                Pixel[,] rotated = new Pixel[largeur2, hauteur2];

                for(int i = 0; i < rotated.GetLength(0); i++)
                {
                    for(int j = 0; j < rotated.GetLength(1); j++)
                    {
                        rotated[i, j] = new Pixel();
                    }
                }

                //Centre de l'image d'origine
                double C_X = hauteur / 2.0;
                double C_Y = largeur / 2.0;
                double C_X2 = hauteur2 / 2.0;
                double C_Y2 = largeur2 / 2.0;

                for(int i = 0; i < largeur2; i++)
                {
                    for(int j  = 0; j < hauteur2; j++)
                    {
                        double O_X = (double)(cos * (j - C_X2) + sin * (i - C_Y2)) + C_X;
                        double O_Y = (double)(-sin * (j - C_X2) + cos * (i - C_Y2)) + C_Y;

                        if(O_X >= 0 && O_X <= hauteur - 1 && O_Y >= 0 && O_Y <= largeur - 1)
                        {
                            int OX1 = (int)Math.Floor(O_X);
                            int OX2 = (int)Math.Ceiling(O_X);
                            int OY1 = (int)Math.Floor(O_Y);
                            int OY2 = (int)Math.Ceiling(O_Y);

                            Pixel c1 = image[OX1, OY1];
                            Pixel c2 = image[OX2, OY1];
                            Pixel c3 = image[OX1, OY2];
                            Pixel c4 = image[OX2, OY2];

                            byte red = (byte)Math.Round((c1.R + c2.R + c3.R + c4.R) / 4.0);
                            byte green = (byte)Math.Round((c1.G + c2.G + c3.G + c4.G) / 4.0);
                            byte blue = (byte)Math.Round((c1.B + c2.B + c3.B + c4.B) / 4.0);

                            rotated[j, i] = new Pixel(red, green, blue);
                        }
                    }
                }
                image = rotated;
                UpdateHeader();
            }
        }
        public void BaseConvolution(int G0, int G1, Pixel[,] matrice1)
        {
            for (int i = 0; i < matrice1.GetLength(0); i++)
            {
                for (int j = 0; j < matrice1.GetLength(1); j++)
                {
                    matrice1[i, j] = new Pixel();
                }
            }

            //Agrandissement des bords
            int k = 0;
            int h = matrice1.GetLength(1) - 1; //Hauteur
            for (int i = 1; i < matrice1.GetLength(0) - 1; i++)
            {
                matrice1[i, k] = image[i - 1, k + 1];
                matrice1[i, h] = image[i - 1, h - 2];
            }

            h = matrice1.GetLength(0) - 1; //Longueur
            for (int i = 1; i < matrice1.GetLength(1) - 1; i++)
            {
                matrice1[k, i] = image[k + 1, i - 1];
                matrice1[k, i] = image[h - 2, i - 1];
            }

            //Agrandissement des coins
            matrice1[0, 0] = image[0, 0];
            matrice1[matrice1.GetLength(0) - 1, 0] = image[G0 - 1, 0];
            matrice1[0, matrice1.GetLength(1) - 1] = image[0, G1 - 1];
            matrice1[matrice1.GetLength(0) - 1, matrice1.GetLength(1) - 1] = image[G0 - 1, G1 - 1];

            //On remplit le tout avec l'image
            for (int i = 0; i < G0; i++)
            {
                for (int j = 0; j < G1; j++)
                {
                    matrice1[i + 1, j + 1] = image[i, j];
                }
            }
        }


        //Méthode de Kernel pour la detection de contours
        public void DetectionContours()
        {
            int G0 = image.GetLength(0);
            int G1 = image.GetLength(1);
            Pixel[,] matrice1 = new Pixel[G0 + 2, G1 + 2];
            BaseConvolution(G0, G1, matrice1);

            //Deux matrices donc deux calculs de gradients
            //Calcul du gradient horizontale
            Pixel[,] GradY = new Pixel[G0, G1];
            for(int i = 0; i < GradY.GetLength(0); i++)
            {
                for(int j = 0; j < GradY.GetLength(1); j++)
                {
                    byte r = (byte)Math.Abs(matrice1[i, j].R + 2 * matrice1[i, j + 1].R + matrice1[i, j + 1].R - matrice1[i + 2, j].R - 2 * matrice1[i + 2, j + 1].R - matrice1[i + 2, j + 2].R);
                    byte g = (byte)Math.Abs(matrice1[i, j].G + 2 * matrice1[i, j + 1].G + matrice1[i, j + 1].G - matrice1[i + 2, j].G - 2 * matrice1[i + 2, j + 1].G - matrice1[i + 2, j + 2].G);
                    byte b = (byte)Math.Abs(matrice1[i, j].B + 2 * matrice1[i, j + 1].B + matrice1[i, j + 1].B - matrice1[i + 2, j].B - 2 * matrice1[i + 2, j + 1].B - matrice1[i + 2, j + 2].B);
                    GradY[i, j] = new Pixel(r, g, b);
                }
            }

            //Calcul du gradient verticale
            Pixel[,] GradX = new Pixel[G0, G1];
            for (int i = 0; i < GradX.GetLength(0); i++)
            {
                for (int j = 0; j < GradX.GetLength(1); j++)
                {
                    byte r = (byte)Math.Abs(matrice1[i, j].R + 2 * matrice1[i+1, j].R + matrice1[i+2, j].R - matrice1[i, j+2].R - 2 * matrice1[i + 1, j + 2].R - matrice1[i + 2, j + 2].R);
                    byte g = (byte)Math.Abs(matrice1[i, j].G + 2 * matrice1[i+1, j].G + matrice1[i+2, j].G - matrice1[i, j+2].G - 2 * matrice1[i + 1, j + 2].G - matrice1[i + 2, j + 2].G);
                    byte b = (byte)Math.Abs(matrice1[i, j].B + 2 * matrice1[i+1, j].B + matrice1[i+2, j].B - matrice1[i, j+2].B - 2 * matrice1[i + 1, j + 2].B - matrice1[i + 2, j + 2].B);
                    GradX[i, j] = new Pixel(r, g, b);
                }
            }

            //Calcul de la magnitude du gradient
            Pixel[,] matrice2 = new Pixel[G0, G1];
            for(int i = 0; i < G0; i++)
            {
                for(int j = 0; j < G1; j++)
                {
                    byte r = (byte)Math.Round(Math.Sqrt(GradX[i, j].R * GradX[i, j].R + GradY[i, j].R * GradY[i, j].R));
                    byte g = (byte)Math.Round(Math.Sqrt(GradX[i, j].G * GradX[i, j].G + GradY[i, j].G * GradY[i, j].G));
                    byte b = (byte)Math.Round(Math.Sqrt(GradX[i, j].B * GradX[i, j].B + GradY[i, j].B * GradY[i, j].B));

                    matrice2[i, j] = new Pixel(r, g, b);
                }
            }
            image = matrice2;
            NuanceGris();
            UpdateHeader();
        }


        public void Renforcement()
        {
            int G0 = image.GetLength(0);
            int G1 = image.GetLength(1);
            Pixel[,] matrice1 = new Pixel[G0 + 2, G1 + 2];
            BaseConvolution(G0, G1, matrice1);

            Pixel[,] mat = new Pixel[G0, G1];
            for(int i = 0; i < mat.GetLength(0); i++)
            {
                for(int j = 0; j < mat.GetLength(1); j++)
                {
                    // Calcul pour chaques couleurs entre la droite et la gauche
                    byte r = (byte)Math.Abs(matrice1[i + 1, j + 1].R - matrice1[i + 1, j].R);
                    byte g = (byte)Math.Abs(matrice1[i + 1, j + 1].G - matrice1[i + 1, j].G);
                    byte b = (byte)Math.Abs(matrice1[i + 1, j + 1].B - matrice1[i + 1, j].B);

                    mat[i, j] = new Pixel(r, g, b);
                }
            }
            image = mat;
            NuanceGris();
            UpdateHeader();

        }

        

        public void Flou()
        {
            int G0 = image.GetLength(0);
            int G1 = image.GetLength(1);
            Pixel[,] matrice1 = new Pixel[G0 + 2, G1 + 2];
            BaseConvolution(G0, G1, matrice1);

            Pixel[,] Mat = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    byte r = (byte)Math.Abs(Math.Round((matrice1[i, j].R + matrice1[i + 2, j].R + matrice1[i, j + 2].R + matrice1[i + 2, j + 2].R + 2 * (matrice1[i + 1, j].R + matrice1[i, j + 1].R + matrice1[i + 2, j + 1].R + matrice1[i + 1, j + 2].R) + 4 * matrice1[i + 1, j + 1].R) / 16.0));
                    byte g = (byte)Math.Abs(Math.Round((matrice1[i, j].G + matrice1[i + 2, j].G + matrice1[i, j + 2].G + matrice1[i + 2, j + 2].G + 2 * (matrice1[i + 1, j].G + matrice1[i, j + 1].G + matrice1[i + 2, j + 1].G + matrice1[i + 1, j + 2].G) + 4 * matrice1[i + 1, j + 1].G) / 16.0));
                    byte b = (byte)Math.Abs(Math.Round((matrice1[i, j].B + matrice1[i + 2, j].B + matrice1[i, j + 2].B + matrice1[i + 2, j + 2].B + 2 * (matrice1[i + 1, j].B + matrice1[i, j + 1].B + matrice1[i + 2, j + 1].B + matrice1[i + 1, j + 2].B) + 4 * matrice1[i + 1, j + 1].B) / 16.0));

                    Mat[i, j] = new Pixel(r, g, b);
                }
            }
            image = Mat;
            UpdateHeader();
        }

        public void Repoussage()
        {
            int G0 = image.GetLength(0);
            int G1 = image.GetLength(1);
            Pixel[,] matrice1 = new Pixel[G0 + 2, G1 + 2];
            BaseConvolution(G0, G1, matrice1);

            Pixel[,] Mat = new Pixel[image.GetLength(0), image.GetLength(1)];
            for (int i = 0; i < Mat.GetLength(0); i++)
            {
                for (int j = 0; j < Mat.GetLength(1); j++)
                {
                    byte r = (byte)Math.Abs(0 * matrice1[i, j].R - 2 * matrice1[i + 2, j].R + 2 * matrice1[i, j + 2].R + 0 * matrice1[i + 2, j + 2].R - 1 * matrice1[i + 1, j].R + 1 * matrice1[i, j + 1].R - 1 * matrice1[i + 2, j + 1].R + 1 * matrice1[i + 1, j + 2].R + 1 * matrice1[i + 1, j + 1].R);
                    byte g = (byte)Math.Abs(0 * matrice1[i, j].G - 2 * matrice1[i + 2, j].G + 2 * matrice1[i, j + 2].G + 0 * matrice1[i + 2, j + 2].G - 1 * matrice1[i + 1, j].G + 1 * matrice1[i, j + 1].G - 1 * matrice1[i + 2, j + 1].G + 1 * matrice1[i + 1, j + 2].G + 1 * matrice1[i + 1, j + 1].G);
                    byte b = (byte)Math.Abs(0 * matrice1[i, j].B - 2 * matrice1[i + 2, j].B + 2 * matrice1[i, j + 2].B + 0 * matrice1[i + 2, j + 2].B - 1 * matrice1[i + 1, j].B + 1 * matrice1[i, j + 1].B - 1 * matrice1[i + 2, j + 1].B + 1 * matrice1[i + 1, j + 2].B + 1 * matrice1[i + 1, j + 1].B);

                    Mat[i, j] = new Pixel(r, g, b);
                }
            }
            image = Mat;
            UpdateHeader();
        }

        public void Fractale2(int maxIterations)
        {
            double zoom = 1;
            double moveX = -0.5;
            double moveY = 0;
            double zx, zy, cx, cy, tmp;
            int i;
            double colorDepth = 255.0 / maxIterations;

            for (int x = 0; x < largeur; x++)
            {
                for (int y = 0; y < hauteur; y++)
                {
                    zx = 1.5 * (x - largeur / 2) / (0.5 * zoom * largeur) + moveX;
                    zy = (y - hauteur / 2) / (0.5 * zoom * hauteur) + moveY;
                    cx = zx;
                    cy = zy;
                    i = maxIterations;

                    while (zx * zx + zy * zy < 4 && i > 0)
                    {
                        tmp = zx * zx - zy * zy + cx;
                        zy = 2.0 * zx * zy + cy;
                        zx = tmp;
                        i--;
                    }

                    int pixelIndex = y * largeur + x;
                    if (i > 0)
                    {
                        double c = i * colorDepth;
                        image[y, x] = new Pixel((byte)c, (byte)c, (byte)c);
                    }
                    else
                    {
                        image[y, x] = new Pixel(0, 0, 0);
                    }
                }
            }
            UpdateHeader();
        }

        public void Fractale(int largeur, int hauteur, int IterMax, double xMin, double xMax, double yMin, double yMax)
        {
            Pixel[,] fracMat = new Pixel[largeur, hauteur];
            for(int i = 0; i < fracMat.GetLength(0); i++)
            {
                for(int j = 0; j < fracMat.GetLength(1); j++)
                {
                    double x = xMin + (xMax - xMin) * i / (largeur - 1.0);
                    double y = yMin + (yMax - yMin) * j / (hauteur - 1.0);

                    int iter = 0;
                    double zr = 0.0;
                    double zi = 0.0;

                    while(iter < IterMax && zr * zr * zi < 4.0)
                    {
                        double newZr = zr * zr - zi * zi + x;
                        double newZi = 2.0 * zr * zi + y;

                        zr = newZr;
                        zi = newZi;

                        iter++;
                    }

                    if(iter == IterMax)
                    {
                        fracMat[i, j] = new Pixel(0, 0, 0);
                    }
                    else
                    {
                        fracMat[i, j] = new Pixel(iter % 32 * 8, iter % 16 * 16, iter % 8 * 32);
                    }
                }
            }

            image = fracMat;
            UpdateHeader();
        }


        public void Codage(MyImage img)
        {
            //Remettre à l'échelle
            while(img.largeur > largeur)
            {
                double coef = img.largeur / largeur;
                Agrandissement(coef);
            }

            //Remise à l'échelle
            while (img.hauteur > hauteur)
            {
                double coef = img.hauteur / hauteur;
                Agrandissement(coef);
            }

            for (int i = 0; i < img.hauteur; i++)
            {
                for (int j = 0; j < img.largeur; j++)
                {
                    Pixel pixel = img.image[i, j];

                    byte r = img.image[i, j].R;
                    byte g = img.image[i, j].G;
                    byte b = img.image[i, j].B;

                    //Combiner les 4 bits de poids fort des composante de l'image avec img
                    //0cF0 pour enlever les 4 bits de poids faible
                    byte R2 = (byte)((this.image[i, j].R & 0xF0) | (r >> 4));
                    byte G2 = (byte)((this.image[i, j].G & 0xF0) | (g >> 4));
                    byte B2 = (byte)((this.image[i, j].B & 0xF0) | (b >> 4));

                    Pixel newPx = new Pixel(R2, G2, B2);


                    this.image[i, j] = newPx;
                }
            }

        }

        public MyImage Decode()
        {
            // Création d'une nouvelle instance de MyImage pour stocker l'image décodée
            MyImage imgD = new MyImage(hauteur, largeur);

            // Boucle à travers toutes les colonnes et les lignes de l'image actuelle
            for (int i = 0; i < largeur; i++)
            {
                for (int j = 0; j < hauteur; j++)
                {
                    Pixel PX = this.image[j, i];

                    // On enleve les 4 bits de poids fort - On décale de 4
                    byte RD = (byte)(PX.R << 4);
                    byte GD = (byte)(PX.G << 4);
                    byte BD = (byte)(PX.B << 4);

                    Pixel PX_D = new Pixel(RD, GD, BD);

                    imgD.image[j, i] = PX_D;
                }
            }

            imgD.UpdateHeader();

            return imgD;
        }

        public void miroir()
        {
            Pixel[,] imageMiroir = new Pixel[hauteur, largeur * 2];

            for (int ligne = 0; ligne < hauteur; ligne++)
            {
                for (int colonne = 0; colonne < largeur; colonne++)
                {
                    imageMiroir[ligne, colonne] = image[ligne, colonne];
                    imageMiroir[ligne, imageMiroir.GetLength(1) - 1 - colonne] = image[ligne, colonne];
                }
            }
            image = imageMiroir;
            UpdateHeader();
        }
    }


}