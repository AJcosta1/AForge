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

        #region Event handlers


        /// <summary>
        /// Frame received callback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void video_NewFrame(object sender, AForge.Video.NewFrameEventArgs eventArgs)
        {
            try
            {
                BitmapImage bi;
                using (var bitmap = (Bitmap)eventArgs.Frame.Clone())
                {
                    // Here we choose what image to display
                    if (ColorFiltered)
                    {
                        new EuclideanColorFiltering(new AForge.Imaging.RGB((byte)Red, (byte)Green, (byte)Blue), Radius).ApplyInPlace(bitmap);
                    }
                    if (Grayscaled)
                    {
                        using (var grayscaledBitmap = Grayscale.CommonAlgorithms.BT709.Apply(bitmap))
                        {
                            bi = grayscaledBitmap.ToBitmapImage();
                        }
                    }
                    else if (Thresholded)
                    {
                        using (var grayscaledBitmap = Grayscale.CommonAlgorithms.BT709.Apply(bitmap))
                        using (var thresholdedBitmap = new Threshold(Threshold).Apply(grayscaledBitmap))
                        {
                            if (Inverted)
                            {
                                new Invert().ApplyInPlace(thresholdedBitmap);
                            }
                            bi = thresholdedBitmap.ToBitmapImage();
                        }
                    }
                    else // original
                    {
                        var corners = FindCorners(bitmap);
                        PosX = corners.Count() > 0 ? corners.Select(c => c.X).Average() : PosX;
                        if (corners.Any())
                        {
                            PaintCorners(corners, bitmap);
                        }
                        bi = bitmap.ToBitmapImage();
                    }
                }
                bi.Freeze(); // avoid cross thread operations and prevents leaks
                Dispatcher.BeginInvoke(new ThreadStart(delegate { videoPlayer.Source = bi; }));
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error on _videoSource_NewFrame:\n" + exc.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                StopCamera();
            }
        }

 

        /// <summary>
        /// Handles the click when the user picks a color from the image
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void videoPlayer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PickingColor)
            {
                var clickPoint = e.GetPosition(videoPlayer);
                var image = videoPlayer.Source as BitmapImage;
                // color finding algorithm taken from:
                // http://stackoverflow.com/questions/1176910/finding-specific-pixel-colors-of-a-bitmapimage
                int stride = image.PixelWidth * 4;
                int size = image.PixelHeight * stride;
                byte[] pixels = new byte[size];
                image.CopyPixels(pixels, stride, 0);
                int index = ((int)clickPoint.Y) * stride + 4 * ((int)clickPoint.X);
                Blue = pixels[index];
                Green = pixels[index + 1];
                Red = pixels[index + 2];
                PickingColor = false;
                Cursor = Cursors.Arrow;
            }
        }

        #endregion

        /// <summary>
        /// We process the image applying all the filters, then we filter the blobs and we find the border or the biggest one
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        private List<IntPoint> FindCorners(Bitmap bitmap)
        {
            List<IntPoint> corners = new List<IntPoint>();
            using (var clone = bitmap.Clone() as Bitmap)
            {
                new EuclideanColorFiltering(new AForge.Imaging.RGB((byte)Red, (byte)Green, (byte)Blue), Radius).ApplyInPlace(clone);
                using (var grayscaledBitmap = Grayscale.CommonAlgorithms.BT709.Apply(clone))
                {
                    new Threshold(Threshold).ApplyInPlace(grayscaledBitmap);
                    if (Inverted)
                    {
                        new Invert().ApplyInPlace(grayscaledBitmap);
                    }
                    BlobCounter blobCounter = new BlobCounter();
                    blobCounter.FilterBlobs = true;
                    blobCounter.MinWidth = 50;
                    blobCounter.MinHeight = 50;
                    blobCounter.ObjectsOrder = ObjectsOrder.Size;
                    blobCounter.ProcessImage(grayscaledBitmap);
                    Blob[] blobs = blobCounter.GetObjectsInformation();
                    // create convex hull searching algorithm
                    GrahamConvexHull hullFinder = new GrahamConvexHull();
                    for (int i = 0, n = blobs.Length; i < n; i++)
                    {
                        List<IntPoint> leftPoints, rightPoints;
                        List<IntPoint> edgePoints = new List<IntPoint>();
                        // get blob's edge points
                        blobCounter.GetBlobsLeftAndRightEdges(blobs[i], out leftPoints, out rightPoints);
                        edgePoints.AddRange(leftPoints);
                        edgePoints.AddRange(rightPoints);
                        // blob's convex hull
                        corners = hullFinder.FindHull(edgePoints);
                    }
                }
            }
            return corners;
        }

        /// <summary>
        /// Given a list of points, draws blue lines that connects the points on a given bitmap
        /// </summary>
        /// <param name="corners"></param>
        /// <param name="bitmap"></param>
        private void PaintCorners(List<IntPoint> corners, Bitmap bitmap)
        {
            using (Graphics g = Graphics.FromImage(bitmap))
            using (System.Drawing.Pen bluePen = new System.Drawing.Pen(Color.Blue, 2))
            {
                g.DrawPolygon(bluePen, ToPointsArray(corners));
            }
        }

        private void GetVideoDevices()
        {
            VideoDevices = new ObservableCollection<FilterInfo>();
            foreach (FilterInfo filterInfo in new FilterInfoCollection(FilterCategory.VideoInputDevice))
            {
                VideoDevices.Add(filterInfo);
            }
            if (VideoDevices.Any())
            {
                CurrentDevice = VideoDevices[0];
            }
            else
            {
                MessageBox.Show("No video sources found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        // Conver list of AForge.NET's points to array of .NET points
        private System.Drawing.Point[] ToPointsArray(List<IntPoint> points)
        {
            System.Drawing.Point[] array = new System.Drawing.Point[points.Count];

            for (int i = 0, n = points.Count; i < n; i++)
            {
                array[i] = new System.Drawing.Point(points[i].X, points[i].Y);
            }

            return array;
        }


        private void StartCamera()
        {
            if (CurrentDevice != null)
            {
                _videoSource = new VideoCaptureDevice(CurrentDevice.MonikerString);
                _videoSource.NewFrame += video_NewFrame;
                _videoSource.Start();
            }
        }

        private void StopCamera()
        {
            if (_videoSource != null && _videoSource.IsRunning)
            {
                _videoSource.SignalToStop();
                _videoSource.NewFrame -= new NewFrameEventHandler(video_NewFrame);
            }
        }

    }


}

