using System;
using System.Collections.Generic;
using System.Text;

namespace Beta_elliptic_model
{
    /*
             La fonction extract_pseudo_mot permet de delimiter les données (x,y,p) de chaque pseudo-mot dans la matrice de données (a) représentant les inscriptions successives des pseudo-mots                  
             ou segments d'une même phrase ou proposition saisie sur la tablette. Cette delimitation est effectuée en transformant (dévisant) la matrice 2Dim (a) (a=[(x,y,p)*nbr point])   
             en une matrice de 3Dim (matpseudomots) (matpseudomots=[ [(x,y,p)*nbr point/pseudo_mot]*nbr de pseudo_mot]
             */
    public class VS_extract_pseudo_mot_p
    {

        public int indicateur_premier_passage, indicateur_atente_debut_pseudo_mot, k, j, size_a, nbr_pseudomots, taille, indice_pseudo_word, idince_dans_pseudo_word, nombre_pseudo;
        public double[,] a2;
        public double[] vect;
        public int[,] taille_pseudomot, taillepmot;
        public double[][,] matpseudomots;

        public double[][,] Method_extract_pseudo_mot_p(double[,] a, int longueur_nbr_points_min)
        {
            nombre_pseudo = Method_nbr_pseudomots(a, longueur_nbr_points_min);
            taillepmot = Method_extract_taille_pseudo_mot_p(a, longueur_nbr_points_min);


            matpseudomots = new double[nombre_pseudo][,];
            // creation matrice pseudo mot
            for (int i = 0; i < nombre_pseudo; i++)
            {
                matpseudomots[i] = new double[taillepmot[i, 1], 2];
            }

            ////matpseudomots = new double[a.GetLength(0), a.GetLength(1), 2]; ??
            vect = new double[a.Rank];
            taille = a.Length / a.Rank + 1;
            for (int i = a.Length / a.Rank - 1; i >= 0; i--)
            {
                if ((a[i, 0] == 0) && (a[i, 1] == 0))
                {
                    taille -= 1;
                    a2 = new double[taille, a.Rank];
                    for (int l = 0; l < taille; l++)
                    {
                        a2[l, 0] = a[l, 0];
                        a2[l, 1] = a[l, 1];

                    }
                    //a[i, 1] = 0; a[i, 2] = 0;        // a(i,:) = [];
                    //a[i, 1] = []; a[i, 2] = [];
                    if (i > 1)
                    {
                        if ((a[i - 1, 0]) != 0 || (a[i - 1, 1] != 0))

                            break;
                    }


                }
                else
                    break;
            }

            indicateur_premier_passage = 0;
            indicateur_atente_debut_pseudo_mot = 1;
            k = 0;  // indice du pseudo_mot courant
            j = 0; // 0 indice dans la nouvelle matrice 'matpseudomots'
            //indice_pseudo_word = 1;
            //idince_dans_pseudo_word = 1;
            size_a = a2.Length / a2.Rank;

            if (size_a > 1)
            {
                for (int i = 0; i < size_a - 1; i++) //
                {
                    if ((a2[i, 0] != 0) || (a2[i, 1] != 0))
                    {

                        matpseudomots[k][j, 0] = a2[i, 0];
                        matpseudomots[k][j, 1] = a2[i, 1];

                        //matpseudomots[k, j,2] = a2[i, 1]; a verifier

                        if (i < a2.GetLength(0))
                        {
                            if ((j < longueur_nbr_points_min) && ((a2[i + 1, 0] == 0) && (a2[i + 1, 1] == 0)))
                            {
                                matpseudomots[k] = new double[taillepmot[k, 1], 2];       // ????????????????????
                                //matpseudomots = new double[a2.Length, a2.Rank];// matpseudomots(:,:, k) = [];
                                k -= 1;
                            }

                        }
                        else if (i == a2.GetLength(0))
                        {
                            if (j < longueur_nbr_points_min)
                            {
                                matpseudomots[i] = new double[taillepmot[k, 1], 2];
                                //matpseudomots = new double[a2.Length, a2.Rank]; //matpseudomots(:,:, k) = [];
                                k -= 1;
                            }


                        }

                        j += 1;
                        indicateur_premier_passage = 1;
                        indicateur_atente_debut_pseudo_mot = 0;
                    }
                    else
                    {
                        if (((a2[i, 0] == 0) && (a2[i, 1] == 0)) && (((a2[i + 1, 0]) != 0) || ((a2[i + 1, 1]) != 0)) && (indicateur_premier_passage == 1))
                        {
                            k += 1;
                            j = 0; // 1
                            indicateur_atente_debut_pseudo_mot = 1;
                        }
                    }
                }
            }
            nbr_pseudomots = k;
            if ((size_a == 0) || (size_a == 1))
            {
                matpseudomots = new double[nombre_pseudo][,];  //matpseudomots = [];

            }
            return matpseudomots;

        }



   // taille pseudo mot

