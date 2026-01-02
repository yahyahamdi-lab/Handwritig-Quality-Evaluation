using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Beta_elliptic_model
{
    public class WRDFile
    {
        public double[,] arr2;
        public double[,] arr22;
        public int[] arr;
        public double[] arr3;
        public int ligne, colonne;
        public string content;
        public string[] tt;
        public static bool littleEndian = true;
        public  void Method_WriteFile(double[,] matrix, string file)
       {

            ///Fill the table

            List<string> linesToWrite = new List<string>();
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
            {
                StringBuilder line = new StringBuilder();
                for (int colIndex = 0; colIndex < matrix.GetLength(1); colIndex++)
                    line.Append(matrix[rowIndex, colIndex]).Append(" ");
                linesToWrite.Add(line.ToString());
            }

            System.IO.File.WriteAllLines(file, linesToWrite.ToArray());



        }

        public void Method_WriteFile_double(double[] matrix, string file)
        {

            List<string> linesToWrite = new List<string>();
            StringBuilder line = new StringBuilder();
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)
                line.Append(matrix[rowIndex]).Append(" ");
            linesToWrite.Add(line.ToString());
            System.IO.File.WriteAllLines(file, linesToWrite.ToArray());

        }

        public void Method_WriteFile_Int(int[] matrix, string file)
        {

            List<string> linesToWrite = new List<string>();
            StringBuilder line = new StringBuilder();
            for (int rowIndex = 0; rowIndex < matrix.GetLength(0); rowIndex++)               
                line.Append(matrix[rowIndex]).Append(" ");         
               linesToWrite.Add(line.ToString());
            System.IO.File.WriteAllLines(file, linesToWrite.ToArray());

        }

        public double[,] Method_ReadFile_Param(string file)
        {
           
            string[] lines = System.IO.File.ReadAllLines(file);
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                //////Console.WriteLine("\t" + line);
                char[] t = { '\r', ' ' };
                string[] multiArray = line.Split(t);
                ligne = lines.Length;
                 colonne = multiArray.Length;

            }
            //ligne = lines.Length;
            double[,] montab = new double[ligne, colonne-1];
            using (StreamReader reader = new StreamReader(file))
                {
                    int idxLigne = 0;
                    string ligne;
                    string[] tabLigne;
                    while ((ligne = reader.ReadLine()) != null)
                    {
                        tabLigne = ligne.Split('\r', ' ');
                        for (int i = 0; i < tabLigne.Length-1; i++)
                        {
                            //montab[idxLigne, i] = Convert.ToDouble(tabLigne[i]);
                            //montab[i, j]= .ToString(new CultureInfo("en-US"));
                            montab[idxLigne, i] = Convert.ToDouble(tabLigne[i], new CultureInfo("en-US"));
                            //////Console.WriteLine(montab[idxLigne, i]);
                        }
                        idxLigne++;
                    }
            }
           
                    return montab;
 
        }
        // read file seuil
        public void Method_WriteFileBinSeuil(string file, double[] matrice)
        {

            FileStream fs = new FileStream(file, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            using (var binarystream = new BinaryWriter(fs))
            {
                foreach (var i in matrice)
                {
                    binarystream.Write(i * 7);
                }


            }
            bw.Close();
        }

        public void Method_WriteFile_Bin(string file, double[,] matrice)
        {
           
            FileStream fs = new FileStream(file, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            using (var binarystream = new BinaryWriter(fs))
            {
                foreach (double i in matrice)
                {
                    binarystream.Write(i);
                }


            }
            bw.Close();
            
        }


        public void Method_WriteFile_Txt_string(string file, string i)
        {
            ///Fill the table

            using (StreamWriter sw = File.AppendText(file))
            {
                sw.WriteLine(i);
            }
        }
        public string[] Method_ReadFileBinString(string file)
        {
            
           
            List<string> cor14 = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(file);
            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            
             tt = new string[lines.Length];
             for (int j = 0; j < tt.Length; j++)
                {
                    tt[j] = lines[j];
                    Console.WriteLine(tt[j]);
                }


           
            return tt;
        }

        public int Method_ReadFileTailleString(string file)
        {
            

            List<string> cor14 = new List<string>();
            string[] lines = System.IO.File.ReadAllLines(file);
            // Display the file contents by using a foreach loop.
            System.Console.WriteLine("Contents of WriteLines2.txt = ");
            foreach (string line in lines)
            {


                tt = new string[lines.Length];
                for (int j = 0; j < tt.Length - 1; j++)
                {
                    tt[j] = line;
                    Console.WriteLine(tt[j]);
                }

            }

            return lines.Length;
        }


        public double[] Method_ReadFileBinSeuil(string file)
        {
            double[] l = new double[6];
            using (var filestream = File.Open(file, FileMode.Open))

            using (var binaryStream1 = new BinaryReader(filestream))
            {
                for (int i = 0; i < 6; ++i)
                {
                    l[i] = binaryStream1.ReadDouble() / 7;
                }
                

            }
            return l;
        }

        public double[,] Method_ReadFile_Bin_Param(string file)
        {
          
             double aspectRatio;
            int n = 0;
            int l;
            if (File.Exists(file))
                {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    //var count = reader.BaseStream.Length / sizeof(int);
                    //for (var i = 0; i < count-8; i++)

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {

                        n = n + 1;
                        aspectRatio = reader.ReadDouble();
                        //Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                    }

                    //Console.WriteLine("Taille: " + n / 12);
                    //l = n / 12;
                    l = n / 17;
                    //tempDirectory = reader.ReadString();
                    //autoSaveTime = reader.ReadInt32();
                    //showStatusBar = reader.ReadBoolean();
                }
                if (File.Exists(file))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            //arr2 = new double[12, l];
                            arr2 = new double[17, l];
                            //for (int j = 0; j < 12; j++)
                            for (int j = 0; j < 17; j++)
                            {
                                for (int k = 0; k < l; k++)
                                {
                                    arr2[j, k] = reader.ReadDouble();
                                    //Console.WriteLine("Ligne: " + j + k + " " + arr2[j, k]);
                                }
                            }


                        }
                    }
                }

            }
                    
            
            return arr2;

        }

        public double[,] Method_ReadFile_Bin_Param_DF(string file)
        {

            double aspectRatio;
            int n = 0;
            int l;
            if (File.Exists(file))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    //var count = reader.BaseStream.Length / sizeof(int);
                    //for (var i = 0; i < count-8; i++)

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {

                        n = n + 1;
                        aspectRatio = reader.ReadDouble();
                        //Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                    }


                    l = n / 36; // 24;
                   
                  
                }
                if (File.Exists(file))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            
                            arr2 = new double[36, l];
                            
                            for (int j = 0; j < 36; j++)
                            {
                                for (int k = 0; k < l; k++)
                                {
                                    arr2[j, k] = reader.ReadDouble();
                                    
                                }
                            }


                        }
                    }
                }

            }


            return arr2;

        }
        public double[,] Method_ReadFile_Bin_Param_DFcar2(string file)
        {

            double aspectRatio;
            int n = 0;
            int l;
            if (File.Exists(file))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    //var count = reader.BaseStream.Length / sizeof(int);
                    //for (var i = 0; i < count-8; i++)

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {

                        n = n + 1;
                        aspectRatio = reader.ReadDouble();
                        //Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                    }


                    l = n / 49; // 24;


                }
                if (File.Exists(file))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {

                            arr2 = new double[49, l];

                            for (int j = 0; j < 49; j++)
                            {
                                for (int k = 0; k < l; k++)
                                {
                                    arr2[j, k] = reader.ReadDouble();

                                }
                            }


                        }
                    }
                }

            }


            return arr2;

        }
        public double[,] Method_ReadFile_Bin_Param_DF2(string file)
        {

            double aspectRatio;
            int n = 0;
            int l;
            if (File.Exists(file))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    //var count = reader.BaseStream.Length / sizeof(int);
                    //for (var i = 0; i < count-8; i++)

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {

                        n = n + 1;
                        aspectRatio = reader.ReadDouble();
                        //Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                    }


                    l = n / 35; // 24;


                }
                if (File.Exists(file))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {

                            arr2 = new double[35, l];

                            for (int j = 0; j < 35; j++)
                            {
                                for (int k = 0; k < l; k++)
                                {
                                    arr2[j, k] = reader.ReadDouble();

                                }
                            }


                        }
                    }
                }

            }


            return arr2;

        }

        public double[,] Method_ReadFile_Bin_Param_DFcar(string file)
        {

            double aspectRatio;
            int n = 0;
            int l;
            if (File.Exists(file))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                   
                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {

                        n = n + 1;
                        aspectRatio = reader.ReadDouble();
                        
                    }


                    l = n /50; 


                }
                if (File.Exists(file))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {

                            arr22 = new double[50, l];

                            for (int j = 0; j < 50; j++)
                            {
                                for (int k = 0; k < l; k++)
                                {
                                    arr22[j, k] = reader.ReadDouble();

                                }
                            }


                        }
                    }
                }

            }


            return arr22;


        }

        public double[,] Method_ReadFile_Bin_XY(string file)
        {

            double aspectRatio;
            int n = 0;
            int l;
            if (File.Exists(file))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    //var count = reader.BaseStream.Length / sizeof(int);
                    //for (var i = 0; i < count-8; i++)

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {

                        n = n + 1;
                        aspectRatio = reader.ReadDouble();
                        //Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                    }

                   // Console.WriteLine("Taille: " + n / 2);
                    l = n / 2;
                    //tempDirectory = reader.ReadString();
                    //autoSaveTime = reader.ReadInt32();
                    //showStatusBar = reader.ReadBoolean();
                }
                if (File.Exists(file))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            arr2 = new double[l, 2];
                            for (int j = 0; j < l; j++)
                            {
                                for (int k = 0; k < 2; k++)
                                {
                                    arr2[j, k] = reader.ReadDouble();
                                    //Console.WriteLine("Ligne: " + j + k + " " + arr2[j, k]);
                                }
                            }


                        }
                    }
                }

            }


            return arr2;

        }
        public double[,] Method_ReadFile_Bin_Distance(string file)
        {

            double aspectRatio;
            int n = 0;
            int l;
            if (File.Exists(file))
            {
                using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                {
                    //var count = reader.BaseStream.Length / sizeof(int);
                    //for (var i = 0; i < count-8; i++)

                    while (reader.BaseStream.Position != reader.BaseStream.Length)
                    {

                        n = n + 1;
                        aspectRatio = reader.ReadDouble();
                        //Console.WriteLine("Aspect ratio set to: " + aspectRatio);
                    }

                    //Console.WriteLine("Taille: " + n / 3);
                    l = n / 3;
                    //tempDirectory = reader.ReadString();
                    //autoSaveTime = reader.ReadInt32();
                    //showStatusBar = reader.ReadBoolean();
                }
                if (File.Exists(file))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open)))
                    {
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            arr2 = new double[l, 3];
                            for (int j = 0; j < l; j++)
                            {
                                for (int k = 0; k < 3; k++)
                                {
                                    arr2[j, k] = reader.ReadDouble();
                                    //Console.WriteLine("Ligne: " + j + k + " " + arr2[j, k]);
                                }
                            }


                        }
                    }
                }

            }


            return arr2;

        }


        public void Method_DeleteFiles(string fileToDelete)
        {
            if (System.IO.File.Exists(fileToDelete))
            { System.IO.File.Delete(fileToDelete); }
            
        }

        public int[] Method_ReadFile_Int(string file)
        {
             string[] lines = System.IO.File.ReadAllLines(file);
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                //////Console.WriteLine("\t" + line);
                char[] t = { '\r', ' ' };
                string[] multiArray = line.Split(t);
                //ligne = lines.Length;
                colonne = multiArray.Length;

            }
            //ligne = lines.Length;
            arr= new int[colonne - 1];
            using (StreamReader reader = new StreamReader(file))
            {
                
                string ligne;
                string[] tabLigne;
                while ((ligne = reader.ReadLine()) != null)
                {
                    tabLigne = ligne.Split('\r', ' ');
                    for (int i = 0; i < tabLigne.Length - 1; i++)
                    {

                        arr[i] = Int32.Parse(tabLigne[i]);
                    } 
                   
                }
            }
          
           
            return arr;
        }

        public double[] Method_ReadFile_double(string file)
        {
            string[] lines = System.IO.File.ReadAllLines(file);
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                //////Console.WriteLine("\t" + line);
                char[] t = { '\r', ' ' };
                string[] multiArray = line.Split(t);
                //ligne = lines.Length;
                colonne = multiArray.Length;

            }
            //ligne = lines.Length;
            arr3= new double[colonne - 1];
            using (StreamReader reader = new StreamReader(file))
            {
                
                string ligne;
                string[] tabLigne;
                while ((ligne = reader.ReadLine()) != null)
                {
                    tabLigne = ligne.Split('\r', ' ');
                    for (int i = 0; i < tabLigne.Length - 1; i++)
                    {

                        arr3[i] = Convert.ToDouble(tabLigne[i], new CultureInfo("en-US"));
                    } 
                   
                }
            }
          
            return arr3;

           
        }
        public double[,] Method_ReadFile_xy(string file)
        {
            string[] lines = System.IO.File.ReadAllLines(file);
            foreach (string line in lines)
            {
                // Use a tab to indent each line of the file.
                //////Console.WriteLine("\t" + line);
                char[] t = { '\r', ' ' };
                string[] multiArray = line.Split(t);
                ligne = lines.Length;
                colonne = multiArray.Length;


            }
            //ligne = lines.Length;
            double[,] arr2 = new double[ligne-1, colonne+1];
            using (StreamReader reader = new StreamReader(file))
            {
                int idxLigne = 0;
                string ligne;
                string[] tabLigne;
                while ((ligne = reader.ReadLine()) != null)
                {
                    tabLigne = ligne.Split('\r', ' ');

                    for (int i = 0; i < tabLigne.Length-1; i++)
                    {
                        //montab[idxLigne, i] = Convert.ToDouble(tabLigne[i]);
                        //montab[i, j]= .ToString(new CultureInfo("en-US"));
                        arr2[idxLigne, i] = Convert.ToDouble(tabLigne[i], new CultureInfo("en-US"));
                        //////Console.WriteLine(montab[idxLigne, i]);
                    }
                    idxLigne++;
                }
            }

            return arr2;
        }
        public void ReadBinary(string file)
        {
             using (FileStream stream = new FileStream(file, FileMode.Open))
                {
                    // Read bytes from stream and interpret them as ints
                    byte[] buffer = new byte[1024];
                    int count;
                    // Read from the IO stream fewer times.
                    while ((count = stream.Read(buffer, 0, buffer.Length)) > 0)
                        for (int i = 0; i < count; i++)
                            Console.WriteLine(Convert.ToDouble(buffer[i]));
                }
            }




        public void ReadBinaryfile(string file)
        {
            long offset;
            int nextByte;
            byte[] data = File.ReadAllBytes(file);
            double  u = BitConverter.ToDouble(data, 0);
            //Console.WriteLine("double: {0}", u);

            // alphabet.txt contains "abcdefghijklmnopqrstuvwxyz"
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                for (offset = 1; offset <= fs.Length; offset++)
                {
                    fs.Seek(-offset, SeekOrigin.End);

                    Console.Write(Convert.ToDouble(fs.ReadByte()));
                }
                Console.WriteLine();

                fs.Seek(20, SeekOrigin.Begin);

                while ((nextByte = fs.ReadByte()) > 0)
                {
                    Console.Write(Convert.ToDouble(nextByte));
                }
                Console.WriteLine();
            }
        }

        public void lireBin_file(string file)
        {
            BinaryReader br;
            try
            {                      
                br = new BinaryReader(new FileStream(file, FileMode.Open));
                while (!br.Equals(""))     
                {
                    
                    Console.WriteLine(br.ReadDouble());
                }

            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message + "\n Cannot open file.");
                return;
            }

            br.Close();

        }
    }
}
