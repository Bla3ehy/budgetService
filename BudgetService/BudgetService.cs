using System.Globalization;

namespace BudgetService;

public class BudgetService
{
    private IBudgetRepo _budgetRepo;
    public BudgetService(IBudgetRepo repo)
    {
        _budgetRepo = repo;
    }

    public decimal Query(DateTime start, DateTime end)
    {
        var budgets = _budgetRepo.GetAll();
        var result = decimal.Zero;
        
        if (start.Day == 1 && end.Day == DateTime.DaysInMonth(end.Year, end.Month))
        {
            
           result = budgets.Where(o => start == DateTime.ParseExact(o.YearMonth+"01","yyyyMMdd",CultureInfo.CurrentCulture))
               .Select(o => Convert.ToDecimal(o.Amount))
               .First();
        }

        return result;
    }
}

public interface IBudgetRepo
{
    public List<Budget> GetAll();
}

public class Budget
{
    public string YearMonth { get; set; }
    public int Amount { get; set; }
}