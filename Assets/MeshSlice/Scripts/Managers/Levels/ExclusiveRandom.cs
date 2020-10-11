using System;

namespace MeshSlice
{
  public class ExlusiveRandom<T>
  {
    private Random random;
    private int nextIndex;
    
    private readonly T[] elements;

    public ExlusiveRandom(T[] items)
    {
      random = new System.Random();
      elements = items;
    }

    public T GetNext()
    {
      if (nextIndex == elements.Length)
      {
        Shuffle();
        nextIndex = 0;
      }

      return elements[nextIndex++];
    }

    private void Shuffle()
    {
      int n = elements.Length;
      while (n > 1)
      {
        int k = random.Next(n--);
        T temp = elements[n];
        elements[n] = elements[k];
        elements[k] = temp;
      }
    }
  }
}
