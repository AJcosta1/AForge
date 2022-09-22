#region Using
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using AForge.Video;
using AForge.Video.DirectShow;
using SDKSmartTrainnerAdaptor;

using Color = System.Drawing.Color;

#endregion

namespace SDKSmartTrainnerAdaptor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
     
        #region Private fields

        /// <summary>
        /// The camera which we use to acquire bitmaps
        /// </summary>
        private IVideoSource _videoSource;

        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            servicesStart();
            this.DataContext = this;
            GetVideoDevices();
            Threshold = 127;
            Radius = 30;
            Original = true;
            this.Closing += MainWindow_Closing;
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// On closing the application stops the camera if active and restore the cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Cursor = Cursors.Arrow;
            StopCamera();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            StartCamera();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            StopCamera();
        }


        /// <summary>
        /// Handles the mouse enter event when a user is picking a color, to display a special cursor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoPlayer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (PickingColor)
            {
                var cursor = ((FrameworkElement)App.Current.Resources["CursorPicker"]).Cursor;
                Cursor = cursor;
            }
        }

        /// <summary>
        /// Restores the cursor when user is leaving the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoPlayer_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }



        private void SetDefaults(object sender, RoutedEventArgs e)
        {
            Red = 255;
            Green = 255;
            Blue = 255;
        }

        private void Calibrate(object sender, RoutedEventArgs e)
        {
            _PosX0 = _PosX;

        }

        #endregion
    }


}

