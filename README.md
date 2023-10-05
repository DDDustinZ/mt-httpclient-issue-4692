# mt-httpclient-issue-4692

## Steps
- Run `docker compose up -d`
- Run Consumer project
- Publish message
```
{
  "messageType": [
    "urn:message:Consumer:TestCommand"
  ],
  "message": {},
}
```