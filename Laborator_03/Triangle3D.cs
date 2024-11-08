using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using System;
using System.IO;
using System.Configuration;

namespace Laborator_03
{
    class Triangle3D
    {
        private Vector3[] triangle;
        private Color4 color;
        private Color4[] colors;

        // CONST
        private const float MIN_VAL = 0.0f; // Valoare minima interval RGBA
        private const float MAX_VAL = 1.0f; // Valoare maxima interval RGBA
        private const float VAL = 0.05f; // Valoare cu care se schimba RGBA (incrementare, decrementare)

        public Triangle3D()
        {
            // setare locatie fisier in directorul corespunzator solutiei
            string filepath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + "\\" +
                ConfigurationManager.AppSettings["NumeFisier"];

            triangle = ReadTriangleCoord(filepath);

            // Exercitiul 8 laboartor 3 
            //color = new Color4(0.0f, 0.0f, 0.0f, 1.0f); 

            colors = new Color4[3]
            {
                new Color4(0.0f, 0.0f, 0.0f, 1.0f), // Vertex 1
                new Color4(0.0f, 0.0f, 0.0f, 1.0f), // Vertex 2
                new Color4(0.0f, 0.0f, 0.0f, 1.0f)  // Vertex 3
            };
        }

        public void Draw()
        {
            GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);

            GL.Begin(PrimitiveType.Triangles);

            ///Exercitiul 8 laborator 3 
            //GL.Color4(color);

            // Desenare si aplicare culoare vertex

            for (int i = 0; i < triangle.Length; i++)
            {
                GL.Color4(colors[i]);
                GL.Vertex3(triangle[i]); // Exercitiul 8 laboartor 3 
            }
            GL.End();
        }

        ///Exercitiul 8 laborator 3 

        /*public void ModifyColor(string letter)
        {

            // Modificare culoare/transparenta

            if (letter.ToUpper() == "R")
                color.R = MathHelper.Clamp(color.R + VAL, MIN_VAL, MAX_VAL);
            else if (letter.ToUpper() == "G")
                color.G = MathHelper.Clamp(color.G + VAL, MIN_VAL, MAX_VAL);
            else if (letter.ToUpper() == "B")
                color.B = MathHelper.Clamp(color.B + VAL, MIN_VAL, MAX_VAL);
            else if (letter.ToUpper() == "A")
                color.A = MathHelper.Clamp(color.A - VAL, MIN_VAL, MAX_VAL);
        }*/

        public void ModifyColor(string colorChannel, int vertexIndex)
        {

            DisplayVertexColors();

            if (vertexIndex < 0 || vertexIndex >= colors.Length)
                return; // Iesim daca indexul vertex-ului este invalid

            // Modificare culoare/transparenta in functie de vertex si de canalul de culoare aleas (RGBA)
            switch (colorChannel.ToUpper())
            {
                case "R":
                    colors[vertexIndex].R = MathHelper.Clamp(colors[vertexIndex].R + VAL, MIN_VAL, MAX_VAL);
                    break;
                case "G":
                    colors[vertexIndex].G = MathHelper.Clamp(colors[vertexIndex].G + VAL, MIN_VAL, MAX_VAL);
                    break;
                case "B":
                    colors[vertexIndex].B = MathHelper.Clamp(colors[vertexIndex].B + VAL, MIN_VAL, MAX_VAL);
                    break;
                case "A":
                    colors[vertexIndex].A = MathHelper.Clamp(colors[vertexIndex].A - VAL, MIN_VAL, MAX_VAL);
                    break;
            }
        }

        // Afisare RGBA pentru fiecare vertex
        public void DisplayVertexColors()
        {
            Console.WriteLine("\nCulori pentru fiecare vertex:");
            for (int i = 0; i < colors.Length; i++)
            {
                Console.WriteLine($"Vertex {i + 1} - R: {colors[i].R}, G: {colors[i].G}, B: {colors[i].B}, A: {colors[i].A}");
            }
        }
        public void ResetRGBA()
        {
            ///Exercitiul 8 laborator 3

            /*color.R = color.G = color.B = 0.0f;
            color.A = 1.0f;*/

            // Resetare RGBA triunghi la parametrii initiali
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].R = colors[i].G = colors[i].B = 0.0f; // Resetare la negru
                colors[i].A = 1.0f; // Opac
            }
        }

        private Vector3[] ReadTriangleCoord(string fileName)
        {
            Vector3[] points = new Vector3[3];

            // Citire coordonate triunghi din fisier text linie cu linie

            try
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    string line;
                    string[] coord;

                    for (int i = 0; i < 3; i++)
                    {
                        line = streamReader.ReadLine();

                        // Extragere coordonate vertex dintr-o linie din fiser
                        coord = line.Split(',');
                        points[i] = new Vector3(float.Parse(coord[0]), float.Parse(coord[1]),
                        float.Parse(coord[2]));
                    }
                }
            }
            catch (IOException eIO)
            {
                throw new Exception(eIO.Message);
            }
            return points;
        }
    }
}