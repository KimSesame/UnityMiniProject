using System;
using System.Collections.Generic;
using UnityEditor;

public class MonsterContatiner
{
    private List<Monster> container = new List<Monster>();

    public int Count => container.Count;

    public void Insert(Monster item)
    {
        container.Add(item);
        HeapifyUp(container.Count - 1);
    }

    public Monster ExtractMax()
    {
        if (container.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        Monster max = container[0];
        container[0] = container[container.Count - 1];
        container.RemoveAt(container.Count - 1);
        HeapifyDown(0);

        return max;
    }

    public Monster PeekMax()
    {
        if (container.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        return container[0];
    }

    public void RemoveAt(int index)
    {
        if (index < 0 || index >= container.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range");

        if (index == container.Count - 1)
        {
            container.RemoveAt(index);
        }
        else
        {
            container[index] = container[container.Count - 1];
            container.RemoveAt(container.Count - 1);

            if (index > 0 && container[index].CompareTo(container[(index - 1) / 2]) > 0)
            {
                HeapifyUp(index);
            }
            else
            {
                HeapifyDown(index);
            }
        }
    }

    public int FindIndex(Monster element)
    {
        for (int i = 0; i < container.Count; i++)
        {
            if (container[i].Equals(element))
                return i;
        }
        return -1;
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;
            if (container[index].CompareTo(container[parentIndex]) <= 0)
                break;

            Swap(index, parentIndex);
            index = parentIndex;
        }
    }

    private void HeapifyDown(int index)
    {
        while (index < container.Count)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;
            int largest = index;

            if (leftChild < container.Count && container[leftChild].CompareTo(container[largest]) > 0)
                largest = leftChild;

            if (rightChild < container.Count && container[rightChild].CompareTo(container[largest]) > 0)
                largest = rightChild;

            if (largest == index)
                break;

            Swap(index, largest);
            index = largest;
        }
    }

    private void Swap(int index1, int index2)
    {
        Monster temp = container[index1];
        container[index1] = container[index2];
        container[index2] = temp;
    }
}
