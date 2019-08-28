import services from '../../services'

module.exports = (app) => {
    app.post('/api/accounts/', async(req, res) => {
  
        const response =  await services.AccountService.createAccount(req.body)

        return res.status(response.statusCode).json(response)
    })

    app.put('/api/accounts/:id', async(req, res) => {

        const { id } = req.params
        const { firstName, lastName, address, age } = req.body

        const request = {
            id,
            data : {
                firstName,
                lastName,
                address,
                age
            }            
        }

        const response =  await services.AccountService.updateAccount(request)

        return res.status(response.statusCode).json(response)
    })

    app.get('/api/accounts/:id', async(req, res) => {

        const { id } = req.params

        const response =  await services.AccountService.getAccountById(id)

        return res.status(response.statusCode).json(response)
    })

    app.delete('/api/accounts/:id', async(req, res) => {

        const { id } = req.params

        const response =  await services.AccountService.deleteAndUpdateAccount(id)

        return res.status(response.statusCode).json(response)
    })

    app.put('/api/accounts/:id/balance', async(req, res) => {

        const { id } = req.params
        const { balance } = req.body

        const request = {
            id,
            data : {
                balance
            }            
        }

        const response =  await services.AccountService.updateAccount(request)

        return res.status(response.statusCode).json(response)
    })
}