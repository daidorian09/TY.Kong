import express from 'express'
import morgan from 'morgan'

const logger = require('./utils/logger/logger')


// Load environment variables using dotenv
require('dotenv').config({
    path: 'variables.env'
})

const app = express()

// HTTP Requeest Logger
app.use(morgan('combined', { stream: logger.stream }))

// Support application/x-www-form-urlencoded post data or json
app.use(bodyParser.json())
app.use(bodyParser.urlencoded({
    extended: true
}))

const port = process.env.PORT

if (!port) {
    logger.error('Port is missing')
} else {
    // Start the app
    app.listen(port, () => {
        logger.info(`🚀 Bank Persistent App is now running on http://localhost:${port}/`)
    })
}