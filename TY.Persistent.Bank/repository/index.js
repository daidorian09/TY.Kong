import { createAccount, updateAccount, getAccountById, deleteAndUpdateAccount } from './accountRepository'
import { createTransaction, deleteAndUpdateTransaction, getTransactionById, updateTransaction } from './transactionRepository'

const repository = Object.freeze({
    AccountRepository : {
        createAccount,
        updateAccount,
        getAccountById,
        deleteAndUpdateAccount
    },
    TransactionRepository : {
        createTransaction,
        deleteAndUpdateTransaction,
        getTransactionById,
        updateTransaction
    }
})

export default repository