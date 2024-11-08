// Stoica Maria Alexandra 3131B
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;

namespace Laborator_05
{
    /// <summary>
    /// The graphic window. Contains the canvas (viewport to be draw).
    /// </summary>
    class Window3D : GameWindow
    {
        private KeyboardState previousKeyboard;
        private MouseState previousMouse;

        private readonly Randomizer rando;
        private readonly Axes ax;

        private Cube3D cube;

        // Rotire cub
        private const float rotation_speed = 180.0f;
        private float angleKeyHorizontal = 0.0f;
        private float angleKeyVertical = 0.0f;

        //DEFAULTS
        private readonly Color DEFAULT_BKG_COLOR = Color.LightBlue;


        /// <summary>
        /// Parametrised constructor. Invokes the anti-aliasing engine. All inits are placed here, for convenience.
        /// </summary>
        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            //inits
            rando = new Randomizer();
            ax = new Axes();
            cube = new Cube3D();

            DisplayHelp();
        }

        /// <summary>
        /// OnLoad() method. Part of the control loop of the OpenTK API. Executed only once.
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);
        }

        /// <summary>
        /// OnResize() method. Part of the control loop of the OpenTK API. Executed at least once (after OnLoad()).
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // set background
            GL.ClearColor(DEFAULT_BKG_COLOR);

            // set viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // set perspective
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 1024);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // set the eye
            Matrix4 camera = Matrix4.LookAt(120, 120, 120, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref camera);

        }

        /// <summary>
        /// OnUpdateFrame() method. Part of the control loop of the OpenTK API. Executed periodically, with a frequency determined when launching
        /// the graphics window(<see cref = "GameWindow.Run(double,double)" />). In this case should be 30.00 (if unmodified).
        /// 
        /// All logic should reside here!
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            // LOGIC CODE
            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            // Exit
            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            // Help menu
            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }

            // Reset colors
            if (currentKeyboard[Key.C] && !previousKeyboard[Key.C])
            {
                cube.ResetRGBA();
            }

            // Axes visibility
            if (currentKeyboard[Key.V] && !previousKeyboard[Key.V])
            {
                ax.ToggleVisibility();
            }

            // Display RGBA face
            if (currentKeyboard[Key.D] && !previousKeyboard[Key.D])
            {
                cube.DisplayColors();
            }

            // Modificare culori fete random -- exercitiul 3
            if (currentKeyboard[Key.F] && !previousKeyboard[Key.F])
            {
                cube.ModifyColorFacesRandom();
            }

            // Modificare culoare fata in functie de RGBA si Up/Down -- exercitiul 1
            if (currentKeyboard[Key.Up] && !previousKeyboard[Key.Up])
            {
                cube.ModifyColorFace(Key.Up);
            }

            if (currentKeyboard[Key.Down] && !previousKeyboard[Key.Down])
            {
                cube.ModifyColorFace(Key.Down);
            }


            // Rotire cub
            if (currentMouse[OpenTK.Input.MouseButton.Left])
                angleKeyHorizontal += rotation_speed / 3 * (float)e.Time;

            if (currentMouse[OpenTK.Input.MouseButton.Right])
                angleKeyVertical += rotation_speed / 3 * (float)e.Time;


            previousKeyboard = currentKeyboard;
            previousMouse = currentMouse;

            // END logic code
        }

        /// <summary>
        /// OnRenderFrame() method. Part of the control loop of the OpenTK API. Executed periodically, with a frequency determined when launching
        /// the graphics window (<see cref="GameWindow.Run(double,double)"/>). In this case should be 0.00 (if unmodified) - the renderinh is triggered
        /// only when the scene is modified.
        /// 
        /// All render calls should reside here!
        /// </summary>
        /// <param name="e">event parameters that triggered the method;</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // RENDER CODE

            GL.PushMatrix();

            // Rotire cub
            GL.Rotate(angleKeyHorizontal, 0.0f, 1.0f, 0.0f);
            GL.Rotate(angleKeyVertical, 0.0f, 0.0f, 1.0f);

            cube.Draw();
            ax.Draw();

            GL.PopMatrix();

            GL.Flush();


            // END render code
            SwapBuffers();
        }

        /// <summary>
        /// Internal method, used to dump the menu on the console window (text mode!)...
        /// </summary>
        private void DisplayHelp()
        {
            Console.WriteLine("\n     MENIU");
            Console.WriteLine(" (H) - meniu");
            Console.WriteLine(" (ESC) - parasire aplicatie");
            Console.WriteLine(" (V) - schimbare vizibilitate sistem de axe");
            Console.WriteLine(" (C) - resetare culori fete cub");
            Console.WriteLine(" (D) - afisare culori fete cub");
            Console.WriteLine(" (F) - schimbare culori fete cub random (exercitiul 3)");

            Console.WriteLine(" (Up & R/G/B/A & 1/2/3/4/5/6) - crestere valoare canal de\n\t culoare pentru o fata a cubului, pentru transparenta (A) se scade");
            Console.WriteLine(" (Down & R/G/B/A & 1/2/3/4/5/6) - scadere valoare canal de\n\t culoare pentru o fata a cubului, pentru transparenta (A) se creste");
            Console.WriteLine(" Pentru schimbarea culorii fetei unui cub in functie de RGBA\n\t" +
                " (exercitiul 1) se poate tine apasat continuu canalul de culoare (R/G/B/A)\n\t " +
                "si numarul fetei (1/2/3/4/5/6) si prin apasarea lui Up/Down se modifica valoarea");

            Console.WriteLine(" (Clic stanga) - rotire cub si axe pe orizontala");
            Console.WriteLine(" (Clic dreapta) - rotire cub si axe pe verticala");

        }
    }
}