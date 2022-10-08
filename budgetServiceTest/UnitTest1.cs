using BudgetService;
using FluentAssertions;
using NSubstitute;

namespace budgetServiceTest;

public class Tests
{
    private BudgetService.BudgetService _budgetService;
    private IBudgetRepo? _budgetRepo;

    [SetUp]
    public void Setup()
    {
        _budgetRepo = Substitute.For<IBudgetRepo>();

        _budgetService = new BudgetService.BudgetService(_budgetRepo);
    }

    [Test]
    public void Budget_get_full_month()
    {
        var start = new DateTime(2022, 10, 01);
        var end = new DateTime(2022, 10, 31);
        _budgetRepo.GetAll().Returns(new List<Budget>
        {
            new Budget()
            {
                YearMonth = "202210",
                Amount = 3100
            }
        });
        var result = _budgetService.Query(start,end);
        result.Should().Be(3100);
    }
}