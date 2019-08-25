import * as repository from '../../repository'

export default (app) => {
    app.post('/api/accounts/', (req, res) => {

        const response =  await repository.AccountRepository.create(req.body)

        res.status(response.statusCode).set(response)
    })
}