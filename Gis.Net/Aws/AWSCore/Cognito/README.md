# AWSCore

# netAWSCognito

Library to manage AWS Cognito Identity Provider with .Net

## Configure AWS

Run this command to quickly set and view your credentials, Region, and output format. The following example shows sample values.

```bash
aws configure

 AWS Access Key ID [None]: AKIAIOSFODNN7EXAMPLE
 AWS Secret Access Key [None]: wJalrXUtnFEMI/K7MDENG/bPxRfiCYEXAMPLEKEY
 Default region name [None]: us-west-2
 Default output format [None]: json
```

To use the sample application with your Amazon Cognito user pool, just make the necessary changes to the following properties in the `appsettings.json` file (or create file `input.json` to configure user-secrets):

```json
{
    "AWS_REGION": "<REGION>",
    "AWS_PROFILE": "default",
    "AWS_ACCESS_KEY_ID": "xxxxxxxxxxxxxxxxxxx",
    "AWS_SECRET_ACCESS_KEY": "xxxxxxxxxxxxxxxxxxx",
    "AWS_USERPOOLID": "xxxxxxxxxxxxxxxxxxx",
    "AWS_USERPOOLCLIENTID": "xxxxxxxxxxxxxxxxxxx",
    "AWS_USERPOOLCLIENTSECRET": "",
    "AWS_ISSUER": "https://cognito-idp.eu-central-1.amazonaws.com/{AWS_USERPOOLID}"
}
```

**It is highly recommended to configure *user secrets* instead of appsettings.json**.
Create a json file with the AWS settings as above and run this command:

```bash
cat ./input.json | dotnet user-secrets set
```

### Configure AWS on Docker

To create temporary read-only volume in docker-compose.yaml with default profile in ~/.aws/credentials file.

```yaml
version: '3'

services:
  service-name:
    image: docker-image-name:latest
    volumes:
      - ~/.aws/:/root/.aws:ro
```

or use environment variable with .env file:

```yaml
services:
  web:
    build: .
    environment:
      - AWS_ACCESS_KEY_ID=${AWS_ACCESS_KEY_ID}
      - AWS_SECRET_ACCESS_KEY=${AWS_SECRET_ACCESS_KEY}
      - AWS_DEFAULT_REGION=${AWS_DEFAULT_REGION}
```

## Configure ASP.Net Core Application

To upgrade an existing web application to use Amazon Cognito as the Identity provider, you need to add the following NuGet dependencies to your ASP.NET Core web application:

- [Amazon.AspNetCore.Identity.Cognito](https://www.nuget.org/packages/Amazon.AspNetCore.Identity.Cognito/)
- [Amazon.Extensions.CognitoAuthentication](https://www.nuget.org/packages/Amazon.Extensions.CognitoAuthentication/)
- *netAWSCognito* Package or add project reference to solution

### Dependency Injection


### Controllers

Apply policies to Controllers with the policy name:

```C#

using TeamSviluppo.AWS.Cognito;

namespace MyProject.Controllers
{
    public class AuthController : AccountController
    {
        public AuthController(IAWSCognitoServiceServiceInterface awsCognitoAuth) : base(awsCognitoAuth)
        {
        }
    }
}

```

### EndPoint Controllers

The standard authentication flow with AWS Cognito requires the registration of a user with username and password and the confirmation of a code received by mail.

#### Authentication and authorization

By default, authentication is supported by the Amazon CognitoAuthentication Extension Library using the Secure Remote Password protocol. 
In addition, ASP.NET Core authorization provides a simple, declarative role and a rich policy-based model to handle authorization.
We use Amazon Cognito groups to support role-based authorization. 
Restricting access to only users who are part of an “Admin” group is as simple as adding the following attribute to the controllers or methods you want to restrict access to:

```
[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
}
```

Similarly, we use Amazon Cognito users attributes to support claim-based authorization. 
Amazon Cognito prefixes custom attributes with the key “custom:”

## 1 - SignUp

```bash

curl --location 'http://localhost:5130/api/auth/SignUp' \
--header 'Content-Type: application/json' \
--data-raw '{
  "UserName": "yourname@email.com",
  "EMail": "yourname@email.com",
  "Password": "password",
}'

```

### 2: Confirmation Code

```bash

curl --location 'http://localhost:5130/api/auth/Confirm' \
--header 'Content-Type: application/json' \
--data-raw '{
    "UserName": "yourname@email.com",
    "Code": "897875",
    "Password": "password"
}'

```

### 3- Resend Code

```bash

curl --location 'http://localhost:5130/api/auth/SendCode' \
--header 'Content-Type: application/json' \
--data-raw '{
    "UserName": "yourname@email.com"
}'

```

## 4 - Login

```bash

curl --location 'http://localhost:5130/api/auth/LogIn' \
--header 'Content-Type: application/json' \ 
--data-raw '{
  "UserName": "yourname@email.com",
  "EMail": "yourname@email.com",
  "Password": "password",
  "RememberMe": true
}'

```

## 5 - Forgot Password

```bash

curl --location 'http://localhost:5130/api/auth/ForgotPassword' \
--header 'Content-Type: application/json' \
--data-raw '{
    "UserName": "yourname@email.com"
}'

```

## 6 - Reset Password

```bash

curl --location 'http://localhost:5130/api/auth/ResetPassword' \
--header 'Content-Type: application/json' \
--data-raw '{
    "UserName": "yourname@email.com",
    "code": "123456",
    "password": "abcdefgh"
}'

```

### [Fix to Problem 401 Unauthorized](https://stackoverflow.com/questions/54395859/c-sharp-asp-net-core-bearer-error-invalid-token)

As fix: Install nuget package System.IdentityModel.Tokens.Jwt Version="6.16.0"
