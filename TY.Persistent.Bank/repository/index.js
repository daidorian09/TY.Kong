import { createAccount, updateAccount, getAccountById, deleteAndUpdateAccount, getAccountByEmail } from './accountRepository'
import { createTransaction, deleteAndUpdateTransaction, getTransactionById, updateTransaction } from './transactionRepository'

const repository = Object.freeze({
    AccountRepository : {
        createAccount,
        updateAccount,
        getAccountById,
        deleteAndUpdateAccount,
        getAccountByEmail
    },
    TransactionRepository : {
        createTransaction,
        deleteAndUpdateTransaction,
        getTransactionById,
        updateTransaction
    }
})

export default repository