using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add three items with different priorities.
    // Expected Result: The item with the highest priority should be returned first.
    // Defect(s) Found:
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 1);
        priorityQueue.Enqueue("Sue", 5);
        priorityQueue.Enqueue("Tim", 3);

        string result = priorityQueue.Dequeue();

        Assert.AreEqual("Sue", result);
    }

    [TestMethod]
    // Scenario: Add three items where two have the same highest priority.
    // Expected Result: The first item with the highest priority should be returned.
    // Defect(s) Found:
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("Bob", 5);
        priorityQueue.Enqueue("Sue", 5);
        priorityQueue.Enqueue("Tim", 2);

        string result = priorityQueue.Dequeue();

        Assert.AreEqual("Bob", result);
    }

}