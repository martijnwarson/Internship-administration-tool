# 1920-team1-frontend

Frontend using API from project 1920-team1-backend 

Clone project onto your own computer.
- Move to the ITProject map
- use npm install
- ng serve when done

You will now be connected with the deployed back-end on a local front-end

Note for setting up.

- Always git pull
- npm install is required since /node.modules is not pushed into GIT

When working on issues.

- Before merging always use  #git pull -rebase this will get the latest changes into your environment and puts your commits ontop of the latest version.

Deployed Url:

http://itproject1.s3-website.eu-west-3.amazonaws.com/

Webhook connected to MASTER branch.

Each push will trigger a CodePipeline including an AWS Codebuild. If the build is approved, a deploy will be triggered to an S3 Bucket.
