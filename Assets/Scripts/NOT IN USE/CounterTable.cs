//using System.Collections;
//using System.Collections.Generic;
//using System;
//using UnityEngine;

//public class Counter : IComparable<Counter>
//{
//    public Element myType;
//    public Element iCounter;

//    public Counter(Element newMyType, Element newICounter)
//    {
//        myType = newMyType;
//        iCounter = newICounter;
//    }

//    //This method is required by the IComparable
//    //interface. 
//    public int CompareTo(Counter other)
//    {
//        return 1;
//    }
//}

//public class CounterTable : MonoBehaviour
//{
//    static Dictionary<Element, Element> counters = new Dictionary<Element, Element>();
//    Counter Fire = new Counter(Element.Fire, Element.Earth);
//    Counter Earth = new Counter(Element.Earth, Element.Air);
//    Counter Air = new Counter(Element.Air, Element.Ice);
//    Counter Ice = new Counter(Element.Ice, Element.Fire);
//}