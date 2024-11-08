using OpenTK;
using OpenTK.Graphics;
using System;
using System.Drawing;

namespace Laborator_05
{
    /// <summary>
    /// This class generates various random values for different kind of parameters (<see cref="Random()"/>)
    /// </summary>
    public class Randomizer
    {
        private Random r;
        private const int LOW_INT_VAL = -25;
        private const int HIGH_INT_VAL = 25;
        private const int LOW_COORD_VAL = -50;
        private const int HIGH_COORD_VAL = 50;

        /// <summary>
        /// Standard constructor. Initialised with the system clock for seed.
        /// </summary>
        public Randomizer()
        {
            r = new Random();
        }

        /// <summary>
        /// This method returns a random Color4 when requested.
        /// The RGBA values are generated randomly within the range of 0 to 1.
        /// </summary>
        /// <returns>the Color4, randomly generated!</returns>
        public Color4 RandomColor()
        {
            float genR = (float)r.NextDouble();
            float genG = (float)r.NextDouble();
            float genB = (float)r.NextDouble();

            Color4 col = new Color4(genR, genG, genB, 1.0f);

            return col;
        }

    }
}