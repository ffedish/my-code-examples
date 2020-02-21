using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics;

namespace New_Semestr
{
    public class Example
    {
        static long McrsftSpd;
        static long MLSpd;
        static int AddLastCount = 1000000;
        static int AddFirstCount = 1000000;
        static int RemoveBordersCount = 300000;

        public static void Main()
        {
            MicrosoftList_BaseTest();
            MyList_BaseTest();

            Console.WriteLine("press any key to continue");
            Console.ReadKey();
            Console.Clear();

            MicrosoftList_PerformanceTest();
            MyList_PerformanceTest();

            Console.WriteLine("press any key to continue");
            Console.ReadKey();
            Console.Clear();

            CorrectCollection(new LinkedList<string>());
            CorrectCollection(new MyLinkedList<string>());
        }

        static private void CorrectCollection(ICollection<string> insert)
        {
            Console.WriteLine(insert.GetType() + " is being tested\n");
            insert.Clear();
            if (insert.Count != 0)
                throw new Exception("incorrect collection");

            insert.Add("first value");
            insert.Add("second value");
            insert.Add("third value");

            if (insert.Count != 3)
                throw new Exception("incorrect collection");

            if (!insert.Contains("first value"))
                throw new Exception("incorrect collection");

            insert.Remove("first value");
            if (insert.Count != 2)
                throw new Exception("incorrect collection");

            if (insert.Contains("first value"))
                throw new Exception("incorrect collection");

            var c = new string[2];

            insert.CopyTo(c, 0);
            if (!(c[0] == "second value" && c[1] == "third value"))
                throw new Exception("incorrect collection");

            Console.WriteLine("this collection completed tests correctly\n=============================\n");
        }

