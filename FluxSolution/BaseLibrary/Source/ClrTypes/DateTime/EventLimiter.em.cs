namespace Flux
{
  public class EventLimiter
  {
    private readonly System.Collections.Generic.Queue<System.DateTime> m_history;
    private readonly int m_limit;
    private System.TimeSpan m_window;

    public EventLimiter(int limit, System.TimeSpan interval)
    {
      m_limit = limit;
      m_window = interval;
      m_history = new System.Collections.Generic.Queue<System.DateTime>(limit);
    }

    private void Synchronize()
    {
      while ((m_history.Count > 0) && (m_history.Peek().Add(m_window) < System.DateTime.UtcNow))
      {
        m_history.Dequeue();
      }
    }

    public bool CanRequest()
    {
      Synchronize();

      return m_history.Count < m_limit;
    }

    public void Request()
    {
      while (!CanRequest())
      {
        System.Threading.Thread.Sleep(m_history.Peek().Add(m_window).Subtract(System.DateTime.UtcNow));
      }

      m_history.Enqueue(System.DateTime.UtcNow);
    }
  }
}
