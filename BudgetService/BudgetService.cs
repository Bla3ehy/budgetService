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
        if (start.Month != end.Month)
        {
            var startDate = DateTime.ParseExact(start.ToString("yyyyMM") + "01", "yyyyMMdd", CultureInfo.CurrentCulture);
            var endDate = DateTime.ParseExact(end.ToString("yyyyMM") + "01", "yyyyMMdd", CultureInfo.CurrentCulture);
            var budgetList = budgets.Where(o =>
                    startDate <= DateTime.ParseExact(o.YearMonth + "01", "yyyyMMdd", CultureInfo.CurrentCulture)
                    && endDate >= DateTime.ParseExact(o.YearMonth + "01", "yyyyMMdd",
                        CultureInfo.CurrentCulture))
                .ToList();

            foreach (var budget in budgetList)
            {
                if (budget.YearMonth == start.ToString("yyyyMM"))
                {
                    result += budget.Amount / DateTime.DaysInMonth(start.Year, start.Month) *
                              (DateTime.DaysInMonth(start.Year, start.Month) - start.Day + 1);
                    continue;
                }

                if (budget.YearMonth == end.ToString("yyyyMM"))
                {
                    result += budget.Amount / DateTime.DaysInMonth(end.Year, end.Month) * end.Day;
                    continue;
                }

                result += budget.Amount;
            }

            return result;
        }

        if (start.Day >= 1 || end.Day <= DateTime.DaysInMonth(end.Year, end.Month))
        {
            var monthBudget = budgets.Where(o =>
                    start == DateTime.ParseExact(o.YearMonth + "01", "yyyyMMdd", CultureInfo.CurrentCulture))
                .Select(o => Convert.ToDecimal(o.Amount))
                .First();

            result = monthBudget / DateTime.DaysInMonth(end.Year, end.Month) * (end.Day - start.Day + 1);
        }

        if (start.Day == 1 && end.Day == DateTime.DaysInMonth(end.Year, end.Month))
        {
            result = budgets.Where(o =>
                    start == DateTime.ParseExact(o.YearMonth + "01", "yyyyMMdd", CultureInfo.CurrentCulture))
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