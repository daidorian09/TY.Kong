import { createAccount, deleteAndUpdateAccount, getAccountById, updateAccount, getAccountByEmail } from './accountService'
import { createTransaction, deleteAndUpdateTransaction, getTransactionById, updateTransaction } from './transactionService'

const services = Object.freeze({
    AccountService : {
        createAccount,
        deleteAndUpdateAccount,
        updateAccount,
        getAccountById,
        getAccountByEmail
    },
    TransactionService : {
        createTransaction,
        deleteAndUpdateTransaction,
        updateTransaction,
        getTransactionById
    }
})

export default services