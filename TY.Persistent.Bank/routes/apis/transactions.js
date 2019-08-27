import services from '../../services'

module.exports = (app) => {
    app.post('/api/transactions/', async(req, res) => {
  
        const response =  await services.TransactionService.createTransaction(req.body)

        return res.status(response.statusCode).json(response)
    })

    app.put('/api/transactions/:id', async(req, res) => {

        const { id } = req.params
        const { account, amount, oldBalance, currentBalance, transactionType } = req.body

        const request = {
            id,
            account,
            amount,
            oldBalance,
            currentBalance,
            transactionType
        }
        const response =  await services.TransactionService.updateTransaction(request)

        return res.status(response.statusCode).json(response)
    })

    app.get('/api/transactions/:id', async(req, res) => {

        const { id } = req.params

        const response =  await services.TransactionService.getTransactionById(id)

        return res.status(response.statusCode).json(response)
    })

    app.delete('/api/transactions/:id', async(req, res) => {

        const { id } = req.params

        const response =  await services.TransactionService.deleteAndUpdateTransaction(id)

        return res.status(response.statusCode).json(response)
    })
}