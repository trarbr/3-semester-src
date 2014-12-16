using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

using bt05;
using System.Threading;

namespace BinaryTreeVisualisation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void drawButton_Click(object sender, RoutedEventArgs e)
        {
            BinarySearchTree bst = new BinarySearchTree();

            bst.Insert(25);
            //bst.Insert(10);
            //bst.Insert(40);
            //bst.Insert(30);
            //bst.Insert(60);
            //bst.Insert(5);
            //bst.Insert(12);
            //bst.Insert(8);

            Random rnd = new Random();

            for (int i = 0; i < 5; i++)
            {
                bst.Insert(rnd.Next(0, 5));
            }

            drawNode(bst.Root, (int)canvas.Width/2, 20, 250, 20);
        }

        private void drawNode(BinarySearchTree.Node node, int x, int y, int dx, int dy)
        {
            if (node == null) return;
            Ellipse ellipse = new Ellipse();
            ellipse.Stroke = Brushes.Black;
            ellipse.StrokeThickness = 3;
            ellipse.Width = 10;
            ellipse.Height = 10;
            ellipse.Fill = Brushes.Black;

            canvas.Children.Add(ellipse);

            Canvas.SetLeft(ellipse, x - 5);
            Canvas.SetTop(ellipse, y - 5);

            if (node.LeftChild != null)
            {
                drawNode(node.LeftChild, x - dx, y + dy, dx/2, dy);
                Line line = new Line()
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 2,
                    X1 = x,
                    Y1 = y,
                    X2 = x - dx,
                    Y2 = y + dy
                };
                canvas.Children.Add(line);
            }
            if (node.RightChild != null)
            {
                drawNode(node.RightChild, x + dx, y + dy, dx/2, dy);
                // draw line to node
                Line line = new Line()
                {
                    Stroke = Brushes.Red,
                    StrokeThickness = 2,
                    X1 = x,
                    Y1 = y,
                    X2 = x + dx,
                    Y2 = y + dy
                };
                canvas.Children.Add(line);
            }
        }


    }
}
