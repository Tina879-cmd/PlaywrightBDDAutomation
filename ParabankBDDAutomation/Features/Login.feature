Feature: Login
  As a ParaBank customer
  I want to log into my account
  So I can access online banking featuresary of the feature

  Scenario: Successful login with valid credentials
    Given I navigate to the login page
    When Logged in successfully with valid credentials
    Then I should be able to view the accounts overview

  Scenario: Failed login with invalid credentials
    Given I navigate to the login page
    When I log in with username "invalidUserName" and password "invalidPwd"
    Then I should be able to view a login error message "The username and password could not be verified."
