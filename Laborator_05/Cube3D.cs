using OpenTK.Graphics;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Laborator_05
{
    /// <summary>
    /// Represents a 3D cube that can have its faces colored and modified.
    /// </summary>
    public class Cube3D
    {
        private Vector3[][] cube;                        // 6 faces with 4 points each

        private Color4[] colorsFaces;                    // Colors for faces
        private Color4[][] colorsVertex;                    // Colors for faces with vertex


        private Randomizer random;

        // CONST
        private const float MIN_VAL = 0.0f; // Valoare minima interval RGBA
        private const float MAX_VAL = 1.0f; // Valoare maxima interval RGBA
        private const float VAL = 0.05f;    // Valoare cu care se schimba RGBA (incrementare, decrementare)

        /// <summary>
        /// This constructor initializes a new instance of the Cube3D class and loads the cube's vertex data.
        /// </summary>
        public Cube3D()
        {
            // setare locatie fisier in directorul corespunzator solutiei
            string filepath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName + "\\" +
                ConfigurationManager.AppSettings["NumeFisier"];

            cube = ReadCubeCoord(filepath);

            // culori fete
            colorsFaces = new Color4[6];
            for (int i = 0; i < 6; i++)
                colorsFaces[i] = new Color4(0.0f, 0.0f, 0.0f, 1.0f);

            // culori vertecsi fete
            colorsVertex = new Color4[6][];
            for (int i = 0; i < colorsVertex.Length; i++)
                colorsVertex[i] = new Color4[4];

            for (int j = 0; j < colorsVertex.Length; j++)
                for (int i = 0; i < colorsVertex[j].Length; i++)
                    colorsVertex[j][i] = new Color4(0.0f, 0.0f, 0.0f, 1.0f);

            random = new Randomizer();

        }

        /// <summary>
        /// Draws the cube with the current colors applied to each face.
        /// </summary>
        public void Draw()
        {
            GL.Begin(PrimitiveType.Quads);

            // Desenare si aplicare culoare fata

            for (int i = 0; i < cube.Length; i++)
            {
                GL.Color4(colorsFaces[i]);

                for (int j = 0; j < cube[i].Length; j++)
                {
                    GL.Vertex3(cube[i][j]);
                }
            }
            GL.End();
        }

        /// <summary>
        /// Draws the cube with per-vertex colors applied to each face.
        /// </summary>
        public void DrawColorVertex()
        {
            GL.Begin(PrimitiveType.Quads);

            // Desenare si aplicare culoare fata

            for (int i = 0; i < cube.Length; i++)
            {
                for (int j = 0; j < cube[i].Length; j++)
                {
                    GL.Color4(colorsVertex[i][j]);
                    GL.Vertex3(cube[i][j]);
                }
            }
            GL.End();
        }

        /// <summary>
        /// Modifies the color of a selected face based on user input for a color channel (R, G, B, A).
        /// </summary>
        /// <param name="UpOrDown">Indicates whether the color should be increased or decreased.</param>
        public void ModifyColorFace(Key UpOrDown)
        {
            KeyboardState keyboard = Keyboard.GetState();

            // Semn operatie
            int sign = UpOrDown.Equals(Key.Up) ? 1 : -1;

            int face = getFace();

            // Modificare culoare/transparenta in functie de fata si de canalul de culoare aleas (RGBA)
            switch (true)
            {
                case var _ when keyboard[Key.R] && keyboard[UpOrDown]:          ///match any value
                    colorsFaces[face].R = MathHelper.Clamp(colorsFaces[face].R + sign * VAL, MIN_VAL, MAX_VAL);
                    DisplayFaceColors(face);
                    break;

                case var _ when keyboard[Key.G] && keyboard[UpOrDown]:
                    colorsFaces[face].G = MathHelper.Clamp(colorsFaces[face].G + sign * VAL, MIN_VAL, MAX_VAL);
                    DisplayFaceColors(face);
                    break;

                case var _ when keyboard[Key.B] && keyboard[UpOrDown]:
                    colorsFaces[face].B = MathHelper.Clamp(colorsFaces[face].B + sign * VAL, MIN_VAL, MAX_VAL);
                    DisplayFaceColors(face);
                    break;

                case var _ when keyboard[Key.A] && keyboard[UpOrDown]:
                    colorsFaces[face].A = MathHelper.Clamp(colorsFaces[face].A - sign * VAL, MIN_VAL, MAX_VAL);
                    DisplayFaceColors(face);
                    break;

            }

        }

        /// <summary>
        /// Modifies the color of a specific vertex within a specified face.
        /// </summary>
        /// <param name="vertex">The index of the vertex within the face.</param>
        public void ModifyColorVertexOfFace(int vertex)
        {
            KeyboardState keyboard = Keyboard.GetState();

            int face = getFace();

            // Modificare culoare/transparenta in functie de fata si de canalul de culoare aleas (RGBA)
            switch (true)
            {
                case var _ when keyboard[Key.R]:          ///match any value
                    colorsVertex[face][vertex].R = MathHelper.Clamp(colorsVertex[face][vertex].R + VAL, MIN_VAL, MAX_VAL);
                    DisplayFaceColorsVertex(face);
                    break;

                case var _ when keyboard[Key.G]:
                    colorsVertex[face][vertex].G = MathHelper.Clamp(colorsVertex[face][vertex].G + VAL, MIN_VAL, MAX_VAL);
                    DisplayFaceColorsVertex(face);
                    break;

                case var _ when keyboard[Key.B]:
                    colorsVertex[face][vertex].B = MathHelper.Clamp(colorsVertex[face][vertex].B + VAL, MIN_VAL, MAX_VAL);
                    DisplayFaceColorsVertex(face);
                    break;

                case var _ when keyboard[Key.A]:
                    colorsVertex[face][vertex].A = MathHelper.Clamp(colorsVertex[face][vertex].A - VAL, MIN_VAL, MAX_VAL);
                    DisplayFaceColorsVertex(face);
                    break;

            }

        }

        /// <summary>
        /// Helper method to retrieve the selected face based on key input.
        /// </summary>
        /// <returns>An integer representing the selected face index.</returns>
        private static int getFace()
        {
            // Get face
            KeyboardState keyboard = Keyboard.GetState();

            int face = -1;
            if (keyboard[Key.Number1])          // Top-face
                face = 0;
            else
                if (keyboard[Key.Number2])      // Bottom-face
                face = 1;
            else
                if (keyboard[Key.Number3])      // Front-face
                face = 2;
            else
                if (keyboard[Key.Number4])      // Back-face
                face = 3;
            else
                if (keyboard[Key.Number5])      // Left-face
                face = 4;
            else
                if (keyboard[Key.Number6])      // Right-face
                face = 5;

            return face;
        }

        /// <summary>
        /// Randomly modifies the colors of all faces of the cube.
        /// </summary>
        public void ModifyColorFacesRandom()
        {
            for (int i = 0; i < colorsFaces.Length; i++)
                colorsFaces[i] = random.RandomColor();
        }


        /// <summary>
        /// Displays the RGBA values for a specific face in the console.
        /// </summary>
        /// <param name="index">The index of the face (0-based).</param>
        public void DisplayFaceColors(int index)
        {
            Console.WriteLine($"\nFata {index + 1} - R: {colorsFaces[index].R}, " +
                $"G: {colorsFaces[index].G}, B: {colorsFaces[index].B}, A: {colorsFaces[index].A}");
        }

        /// <summary>
        /// Displays the RGBA color values for each vertex on a specified face of the cube.
        /// </summary>
        /// <param name="face">The index of the face (0-based) for which vertex colors will be displayed.</param>
        public void DisplayFaceColorsVertex(int face)
        {
            Console.WriteLine("\nCulori pentru fiecare vertex:");
            for (int i = 0; i < colorsVertex[face].Length; i++)
                Console.WriteLine($"Fata {face + 1}, vertex{i + 1} - R: {colorsVertex[face][i].R}," +
                    $" G: {colorsVertex[face][i].G}, B: {colorsVertex[face][i].B}, A: {colorsVertex[face][i].A}");
        }

        /// <summary>
        /// Displays the RGBA color values for each face of the cube.
        /// </summary>
        public void DisplayColors()
        {
            Console.WriteLine("\nCulori pentru fiecare fata:");
            for (int i = 0; i < colorsFaces.Length; i++)
                Console.WriteLine($"Fata {i + 1} - R: {colorsFaces[i].R}," +
                    $" G: {colorsFaces[i].G}, B: {colorsFaces[i].B}, A: {colorsFaces[i].A}");
        }

        /// <summary>
        /// Resets all RGBA values to the default color (black with full opacity).
        /// </summary>
        public void ResetRGBA()
        {
            // Resetare RGBA triunghi la parametrii initiali
            for (int i = 0; i < colorsFaces.Length; i++)
            {
                colorsFaces[i].R = colorsFaces[i].G = colorsFaces[i].B = 0.0f; // Resetare la negru
                colorsFaces[i].A = 1.0f; // Opac
            }
        }

        /// <summary>
        /// Reads the coordinates of the cube's vertices from a file.
        /// </summary>
        /// <param name="fileName">The file containing the coordinates of the cube's faces.</param>
        /// <returns>A 2D array of vertices, where each face contains 4 vertices.</returns>
        private Vector3[][] ReadCubeCoord(string fileName)
        {
            Vector3[][] points = new Vector3[6][]; // 6 faces with 4 points each
            int indexFata = 0, indexVertex = 0;

            for (int i = 0; i < points.Length; i++)
                points[i] = new Vector3[4];

            try
            {
                using (StreamReader streamReader = new StreamReader(fileName))
                {
                    string line;
                    string[] coord;

                    // Citirea coordonatelor pentru fiecare fata
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        // Ignorare comentarii
                        if (line.StartsWith("#"))
                            continue;

                        // Extragere coordonate vertex dintr-o linie
                        coord = line.Split(',');


                        if (indexVertex == 4)
                        {
                            indexVertex = 0;
                            indexFata++;
                        }

                        // Creare vertex
                        points[indexFata][indexVertex++] = new Vector3(
                                float.Parse(coord[0]),
                                float.Parse(coord[1]),
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