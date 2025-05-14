[ApiController]
[Route("api")]
public class ExpenseController : ControllerBase
{
    private readonly FinanceService _financeService;

    public ExpenseController(FinanceService financeService)
    {
        _financeService = financeService;
    }

    [HttpPost("addexpense")]
    public IActionResult AddExpense([FromForm] ExpenseRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var expense = _financeService.AddExpense(request.Amount, request.Category, request.Date);
        var expenses = _financeService.GetAllExpenses();
        
        return Ok(expenses);
    }
}