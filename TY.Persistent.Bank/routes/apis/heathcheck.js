module.exports =  (app) => {

    app.get('/api/healthcheck/readiness', (req, res) => {
        res.status(200).json({message: 'app is up & run'})
    })
    
}