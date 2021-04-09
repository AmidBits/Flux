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

      var size = new Flux.Geometry.Size2(40, 40);

      m_grid = new Flux.Model.Maze.Grid(size);
    }

    Flux.Model.Maze.Grid m_grid;

    Flux.Model.Maze.AMaze m_maze;

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

    void PaintMaze()
    {
      float wallWidth = 1;
      //Windows.UI.Color wallColor = Windows.UI.Colors.White;

      float cellSize = (int)((canvas.Height - 100) / m_grid.Size.Height) - wallWidth * 1.5F;

      float width = cellSize;
      float height = cellSize;

      canvas.Children.Clear();

      for (var row = 0; row < m_grid.Size.Height; row++)
      {
        for (var column = 0; column < m_grid.Size.Width; column++)
        {
          var cell = m_grid[row, column];

          float x = column * (width + wallWidth) + wallWidth * 2F;
          float y = row * (width + wallWidth) + wallWidth * 2F;

          foreach (Flux.CardinalDirection direction in Enum.GetValues(typeof(Flux.CardinalDirection)))
          {
            if (!cell.Edges.ContainsKey((int)direction) || !cell.Paths.ContainsKey((int)direction))
            {
              switch (direction)
              {
                case Flux.CardinalDirection.N:
                  canvas.Children.Add(CreateLine(x, y, x + width, y, System.Windows.Media.Brushes.Green, wallWidth));
                  break;
                case Flux.CardinalDirection.E:
                  canvas.Children.Add(CreateLine(x + width, y, x + width, y + height, System.Windows.Media.Brushes.Yellow, wallWidth * 0.5F));
                  break;
                case Flux.CardinalDirection.S:
                  canvas.Children.Add(CreateLine(x, y + height, x + width, y + height, System.Windows.Media.Brushes.Red, wallWidth));
                  break;
                case Flux.CardinalDirection.W:
                  canvas.Children.Add(CreateLine(x, y, x, y + height, System.Windows.Media.Brushes.Blue, wallWidth));
                  break;
              }
            }
          }
        }
      }
      foreach (var cell in m_grid.Values)
      {
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
      }

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

    private void Button_Click_Clear(object sender, RoutedEventArgs e)
      => ResetMaze();

    private void Button_Click_AldousBroder(object sender, RoutedEventArgs e)
      => CarveMaze(new Flux.Model.Maze.AldusBroderMaze());

    private void Button_Click_BackTracker(object sender, RoutedEventArgs e)
      => CarveMaze(new Flux.Model.Maze.BackTrackerMaze());

    private void Button_Click_BinaryTree(object sender, RoutedEventArgs e)
      => CarveMaze(new Flux.Model.Maze.BinaryTreeMaze());

    private void Button_Click_GrowingTree(object sender, RoutedEventArgs e)
      => CarveMaze(new Flux.Model.Maze.GrowingTreeMaze());

    private void Button_Click_HuntAndKill(object sender, RoutedEventArgs e)
      => CarveMaze(new Flux.Model.Maze.HuntAndKillMaze());

    private void Button_Click_RecursiveDivision(object sender, RoutedEventArgs e)
      => CarveMaze(new Flux.Model.Maze.RecursiveDivisionMaze());

    private void Button_Click_SideWinder(object sender, RoutedEventArgs e)
      => CarveMaze(new Flux.Model.Maze.SidewinderMaze());

    private void Button_Click_Wilsons(object sender, RoutedEventArgs e)
      => CarveMaze(new Flux.Model.Maze.WilsonsMaze());

    private void Button_Click_Braid(object sender, RoutedEventArgs e)
    {
      m_maze?.BraidMaze(m_grid);

      PaintMaze();
    }
  }
}
