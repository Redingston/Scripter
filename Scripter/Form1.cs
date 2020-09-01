using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;


namespace Scripter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            _ = GetImage();

            InitializeComponent();

        }

      
        private void Button1_ClickAsync(object sender, EventArgs e)
        {
            GetImage();
        }

        public async Task GetImage() {
            var png = "http://185.80.129.249:4222/getImage";
            HttpClient client = new HttpClient();
            HttpResponseMessage http = await client.GetAsync(png);
            byte[] content = await http.Content.ReadAsByteArrayAsync();
            MemoryStream ms = new MemoryStream(content);
            Image image = Image.FromStream(ms, true, true);
            pictureBox1.Image = image;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var src = new Mat(pictureBox1.ImageLocation);

            //var src = new Mat("C:\\Users\\Endrew\\Pictures\\man.jpg");
            var dst = new Mat(50, 200, MatType.CV_16SC1);

            Cv2.Canny(src, dst, 50, 200);
            using (new Window("src image", src))
            using (new Window("dst image", dst))
            {
                Cv2.WaitKey();
            }
        }
    }
}