        public int[,] Method_extract_taille_pseudo_mot_p(double[,] a, int longueur_nbr_points_min)
        {
            nombre_pseudo = Method_nbr_pseudomots(a, longueur_nbr_points_min);
            taille_pseudomot = new int[nombre_pseudo, 2];  // à verifier

            vect = new double[a.Rank];
            taille = a.Length / a.Rank + 1;
            for (int i = a.Length / a.Rank - 1; i >= 0; i--)
            {
                if ((a[i, 0] == 0) && (a[i, 1] == 0))
                {
                    taille -= 1;
                    a2 = new double[taille, a.Rank];
                    for (int l = 0; l < taille; l++)
                    {
                        a2[l, 0] = a[l, 0];
                        a2[l, 1] = a[l, 1];

                    }
                    //a[i, 1] = 0; a[i, 2] = 0;        // a(i,:) = [];
                    //a[i, 1] = []; a[i, 2] = [];
                    if (i > 1)
                    {
                        if ((a[i - 1, 0]) != 0 || (a[i - 1, 1] != 0))

                            break;
                    }


                }
                else
                    break;
            }

            indicateur_premier_passage = 0;
            indicateur_atente_debut_pseudo_mot = 1;
            k = 0;  // indice du pseudo_mot courant  k=1
            j = 1; // indice dans la nouvelle matrice 'matpseudomots'
            size_a = a2.Length / a2.Rank;

            if (size_a > 1)
            {
                for (int i = 0; i < size_a - 1; i++)
                {
                    if ((a2[i, 0] != 0) || (a2[i, 1] != 0))
                    {
                        //matpseudomots[k, j,2] = a2[i, 1]; a verifier

                        if (i < a2.GetLength(0))
                        {
                            if ((j < longueur_nbr_points_min) && ((a2[i + 1, 0] == 0) && (a2[i + 1, 1] == 0)))
                            {
                                k -= 1;
                            }
                            if ((j >= longueur_nbr_points_min) && ((a2[i + 1, 0] == 0) && (a2[i + 1, 1] == 0)))
                            {
                                //System.Console.Write(k);
                                //System.Console.Write(j);
                                taille_pseudomot[k, 0] = k;
                                taille_pseudomot[k, 1] = j;
                                //taille_pseudomot = [taille_pseudomot; k , j];
                            }

                        }
                        else if (i == a2.GetLength(0))             //a2.Length
                        {
                            if (j < longueur_nbr_points_min)
                            {
                                k -= 1;
                            }
                            if (j >= longueur_nbr_points_min)
                            {
                                taille_pseudomot[k, 0] = k;
                                taille_pseudomot[k, 1] = j;
                            }


                            //taille_pseudomot = [taille_pseudomot; k , j];

                        }

                        j += 1;
                        indicateur_premier_passage = 1;
                        indicateur_atente_debut_pseudo_mot = 0;
                    }
                    else
                    {
                        if (((a2[i, 0] == 0) && (a2[i, 1] == 0)) && (((a2[i + 1, 0]) != 0) || ((a2[i + 1, 1]) != 0)) && (indicateur_premier_passage == 1))
                        {
                            k += 1;
                            j = 1;
                            indicateur_atente_debut_pseudo_mot = 1;
                        }
                    }
                    //System.Console.Write(j);
                }
            }
            nbr_pseudomots = k;
            if ((size_a == 0) || (size_a == 1))
            {
                nbr_pseudomots = 0;
                taille_pseudomot = new int[nombre_pseudo, 2];

            }
            return taille_pseudomot;

        }


  // nombre pseudo mot
        public int Method_nbr_pseudomots(double[,] a, int longueur_nbr_points_min)
        {
            //taille_pseudomot = new int[2]; //

            vect = new double[a.Rank];
            taille = a.Length / a.Rank;
            //System.//Console.WriteLine(a.GetLength(0));
            //System.Console.ReadKey();
            for (int i = a.GetLength(0) - 1; i >= 0; i--)
            {
                if ((a[i, 0] == 0) && (a[i, 1] == 0))
                {
                    taille -= 1;
                    a2 = new double[taille, a.Rank];
                    for (int l = 0; l < taille; l++)
                    {
                        a2[l, 0] = a[l, 0];
                        a2[l, 1] = a[l, 1];

                    }
                    //a[i, 1] = 0; a[i, 2] = 0;        // a(i,:) = [];
                    //a[i, 1] = []; a[i, 2] = [];
                    if (i > 1)
                    {
                        if ((a[i - 1, 0]) != 0 || (a[i - 1, 1] != 0))

                            break;
                    }
                }
                else
                {
                    break;
                }
                   
            }

            indicateur_premier_passage = 0;
            indicateur_atente_debut_pseudo_mot = 1;
            k = 1;  // indice du pseudo_mot courant
            j = 1; // indice dans la nouvelle matrice 'matpseudomots'

            size_a = a2.GetLength(0);
            //System.//Console.WriteLine(size_a);
            if (size_a > 1)
            {
                for (int i = 0; i < size_a; i++)
                {
                    if ((a2[i, 0] != 0) || (a2[i, 1] != 0))
                    {


                        if (i < a2.GetLength(0))
                        {
                            if ((j < longueur_nbr_points_min) && ((a2[i + 1, 0] == 0) && (a2[i + 1, 1] == 0)))
                            {

                                k -= 1;
                            }


                        }
                        else if (i == a2.Length / 2)
                        {
                            if (j < longueur_nbr_points_min)
                            {
                                k -= 1;
                            }

                        }

                        j += 1;
                        indicateur_premier_passage = 1;
                        indicateur_atente_debut_pseudo_mot = 0;
                    }
                    else
                    {
                        if (((a2[i, 0] == 0) && (a2[i, 1] == 0)) && (((a2[i + 1, 0]) != 0) || ((a2[i + 1, 1]) != 0)) && (indicateur_premier_passage == 1))
                        {
                            k += 1;
                            j = 1;
                            indicateur_atente_debut_pseudo_mot = 1;
                        }
                    }

                }
            }
            nbr_pseudomots = k;
            if ((size_a == 0) || (size_a == 1))

                nbr_pseudomots = 0;

            return nbr_pseudomots;
        }




        public int max_matrice(int[] tab)
        {
            int maximum = tab[0];
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] > maximum)
                    maximum = tab[i];
            }
            return maximum;
        }

        public int min_matrice(int[] tab)
        {
            int minimum = tab[0];
            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] < minimum)
                    minimum = tab[i];
            }
            return minimum;
        }



    }



}
