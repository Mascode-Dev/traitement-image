using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Diagnostics;

namespace PSI_Jusseaume_Jego
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Bienvenue dans notre projet : Problème Scientifique et Informtique\n");
                Console.WriteLine("Charger une image de votre choix (n'oubliez pas l'extension .bmp) :");
                string ch = Console.ReadLine();
                if (File.Exists(ch))
                {
                    while (true)
                    {
                        Console.Clear();
                        MyImage image1 = new MyImage(ch);
                        Console.WriteLine("Voici le menu !\n");
                        Console.WriteLine("0. Changer d'image");
                        Console.WriteLine("1. Appliquer une transformation de base (Nuance de gris/Noir et Blanc)");
                        Console.WriteLine("2. Appliquer une transformation (Agrandissement/Retrecissement/Rotation)");
                        Console.WriteLine("3. Appliquer un filre de convolution");
                        Console.WriteLine("4. Codage et décodage d'une image dans une autre & Fractale\n");
                        Console.WriteLine("5. Afficher les informations de l'image");
                        Console.WriteLine("6. Innovation (Mirroir & Luminosité)");

                        ConsoleKeyInfo entry = Console.ReadKey();
                        if(entry.KeyChar == '0')
                        {
                            Console.Clear();
                            break;
                        }
                        if(entry.KeyChar == '1')
                        {
                            Console.Clear();
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Voici le menu !\n");
                                Console.WriteLine("0. Menu précedent");
                                Console.WriteLine("1. Nuance de gris");
                                Console.WriteLine("2. Noir et Blanc");

                                entry = Console.ReadKey();
                                if(entry.KeyChar == '0')
                                {
                                    Console.Clear();
                                    break;
                                }
                                if(entry.KeyChar == '1')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi les nuances de gris");
                                    image1.NuanceGris();
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                                if (entry.KeyChar == '2')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi le noir et blanc");
                                    image1.BAW();
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }

                            }
                        }
                        if(entry.KeyChar == '2')
                        {
                            Console.Clear();
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Voici le menu !\n");
                                Console.WriteLine("0. Menu précedent");
                                Console.WriteLine("1. Aggrandissement");
                                Console.WriteLine("2. Rotation");

                                entry = Console.ReadKey();
                                if (entry.KeyChar == '0')
                                {
                                    Console.Clear();
                                    break;
                                }
                                if (entry.KeyChar == '1')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi l'agrandissement");
                                    Console.WriteLine("Veuillez choisir votre coefficient : \n");
                                    double coef = Convert.ToDouble(Console.ReadLine());
                                    while(coef <= 0)
                                    {
                                        Console.WriteLine("Vous avez choisi un coef négatif ou nul, veuillez en mettre un valide : ");
                                        coef = Convert.ToDouble(Console.ReadLine());
                                    }
                                    image1.Agrandissement(coef);
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                                if (entry.KeyChar == '2')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi la rotation");
                                    Console.WriteLine("Veuillez choisir votre angle : \n");
                                    int coef = int.Parse(Console.ReadLine());
                                    while (coef <= 0)
                                    {
                                        Console.WriteLine("Vous avez choisi un coef négatif ou nul, veuillez en mettre un valide : ");
                                        coef = int.Parse(Console.ReadLine());
                                    }
                                    image1.Rotation(coef);
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                            }
                        }
                        if(entry.KeyChar == '3')
                        {
                            Console.Clear();
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Voici le menu !\n");
                                Console.WriteLine("0. Menu précedent");
                                Console.WriteLine("1. Detection des bords");
                                Console.WriteLine("2. Renforcement");
                                Console.WriteLine("3. Repoussage");
                                Console.WriteLine("4. Flou");


                                entry = Console.ReadKey();
                                if (entry.KeyChar == '0')
                                {
                                    Console.Clear();
                                    break;
                                }
                                if (entry.KeyChar == '1')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi la detection de bords");

                                    image1.DetectionContours();
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                                if (entry.KeyChar == '2')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi le renforcement");

                                    image1.Renforcement();
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                                if (entry.KeyChar == '3')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi le repoussage");

                                    image1.Repoussage();
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                                if (entry.KeyChar == '4')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi le flou");

                                    image1.Flou();
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }

                            }
                        }
                        if (entry.KeyChar == '4')
                        {
                            Console.Clear();
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Voici le menu !\n");
                                Console.WriteLine("0. Menu précedent");
                                Console.WriteLine("1. Codage");
                                Console.WriteLine("2. Decodage");
                                Console.WriteLine("3. Fractale");

                                entry = Console.ReadKey();
                                if (entry.KeyChar == '0')
                                {
                                    Console.Clear();
                                    break;
                                }
                                if (entry.KeyChar == '1')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi le codage");
                                    Console.WriteLine("Veuillez taper le nom d'une premiere image (sans oublier l'extension .bmp) :");
                                    string ch2 = Console.ReadLine();
                                    while (!File.Exists(ch2))
                                    {
                                        Console.WriteLine("Le fichier est inexistant");
                                        Console.WriteLine("Veuillez taper le nom d'une premiere image (sans oublier l'extension .bmp) :");
                                        ch2 = Console.ReadLine();
                                    }
                                    MyImage image_codage_1 = new MyImage(ch2);
                                    Console.WriteLine("Veuillez taper le nom d'une deuxième image de même taille (sans oublier l'extension .bmp) :");
                                    string ch3 = Console.ReadLine();
                                    while (!File.Exists(ch3))
                                    {
                                        
                                        Console.WriteLine("Le fichier est inexistant");
                                        Console.WriteLine("Veuillez taper le nom d'une premiere image (sans oublier l'extension .bmp) :");
                                        ch3 = Console.ReadLine();
                                    }
                                    MyImage image_codage_2 = new MyImage(ch3);
                                    image_codage_2.Codage(image_codage_1);
                                    image_codage_2.Image2File("Resultat.bmp");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                    
                                    image_codage_1.Codage(image_codage_2);
                                    image_codage_1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                                if (entry.KeyChar == '2')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi le decodage");
                                    Console.WriteLine("Veuillez taper le nom d'une premiere image (sans oublier l'extension .bmp) :");
                                    string ch4 = Console.ReadLine();
                                    while (!File.Exists(ch4))
                                    {
                                        Console.WriteLine("Le fichier est inexistant");
                                        Console.WriteLine("Veuillez taper le nom d'une premiere image (sans oublier l'extension .bmp) :");
                                        ch4 = Console.ReadLine();
                                    }
                                    MyImage image_decodage_1 = new MyImage(ch4);
                                    MyImage newIm = image_decodage_1.Decode();
                                    newIm.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                                if (entry.KeyChar == '3')
                                {
                                    Console.Clear();
                                    while (true)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Vous avez choisi la fractale");
                                        Console.WriteLine("Veuillez entrer le nombre maximal d'iteration :");
                                        int iterMax = Convert.ToInt32(Console.ReadLine());

                                        image1.Fractale2(iterMax);
                                        image1.Image2File("Resultat.bmp");

                                        Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                        while (entry.KeyChar != '0')
                                        {
                                            Console.Clear();
                                            Console.WriteLine("Appuyez sur 0 pour continuer");
                                            entry = Console.ReadKey();
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        if(entry.KeyChar == '5')
                        {
                            Console.Clear();
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine(image1.ToString());
                                Console.WriteLine("Appuyez sur 0 pour continuer");
                                entry = Console.ReadKey();
                                while (entry.KeyChar != '0')
                                {
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                }
                                break;
                            }
                        }
                        if (entry.KeyChar == '6')
                        {
                            Console.Clear();
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Voici le menu !\n");
                                Console.WriteLine("0. Menu précedent");
                                Console.WriteLine("1. Miroir");
                                Console.WriteLine("2. Luminosité");

                                entry = Console.ReadKey();
                                if (entry.KeyChar == '0')
                                {
                                    Console.Clear();
                                    break;
                                }
                                if (entry.KeyChar == '1')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi l'innovation : miroir");
                                    image1.miroir();
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }
                                if (entry.KeyChar == '2')
                                {
                                    Console.Clear();
                                    Console.WriteLine("Vous avez choisi l'innovation : luminosité");
                                    Console.WriteLine("Voulez vous: \n1) Eclaircir \n2) Assombrir");
                                    int value = Convert.ToInt32(Console.ReadLine());
                                    while (value!=1 && value!=2)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Choississez une valeur correcte");
                                        Console.WriteLine("Voulez vous: \n1) Eclaircir \n2) Assombrir");
                                        value = Convert.ToInt32(Console.ReadLine());
                                    }
                                    bool assombrir = true;
                                    if(value==1)
                                    {
                                        assombrir = false;
                                    }
                                    Console.WriteLine("Entrez une valeur entre 0 et 255");
                                    byte lumi = Convert.ToByte(Console.ReadLine());
                                    image1.Luminosite(lumi,assombrir);
                                    image1.Image2File("Resultat.bmp");
                                    Console.WriteLine("Allez voir dans bin/Debug/net6.0 -> Resultat.bmp\n");
                                    Console.WriteLine("Appuyez sur 0 pour continuer");
                                    entry = Console.ReadKey();
                                    while (entry.KeyChar != '0')
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Appuyez sur 0 pour continuer");
                                        entry = Console.ReadKey();
                                    }
                                    break;
                                }

                            }
                        }
                    }

                }
            }
            string file = "image1.bmp";
            MyImage tst = new MyImage(file);
            MyImage img = new MyImage("image2.bmp");
            MyImage im3 = new MyImage("image3.bmp");
            MyImage im5 = im3.Decode();
            //tst.Fractale(200,200,80,20,100,20,100);

            im5.Image2File("image4.bmp");
        }

    }
}