namespace BSynchro.RJP.Transactions.Domain.Enums
{
    public enum TransactionTypeEnum
    {
        Credit = 1,           // Amount added to the account
        Debit = 2,            // Amount withdrawn from the account
        TransferIn = 3,       // Funds transferred into this account
        TransferOut = 4,      // Funds transferred from this account
        Refund = 5,           // Refund of a previous transaction
        Fee = 6,              // Service or transaction fee
        Chargeback = 7,       // Chargeback on a previous transaction
        Interest = 8,         // Interest credited to the account
        LoanDisbursement = 9, // Loan amount disbursed to the account
        LoanRepayment = 10,   // Repayment of loan from the account
        Adjustment = 11       // Adjustment made to balance due to errors or corrections
    }
}
