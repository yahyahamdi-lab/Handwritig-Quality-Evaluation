using System;
using System.Collections.Generic;
using System.Text;

namespace Kaligo_Beta_elliptic.Beta_elliptic_model
{
    class RunD_Description_par_Series_de_Fourier_dune_Trajectoire_Online
    {
        public string elimin_trait_excedent,t, numero_scripteur_en_caracter;
        public string chemin_acces_dossier_Base_d_Echantillons, chemin_acces_dossier_Base_de_Parametres_I, chemin_acces_dossier_Base_de_Parametres_II;
        public int numero_scripteur, numero_mot;
        public double[,] Matrice_de_Donnees_XY, grande_mmatrice_param_1,
        grande_mmatrice_param_2, grande_mmatrice_param_extra_etendu_1,
                Matrice_de_Donnees;
        public double [] Grande_mmatrice_param_extra_etendu_ECH_Apprentissage ,matrice_indice_deb_fin_ECH_Apr, colonne_class_echantillon_apprentissage ;
        public void run_descriptor()
        {
           
            elimin_trait_excedent = "non";
            for (int k = 1; k < 1; k++)
            {
                t = k.ToString();
           

           // chemin_acces_dossier_Base_d_Echantillons = ['f:\' , 'SIGNATURE\2\fourrierSignature\2\'];
           

            numero_scripteur = k;

           //chemin_acces_dossier_Base_de_Parametres_I = ['F:\' , 'SIGNATURE\ECH\TEST3_faux\Base de ParametresI\\'];
    
           //chemin_acces_dossier_Base_de_Parametres_II = ['F:\' , 'SIGNATURE\ECH\TEST3_faux\Base de ParametresII\\'];
    
                elimin_trait_excedent = "non";




              //  chemin_acces_dossier_Base_de_Parametres_I = ['F:\' , 'SIGNATURE\ECH\TEST3_faux\Base de ParametresI\\'];
    
               // chemin_acces_dossier_Base_de_Parametres_II = ['F:\' , 'SIGNATURE\ECH\TEST3_faux\Base de ParametresII\\'];

                
                numero_scripteur_en_caracter = numero_scripteur.ToString();

               //chemin_acces_dossier_Base_de_Parametres_I_Scripteur_n = [chemin_acces_dossier_Base_de_Parametres_I, 'writer_', numero_scripteur_en_caracter, '\'];
    


              //  chemin_acces_dossier_Base_de_Parametres_II_Scripteur_n = [chemin_acces_dossier_Base_de_Parametres_II, 'writer_', numero_scripteur_en_caracter, '\'];
    


                //chemin_acces_dossier_Base_d_Echantillons_save = [chemin_acces_dossier_Base_d_Echantillons, 'Signer_', numero_scripteur_en_caracter, '\'];
    


                numero_mot = 0;

                var av_files = System.IO.Directory.GetFiles(chemin_acces_dossier_Base_d_Echantillons, "*.bin");

                int nombre_des_fichiers_echantillons =System.IO.Directory.GetFiles(chemin_acces_dossier_Base_d_Echantillons, "*.bin").Length;
                
                for (int fid =0; fid < nombre_des_fichiers_echantillons;fid++)
                {

                string nom_du_fichier_echantillon = av_files[fid];


                    numero_mot = numero_mot + 1;
                    //[x, y, z, az,in, pps]=FPG_Signature_Read(chemin_acces_fichier_txt, 1, 1)
                    //Matrice_de_Donnees = [Matrice_de_Donnees, x, y, z,in];



                    //Matrice_de_Donnees_XY = [Matrice_de_Donnees_XY, x, y];
                    //Matrice_de_Donnees_XY = Matrice_de_Donnees_XY;

                    //Matrice_de_Donnees = Matrice_de_Donnees;
                    //[param_description_par_suite_de_Fourier] = Description_par_Series_de_Fourier_dune_Trajectoire_Online(Matrice_de_Donnees_XY, 30)

                }
            }

        }


    }
}