        private static void MyList_PerformanceTest()
        {
            Console.WriteLine("Start performance test My List");
            var s = new Stopwatch();
            s.Start();

            MyLinkedList<string> tested = new MyLinkedList<string>();

            for (int i = 0; i < AddLastCount; i++)
                tested.AddLast("gjcjcb vjq gbcjc chfysq xktyjcjc? hjn ndjq t,fk z gjrf ns gjk eyjq cnjzk");

            for (int i = 0; i < AddFirstCount; i++)
                tested.AddFirst("gjcjcb vjq gbcjc chfysq xktyjcjc? hjn ndjq t,fk z gjrf ns gjk eyjq cnjzk");

            for (int i = 0; i < RemoveBordersCount; i++)
            {
                tested.RemoveLast();
                tested.RemoveFirst();
            }
            MLSpd = s.ElapsedMilliseconds;
            s.Stop();

            Console.WriteLine("Test completed, duration:");
            Console.WriteLine(MLSpd + " ms");
            Console.WriteLine();
            if (tested.Count == AddLastCount + AddFirstCount - 2 * RemoveBordersCount)
                Console.WriteLine("My list Count is correct");
            Console.WriteLine("=================================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("performance difference: " + (MLSpd - McrsftSpd) / (double)McrsftSpd * 100 + " %");
            Console.WriteLine();
            Console.WriteLine();

        }

        private static void MicrosoftList_PerformanceTest()
        {
            Console.WriteLine("Start performance test Microsoft List");
            var s = new Stopwatch();
            s.Start();

            LinkedList<string> tested = new LinkedList<string>();

            for (int i = 0; i < AddLastCount; i++)
                tested.AddLast("gjcjcb vjq gbcjc chfysq xktyjcjc? hjn ndjq t,fk z gjrf ns gjk eyjq cnjzk");

            for (int i = 0; i < AddFirstCount; i++)
                tested.AddFirst("gjcjcb vjq gbcjc chfysq xktyjcjc? hjn ndjq t,fk z gjrf ns gjk eyjq cnjzk");

            for (int i = 0; i < RemoveBordersCount; i++)
            {
                tested.RemoveLast();
                tested.RemoveFirst();
            }
            McrsftSpd = s.ElapsedMilliseconds;
            s.Stop();

            Console.WriteLine("Test completed, duration:");
            Console.WriteLine(McrsftSpd + " ms");
            Console.WriteLine();
            Console.WriteLine();
            if (tested.Count == AddLastCount + AddFirstCount - 2 * RemoveBordersCount)
                Console.WriteLine("Microsoft list Count is correct");
            Console.WriteLine("=================================================");
            Console.WriteLine();
        }

        public static void MyList_BaseTest()
        {
            Console.WriteLine("My list base test started");
            Console.WriteLine("=================================================");
            // Create the link list.
            string[] words =
                { "the", "fox", "jumps", "over", "the", "dog" };
            MyLinkedList<string> sentence = new MyLinkedList<string>(words);
            Display(sentence, "The linked list values:");
            Console.WriteLine("sentence.Contains(\"jumps\") = {0}",
                sentence.Contains("jumps"));

            // Add the word 'today' to the beginning of the linked list.
            sentence.AddFirst("today");
            Display(sentence, "Test 1: Add 'today' to beginning of the list:");

            // Move the first node to be the last node.
            MyLinkedListNode<string> mark1 = sentence.First;
            sentence.RemoveFirst();
            sentence.AddLast(mark1);
            Display(sentence, "Test 2: Move first node to be last node:");

            // Change the last node to 'yesterday'.
            sentence.RemoveLast();
            sentence.AddLast("yesterday");
            Display(sentence, "Test 3: Change the last node to 'yesterday':");

            // Move the last node to be the first node.
            mark1 = sentence.Last;
            sentence.RemoveLast();
            sentence.AddFirst(mark1);
            Display(sentence, "Test 4: Move last node to be first node:");

            // Indicate the last occurence of 'the'.
            sentence.RemoveFirst();
            MyLinkedListNode<string> current = sentence.FindLast("the");
            IndicateNode(current, "Test 5: Indicate last occurence of 'the':");

            // Add 'lazy' and 'old' after 'the' (the LinkedListNode named current).
            sentence.AddAfter(current, "old");
            sentence.AddAfter(current, "lazy");
            IndicateNode(current, "Test 6: Add 'lazy' and 'old' after 'the':");

            // Indicate 'fox' node.
            current = sentence.Find("fox");
            IndicateNode(current, "Test 7: Indicate the 'fox' node:");

            // Add 'quick' and 'brown' before 'fox':
            sentence.AddBefore(current, "quick");
            sentence.AddBefore(current, "brown");
            IndicateNode(current, "Test 8: Add 'quick' and 'brown' before 'fox':");

            // Keep a reference to the current node, 'fox',
            // and to the previous node in the list. Indicate the 'dog' node.
            mark1 = current;
            MyLinkedListNode<string> mark2 = current.Previous;
            current = sentence.Find("dog");
            IndicateNode(current, "Test 9: Indicate the 'dog' node:");

            // The AddBefore method throws an InvalidOperationException
            // if you try to add a node that already belongs to a list.
            Console.WriteLine("Test 10: Throw exception by adding node (fox) already in the list:");
            try
            {
                sentence.AddBefore(current, mark1);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Exception message: {0}", ex.Message);
            }
            Console.WriteLine();

            // Remove the node referred to by mark1, and then add it
            // before the node referred to by current.
            // Indicate the node referred to by current.
            sentence.Remove(mark1);
            sentence.AddBefore(current, mark1);
            IndicateNode(current, "Test 11: Move a referenced node (fox) before the current node (dog):");

            // Remove the node referred to by current.
            sentence.Remove(current);
            IndicateNode(current, "Test 12: Remove current node (dog) and attempt to indicate it:");

            // Add the node after the node referred to by mark2.
            sentence.AddAfter(mark2, current);
            IndicateNode(current, "Test 13: Add node removed in test 11 after a referenced node (brown):");

            // The Remove method finds and removes the
            // first node that that has the specified value.
            sentence.Remove("old");
            Display(sentence, "Test 14: Remove node that has the value 'old':");

            // When the linked list is cast to ICollection(Of String),
            // the Add method adds a node to the end of the list.
            sentence.RemoveLast();
            ICollection<string> icoll = sentence;
            icoll.Add("rhinoceros");
            Display(sentence, "Test 15: Remove last node, cast to ICollection, and add 'rhinoceros':");

            Console.WriteLine("Test 16: Copy the list to an array:");
            // Create an array with the same number of
            // elements as the inked list.
            string[] sArray = new string[sentence.Count];
            sentence.CopyTo(sArray, 0);

            foreach (string s in sArray)
            {
                Console.WriteLine(s);
            }

            // Release all the nodes.
            sentence.Clear();

            Console.WriteLine();
            Console.WriteLine("Test 17: Clear linked list. Contains 'jumps' = {0}",
                sentence.Contains("jumps"));

            Console.WriteLine();
            Console.WriteLine("My list base test finished");
            Console.WriteLine("=================================================");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }

        public static void MicrosoftList_BaseTest()
        {
            Console.WriteLine("Mircosoft list base test started");
            Console.WriteLine("=================================================");
            // Create the link list.
            string[] words =
                { "the", "fox", "jumps", "over", "the", "dog" };
            LinkedList<string> sentence = new LinkedList<string>(words);
            Display(sentence, "The linked list values:");
            Console.WriteLine("sentence.Contains(\"jumps\") = {0}",
                sentence.Contains("jumps"));

            // Add the word 'today' to the beginning of the linked list.
            sentence.AddFirst("today");
            Display(sentence, "Test 1: Add 'today' to beginning of the list:");

            // Move the first node to be the last node.
            LinkedListNode<string> mark1 = sentence.First;
            sentence.RemoveFirst();
            sentence.AddLast(mark1);
            Display(sentence, "Test 2: Move first node to be last node:");

            // Change the last node to 'yesterday'.
            sentence.RemoveLast();
            sentence.AddLast("yesterday");
            Display(sentence, "Test 3: Change the last node to 'yesterday':");

            // Move the last node to be the first node.
            mark1 = sentence.Last;
            sentence.RemoveLast();
            sentence.AddFirst(mark1);
            Display(sentence, "Test 4: Move last node to be first node:");

            // Indicate the last occurence of 'the'.
            sentence.RemoveFirst();
            LinkedListNode<string> current = sentence.FindLast("the");
            IndicateNode(current, "Test 5: Indicate last occurence of 'the':");

            // Add 'lazy' and 'old' after 'the' (the LinkedListNode named current).
            sentence.AddAfter(current, "old");
            sentence.AddAfter(current, "lazy");
            IndicateNode(current, "Test 6: Add 'lazy' and 'old' after 'the':");

            // Indicate 'fox' node.
            current = sentence.Find("fox");
            IndicateNode(current, "Test 7: Indicate the 'fox' node:");

            // Add 'quick' and 'brown' before 'fox':
            sentence.AddBefore(current, "quick");
            sentence.AddBefore(current, "brown");
            IndicateNode(current, "Test 8: Add 'quick' and 'brown' before 'fox':");

            // Keep a reference to the current node, 'fox',
            // and to the previous node in the list. Indicate the 'dog' node.
            mark1 = current;
            LinkedListNode<string> mark2 = current.Previous;
            current = sentence.Find("dog");
            IndicateNode(current, "Test 9: Indicate the 'dog' node:");

            // The AddBefore method throws an InvalidOperationException
            // if you try to add a node that already belongs to a list.
            Console.WriteLine("Test 10: Throw exception by adding node (fox) already in the list:");
            try
            {
                sentence.AddBefore(current, mark1);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Exception message: {0}", ex.Message);
            }
            Console.WriteLine();

            // Remove the node referred to by mark1, and then add it
            // before the node referred to by current.
            // Indicate the node referred to by current.
            sentence.Remove(mark1);
            sentence.AddBefore(current, mark1);
            IndicateNode(current, "Test 11: Move a referenced node (fox) before the current node (dog):");

            // Remove the node referred to by current.
            sentence.Remove(current);
            IndicateNode(current, "Test 12: Remove current node (dog) and attempt to indicate it:");

            // Add the node after the node referred to by mark2.
            sentence.AddAfter(mark2, current);
            IndicateNode(current, "Test 13: Add node removed in test 11 after a referenced node (brown):");

            // The Remove method finds and removes the
            // first node that that has the specified value.
            sentence.Remove("old");
            Display(sentence, "Test 14: Remove node that has the value 'old':");

            // When the linked list is cast to ICollection(Of String),
            // the Add method adds a node to the end of the list.
            sentence.RemoveLast();
            ICollection<string> icoll = sentence;
            icoll.Add("rhinoceros");
            Display(sentence, "Test 15: Remove last node, cast to ICollection, and add 'rhinoceros':");

            Console.WriteLine("Test 16: Copy the list to an array:");
            // Create an array with the same number of
            // elements as the inked list.
            string[] sArray = new string[sentence.Count];
            sentence.CopyTo(sArray, 0);

            foreach (string s in sArray)
            {
                Console.WriteLine(s);
            }

            // Release all the nodes.
            sentence.Clear();

            Console.WriteLine();
            Console.WriteLine("Test 17: Clear linked list. Contains 'jumps' = {0}",
                sentence.Contains("jumps"));

            Console.WriteLine();
            Console.WriteLine("Mircosoft list base test finished");
            Console.WriteLine();

        }

        private static void Display<T>(LinkedList<T> words, string test)
        {
            Console.WriteLine(test);
            foreach (var word in words)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void Display<T>(MyLinkedList<T> words, string test)
        {
            Console.WriteLine(test);
            foreach (var word in words)
            {
                Console.Write(word + " ");
            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private static void IndicateNode(LinkedListNode<string> node, string test)
        {
            Console.WriteLine(test);
            if (node.List == null)
            {
                Console.WriteLine("Node '{0}' is not in the list.\n",
                    node.Value);
                return;
            }

            StringBuilder result = new StringBuilder("(" + node.Value + ")");
            LinkedListNode<string> nodeP = node.Previous;

            while (nodeP != null)
            {
                result.Insert(0, nodeP.Value + " ");
                nodeP = nodeP.Previous;
            }

            node = node.Next;
            while (node != null)
            {
                result.Append(" " + node.Value);
                node = node.Next;
            }

            Console.WriteLine(result);
            Console.WriteLine();
        }

        private static void IndicateNode(MyLinkedListNode<string> node, string test)
        {
            Console.WriteLine(test);
            if (node.List == null)
            {
                Console.WriteLine("Node '{0}' is not in the list.\n",
                    node.Value);
                return;
            }

            StringBuilder result = new StringBuilder("(" + node.Value + ")");
            MyLinkedListNode<string> nodeP = node.Previous;

            while (nodeP != null)
            {
                result.Insert(0, nodeP.Value + " ");
                nodeP = nodeP.Previous;
            }

            node = node.Next;
            while (node != null)
            {
                result.Append(" " + node.Value);
                node = node.Next;
            }

            Console.WriteLine(result);
            Console.WriteLine();
        }
    }
}