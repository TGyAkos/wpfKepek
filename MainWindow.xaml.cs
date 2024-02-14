using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace wpfKepek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        List<BitmapImage> kepek = new List<BitmapImage>();
        int SelectedIndex = 0;
        bool isAuto = false;
        DispatcherTimer timer = new DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
            // This is VERY DUMB
            string[] picNames = Directory.GetFiles(Directory.GetCurrentDirectory() + "/../../../kepek").ToArray();

            foreach (string pic in picNames)
            {
                kepek.Add(convertToUri(pic));
            }

            TheImages.Source = kepek.First();
            FileName.Text = Path.GetFileName(kepek[SelectedIndex].UriSource.ToString());

            progressBar.Maximum = kepek.Count;
            progressBar.Minimum = 0;
        }

        private void elozo_click(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex > 0)
            {
                SelectedIndex--;
            }
            else
            {
                SelectedIndex = kepek.Count - 1;
            }

            TheImages.Source = kepek[SelectedIndex];
            FileName.Text = Path.GetFileName(kepek[SelectedIndex].UriSource.ToString());
            progressBar.Value = SelectedIndex;
        }

        private void kovetkezo_click(object sender, RoutedEventArgs e)
        {
            if (SelectedIndex < kepek.Count - 1)
            {
                SelectedIndex++;
            }
            else
            {
                SelectedIndex = 0;
            }

            TheImages.Source = kepek[SelectedIndex];
            FileName.Text = Path.GetFileName(kepek[SelectedIndex].UriSource.ToString());
            progressBar.Value = SelectedIndex;

        }

        private void automatikus_click(object sender, RoutedEventArgs e)
        {
            if (isAuto == false)
            {
                elozo.IsEnabled = false;
                kovetkezo.IsEnabled = false;
                timer.Interval = TimeSpan.FromSeconds(2);
                DoubleAnimation doubleanimation = new DoubleAnimation(100.0, timer.Interval);
                timer.Tick += (s, e) =>
                {
                    progressBarSeconds.BeginAnimation(ProgressBar.ValueProperty, null);
                    progressBarSeconds.Value = 0;
                    progressBarSeconds.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
                    kovetkezo_click(null, null);
                };

                timer.Start();
                progressBarSeconds.BeginAnimation(ProgressBar.ValueProperty, doubleanimation);
            }
            else
            {
                timer.Stop();
                progressBarSeconds.BeginAnimation(ProgressBar.ValueProperty, null);
                progressBarSeconds.Value = 0;
                elozo.IsEnabled = true;
                kovetkezo.IsEnabled = true;
            }
            isAuto = !isAuto;
        }

        private static BitmapImage convertToUri(string path)
        {
            Uri uri = new Uri(path);
            BitmapImage pic = new BitmapImage(uri);
            pic.UriSource = uri;
            return pic;
        }


    }
}