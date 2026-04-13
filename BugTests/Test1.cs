using Microsoft.VisualStudio.TestTools.UnitTesting;
using BugPro;

namespace BugTests;

[TestClass]
public class BugTests
{
    [TestMethod]
    public void InitialState_IsNew()
    {
        var bug = new Bug("BUG-001", "Test");
        Assert.AreEqual(Bug.State.New, bug.GetState());
    }

    [TestMethod]
    public void Assign_FromNew_StateBecomesAssigned()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        Assert.AreEqual(Bug.State.Assigned, bug.GetState());
    }

    [TestMethod]
    public void Start_FromAssigned_StateBecomesInProgress()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Start();
        Assert.AreEqual(Bug.State.InProgress, bug.GetState());
    }

    [TestMethod]
    public void Fix_FromInProgress_StateBecomesFixed()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Start();
        bug.Fix();
        Assert.AreEqual(Bug.State.Fixed, bug.GetState());
    }

    [TestMethod]
    public void Test_FromFixed_StateBecomesTested()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Start();
        bug.Fix();
        bug.Test();
        Assert.AreEqual(Bug.State.Tested, bug.GetState());
    }

    [TestMethod]
    public void Close_FromTested_StateBecomesClosed()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Start();
        bug.Fix();
        bug.Test();
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.GetState());
    }

    [TestMethod]
    public void Reopen_FromClosed_StateBecomesReopened()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Start();
        bug.Fix();
        bug.Test();
        bug.Close();
        bug.Reopen();
        Assert.AreEqual(Bug.State.Reopened, bug.GetState());
    }

    [TestMethod]
    public void Assign_FromReopened_StateBecomesAssigned()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Start();
        bug.Fix();
        bug.Test();
        bug.Close();
        bug.Reopen();
        bug.Assign("Petrov");
        Assert.AreEqual(Bug.State.Assigned, bug.GetState());
    }

    [TestMethod]
    public void Start_FromNew_StateRemainsNew()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Start();
        Assert.AreEqual(Bug.State.New, bug.GetState());
    }

    [TestMethod]
    public void Fix_FromAssigned_StateRemainsAssigned()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Fix();
        Assert.AreEqual(Bug.State.Assigned, bug.GetState());
    }

    [TestMethod]
    public void Close_FromInProgress_StateRemainsInProgress()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Start();
        bug.Close();
        Assert.AreEqual(Bug.State.InProgress, bug.GetState());
    }

    [TestMethod]
    public void Reopen_FromTested_StateRemainsTested()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Start();
        bug.Fix();
        bug.Test();
        bug.Reopen();
        Assert.AreEqual(Bug.State.Tested, bug.GetState());
    }

    [TestMethod]
    public void FullWorkflow_CompletesSuccessfully()
    {
        var bug = new Bug("BUG-001", "Test");
        
        Assert.AreEqual(Bug.State.New, bug.GetState());
        bug.Assign("Ivanov");
        Assert.AreEqual(Bug.State.Assigned, bug.GetState());
        bug.Start();
        Assert.AreEqual(Bug.State.InProgress, bug.GetState());
        bug.Fix();
        Assert.AreEqual(Bug.State.Fixed, bug.GetState());
        bug.Test();
        Assert.AreEqual(Bug.State.Tested, bug.GetState());
        bug.Close();
        Assert.AreEqual(Bug.State.Closed, bug.GetState());
        bug.Reopen();
        Assert.AreEqual(Bug.State.Reopened, bug.GetState());
        bug.Assign("Petrov");
        Assert.AreEqual(Bug.State.Assigned, bug.GetState());
    }

    [TestMethod]
    public void Assign_MultipleTimes_StateRemainsAssigned()
    {
        var bug = new Bug("BUG-001", "Test");
        bug.Assign("Ivanov");
        bug.Assign("Petrov");
        Assert.AreEqual(Bug.State.Assigned, bug.GetState());
    }

    [TestMethod]
    public void GetState_ReturnsCurrentState()
    {
        var bug = new Bug("BUG-001", "Test");
        Assert.AreEqual(Bug.State.New, bug.GetState());
    }
}