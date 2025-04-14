/// <summary>
/// Abstract base class for all bank operations that require execution.
/// </summary>
namespace FinanceApp.Models
{
    public abstract class BankOperation
    {
       
        public abstract void Execute();
    }
}