using System.Windows.Forms;

namespace MovieClientApplication
{
    public partial class MovieControl : UserControl
    {
        public string Title { get { return labelTitle.Text; } set { labelTitle.Text = value; toolTip1.SetToolTip(pictureBox1, value); } }
        public string Genre { get { return labelGenre.Text; } set { labelGenre.Text = value; } }
        public string Year { get { return labelYear.Text; } set { labelYear.Text = value; } }

        public string ImageUrl { get { return pictureBox1.ImageLocation; }  set  { pictureBox1.ImageLocation = value; } }


        public MovieControl()
        {
            InitializeComponent();
        }
    }
}
