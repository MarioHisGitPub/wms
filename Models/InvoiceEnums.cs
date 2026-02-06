namespace MyConsoleApp.models
{
    public enum SupplierInvoiceStatus
    {
        Pending,   // ← Add this
        Draft,
        Approved,
        SentToERP,
        Paid,
        Cancelled
    }
    
    public enum CustomerInvoiceStatus
    {
        Draft,
        Sent,
        PartiallyPaid,
        Paid,
        Overdue,
        Cancelled,
        Refunded
    }
    
    public enum PaymentMethod
    {
        Cash,
        CreditCard,
        DebitCard,
        BankTransfer,
        Check,
        PayPal,
        Other
    }
    
    public enum PaymentStatus
    {
        Pending,
        Completed,
        Failed,
        Refunded,
        Cancelled
    }
}