import services from '../../services'

module.exports = (app) => {
    app.post('/api/accounts/', async(req, res) => {

        const response =  await services.AccountService.create(req.body)

        res.status(response.statusCode).set(response)
    })

    app.put('/api/accounts/:id', async(req, res) => {

        const { id } = req.params
        const { firstName, lastName, address, age } = req.body

        const request = {
            id,
            firstName,
            lastName,
            address,
            age
        }
        const response =  await services.AccountService.update(request)

        res.status(response.statusCode).set(response)
    })

    app.get('/api/accounts/:id', async(req, res) => {

        const { id } = req.params

        const response =  await services.AccountService.getById(id)

        res.status(response.statusCode).set(response)
    })

    app.delete('/api/accounts/:id', async(req, res) => {

        const { id } = req.params

        const response =  await services.AccountService.deleteAndUpdate(id)

        res.status(response.statusCode).set(response)
    })
}