using System;
using System.Collections.Generic;
using System.Text;

namespace MyFirstDemo
{
    /// <summary>
    /// 所索引器实例
    /// </summary>
    public class IndexerExample
    {
        private string[] nameList = new string[size];
        private static int size = 10; 
        public IndexerExample()
        {
            for(var i=0;i<size;i++)
            {
                nameList[i] = ".N.A";
            }
        }

        public string this[int index]
        {
            get
            {
                string temp;
                if (index >= 0 && index < size - 1)
                {
                    temp = nameList[index];
                }
                else
                {
                    temp = string.Empty;
                }
                return (temp);
            }
            set
            {
                if (index >= 0 && index < size - 1)
                {
                    nameList[index] = value;
                }
            }
        }
    }

    public class IndexerExampleFor6<T>
    {
        private T[] arr = new T[100];
        int nextIndex = 0;
        public T this[int i]=>arr[i];
        public void Add(T Value)
        {
            if (nextIndex >= arr.Length)
            {
                throw new IndexOutOfRangeException($"The collection can hold only {arr.Length} elements");
            }
            arr[nextIndex++] = Value;
        }
    }

    public class IndexerExample7<T>
    {
        private T[] arr = new T[100];
        public T this[int i]
        {
            get => arr[i];
            set => arr[i] = value;
        }
    }
}
