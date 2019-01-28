using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Drawing.Imaging;

namespace howto_split_bitmap
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        // Split the file.
        private void btnGo_Click(object sender, EventArgs e)
        {
            // Get the inputs.
            int wid = int.Parse(txtWidth.Text);
            int hgt = int.Parse(txtHeight.Text);
            Bitmap bm = LoadUnlocked(txtFile.Text);

            // Start splitting the Bitmap.
            string piece_name = Path.GetFileNameWithoutExtension(txtFile.Text);
            Bitmap piece = new Bitmap(wid, hgt);
            Rectangle dest_rect = new Rectangle(0, 0, wid, hgt);
            using (Graphics gr = Graphics.FromImage(piece))
            {
                int num_rows = bm.Height / hgt;
                int num_cols = bm.Width / wid;
                Rectangle source_rect = new Rectangle(0, 0, wid, hgt);
                for (int row = 0; row < num_rows; row++)
                {
                    source_rect.X = 0;
                    for (int col = 0; col < num_cols; col++)
                    {
                        // Copy the piece of the image.
                        gr.DrawImage(bm, dest_rect, source_rect, GraphicsUnit.Pixel);

                        // Save the piece.
                        string filename = piece_name +
                            row.ToString("00") +
                            col.ToString("00") + ".bmp";
                        piece.Save(filename, ImageFormat.Bmp);

                        // Move to the next column.
                        source_rect.X += wid;
                    }
                    source_rect.Y += hgt;
                }
                MessageBox.Show("Created " + num_rows * num_cols + " files",
                    "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Load a Bitmap without locking its file.
        private Bitmap LoadUnlocked(string file_name)
        {
            using (Bitmap bm = new Bitmap(file_name))
            {
                return new Bitmap(bm);
            }
        }
    }
}
