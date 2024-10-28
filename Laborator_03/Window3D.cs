// Stoica Maria Alexandra 3131B
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Laborator_03
{
    class Window3D : GameWindow
    {

        private KeyboardState previousKeyboard;
        private Triangle3D firstTriangle;

        private float rotationX, rotationY;
        private bool isMousePressed = false;

        private int selectedVertex = -1; // Variabila pentru colt/vertex triunghi

        // CONST
        private Color DEFAULT_BACK_COLOR = Color.LightSteelBlue;

        public Window3D() : base(1280, 768, new GraphicsMode(32, 24, 0, 8))
        {
            VSync = VSyncMode.On;

            firstTriangle = new Triangle3D();

            DisplayHelp();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            // Adaugare suport transparenta
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // setare fundal
            GL.ClearColor(DEFAULT_BACK_COLOR);

            // setare viewport
            GL.Viewport(0, 0, this.Width, this.Height);

            // setare proiectie/con vizualizare
            Matrix4 perspectiva = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)this.Width / (float)this.Height, 1, 250);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspectiva);

            // setare ochi
            Matrix4 ochi = Matrix4.LookAt(0, 30, 30, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref ochi);

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);


            // LOGIC CODE
            KeyboardState currentKeyboard = Keyboard.GetState();
            MouseState currentMouse = Mouse.GetState();

            if (currentKeyboard[Key.Escape])
            {
                Exit();
            }

            // Afisare meniu ajutor
            if (currentKeyboard[Key.H] && !previousKeyboard[Key.H])
            {
                DisplayHelp();
            }

            ///Exercitiul 8 laborator 3

            /*if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                firstTriangle.ModifyColor(Key.R.ToString());
            }

            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                firstTriangle.ModifyColor(Key.G.ToString());
            }

            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                firstTriangle.ModifyColor(Key.B.ToString());
            }

            if (currentKeyboard[Key.A] && !previousKeyboard[Key.A])
            {
                firstTriangle.ModifyColor(Key.A.ToString());
            }

            if (currentKeyboard[Key.C] && !previousKeyboard[Key.C])
            {
                firstTriangle.ResetRGBA();
            }*/

            // Selectarea vertex-ului pentru care se modifica RGBA
            if (currentKeyboard[Key.Number1]) selectedVertex = 0;
            if (currentKeyboard[Key.Number2]) selectedVertex = 1;
            if (currentKeyboard[Key.Number3]) selectedVertex = 2;


            // Selectarea culorii
            if (currentKeyboard[Key.R] && !previousKeyboard[Key.R])
            {
                firstTriangle.ModifyColor("R", selectedVertex);
            }

            if (currentKeyboard[Key.G] && !previousKeyboard[Key.G])
            {
                firstTriangle.ModifyColor("G", selectedVertex);
            }

            if (currentKeyboard[Key.B] && !previousKeyboard[Key.B])
            {
                firstTriangle.ModifyColor("B", selectedVertex);
            }

            // Selectarea transparentei
            if (currentKeyboard[Key.A] && !previousKeyboard[Key.A])
            {
                firstTriangle.ModifyColor("A", selectedVertex);
            }

            // Revenire la valori implicite RGBA
            if (currentKeyboard[Key.C] && !previousKeyboard[Key.C])
            {
                firstTriangle.ResetRGBA();
            }

            // Afisare RGBA colturi triunghi
            if (currentKeyboard[Key.D] && !previousKeyboard[Key.D])
            {
                firstTriangle.DisplayVertexColors();
            }

            // Aflare stare mouse. Daca apasam pe click stanga putem roti camera
            isMousePressed = currentMouse.IsButtonDown(MouseButton.Left);

            previousKeyboard = currentKeyboard;

            // END logic code
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Clear(ClearBufferMask.DepthBufferBit);

            // RENDER CODE

            // Rotirea camerei
            if (isMousePressed)
            {
                GL.Rotate(rotationX, 0.0f, 1.0f, 0.0f);
                GL.Rotate(rotationY, 1.0f, 0.0f, 0.0f);
            }

            //desenare triunghi
            firstTriangle.Draw();

            /// Exercitiul 1 laborator 3
            //DrawAxes();


            // END render code

            SwapBuffers();

        }
        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            // Ajustarea unghiului camerei cu miscarea mouse-ului
            if (isMousePressed)
            {
                rotationX += e.XDelta * 0.005f;
                rotationY += e.YDelta * 0.005f;
            }
        }

        private void DisplayHelp()
        {
            Console.WriteLine("\n     === MENU === ");
            Console.WriteLine(" H - Meniu ajutor");
            Console.WriteLine(" ESC - Parasire aplicatie");
            Console.WriteLine(" R - Schimbare pigmet rosu");
            Console.WriteLine(" G - Schimbare pigmet verde");
            Console.WriteLine(" B - Schimbare pigmet albastru");
            Console.WriteLine(" A - Schimbare valoarea intensitaii");
            Console.WriteLine(" C - Resetare RGBA");
            Console.WriteLine(" D - Afisare valori RGBA vertex");
            Console.WriteLine(" Click stanga - Ajustare unghi camera (click lung - rotire)");
            Console.WriteLine(" Pentru a modifica culoarea unui vertex se apasa intai 1, 2 sau 3 si dupa R, G sau B.\n" +
                " Pana nu se alege un nou colt al triunghiului, schimbarea culorilor va afecta coltul curent.");
        }

        /// Exercitiul 1 laborator 3

        private void DrawAxes()
        {
            GL.Begin(PrimitiveType.Lines);
            // desenarea axelor folosind doar un GL.Begin si un GL.End

            // X
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(20, 0, 0);

            // Y
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 20, 0);

            // Z
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 20);

            GL.End();
        }

    }
}
