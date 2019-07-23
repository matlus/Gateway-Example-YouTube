using MovieClientApplication.Exceptions;
using MovieClientApplication.Models;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace MovieClientApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ComboBoxGenre.Items.Add(Genre.Action);
            ComboBoxGenre.Items.Add(Genre.Animation);
            ComboBoxGenre.Items.Add(Genre.Drama);
            ComboBoxGenre.Items.Add(Genre.Musical);
            ComboBoxGenre.Items.Add(Genre.SciFi);
            ComboBoxGenre.Items.Add(Genre.Thriller);
            ComboBoxGenre.Items.Add(Genre.NA);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ComboBoxGenre.SelectedIndex = 0;
        }

        private void ComboBoxGenre_SelectedIndexChanged(object sender, EventArgs e)
        {
            var protocol = "REST";
            if (radioButtonSOAP.Checked)
                protocol = "SOAP";


            var sw = Stopwatch.StartNew();
            var moviesGateway = MoviesGatewayFactory.CreateGateway(protocol, textBoxRemoteAddressBase.Text);
            sw.Stop();
            
            try
            {
                var movies = moviesGateway.GetMoviesByGenre(((Genre)ComboBoxGenre.SelectedItem));
                toolStripStatusLabel1.Text = "Movies Received: " + movies.Count().ToString();
                toolStripStatusLabel2.Text = "Time Taken: " + sw.ElapsedMilliseconds.ToString();

                flowLayoutPanel1.Controls.Clear();

                foreach (var movie in movies)
                {
                    var movieUserControl = new MovieControl();
                    
                    movieUserControl.Title = movie.Title;
                    movieUserControl.Genre = movie.Genre.ToString();
                    movieUserControl.Year = movie.Year.ToString();
                    movieUserControl.ImageUrl = "https://matlusstorage.blob.core.windows.net/membervideos/" + movie.ImageUrl;
                    flowLayoutPanel1.Controls.Add(movieUserControl);
                }
            }
            catch (MoviesBaseException ex)
            {
                MessageBox.Show(ex.Message, "Movies Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
