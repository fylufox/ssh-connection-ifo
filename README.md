# ssh-connection-ifo
起動したLinuxサーバのログイン情報をSlackに通知するAPI。  
API Gateway + LambdaでAPIを提供。  
```
server - (API Request) → API Gateway → Lambda Function → Slack Webhook
```
リクエスト
```
{
	Name:"Server Name",
	Address:"IP Address",
	Username:"User Name"
}
```
