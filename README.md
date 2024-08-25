This program uses Microsoft Playwright , C# and NUnit to automate form validation tests.  
The url used is https://testpages.eviltester.com/styled/validation/input-validation.html  
It includes 4 test cases to check form submissions, ensuring proper error handling and validation messages.  
first testcase: Sign in with valid Name and Surname  
second testcase: Sign in with valid Name and invalid Surname  
third testcase: Sign in with invalid Name and valid Surname  
fourth testcase: Sign in with invalid Name and Surname  
The tests run in parallel and utilize environment variables for configuration, making it a robust solution for automated UI testing.  
