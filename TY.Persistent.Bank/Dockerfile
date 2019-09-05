FROM node:10

#Maintaner
LABEL Maintaner="yigit.at@trendyol" 

# Create app directory
WORKDIR /usr/src/app

# Install app dependencies
# A wildcard is used to ensure both package.json AND package-lock.json are copied
# where available (npm@5+)
COPY package*.json ./

RUN npm install
# If you are building your code for production
# RUN npm ci --only=production

# Bundle app source
COPY . .

#Set env
ENV PORT=7001
ENV CONNECTIONSTRING=mongodb://localhost:27017/bankDb

EXPOSE 7001

#Healthcheck
HEALTHCHECK --interval=5s --timeout=3s --retries=3 CMD curl --fail http://localhost:7001/api/healthcheck/readiness/ || exit 1

CMD [ "npm", "start" ]