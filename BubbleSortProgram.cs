using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;


namespace SorterImager
{
    /// <summary>
    /// This program draws a bunch of rectangles and performs a bubble sort on a big list of rectangles and draws them again.
    /// Very complex. Do not try at home.
    /// </summary>
    class Program
    {
        /// <summary>
        /// MAX HEIGHT:     Controls the max height of rectangles and the height of the panel they are drawn in.
        /// NUM ITEMS:      The total number of rectangles that will be drawn.
        /// PANEL WIDTH:    The width of the panel.
        /// RECT WIDTH:     Controls the width of the panel and is subject to the size of the panel and number of rectangles
        /// </summary>
        private static int MAX_HEIGHT=500; 
        private static int NUM_ITEMS = 500;
        private static int PANEL_WIDTH=500;
        private static int RECT_WIDTH = PANEL_WIDTH / NUM_ITEMS;
        public class SorterBox : System.Windows.Forms.Form
        {
            /// <summary>
            /// Variable run down:
            /// rand        -> used to create random heights for the rectangles
            /// myPanel     -> used for drawing
            /// allRects    -> holds all the Rectangles
            /// </summary>
            private Random rand = new Random();     
            private System.Windows.Forms.Panel myPanel;
            private List<System.Drawing.Rectangle> allRects = new List<System.Drawing.Rectangle>();

            public SorterBox(){
                InitThings();
            }
            private void InitThings(){
                this.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawARectangle_Paint);
                myPanel = new System.Windows.Forms.Panel();
                fillRects();
                this.Width = PANEL_WIDTH;
                this.Height = MAX_HEIGHT+50;
            }

            /// <summary>
            /// Small helper to populate the List with rectangles. All changes should be made to static variables up top.
            /// </summary>
            private void fillRects() {
                for (int i = 0; i < NUM_ITEMS; i++) {
                    allRects.Add(new System.Drawing.Rectangle((i*RECT_WIDTH), 0, RECT_WIDTH, rand.Next(0,MAX_HEIGHT)));
                }
            }
            /// <summary>
            /// Draws all rectangles and then sorts them while continually redrawing.
            /// </summary>
            /// <param name="sender">I dont know what this is</param>
            /// <param name="e">This either</param>
            private void DrawARectangle_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
            {
                Graphics g = e.Graphics;
                System.Drawing.SolidBrush b = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

                do
                {
                    //Draw Step
                    System.Threading.Thread.Sleep(50); //Slows down the drawing in order to decrease the chance of blinky-death.
                    g.Clear(Color.White);
                    
                    foreach (Rectangle r in allRects)
                    { 
                        g.FillRectangle(b, r);
                    }                   
                    //Sort and check if done.
                } while (bubbleSort());
            }

/// <summary>
///     Runs through the allRects list and performs a single pass of the buble sort
/// </summary>
/// <returns>
///     true: if any elements were changed and another pass of the buble sort is needed
///     false: if no elements were changed and another pass is not needed.
/// </returns>
            private bool bubbleSort() {
                bool changed = false;
                for (int i = 0; i < NUM_ITEMS-1; i++)
                {
                    if (allRects[i].Height > allRects[i + 1].Height) { swap(i, i + 1); changed = true; }    
                }
                return changed;
            }
            /// <summary>
            /// A sort helper. Switches around the height value for two rectangles
            /// </summary>
            /// <param name="r1"> the first index of the list that needs to be changed</param>
            /// <param name="r2"> the second index of the list that needs to be changed</param>
            private void swap(int r1, int r2) {
                Rectangle rec1 = allRects[r1];
                Rectangle rec2 = allRects[r2];
                allRects[r1] = new Rectangle(rec1.X,rec1.Y,RECT_WIDTH,rec2.Height);
                allRects[r2] = new Rectangle(rec2.X, rec2.Y, RECT_WIDTH, rec1.Height);
            }
        }         
        
        [STAThread]
        static void Main(string[] args)
        {
            Application.Run(new SorterBox());
        }
    }
}
