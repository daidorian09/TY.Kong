import express from 'express'
import morgan from 'morgan'
import bodyParser from'body-parser'
import mongoose from 'mongoose'
import logger from './utils/logger/logger'
import path from 'path'
import fs from 'fs'

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

//MongoDb connectionstring
const db = process.env.CONNECTIONSTRING
mongoose.Promise = global.Promise
console.log('connectionString', process.env.CONNECTIONSTRING)

// Connect to MongoDB
mongoose
  .connect(db, { useNewUrlParser: true })
  .then(() => logger.info('MongoDB connected'))
  .catch(err => logger.error(err));

//Bind all models
const modelsPath = path.join(__dirname, 'models')
fs.readdirSync(modelsPath).forEach((file) => {
  require(path.join(modelsPath, file))
})
  
// Bind all api endpoints
require('./routes')(app)

const port = process.env.PORT

if (!port) {
    logger.error('Port is missing')
} else {
    // Start the app
    app.listen(port, () => {
        logger.info(`🚀 Bank Persistent App is now running on http://localhost:${port}/`)
    })
}