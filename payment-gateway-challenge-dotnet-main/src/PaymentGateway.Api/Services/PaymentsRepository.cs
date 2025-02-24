using PaymentGateway.Api.Entities;

namespace PaymentGateway.Api.Services;

public class PaymentsRepository
{
    // I changed the repository to store a collection of Payment entities rather than API response model.
    // We usually wouldn't want the storage of the entity to be coupled with the API contract.
    
    // We may also want a different payment status enum for the Payment Entity which can only be Authorized or Declined,
    // as in theory the Rejected payments should never reach storage. But I'll avoid over-engineering and leave the 
    // one enum.
    private readonly List<Payment> _payments = [];
    
    public void Add(Payment payment)
    {
        _payments.Add(payment);
    }

    /// <summary>
    /// Find or FirstOrDefault are both good.
    /// Find is just useful if we switch to a real database via Entity Framework so we can access via change tracking.
    /// </summary>
    public Payment? Get(Guid id)
    {
        return _payments.Find(p => p.Id == id);
    }
}