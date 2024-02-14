using System.IO;
using System.Windows;
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

        }

        private void automatikus_click(object sender, RoutedEventArgs e)
        {
            if (isAuto == false)
            {
                elozo.IsEnabled = false;
                kovetkezo.IsEnabled = false;
                timer.Interval = TimeSpan.FromSeconds(2);
                timer.Tick += (s, e) =>
                {
                    kovetkezo_click(null, null);
                };
                timer.Start();
            }
            else
            {
                timer.Stop();
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