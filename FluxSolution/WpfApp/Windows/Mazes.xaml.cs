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
using System.Windows.Shapes;

using Flux;

namespace WpfApp
{
  /// <summary>
  /// Interaction logic for Mazes.xaml
  /// </summary>
  public partial class Mazes : Window
  {
    public Mazes()
    {
      InitializeComponent();

      var size = new Flux.Size2(60, 60);

      m_grid = new Flux.Model.Maze.MazeGrid(size);
    }

    Flux.Model.Maze.MazeGrid m_grid;

    Flux.Model.Maze.AMaze m_maze;

    protected override void OnRender(DrawingContext drawingContext)
    {
      ResetMaze();

      base.OnRender(drawingContext);
    }

    void EnableControls(bool isEnabled)
    {
      foreach (var child in stackPanelControls.Children)
        if (child is Button button)
          button.IsEnabled = isEnabled;
    }

    void PaintMaze()
    {
      var wallWidth = 1;
      var wallWidthX2 = wallWidth * 2;

      var minSize = (int)System.Math.Min(canvas.ActualWidth, canvas.ActualHeight);

      var width = minSize / m_grid.Size.Width;// (int)(canvas.ActualWidth / m_grid.Size.Width);
      var height = minSize / m_grid.Size.Height;// (int)(canvas.ActualHeight / m_grid.Size.Height);

      canvas.Children.Clear();

      for (var row = 0; row < m_grid.Size.Height; row++)
      {
        for (var column = 0; column < m_grid.Size.Width; column++)
        {
          var cell = m_grid[row, column];

          float x = column * width;// + wallWidth * 2F;
          float y = row * height;// + wallWidth * 2F;

          foreach (Flux.CardinalDirection direction in Enum.GetValues(typeof(Flux.CardinalDirection)))
          {
            if (!cell.Edges.ContainsKey((int)direction) || !cell.Paths.ContainsKey((int)direction))
            {
              switch (direction)
              {
                case Flux.CardinalDirection.N:
                  canvas.Children.Add(CreateLine(x + wallWidth, y, x + width - wallWidthX2, y, System.Windows.Media.Brushes.Green, wallWidth));
                  break;
                case Flux.CardinalDirection.E:
                  canvas.Children.Add(CreateLine(x + width - wallWidth, y + wallWidth, x + width - wallWidth, y + height - wallWidthX2, System.Windows.Media.Brushes.Yellow, wallWidth * 0.5F));
                  break;
                case Flux.CardinalDirection.S:
                  canvas.Children.Add(CreateLine(x + wallWidth, y + height - wallWidth, x + width - wallWidthX2, y + height - wallWidth, System.Windows.Media.Brushes.Red, wallWidth));
                  break;
                case Flux.CardinalDirection.W:
                  canvas.Children.Add(CreateLine(x, y + wallWidth, x, y + height - wallWidthX2, System.Windows.Media.Brushes.Blue, wallWidth));
                  break;
              }
            }
          }
        }
      }
      //foreach (var cell in m_grid.GetValues())
      //{
      //  float x = cell.Column * (width + wallWidth) + wallWidth * 2F;
      //  float y = cell.Row * (width + wallWidth) + wallWidth * 2F;

      //  foreach (Flux.Model.DirectionEnum direction in Enum.GetValues(typeof(Flux.Model.DirectionEnum)))
      //  {
      //    if (!cell.Edges.ContainsKey((int)direction) || !cell.Paths.ContainsKey((int)direction))
      //    //if (!cell.Edges.ContainsKey((int)direction) || cell.Paths[(int)direction] == null)
      //    //if (cell.Edges[(int)direction] == null || cell.Paths[(int)direction] == null)
      //    {
      //      switch (direction)
      //      {
      //        case Flux.Model.DirectionEnum.North:
      //          args.DrawingSession.DrawLine(x, y, x + width, y, Windows.UI.Colors.Green, wallWidth);
      //          break;
      //        case Flux.Model.DirectionEnum.East:
      //          args.DrawingSession.DrawLine(x + width, y, x + width, y + height, Windows.UI.Colors.Yellow, wallWidth * 0.5F);
      //          break;
      //        case Flux.Model.DirectionEnum.South:
      //          args.DrawingSession.DrawLine(x, y + height, x + width, y + height, Windows.UI.Colors.Red, wallWidth);
      //          break;
      //        case Flux.Model.DirectionEnum.West:
      //          args.DrawingSession.DrawLine(x, y, x, y + height, Windows.UI.Colors.Blue, wallWidth);
      //          break;
      //      }
      //    }
      //  }
      //}

      static System.Windows.Shapes.Line CreateLine(double X1, double Y1, double X2, double Y2, System.Windows.Media.Brush brush, double width)
      {
        var line = new System.Windows.Shapes.Line();
        line.Stroke = brush;
        line.X1 = X1;
        line.Y1 = Y1;
        line.X2 = X2;
        line.Y2 = Y2;
        line.StrokeThickness = width;
        return line;
      }
    }

    void ResetMaze()
    {
      m_grid.ResetEdges(true, false);
      m_grid.ResetPaths(false);

      PaintMaze();
    }

    void CarveMaze(Flux.Model.Maze.AMaze maze)
    {
      ResetMaze();

      (m_maze = maze)?.CarveMaze(m_grid);

      PaintMaze();
    }

    private void Button_Click_Clear(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      ResetMaze();
      EnableControls(true);
    }

    private void Button_Click_AldousBroder(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.AldusBroderMaze());
      EnableControls(true);
    }

    private void Button_Click_BackTracker(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.BackTrackerMaze());
      EnableControls(true);
    }

    private void Button_Click_BinaryTree(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.BinaryTreeMaze());
      EnableControls(true);
    }

    private void Button_Click_GrowingTree(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.GrowingTreeMaze());
      EnableControls(true);
    }

    private void Button_Click_HuntAndKill(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.HuntAndKillMaze());
      EnableControls(true);
    }

    private void Button_Click_RecursiveDivision(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.RecursiveDivisionMaze());
      EnableControls(true);
    }

    private void Button_Click_SideWinder(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.SidewinderMaze());
      EnableControls(true);
    }

    private void Button_Click_Walker(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.WalkerCave());
      EnableControls(true);
    }

    private void Button_Click_Wilsons(object sender, RoutedEventArgs e)
    {
      EnableControls(false);
      CarveMaze(new Flux.Model.Maze.WilsonsMaze());
      EnableControls(true);
    }

    private void Button_Click_Braid(object sender, RoutedEventArgs e)
    {
      EnableControls(false);

      m_maze?.BraidMaze(m_grid);

      PaintMaze();

      EnableControls(true);
    }
  }
}
