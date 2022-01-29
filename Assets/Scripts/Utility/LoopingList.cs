using System;
using System.Collections;
using System.Collections.Generic;


//Utility Data Structure
//Looping list is a wrapper class for List.
//Calling next will step the current index to the next index.
//If the current index is at the end of the list, it will instead
//step down to 0.

[Serializable]
public class LoopingList<T>
{
    private int currentIndex = 0;
    public List<T> list; //the internal list

    //Advance current index. Return next index's value.
    public int Next()
    {
        //If the list is empty, return -1
        if (list.Count == 0)
        {
            return -1;
        }

        //if currentIndex is at the end of the list,
        //bring it back to index 0.
        if (currentIndex >= list.Count - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }

        return currentIndex;
    }

    //Get the value in the current index
    public T Current()
    {
        return list[currentIndex];
    }


    public void resetCurrentIndex()
    {
        currentIndex = 0;
    }

    //currentIndex getter
    public int CurrentIndex
    {
        get { return currentIndex; }
    }

}
