namespace BudgetService;

public class BudgetRepo : IBudgetRepo
{
    public List<Budget> GetAll()
    {
        return new List<Budget>()
        {
            new Budget()
            {
                YearMonth = "202210",
                Amount = 3100
            },
            new Budget()
            {
                YearMonth = "202211",
                Amount = 300
            },
            new Budget()
            {
                YearMonth = "202212",
                Amount = 31
            }
        };
    }
}