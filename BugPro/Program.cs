using Stateless;

namespace BugPro;

public class BugInfo
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? AssignedTo { get; set; }
}

public class Bug
{
    public enum State
    {
        New,
        Assigned,
        InProgress,
        Fixed,
        Tested,
        Closed,
        Reopened
    }

    public enum Trigger
    {
        Assign,
        Start,
        Fix,
        Test,
        Close,
        Reopen
    }

    private readonly StateMachine<State, Trigger> _machine;
    private readonly BugInfo _info;

    public Bug(string title, string description)
    {
        _info = new BugInfo { Title = title, Description = description };
        _machine = new StateMachine<State, Trigger>(State.New);

        _machine.Configure(State.New)
            .Permit(Trigger.Assign, State.Assigned);

        _machine.Configure(State.Assigned)
            .Permit(Trigger.Start, State.InProgress);

        _machine.Configure(State.InProgress)
            .Permit(Trigger.Fix, State.Fixed);

        _machine.Configure(State.Fixed)
            .Permit(Trigger.Test, State.Tested);

        _machine.Configure(State.Tested)
            .Permit(Trigger.Close, State.Closed);

        _machine.Configure(State.Closed)
            .Permit(Trigger.Reopen, State.Reopened);

        _machine.Configure(State.Reopened)
            .Permit(Trigger.Assign, State.Assigned);
    }

    public State GetState() => _machine.State;

    public void Assign(string assignee)
    {
        if (_machine.CanFire(Trigger.Assign))
        {
            _info.AssignedTo = assignee;
            _machine.Fire(Trigger.Assign);
        }
    }

    public void Start()
    {
        if (_machine.CanFire(Trigger.Start))
            _machine.Fire(Trigger.Start);
    }

    public void Fix()
    {
        if (_machine.CanFire(Trigger.Fix))
            _machine.Fire(Trigger.Fix);
    }

    public void Test()
    {
        if (_machine.CanFire(Trigger.Test))
            _machine.Fire(Trigger.Test);
    }

    public void Close()
    {
        if (_machine.CanFire(Trigger.Close))
            _machine.Fire(Trigger.Close);
    }

    public void Reopen()
    {
        if (_machine.CanFire(Trigger.Reopen))
            _machine.Fire(Trigger.Reopen);
    }

    public void PrintInfo()
    {
        Console.WriteLine($"\nБаг: {_info.Title}");
        Console.WriteLine($"Описание: {_info.Description}");
        Console.WriteLine($"Исполнитель: {_info.AssignedTo ?? "не назначен"}");
        Console.WriteLine($"Состояние: {_machine.State}\n");
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ РАБОТЫ БАГА ===\n");

        var bug = new Bug("BUG-001", "Ошибка при входе в систему");
        bug.PrintInfo();

        bug.Assign("Иванов И.И.");
        bug.PrintInfo();

        bug.Start();
        bug.PrintInfo();

        bug.Fix();
        bug.PrintInfo();

        bug.Test();
        bug.PrintInfo();

        bug.Close();
        bug.PrintInfo();

        bug.Reopen();
        bug.PrintInfo();

        bug.Assign("Петров П.П.");
        bug.PrintInfo();

        bug.Start();
        bug.PrintInfo();

        bug.Fix();
        bug.PrintInfo();

        bug.Test();
        bug.PrintInfo();

        bug.Close();
        bug.PrintInfo();

        Console.WriteLine("=== ДЕМОНСТРАЦИЯ ЗАВЕРШЕНА ===");
    }
}